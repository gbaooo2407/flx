# scripts/graph_utils.py

import xml.etree.ElementTree as ET
import json
import os

def load_graph(net_path, default_speed=13.9, default_length=100.0):
    """
    Load net.xml → dict G[edge_id] = {"speed_limit", "length", ...}
    """
    tree = ET.parse(net_path)
    root = tree.getroot()
    G = {}

    for edge in root.findall(".//edge"):
        eid = edge.get("id")
        if eid.startswith(":"):  # bỏ edges nội bộ
            continue
        speed = default_speed
        length = default_length
        for lane in edge.findall("lane"):
            speed = float(lane.get("speed", default_speed))
            length = float(lane.get("length", default_length))
            break
        G[eid] = {"speed_limit": speed, "length": length, "closed": False, "delay": 0.0}
    return G

def update_speed_limits(G, speed_band_file):
    with open(speed_band_file) as f:
        data = json.load(f)["value"]
    for item in data:
        eid = item["LinkID"]
        if eid in G:
            try:
                G[eid]["speed_limit"] = float(item["SpeedBand"]) * 5 / 3.6  # 5 → 25 km/h
            except:
                continue
    print(f"✅ Updated speed limits for {len(data)} segments")

def update_graph_with_incidents(G, incident_file, roadwork_file):
    def apply_closure(file_path, reason):
        with open(file_path) as f:
            data = json.load(f)["value"]
        for item in data:
            eid = item.get("LinkID") or item.get("SegmentID")
            if eid and eid in G:
                G[eid]["closed"] = True
                G[eid]["delay"] = G[eid].get("delay", 0.0) + 30  # default 30s delay
    apply_closure(incident_file, "incident")
    apply_closure(roadwork_file, "roadwork")
    print("✅ Applied incident and roadwork closures")

def update_road_openings(G, road_open_file, current_time_unix):
    with open(road_open_file) as f:
        data = json.load(f)["value"]
    for item in data:
        eid = item.get("SegmentID")
        if eid not in G:
            continue
        try:
            start = parse_iso(item["StartTime"])
            end = parse_iso(item["EndTime"])
            if start <= current_time_unix <= end:
                G[eid]["closed"] = False
            else:
                G[eid]["closed"] = True
        except:
            continue
    print("✅ Updated road openings")

def update_faulty_traffic_lights(G, faulty_file):
    with open(faulty_file) as f:
        data = json.load(f)["value"]
    for item in data:
        node = item.get("NodeID")
        if node:
            for eid, attr in G.items():
                if node in eid:  # hơi đơn giản, cần refine
                    attr["delay"] = attr.get("delay", 0.0) + 30
    print("✅ Updated delay for faulty traffic lights")

def parse_iso(tstr):
    from datetime import datetime
    import time
    try:
        dt = datetime.strptime(tstr, "%Y-%m-%dT%H:%M:%S")
        return int(time.mktime(dt.timetuple()))
    except:
        return 0
