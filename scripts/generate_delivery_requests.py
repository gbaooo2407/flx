import random
import json
import os
from sumolib.net import readNet

def generate_deliveries(
    net_path,
    num_deliveries=1000,
    start_hour=8,
    end_hour=12,
    output_path=None,
    seed=42,
    min_distance=500,
    max_distance=10000
):
    # Ensure correct file path
    net_path = os.path.abspath(net_path).replace("\\", "/")
    print("Loading network from:", net_path)
    
    # Check if the file exists
    if not os.path.isfile(net_path):
        raise FileNotFoundError(f"File {net_path} not found.")
    
    # Open the net file (ensure it's not treated as a gzip)
    net = readNet(net_path)  # Read the file as a regular XML file, not gzipped
    
    edges = [e for e in net.getEdges() if not e.isSpecial()]
    deliveries = []
    used_pairs = set()
    random.seed(seed)

    max_trials = num_deliveries * 100
    trials = 0

    while len(deliveries) < num_deliveries and trials < max_trials:
        from_edge = random.choice(edges)
        to_edge = random.choice(edges)
        key = (from_edge.getID(), to_edge.getID())
        if from_edge.getID() == to_edge.getID() or key in used_pairs:
            trials += 1
            continue
        path, dist = net.getShortestPath(from_edge, to_edge)
        if path is None or dist < min_distance or dist > max_distance:
            trials += 1
            continue

        total_time = 0.0
        for edge in path:
            length = edge.getLength()
            speed = edge.getSpeed()
            if speed > 0:
                total_time += length / speed

        hour = random.randint(start_hour, end_hour)
        minute = random.randint(0, 59)
        second = random.randint(0, 59)
        depart_seconds = hour * 3600 + minute * 60 + second
        timestamp = f"{hour:02}:{minute:02}:{second:02}"

        deliveries.append({
            "id": f"delivery{len(deliveries)}",
            "from_edge": from_edge.getID(),
            "to_edge": to_edge.getID(),
            "scheduled_time": timestamp,
            "depart": depart_seconds,
            "distance": int(dist),
            "estimated_time": int(total_time)
        })

    deliveries.sort(key=lambda d: d["scheduled_time"])

    if output_path:
        os.makedirs(os.path.dirname(output_path), exist_ok=True)
        with open(output_path, "w", encoding="utf-8") as f:
            json.dump(deliveries, f, indent=2, ensure_ascii=False)
        print(f"✅ Đã lưu {len(deliveries)} yêu cầu giao hàng tại {output_path}")

    return deliveries


if __name__ == "__main__":
    import argparse
    parser = argparse.ArgumentParser()
    parser.add_argument("--net", required=True, help="Đường dẫn tới file .net.xml")
    parser.add_argument("--num", type=int, default=1000, help="Số lượng đơn hàng")
    parser.add_argument("--out", required=True, help="File json output")
    parser.add_argument("--min-distance", type=int, default=500, help="Khoảng cách tối thiểu (m)")
    parser.add_argument("--max-distance", type=int, default=10000, help="Khoảng cách tối đa (m)")
    parser.add_argument("--seed", type=int, default=42)
    args = parser.parse_args()

    generate_deliveries(
        net_path=args.net,
        num_deliveries=args.num,
        output_path=args.out,
        seed=args.seed,
        min_distance=args.min_distance,
        max_distance=args.max_distance
    )
