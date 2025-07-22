# scripts/agent_loader.py

import json
import os
from collections import defaultdict

def parse_time_to_seconds(tstr):
    h, m, s = map(int, tstr.split(":"))
    return h * 3600 + m * 60 + s


def load_agents_from_json_multi_order(path, start_offset=8*3600):
    """
    Trả về dict: agent_id → {"orders": [Order1, Order2, ...]}
    Mỗi Order = {"from", "to", "deadline", "scheduled_time"}
    """
    with open(path) as f:
        deliveries = json.load(f)

    buckets = defaultdict(list)
    for d in deliveries:
        agent_id = d["id"].split("_")[0]  # agent_0_xx → agent_0
        order = {
            "from": d["from_edge"],
            "to": d["to_edge"],
            "scheduled_time": d["scheduled_time"],
            "deadline": parse_time_to_seconds(d["scheduled_time"]) - start_offset
        }
        buckets[agent_id].append(order)

    agents_config = {}
    for agent_id, orders in buckets.items():
        agents_config[agent_id] = {
            "orders": orders,
            "current_order_idx": 0
        }

    print(f"✅ Loaded {len(agents_config)} multi-order agents from {path}")
    return agents_config
