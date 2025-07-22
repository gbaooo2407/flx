# scripts/preprocess_sequence.py

import numpy as np
from sklearn.preprocessing import StandardScaler
import json

def normalize_and_sequence(data_array, window_size=4):
    """
    Chuẩn hóa và tạo chuỗi thời gian dạng sliding window
    Input: np.array (T, features)
    Output: np.array (T - window_size, window_size, features)
    """
    scaler = StandardScaler().fit(data_array[:1000])  # dùng 1000 dòng đầu làm mẫu
    normalized = scaler.transform(data_array)

    sequences = [
        normalized[i - window_size:i]
        for i in range(window_size, len(normalized))
    ]
    return np.stack(sequences)


def load_weather_traffic_sequence(weather_files, speed_files):
    """
    Tải và kết hợp chuỗi thời tiết + tốc độ thành (T, features)
    Assumes each file is 1 timestamp
    """
    data = []

    for w_file, s_file in zip(weather_files, speed_files):
        try:
            with open(w_file) as f:
                w = json.load(f)
            with open(s_file) as f:
                s = json.load(f)

            speed_avg = np.mean([int(x["SpeedBand"]) * 5 for x in s["value"] if "SpeedBand" in x])
            rain = w["hourly"]["precipitation"][0]
            temp = w["hourly"]["temperature_2m"][0]
            humi = w["hourly"]["relativehumidity_2m"][0]

            data.append([speed_avg, rain, temp, humi])
        except Exception as e:
            print(f"❌ Bỏ qua file lỗi {w_file}: {e}")
            continue

    return np.array(data)


if __name__ == "__main__":
    import glob

    weather_files = sorted(glob.glob("data/raw/weather_data/weather_*.json"))
    speed_files   = sorted(glob.glob("data/raw/lta/traffic_speed_band/*.json"))

    print(f"🔢 Found {len(weather_files)} weather, {len(speed_files)} traffic files")

    array = load_weather_traffic_sequence(weather_files, speed_files)
    print("📐 Raw shape:", array.shape)

    sequences = normalize_and_sequence(array, window_size=4)
    print("✅ Sequence shape:", sequences.shape)

    np.save("data/processed/sequences/traffic_weather_seq.npy", sequences)
    print("💾 Saved to data/processed/sequences/traffic_weather_seq.npy")
