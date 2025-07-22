import ray
from ray import tune
from ray.rllib.algorithms.ppo import PPOConfig
from scripts.delivery_env import DeliveryMultiAgentEnv
from scripts.agent_loader import load_agents_from_json_multi_order
from ray.rllib.env.wrappers.multi_agent_env_compatibility import MultiAgentEnvCompatibility
import gymnasium as gym
from gymnasium.spaces import Discrete, Box
import numpy as np
from ray.tune.registry import register_env

# === Thông tin môi trường ===
ZONE = "cbd"
NET_FILE = f"data/raw/sumo/{ZONE}/{ZONE}.net.xml"
ROUTE_FILE = f"data/raw/sumo/{ZONE}/{ZONE}.weather_adjusted.rou.xml"
SUMOCFG_FILE = f"data/raw/sumo/{ZONE}/{ZONE}.sumocfg"
DELIVERY_PATH = f"data/raw/demand/{ZONE}/delivery_requests.json"

# === Load agent config ===
agents_config = load_agents_from_json_multi_order(DELIVERY_PATH)

# === Env register cho RLlib ===
def env_creator(config):
    # Kiểm tra agents_config trước khi tạo env
    if not agents_config:
        raise ValueError("agents_config is empty!")
        
    raw_env = DeliveryMultiAgentEnv(
        net_file=NET_FILE,
        route_file=ROUTE_FILE,
        sumocfg_file=SUMOCFG_FILE,
        agents_config=agents_config,
        gui=False,
        max_steps=3600,
    )
    return MultiAgentEnvCompatibility(raw_env)

# Đăng ký môi trường multi-agent
register_env("delivery-marl", env_creator)

# === Cấu hình PPO ===
config = (
    PPOConfig()
    .environment("delivery-marl", env_config={})
    .framework("torch")
    .training(
        train_batch_size=512, 
        lr=1e-4, 
        gamma=0.99, 
        model={"fcnet_hiddens": [128, 64]}
    )
    .multi_agent(
        policies={
            # Policy cho từng agent
            "default_policy": (None, Box(low=-1.0, high=1.0, shape=(4,), dtype=np.float32), Discrete(8), {}),
        },
        policy_mapping_fn=lambda agent_id: "default_policy",  # Sử dụng policy mặc định cho tất cả agents
    )
    .rollouts(num_rollout_workers=2)  # Điều chỉnh số lượng workers (môi trường) song song cho huấn luyện
)

# === Huấn luyện mô hình ===
tune.run("PPO", config=config)
