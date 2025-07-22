import requests
import datetime
import json
import os

def fetch_weather():
    now = datetime.datetime.now(datetime.timezone.utc).astimezone()
    timestamp = now.isoformat()

    try:
        # API nguồn NEA
        temp_url = "https://api.data.gov.sg/v1/environment/air-temperature"
        hum_url = "https://api.data.gov.sg/v1/environment/relative-humidity"
        rain_url = "https://api.data.gov.sg/v1/environment/rainfall"

        # Gọi API
        temperature_data = requests.get(temp_url).json()
        humidity_data = requests.get(hum_url).json()
        rainfall_data = requests.get(rain_url).json()

        # Hàm trung bình giá trị từ readings
        def extract_mean(payload):
            readings = payload["items"][0]["readings"]
            values = [r["value"] for r in readings]
            return round(sum(values) / len(values), 2)

        weather = {
            "temperature": extract_mean(temperature_data),
            "humidity": extract_mean(humidity_data),
            "rainfall": extract_mean(rainfall_data),
            "timestamp": timestamp
        }

        # Tạo thư mục & lưu
        out_dir = "data/raw/weather_data"
        os.makedirs(out_dir, exist_ok=True)
        out_path = os.path.join(out_dir, f"weather_{now.strftime('%Y%m%d_%H%M%S')}.json")

        with open(out_path, "w") as f:
            json.dump(weather, f, indent=2)

        print(f"✅ Saved to {out_path}")
        print(weather)

        return weather

    except Exception as e:
        print("❌ Error fetching weather:", e)
        return None

if __name__ == "__main__":
    fetch_weather()
