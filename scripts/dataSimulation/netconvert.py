import os
import subprocess
from dotenv import load_dotenv
from shutil import which
import xml.etree.ElementTree as ET

# Tải biến môi trường
load_dotenv()
sumo_home = os.getenv("SUMO_HOME")

if not sumo_home:
    raise EnvironmentError("❌ SUMO_HOME chưa được thiết lập. Hãy cấu hình trong .env hoặc môi trường hệ thống.")

if which("netconvert") is None:
    raise EnvironmentError("❌ 'netconvert' không tìm thấy. Hãy đảm bảo SUMO đã được cài và thêm vào PATH.")

input_dir = "data/raw/osm"
output_dir = "data/raw/sumo"
os.makedirs(output_dir, exist_ok=True)

areas = ["Jurong", "NUS", "CBD", "Changi"]

common_opts = [
    "--ignore-errors",
    "--geometry.remove",
    "--ramps.guess",
    "--junctions.join",
    "--remove-edges.isolated",
    "--keep-edges.by-vclass", "passenger",
    "--speed-in-kmh",
    "--default.speed", "50.0",
    "--default.lanenumber", "1",
    "--default.priority", "1",
]

def ensure_edge_attrs(net_file):
    tree = ET.parse(net_file)
    root = tree.getroot()
    for edge in root.findall("edge"):
        for lane in edge.findall("lane"):
            if 'speed' not in lane.attrib:
                lane.set("speed", "13.9")  # m/s
            if 'length' not in lane.attrib:
                lane.set("length", "100.0")
    tree.write(net_file)

# Chuyển đổi OSM -> NET + sửa net.xml
for area in areas:
    osm_path = os.path.join(input_dir, f"{area}.osm")
    net_path = os.path.join(output_dir, f"{area.lower()}.net.xml")

    if not os.path.exists(osm_path):
        print(f"\033[93m⚠️ Không tìm thấy file OSM: {osm_path}\033[0m")
        continue

    command = [
        "netconvert",
        "--osm-files", osm_path,
        "-o", net_path,
        *common_opts
    ]

    print(f"\n🚧 Đang chuyển đổi {area}...")
    try:
        subprocess.run(command, check=True)
        ensure_edge_attrs(net_path)  # <-- ✅ THÊM DÒNG NÀY
        print(f"\033[92m✅ Thành công: {net_path}\033[0m")
    except subprocess.CalledProcessError as e:
        print(f"\033[91m❌ Lỗi khi chuyển {area}: {e}\033[0m")
