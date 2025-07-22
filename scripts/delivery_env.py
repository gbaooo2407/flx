import gym
from gym import spaces
import numpy as np
import traci
import random

class DeliveryMultiAgentEnv(gym.Env):
    def __init__(self, net_file, route_file, sumocfg_file, agents_config, max_steps=3600, gui=False):
        super().__init__()
        self.net_file = net_file
        self.route_file = route_file
        self.sumocfg_file = sumocfg_file
        self.max_steps = max_steps
        self.gui = gui

        self.agents_config = agents_config
        self.agent_ids = list(self.agents_config.keys())

        self.current_step = 0
        self.done_agents = set()
        self.last_distances = {}

        # Định nghĩa không gian quan sát cho tất cả agent
        self.observation_space = spaces.Box(low=-1.0, high=1.0, shape=(4,), dtype=np.float32)

        # Định nghĩa không gian hành động cho tất cả agent
        self.action_space = spaces.Discrete(8)  # Ví dụ: 8 hành động rời rạc
        
        # Cập nhật không gian hành động cho từng agent
        self.single_action_spaces = {agent_id: self.action_space for agent_id in self.agent_ids}

    def seed(self, seed=None):
        np.random.seed(seed)
        random.seed(seed)

    def reset(self, *, seed=None, options=None):
        # Khởi tạo lại các giá trị môi trường
        super().reset(seed=seed)
        if traci.isLoaded():
            traci.close()

        sumo_cmd = ["sumo-gui" if self.gui else "sumo", "-c", self.sumocfg_file]
        traci.start(sumo_cmd)

        self.current_step = 0
        self.done_agents = set()
        self.last_distances = {}

        for aid in self.agent_ids:
            self.agents_config[aid]["current_order_idx"] = 0
            order = self.agents_config[aid]["orders"][0]
            start = order["from"]
            goal = order["to"]
            self.agents_config[aid]["current_goal"] = goal

            try:
                path, _ = traci.simulation.findRoute(start, goal)
                route_id = f"route_{aid}_0"
                traci.route.add(route_id, path.edges)
                traci.vehicle.add(vehID=aid, routeID=route_id, typeID="delivery", depart="0")
                traci.vehicle.setColor(aid, (0, 255, 0))
                self.last_distances[aid] = traci.vehicle.getDrivingDistance(aid, goal, 0)
            except Exception as e:
                print(f"❌ Cannot spawn {aid}: {e}")

        # Trả về observation và thông tin (info)
        return self._get_all_observations(), {}

    def step(self, action_dict):
        self.current_step += 1

        for agent_id, action in action_dict.items():
            if agent_id in self.done_agents:
                continue
            self._apply_action(agent_id, action)

        traci.simulationStep()

        obs = self._get_all_observations()
        rewards = self._compute_rewards()
        terminateds = {aid: self._check_done(aid) for aid in self.agent_ids}
        truncateds = {aid: False for aid in self.agent_ids}  # Đảm bảo trả về truncateds
        infos = {aid: {} for aid in self.agent_ids}

        terminateds["__all__"] = all(terminateds.values())
        truncateds["__all__"] = False  # chưa hỗ trợ ngắt do timeout

        return obs, rewards, terminateds, truncateds, infos

    def _apply_action(self, agent_id, action):
        try:
            current_edge = traci.vehicle.getRoadID(agent_id)
            if current_edge == "":
                return
            next_edges = traci.edge.getOutgoing(current_edge)

            if not next_edges:
                return

            possible_edges = [e[0].getID() for e in next_edges]
            safe_action = action % len(possible_edges)  # Đảm bảo action luôn hợp lệ
            target_edge = possible_edges[safe_action]

            traci.vehicle.changeTarget(agent_id, target_edge)
            traci.vehicle.setColor(agent_id, (255, 255, 0))
        except Exception as e:
            print(f"⚠️ Action error {agent_id}: {e}")

    def _get_observation(self, agent_id):
        try:
            x, y = traci.vehicle.getPosition(agent_id)
            goal = self.agents_config[agent_id]["current_goal"]
            dist = traci.vehicle.getDrivingDistance(agent_id, goal, 0)
            time_left = self.max_steps - self.current_step
            return np.array([x / 1000.0, y / 1000.0, dist / 1000.0, time_left / self.max_steps])
        except:
            return np.zeros(4)

    def _get_all_observations(self):
        return {
            aid: self._get_observation(aid)
            for aid in self.agent_ids if aid not in self.done_agents
        }

    def _compute_rewards(self):
        rewards = {}

        for aid in self.agent_ids:
            if aid in self.done_agents:
                rewards[aid] = 0.0
                continue

            goal = self.agents_config[aid]["current_goal"]
            now = self.current_step
            try:
                dist = traci.vehicle.getDrivingDistance(aid, goal, 0)
            except:
                dist = 9999

            last = self.last_distances.get(aid, dist + 1)
            delta = last - dist
            distance_reward = max(0, delta / 100.0)

            curr_idx = self.agents_config[aid]["current_order_idx"]
            orders = self.agents_config[aid]["orders"]
            deadline = orders[curr_idx]["deadline"]

            if self._check_done(aid):
                time_reward = 1.0 if now <= deadline else -0.5 * (now - deadline) / self.max_steps
            else:
                time_reward = 0.0

            collision_penalty = 0.0
            for other in self.agent_ids:
                if other != aid and other not in self.done_agents:
                    try:
                        dx = traci.vehicle.getDistance(aid) - traci.vehicle.getDistance(other)
                        if abs(dx) < 5:
                            collision_penalty -= 0.2
                    except:
                        pass

            total_reward = distance_reward + time_reward + collision_penalty
            total_reward += 0.2 * curr_idx  # thưởng theo số đơn đã hoàn thành
            rewards[aid] = total_reward
            self.last_distances[aid] = dist

        return rewards

    def _check_done(self, agent_id):
        try:
            curr_idx = self.agents_config[agent_id]["current_order_idx"]
            orders = self.agents_config[agent_id]["orders"]
            goal = orders[curr_idx]["to"]

            if traci.vehicle.getRoadID(agent_id) == goal:
                curr_idx += 1
                if curr_idx >= len(orders):
                    self.done_agents.add(agent_id)
                    return True

                self.agents_config[agent_id]["current_order_idx"] = curr_idx
                next_order = orders[curr_idx]
                new_goal = next_order["to"]
                self.agents_config[agent_id]["current_goal"] = new_goal

                path, _ = traci.simulation.findRoute(traci.vehicle.getRoadID(agent_id), new_goal)
                traci.vehicle.setRoute(agent_id, path.edges)
                traci.vehicle.setColor(agent_id, (255, 0, 255))

                return False
        except:
            return True
        return False

    def close(self):
        if traci.isLoaded():
            traci.close()
