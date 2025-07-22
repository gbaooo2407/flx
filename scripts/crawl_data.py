# scripts/crawl_data.py

import requests, json, os, time
from datetime import datetime
from apscheduler.schedulers.background import BackgroundScheduler
from dotenv import load_dotenv

load_dotenv()
API_KEY = os.getenv("LTA_API_KEY")
LTA_HEADERS = {"AccountKey": API_KEY, "accept": "application/json"}

# ==== CẤU HÌNH ====
LTA_ENDPOINTS = {
    "traffic_speed_band": "v3/TrafficSpeedBands",
    "traffic_incident": "TrafficIncidents",
    "faulty_traffic_light": "FaultyTrafficLights",
    "road_work": "RoadWorks",
    "road_open": "RoadOpenings",
    "traffic_flow": "TrafficFlow",
}

BASE_LTA_URL = "https://datamall2.mytransport.sg/ltaodataservice/"
BASE_PATH = "data/raw/lta"
WEATHER_PATH = "data/raw/weather_data"

def timestamp_str():
    return datetime.now().strftime("%Y%m%d%H%M")

# ==== LTA API CALLER ====
def fetch_lta(name, endpoint):
    try:
        r = requests.get(BASE_LTA_URL + endpoint, headers=LTA_HEADERS)
        r.raise_for_status()
        data = r.json()
        ts = timestamp_str()

        save_dir = os.path.join(BASE_PATH, name)
        os.makedirs(save_dir, exist_ok=True)

        # Nếu là Traffic Flow, cần gọi thêm link
        if name == "traffic_flow":
            file_link = data["value"][0]["Link"]
            fr = requests.get(file_link)
            fr.raise_for_status()
            with open(f"{save_dir}/{ts}.json", "wb") as f:
                f.write(fr.content)
        else:
            with open(f"{save_dir}/{ts}.json", "w") as f:
                json.dump(data, f, indent=2)

        print(f"✅ {name} at {ts}")
    except Exception as e:
        print(f"❌ {name} error:", e)

# ==== WEATHER ====
def fetch_weather():
    try:
        url = (
            "https://api.open-meteo.com/v1/forecast"
            "?latitude=1.3521&longitude=103.8198"
            "&hourly=temperature_2m,precipitation,relative_humidity_2m"
        )
        r = requests.get(url)
        r.raise_for_status()
        data = r.json()

        ts = timestamp_str()
        os.makedirs(WEATHER_PATH, exist_ok=True)
        with open(f"{WEATHER_PATH}/weather_{ts}.json", "w") as f:
            json.dump(data, f, indent=2)

        print(f"🌦️  weather at {ts}")
    except Exception as e:
        print("❌ weather error:", e)

# ==== SCHEDULER ====
def start_scheduler():
    scheduler = BackgroundScheduler()

    # LTA
    for name, endpoint in LTA_ENDPOINTS.items():
        interval = 5 if name == "traffic_speed_band" else 15
        scheduler.add_job(lambda n=name, e=endpoint: fetch_lta(n, e), "interval", minutes=interval)

    # Weather
    scheduler.add_job(fetch_weather, "interval", minutes=15)

    scheduler.start()
    print("📡 Scheduler started. Press Ctrl+C to stop.")

if __name__ == "__main__":
    start_scheduler()
    try:
        while True:
            time.sleep(60)
    except (KeyboardInterrupt, SystemExit):
        print("👋 Scheduler stopped.")
