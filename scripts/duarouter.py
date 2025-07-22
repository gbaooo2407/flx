import os
import subprocess

from scripts.utils.graph_utils import load_graph
from scripts.utils.weather_utils import adjust_vehicle_speeds

# ==== CẤU HÌNH ==== #
ZONE = "cbd"  # Thay bằng "nus", "jurong", "changi" nếu muốn
BASE = f"data/raw/sumo/{ZONE}"

NET_PATH = f"{BASE}/{ZONE}.net.xml"
TRIPS_ADJUSTED_PATH = f"{BASE}/{ZONE}.weather_adjusted.trips.xml"
ROUTE_PATH = f"{BASE}/{ZONE}.weather_adjusted.rou.xml"

WEATHER_DIR = "data/raw/weather_data"
LTA_TIME = "202507212335"  # timestamp thống nhất
LTA_DIR = "data/raw/lta"

# ==== TOOL ==== #
def convert_and_patch_routes(net_path, trip_path, route_path):
    """
    This function will use the existing trip file (weather adjusted trips) to generate routes.
    """
    subprocess.run([
        "duarouter",
        "-n", net_path,
        "--route-files", trip_path,
        "-o", route_path,
        "--ignore-errors"
    ], check=True)

    print(f"✅ Đã chuyển trips → routes và thêm vType → {route_path}")

def main():
    print(f"\n📍 Khu vực: {ZONE.upper()}")

    print("🧠 Load graph và cập nhật LTA...")
    G = load_graph(NET_PATH)

    # If there's no need to update the graph anymore, we skip it for simplicity.
    # If you still need to update the graph (e.g. for traffic data, incidents), you can include these steps.
    print("🌧️ Điều chỉnh tốc độ theo thời tiết...")
    latest_weather_file = get_latest_weather_file()
    adjust_vehicle_speeds(
        trip_input_path=TRIPS_ADJUSTED_PATH,
        weather_path=latest_weather_file,
        trip_output_path=TRIPS_ADJUSTED_PATH,
    )
    
    # Chuyển trips → rou + thêm <vType>
    convert_and_patch_routes(
        net_path=NET_PATH,
        trip_path=TRIPS_ADJUSTED_PATH,
        route_path=ROUTE_PATH
    )
    print(f"\n✅ Hoàn tất! File SUMO đã sẵn sàng:\n→ {ROUTE_PATH}")

if __name__ == "__main__":
    main()
