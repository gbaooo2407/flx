import json
import random
import sumolib
from collections import defaultdict

# ⚙️ Tham số
NET_FILE = "data/raw/sumo/cbd/cbd.net.xml"
OUTPUT_PATH = "data/raw/demand/cbd/delivery_requests.json"
NUM_AGENTS = 10
ORDERS_PER_AGENT = 3
MAX_DEPART = 3600  # tối đa 1 tiếng
VEHICLE_SPEED = 10.0  # m/s

# 📍 Load network
net = sumolib.net.readNet(NET_FILE)
edges = [e for e in net.getEdges() if not e.getID().startswith(":")]

# 🛵 Tạo đơn hàng
agents = defaultdict(list)
for i in range(NUM_AGENTS):
    for j in range(ORDERS_PER_AGENT):
        attempts = 0
        while True:
            from_edge = random.choice(edges)
            to_edge = random.choice(edges)
            if from_edge != to_edge:
                path = net.getShortestPath(from_edge, to_edge)
                if path and path[0]:  # path[0] là danh sách edges
                    break
            attempts += 1
            if attempts > 10:
                print(f"⚠️ Bỏ qua delivery agent{i}_order{j} do không tìm được path.")
                break

        if attempts > 10:
            continue

        distance = sum(edge.getLength() for edge in path[0])
        est_time = distance / VEHICLE_SPEED
        depart = random.randint(0, MAX_DEPART)
        deadline = int(depart + est_time * 1.5)

        agents[f"delivery{i}"].append({
            "from": from_edge.getID(),
            "to": to_edge.getID(),
            "deadline": deadline
        })

# 📤 Ghi file JSON
final_data = {agent_id: {"orders": orders} for agent_id, orders in agents.items()}

with open(OUTPUT_PATH, "w", encoding="utf-8") as f:
    json.dump(final_data, f, indent=2)

print(f"✅ Đã tạo {len(final_data)} agents × {ORDERS_PER_AGENT} đơn vào {OUTPUT_PATH}")
