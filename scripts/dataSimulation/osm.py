import os
from pathlib import Path
import requests
import osmnx as ox
import time

# Output
output_dir = Path("data/raw/osm")
output_dir.mkdir(parents=True, exist_ok=True)

# Overpass endpoint
OVERPASS_URL = "http://overpass-api.de/api/interpreter"

# Danh sách khu vực
areas = {
    "CBD": "Downtown Core, Singapore",
    "Changi": "Changi, Singapore"
}

def get_bbox(place):
    gdf = ox.geocode_to_gdf(place)
    bounds = gdf.geometry.iloc[0].bounds  # minx, miny, maxx, maxy
    return bounds

def build_overpass_query(minx, miny, maxx, maxy):
    return f"""
    [out:xml][timeout:60];
    (
      way["highway"]({miny},{minx},{maxy},{maxx});
      >;
    );
    out body;
    """

for name, query in areas.items():
    print(f"📥 Đang tải OSM XML cho: {name} ...")
    try:
        minx, miny, maxx, maxy = get_bbox(query)
        overpass_query = build_overpass_query(minx, miny, maxx, maxy)

        response = requests.post(OVERPASS_URL, data={"data": overpass_query})
        response.raise_for_status()

        output_path = output_dir / f"{name}.osm"
        with open(output_path, "w", encoding="utf-8") as f:
            f.write(response.text)

        print(f"✅ Đã lưu: {output_path}")
        time.sleep(10)  # nghỉ giữa các request để tránh bị chặn
    except Exception as e:
        print(f"❌ Lỗi với {name}: {e}")
