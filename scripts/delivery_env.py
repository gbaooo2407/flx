import gymnasium as gym
from gymnasium.spaces import Box, Discrete
import numpy as np
import traci
import random
from sumolib.net import readNet
from ray.rllib.env.multi_agent_env import MultiAgentEnv
from scripts.utils.weather_utils import get_weather_factor


class DeliveryMultiAgentEnv(MultiAgentEnv):
    def __init__(self, net_file, route_file, sumocfg_file, agents_config, max_steps=3600, gui=False):
        super().__init__()
        self.net_file = net_file
        self.route_file = route_file
        self.sumocfg_file = sumocfg_file
        self.max_steps = max_steps
        self.gui = gui
        self.sumo_net = readNet(self.net_file) 
        self.weather_factor = get_weather_factor()

        self.agents_config = agents_config or {}
        self.agent_ids = list(self.agents_config.keys())
        self._agent_ids = set(self.agent_ids)

        self.current_step = 0
        self.done_agents = set()
        self.last_distances = {}
        
        # Khai báo observation & action space dùng chung cho mọi agent
        self.single_observation_space = Box(low=np.array([0, -1e6, -1e6, 0], dtype=np.float32),
                                            high=np.array([50, 1e6, 1e6, 1e6], dtype=np.float32),
                                            dtype=np.float32)
        self.single_action_space = Discrete(8)

        self.observation_space = self.single_observation_space
        self.action_space = self.single_action_space

    def reset(self, *, seed=None, options=None):
        super().reset(seed=seed)

        if traci.isLoaded():
            traci.close()

        sumo_cmd = ["sumo-gui" if self.gui else "sumo", "-c", self.sumocfg_file]
        traci.start(sumo_cmd)

        self.current_step = 0
        self.done_agents = set()
        self.last_distances = {}

        obs = {}
        infos = {}

        for aid in self.agent_ids:
            self.agents_config[aid]["current_order_idx"] = 0
            order = self.agents_config[aid]["orders"][0]
            start = order["from"]
            goal = order["to"]
            self.agents_config[aid]["current_goal"] = goal

            try:
                route = traci.simulation.findRoute(start, goal)
                edges = route.edges
                route_id = f"route_{aid}_0"
                traci.route.add(route_id, edges)
                traci.vehicle.add(vehID=aid, routeID=route_id, typeID="delivery", depart="0")
                print(f"✅ Spawned {aid} on route: {edges}")
                traci.vehicle.setColor(aid, (0, 255, 0))
                self.last_distances[aid] = traci.vehicle.getDrivingDistance(aid, goal, 0)
            except Exception as e:
                print(f"❌ Cannot spawn {aid}: {e}")

            obs[aid] = np.asarray(self._get_observation(aid), dtype=np.float32)
            infos[aid] = {}  # bắt buộc trả về

        route = traci.simulation.findRoute(start, goal)
        if not route.edges:
            print(f"❌ No route found from {start} to {goal}")

        traci.simulationStep()
        print("🚗 Active vehicles in simulation:", traci.vehicle.getIDList())

        return obs, infos

    def step(self, action_dict):
        self.current_step += 1

        # Chỉ apply action nếu agent chưa done
        for agent_id, action in action_dict.items():
            if agent_id in self.done_agents:
                continue
            self._apply_action(agent_id, action)

        traci.simulationStep()

        obs, rewards, terminateds, truncateds, infos = {}, {}, {}, {}, {}

        # Duyệt từng agent còn sống
        for aid in self.agent_ids:
            if aid in self.done_agents:
                continue

            # Quan sát và phần thưởng
            obs[aid] = self._get_observation(aid)
            rewards[aid] = self._compute_single_reward(aid)
            infos[aid] = {}

            # Check done
            done = self._check_done(aid)
            terminateds[aid] = done
            truncateds[aid] = self.current_step >= self.max_steps

            if done or truncateds[aid]:
                self.done_agents.add(aid)

        #  __all__ keys
        terminateds["__all__"] = len(self.done_agents) == len(self.agent_ids)
        truncateds["__all__"] = self.current_step >= self.max_steps

        print(f"[Step {self.current_step}] Obs: {obs}, Rewards: {rewards}, Terminated: {terminateds}, Truncated: {truncateds}")
        return obs, rewards, terminateds, truncateds, infos

    def _compute_single_reward(self, aid):
        if aid in self.done_agents:
            return 0.0

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
        print(f"[{aid}] now={now}, deadline={deadline}, delta={delta:.1f}, dist={dist:.1f}, last={last:.1f}")

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
        total_reward += 0.2 * curr_idx

        total_reward *= self.weather_factor
        print(f"[REWARD USED] _compute_single_reward({aid}) called")

        self.last_distances[aid] = dist
        return total_reward


    def _apply_action(self, agent_id, action):
        try:
            if agent_id not in traci.vehicle.getIDList():
                return

            current_edge_id = traci.vehicle.getRoadID(agent_id)
            if not current_edge_id or current_edge_id.startswith(":"):
                return

            edge_obj = self.sumo_net.getEdge(current_edge_id)
            
            # ✅ get all connections FROM current edge
            connections = edge_obj.getConnections(None)

            # ✅ Lấy các cạnh đích từ connection
            possible_edges = [
                conn.getTo().getID()
                for conn in connections
                if conn.getTo() and not conn.getTo().getID().startswith(":")
            ]

            if not possible_edges:
                return

            chosen_edge = possible_edges[action % len(possible_edges)]
            traci.vehicle.changeTarget(agent_id, chosen_edge)
            traci.vehicle.setColor(agent_id, (255, 255, 0))

        except Exception as e:
            print(f"⚠️ Action error {agent_id}: {e}")


    def _get_all_observations(self):
        obs = {}
        for aid in self.agent_ids:
            if aid in self.done_agents:
                obs[aid] = np.zeros(4, dtype=np.float32)
            elif aid in traci.vehicle.getIDList():
                obs[aid] = self._get_observation(aid)
            else:
                obs[aid] = np.zeros(4, dtype=np.float32)
        return obs

    def _get_observation(self, agent_id):
        if agent_id not in traci.vehicle.getIDList():
            return np.zeros(4, dtype=np.float32)

        try:
            speed = traci.vehicle.getSpeed(agent_id)
            pos = traci.vehicle.getPosition(agent_id)
            edge = traci.vehicle.getRoadID(agent_id)
            dist = traci.vehicle.getDrivingDistance(agent_id, self.agents_config[agent_id]["current_goal"], 0)

            # ✅ Nếu dist là -1 hoặc âm → đặt thành 1e6 để hợp lệ
            if dist < 0:
                dist = 1e6

        except Exception:
            speed = 0.0
            pos = (0.0, 0.0)
            dist = 1e6  # ✅ tránh giá trị âm

        obs = np.asarray([speed, pos[0], pos[1], dist], dtype=np.float32)
        return obs

    # def _compute_rewards(self):
    #     rewards = {}

    #     for aid in self.agent_ids:
    #         if aid in self.done_agents:
    #             rewards[aid] = 0.0
    #             continue

    #         goal = self.agents_config[aid]["current_goal"]
    #         now = self.current_step
    #         try:
    #             dist = traci.vehicle.getDrivingDistance(aid, goal, 0)
    #         except:
    #             dist = 9999

    #         last = self.last_distances.get(aid, dist + 1)
    #         delta = last - dist
    #         distance_reward = max(0, delta / 100.0)

    #         curr_idx = self.agents_config[aid]["current_order_idx"]
    #         orders = self.agents_config[aid]["orders"]
    #         deadline = orders[curr_idx]["deadline"]

    #         if self._check_done(aid):
    #             time_reward = 1.0 if now <= deadline else -0.5 * (now - deadline) / self.max_steps
    #         else:
    #             time_reward = 0.0

    #         collision_penalty = 0.0
    #         for other in self.agent_ids:
    #             if other != aid and other not in self.done_agents:
    #                 try:
    #                     dx = traci.vehicle.getDistance(aid) - traci.vehicle.getDistance(other)
    #                     if abs(dx) < 5:
    #                         collision_penalty -= 0.2
    #                 except:
    #                     pass

    #         total_reward = distance_reward + time_reward + collision_penalty
    #         total_reward += 0.2 * curr_idx  # thưởng theo số đơn đã hoàn thành
    #         rewards[aid] = total_reward
    #         self.last_distances[aid] = dist

    #     return rewards

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

    def seed(self, seed=None):
        np.random.seed(seed)
        random.seed(seed)
        return [seed]
    @property
    def observation_spaces(self):
        return {aid: self.single_observation_space for aid in self.agent_ids}

    @property
    def action_spaces(self):
        return {aid: self.single_action_space for aid in self.agent_ids}

