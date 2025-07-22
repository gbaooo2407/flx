import os
import json
import random
import xml.etree.ElementTree as ET
from datetime import datetime
import subprocess
import csv

from scripts.utils.weather_utils import adjust_vehicle_speeds
from scripts.utils.graph_utils import (
    load_graph,
    update_speed_limits,
    update_graph_with_incidents,
    update_faulty_traffic_lights,
    update_road_openings
)

# ==== CẤU HÌNH ==== #
ZONE = "cbd"  # Thay bằng "nus", "jurong", "changi" nếu muốn
BASE = f"data/raw/sumo/{ZONE}"

NET_PATH = f"{BASE}/{ZONE}.net.xml"
DELIVERY_PATH = f"data/raw/demand/{ZONE}/delivery_requests.json"
TRIPS_RAW_PATH = f"{BASE}/{ZONE}.trips.xml"
TRIPS_ADJUSTED_PATH = f"{BASE}/{ZONE}.weather_adjusted.trips.xml"
WEATHER_DIR = "data/raw/weather_data"

LTA_TIME = "202507212335"  # timestamp thống nhất
LTA_DIR = "data/raw/lta"

# ==== TOOL ==== #
def get_latest_weather_file():
    files = [f for f in os.listdir(WEATHER_DIR) if f.startswith("weather_") and f.endswith(".json")]
    if not files:
        raise FileNotFoundError("❌ Không tìm thấy file thời tiết trong thư mục.")
    return os.path.join(WEATHER_DIR, sorted(files)[-1])

def convert_and_patch_routes(net_path, trip_path, route_path):
    # Thêm file bổ sung chứa <vType>
    additional_path = os.path.join(os.path.dirname(route_path), "types.add.xml")

    # Nếu chưa có thì tạo luôn
    if not os.path.exists(additional_path):
        with open(additional_path, "w", encoding="utf-8") as f:
            f.write('''<additional>
  <vType id="car" accel="2.6" decel="4.5" sigma="0.5" length="5.0" minGap="2.5" maxSpeed="70" guiShape="passenger"/>
  <vType id="delivery" accel="1.5" decel="4.0" sigma="0.5" length="6.0" minGap="2.5" maxSpeed="60" guiShape="truck"/>
</additional>''')

    # Chạy duarouter với --additional-files
    subprocess.run([
        "duarouter",
        "-n", net_path,
        "--route-files", trip_path,
        "--additional-files", additional_path,
        "-o", route_path,
        "--ignore-errors"
    ], check=True)

    print(f"✅ Đã chuyển trips → routes và thêm vType → {route_path}")

def create_trip_element(trip, trip_type="delivery"):
    """Hàm tạo một element XML cho trip"""
    # If 'type' is not present, use the provided default 'trip_type'
    return ET.Element("trip", {
        "id": trip["id"],
        "type": trip.get("type", trip_type),  # Ensure 'type' is safely accessed
        "depart": str(trip["depart"]),
        "from": trip["from_edge"],
        "to": trip["to_edge"]
    })

def generate_combined_trips(deliveries, output_path, net_path, G, background_vehicle_count=3000):
    from sumolib.net import readNet
    print(f"🚗 Tạo trips với {len(deliveries)} delivery và {background_vehicle_count} background...")

    net = readNet(net_path)
    edges = [e for e in net.getEdges() if not e.getID().startswith(":")]
    edge_ids = {e.getID() for e in edges}

    trips = ET.Element("trips")
    skipped = 0

    # Sắp xếp các chuyến đi giao hàng và chuyến đi nền theo thời gian xuất phát
    deliveries_sorted = sorted(deliveries, key=lambda d: d["depart"])
    background_trips = []

    # Delivery trips
    for d in deliveries_sorted:
        if (d["from_edge"] not in G or d["to_edge"] not in G or G[d["from_edge"]].get("closed") or G[d["to_edge"]].get("closed")):
            skipped += 1
            continue

        # Check nếu route bị đóng
        if G[d["from_edge"]].get("closed") or G[d["to_edge"]].get("closed"):
            print(f"⚠️ Bỏ qua {d['id']} vì đường bị đóng.")
            skipped += 1
            continue

        try:
            path, dist = net.getShortestPath(net.getEdge(d["from_edge"]), net.getEdge(d["to_edge"]))
            if not path:
                skipped += 1
                continue
        except:
            skipped += 1
            continue

        # If 'type' is missing, set a default value, for example "delivery"
        trip_type = d.get("type", "delivery")

        # Add trip to the XML
        ET.SubElement(trips, "trip", {
            "id": d["id"],
            "type": trip_type,  # Use the default type if missing
            "depart": str(d["depart"]),
            "from": d["from_edge"],
            "to": d["to_edge"]
        })

    print(f"✅ Ghi file trips: {output_path} (skipped {skipped} trips bị chặn)")

    # Background trips
    print("📥 Đang sinh background trips...")
    for count in range(background_vehicle_count):
        from_edge, to_edge = random.choice(edges).getID(), random.choice(edges).getID()
        if from_edge == to_edge:
            continue

        try:
            path, dist = net.getShortestPath(net.getEdge(from_edge), net.getEdge(to_edge))
            if not path:
                continue
        except:
            continue

        depart = random.randint(8 * 3600, 12 * 3600)
        background_trips.append({
            "id": f"bg_{count}_{random.randint(1000, 9999)}",
            "type": "car",  # Explicitly assigning 'car' type for background trips
            "depart": depart,
            "from_edge": from_edge,
            "to_edge": to_edge
        })

    # Sắp xếp tất cả trips theo thời gian xuất phát
    all_trips_sorted = deliveries_sorted + background_trips
    all_trips_sorted = sorted(all_trips_sorted, key=lambda x: x["depart"])

    # Ghi các trips đã sắp xếp vào file XML
    for trip in all_trips_sorted:
        # Ensure the 'type' is not missing
        trip_type = trip.get("type", "unknown")  # Default to "unknown" if 'type' is missing
        ET.SubElement(trips, "trip", {
            "id": trip["id"],
            "type": trip_type,  # Ensure there's always a 'type'
            "depart": str(trip["depart"]),
            "from": trip["from_edge"],
            "to": trip["to_edge"]
        })

    print(f"✅ Tạo {count}/{background_vehicle_count} background trips thành công.")
    if count < background_vehicle_count:
        print(f"⚠️ Một số trip bị bỏ do không tìm được đường đi hợp lệ.")

    # Ghi tất cả trips vào file XML
    ET.ElementTree(trips).write(output_path)
    print(f"✅ Ghi file trips: {output_path}")

# ==== MAIN ==== #
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
        deliveries = json.load(f)

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
    # ✅ Chuyển trips → rou + thêm <vType>
    ROUTE_PATH = f"{BASE}/{ZONE}.weather_adjusted.rou.xml"
    convert_and_patch_routes(
        net_path=NET_PATH,
        trip_path=TRIPS_ADJUSTED_PATH,
        route_path=ROUTE_PATH
    )
    print(f"\n✅ Hoàn tất! File SUMO đã sẵn sàng:\n→ {ROUTE_PATH}")

if __name__ == "__main__":
    main()
