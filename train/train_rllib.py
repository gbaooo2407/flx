import ray
from ray import tune
from ray.rllib.algorithms.ppo import PPOConfig
from scripts.delivery_env import DeliveryMultiAgentEnv
from scripts.agent_loader import load_agents_from_json_multi_order
from ray.rllib.env.wrappers.multi_agent_env_compatibility import MultiAgentEnvCompatibility
from ray.tune.registry import register_env
import gymnasium as gym
from gymnasium.spaces import Discrete, Box
import numpy as np
import json
import os  

# === Thông tin môi trường ===
ZONE = "cbd"
NET_FILE = os.path.abspath(f"data/raw/sumo/{ZONE}/{ZONE}.net.xml")
ROUTE_FILE = os.path.abspath(f"data/raw/sumo/{ZONE}/{ZONE}.weather_adjusted.rou.xml")
SUMOCFG_FILE = os.path.abspath(f"data/raw/sumo/{ZONE}/{ZONE}.sumocfg")
DELIVERY_PATH = os.path.abspath(f"data/raw/demand/{ZONE}/delivery_requests.json")

# === Load agent config từ JSON ===
with open(DELIVERY_PATH) as f:
    delivery_data = json.load(f)

if isinstance(delivery_data, dict):
    agents_config = delivery_data
elif isinstance(delivery_data, list):
    agents_config = load_agents_from_json_multi_order(DELIVERY_PATH)
else:
    raise ValueError("❌ Định dạng delivery_requests.json không hợp lệ.")

# === Định nghĩa hàm tạo môi trường và bọc MultiAgentEnvCompatibility ===
def env_creator(config):
    raw_env = DeliveryMultiAgentEnv(
        net_file=NET_FILE,
        route_file=ROUTE_FILE,
        sumocfg_file=SUMOCFG_FILE,
        agents_config=agents_config,
        gui=False,
        max_steps=1000,
    )

    # ✅ Quan trọng: confirm tại đây
    print("🧪 env_creator _agent_ids:", raw_env._agent_ids)

    return raw_env
test_env = env_creator({})
obs_space = test_env.observation_space
action_space = test_env.action_space

# === Đăng ký môi trường RLlib ===
register_env("delivery-marl", env_creator)
def policy_mapping_fn(agent_id, episode=None, worker=None, **kwargs):
    return "default_policy"


# === Cấu hình PPO ===
config = (
    PPOConfig()
    .environment("delivery-marl", env_config={})
    .framework("torch")
    .training(
        train_batch_size=512,
        lr=1e-4,
        gamma=0.99,
        model={"fcnet_hiddens": [128, 64]},
    )
    .multi_agent(
        policies={"default_policy": (None, obs_space, action_space, {})},
        policy_mapping_fn=policy_mapping_fn,
    )
    .rollouts(
        num_rollout_workers=0,        # Đặt = 0 để dễ debug, đổi sang >0 khi đã ổn định
        batch_mode="complete_episodes"
    )  
)

# === Khởi động Ray và huấn luyện ===
ray.init(ignore_reinit_error=True)

tune.run("PPO", 
         config=config,
         stop={"training_iteration": 5})
