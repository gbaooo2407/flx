# scripts/weather_utils.py

import json

def adjust_speed(base_speed, weather_file):
    """
    Giảm tốc độ dựa trên mưa và nhiệt độ trong dữ liệu thời tiết
    """
    try:
        with open(weather_file) as f:
            data = json.load(f)

        rain = data["hourly"]["precipitation"][0]       # mm/h
        temp = data["hourly"]["temperature_2m"][0]       # °C

        speed_factor = max(0.5, 1 - rain / 100)          # Mưa lớn → giảm tối đa 50%
        if temp > 30:
            speed_factor *= 0.95                        # Nhiệt độ cao → giảm thêm 5%

        return base_speed * speed_factor

    except Exception as e:
        print("❌ adjust_speed error:", e)
        return base_speed


def extract_weather_features(weather_file):
    """
    Trích xuất đặc trưng thời tiết cho mô hình học (GNN/PPO)
    Trả về [rain_mm_per_h, temperature_c, relative_humidity]
    """
    try:
        with open(weather_file) as f:
            data = json.load(f)
        return [
            data["hourly"]["precipitation"][0],
            data["hourly"]["temperature_2m"][0],
            data["hourly"]["relativehumidity_2m"][0],
        ]
    except Exception as e:
        print("❌ extract_weather_features error:", e)
        return [0.0, 25.0, 80.0]

import xml.etree.ElementTree as ET

def adjust_vehicle_speeds(trip_input_path, weather_path, trip_output_path):
    """
    Điều chỉnh tốc độ toàn bộ vehicle trong file trips.xml dựa trên thời tiết
    """
    try:
        tree = ET.parse(trip_input_path)
        root = tree.getroot()

        # Tính tốc độ mới
        base_speed = 13.9  # m/s
        new_speed = adjust_speed(base_speed, weather_path)

        for v in root.findall(".//vehicle"):
            v.set("speed", str(round(new_speed, 2)))

        tree.write(trip_output_path)
        print(f"✅ Adjusted vehicle speed → {round(new_speed,2)} m/s → Saved to {trip_output_path}")

    except Exception as e:
        print("❌ adjust_vehicle_speeds error:", e)
        
import requests
from datetime import datetime

def fetch_weather():
    """
    Gọi API thời tiết Open-Meteo (Singapore) → trả về JSON dict
    """
    try:
        url = (
            "https://api.open-meteo.com/v1/forecast"
            "?latitude=1.3521&longitude=103.8198"
            "&hourly=temperature_2m,precipitation,relative_humidity_2m"
        )
        r = requests.get(url)
        r.raise_for_status()
        return r.json()

    except Exception as e:
        print("❌ fetch_weather error:", e)
        return {}