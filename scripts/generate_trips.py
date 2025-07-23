import os
import json
import random
import xml.etree.ElementTree as ET
from datetime import datetime
import subprocess
from scripts.utils.weather_utils import adjust_vehicle_speeds
from scripts.utils.graph_utils import (
    load_graph,
    update_speed_limits,
    update_graph_with_incidents,
    update_faulty_traffic_lights,
    update_road_openings
)

ZONE = "cbd"
BASE = f"data/raw/sumo/{ZONE}"

NET_PATH = f"{BASE}/{ZONE}.net.xml"
DELIVERY_PATH = f"data/raw/demand/{ZONE}/delivery_requests.json"
TRIPS_RAW_PATH = f"{BASE}/{ZONE}.trips.xml"
TRIPS_ADJUSTED_PATH = f"{BASE}/{ZONE}.weather_adjusted.trips.xml"
WEATHER_DIR = "data/raw/weather_data"

LTA_TIME = "202507212335"
LTA_DIR = "data/raw/lta"

def get_latest_weather_file():
    files = [f for f in os.listdir(WEATHER_DIR) if f.startswith("weather_") and f.endswith(".json")]
    if not files:
        raise FileNotFoundError("❌ Không tìm thấy file thời tiết trong thư mục.")
    return os.path.join(WEATHER_DIR, sorted(files)[-1])

def remove_vehicles_from_rou(route_path):
    tree = ET.parse(route_path)
    root = tree.getroot()
    vehicles = root.findall("vehicle")
    for v in vehicles:
        root.remove(v)
    tree.write(route_path)
    print(f"🧹 Đã xoá {len(vehicles)} <vehicle> khỏi {route_path} (giữ lại <route> và <vType>)")

def flatten_delivery_requests(requests_dict):
    trips = []
    global_depart = 0  # ⬅️ Sử dụng biến toàn cục
    for vehicle_id, info in requests_dict.items():
        for i, order in enumerate(info["orders"]):
            trips.append({
                "from_edge": order["from"],
                "to_edge": order["to"],
                "depart": global_depart,
                "id": f"{vehicle_id}_{i}",
                "type": "delivery"
            })
            global_depart += 10  # ⏱ Giãn cách mỗi delivery 3 giây
    return trips

def convert_and_patch_routes(net_path, trip_path, route_path):
    additional_path = os.path.join(os.path.dirname(route_path), "types.add.xml")
    if not os.path.exists(additional_path):
        with open(additional_path, "w", encoding="utf-8") as f:
            f.write('''<additional>
  <vType id="car" accel="2.6" decel="4.5" sigma="0.5" length="5.0" minGap="2.5" maxSpeed="70" guiShape="passenger"/>
</additional>''')
    subprocess.run([
        "duarouter",
        "-n", net_path,
        "--route-files", trip_path,
        "--additional-files", additional_path,
        "-o", route_path,
        "--ignore-errors"
    ], check=True)
    print(f"✅ Đã chuyển trips → routes và thêm vType → {route_path}")

def generate_combined_trips(deliveries, output_path, net_path, G, background_vehicle_count=3000):
    from sumolib.net import readNet
    print(f"🚗 Tạo trips với {len(deliveries)} delivery và {background_vehicle_count} background...")

    net = readNet(net_path)
    edges = [e for e in net.getEdges() if not e.getID().startswith(":")]
    edge_ids = {e.getID() for e in edges}

    trips = ET.Element("trips")
    skipped = 0
    delivery_trips = []
    background_trips = []

    # ✅ DELIVERY
    for idx, d in enumerate(sorted(deliveries, key=lambda d: d["depart"])):
        from_edge = d["from_edge"]
        to_edge = d["to_edge"]

        if from_edge not in G or to_edge not in G:
            print(f"⚠️ Bỏ qua delivery trip {idx} do from/to không có trong graph.")
            skipped += 1
            continue

        if G[from_edge].get("closed") or G[to_edge].get("closed"):
            print(f"⚠️ Bỏ qua delivery trip {idx} do đường bị đóng.")
            skipped += 1
            continue

        try:
            path, dist = net.getShortestPath(net.getEdge(from_edge), net.getEdge(to_edge))
            if not path:
                print(f"⚠️ Không tìm được đường cho delivery trip {idx}")
                skipped += 1
                continue
        except Exception as e:
            print(f"⚠️ Lỗi tìm đường delivery trip {idx}: {e}")
            skipped += 1
            continue

        delivery_trips.append({
            "id": f"delivery_{idx}",
            "type": d.get("type", "delivery"),
            "depart": str(d["depart"]),
            "from_edge": from_edge,
            "to_edge": to_edge
        })

    # ✅ BACKGROUND
    print("📥 Đang sinh background trips...")
    for i in range(background_vehicle_count):
        from_edge = random.choice(edges).getID()
        to_edge = random.choice(edges).getID()
        if from_edge == to_edge:
            continue

        try:
            path, dist = net.getShortestPath(net.getEdge(from_edge), net.getEdge(to_edge))
            if not path:
                continue
        except:
            continue

        background_trips.append({
            "id": f"bg_{i}",
            "type": "car",
            "depart": str(random.randint(8 * 3600, 12 * 3600)),
            "from_edge": from_edge,
            "to_edge": to_edge
        })

    print(f"✅ Tạo {len(delivery_trips)} delivery và {len(background_trips)} background trips (skipped {skipped})")

    # ✅ GHI FILE TRIPS
    all_trips_sorted = sorted(delivery_trips + background_trips, key=lambda x: int(x["depart"]))
    for trip in all_trips_sorted:
        ET.SubElement(trips, "trip", {
            "id": trip["id"],
            "type": trip["type"],
            "depart": trip["depart"],
            "from": trip["from_edge"],
            "to": trip["to_edge"]
        })

    ET.ElementTree(trips).write(output_path)
    print(f"✅ Ghi file trips: {output_path}")
    
def main():
    print(f"\n📍 Khu vực: {ZONE.upper()}")

    print("🧠 Load graph và cập nhật LTA...")
    G = load_graph(NET_PATH)
    update_speed_limits(G, f"{LTA_DIR}/traffic_speed_band/{LTA_TIME}.json")
    update_graph_with_incidents(G, f"{LTA_DIR}/traffic_incident/{LTA_TIME}.json", f"{LTA_DIR}/road_work/{LTA_TIME}.json")
    update_faulty_traffic_lights(G, f"{LTA_DIR}/faulty_traffic_light/{LTA_TIME}.json")
    update_road_openings(G, f"{LTA_DIR}/road_open/{LTA_TIME}.json", current_time_unix=1721570100)

    print("📦 Load yêu cầu giao hàng...")
    with open(DELIVERY_PATH) as f:
        delivery_data = json.load(f)

    if isinstance(delivery_data, dict):
        deliveries = flatten_delivery_requests(delivery_data)
    elif isinstance(delivery_data, list):
        deliveries = delivery_data
    else:
        raise ValueError("❌ Dữ liệu giao hàng không đúng định dạng.")

    print("🛠️ Sinh trips.xml từ delivery + background...")
    generate_combined_trips(
        deliveries=deliveries,
        output_path=TRIPS_RAW_PATH,
        net_path=NET_PATH,
        G=G
    )
    print("🌧️ Điều chỉnh tốc độ theo thời tiết...")
    latest_weather_file = get_latest_weather_file()
    adjust_vehicle_speeds(
        trip_input_path=TRIPS_RAW_PATH,
        weather_path=latest_weather_file,
        trip_output_path=TRIPS_ADJUSTED_PATH,
    )
    ROUTE_PATH = f"{BASE}/{ZONE}.weather_adjusted.rou.xml"
    convert_and_patch_routes(
        net_path=NET_PATH,
        trip_path=TRIPS_ADJUSTED_PATH,
        route_path=ROUTE_PATH
    )
    remove_vehicles_from_rou(ROUTE_PATH)

    print(f"\n✅ Hoàn tất! File SUMO đã sẵn sàng:\n→ {ROUTE_PATH}")

if __name__ == "__main__":
    main()
