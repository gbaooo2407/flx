import json
import numpy as np
from scripts.delivery_env import DeliveryMultiAgentEnv

# === Thông tin môi trường ===
ZONE = "cbd"
NET_FILE = f"data/raw/sumo/{ZONE}/{ZONE}.net.xml"
ROUTE_FILE = f"data/raw/sumo/{ZONE}/{ZONE}.weather_adjusted.rou.xml"
SUMOCFG_FILE = f"data/raw/sumo/{ZONE}/{ZONE}.sumocfg"
DELIVERY_PATH = f"data/raw/demand/{ZONE}/delivery_requests.json"

# === Load agents config từ file JSON ===
with open(DELIVERY_PATH, "r") as f:
    delivery_data = json.load(f)

# Nếu là danh sách -> chuyển sang multi-agent dict
if isinstance(delivery_data, list):
    from scripts.agent_loader import load_agents_from_json_multi_order
    agents_config = load_agents_from_json_multi_order(DELIVERY_PATH)
else:
    agents_config = delivery_data

# === Tạo môi trường ===
env = DeliveryMultiAgentEnv(
    net_file=NET_FILE,
    route_file=ROUTE_FILE,
    sumocfg_file=SUMOCFG_FILE,
    agents_config=agents_config,
    gui=False,
    max_steps=3600
)

# === Reset và kiểm tra bước đầu tiên ===
obs, infos = env.reset()
print("✅ Reset thành công. Quan sát:")
for agent_id, ob in obs.items():
    print(f"{agent_id}: {ob}")

# === Tạo hành động ngẫu nhiên ===
actions = {
    agent_id: env.action_spaces[agent_id].sample()
    for agent_id in obs
}

# === Thực hiện 1 bước ===
for t in range(10):
    obs, rewards, term, trunc, infos = env.step({aid: 0 for aid in obs})
    print(f"[Step {t+1}] Rewards: {rewards}")
    if term.get("__all__", False):
        break


print(f"__all__ done? {term['__all__']}, truncated? {trunc['__all__']}")
env.close()