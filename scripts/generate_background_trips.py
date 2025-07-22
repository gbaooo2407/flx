from sumolib.net import readNet
import random

def generate_background_trips_from_lta(json_path, net_path, start_hour=8, end_hour=12, seed=42):
    import json
    from sumolib.net import readNet
    import random

    with open(json_path) as f:
        lta_data = json.load(f)

    net = readNet(net_path)
    edges = [e for e in net.getEdges() if not e.isSpecial()]
    edge_ids = {e.getID() for e in edges}
    trips = []
    random.seed(seed)

    for record in lta_data["value"]:
        edge_id = record.get("LinkID") or record.get("SegmentID")
        if edge_id not in edge_ids:
            continue
        for _ in range(2):  # ví dụ: sinh 2 xe / record
            from_edge = net.getEdge(edge_id)
            to_edge = random.choice(edges)
            while to_edge.getID() == edge_id:
                to_edge = random.choice(edges)

            from_x, from_y = from_edge.getFromNode().getCoord()
            to_x, to_y = to_edge.getToNode().getCoord()
            from_lon, from_lat = net.convertXY2LonLat(from_x, from_y)
            to_lon, to_lat = net.convertXY2LonLat(to_x, to_y)

            depart = random.randint(start_hour * 3600, end_hour * 3600)
            trips.append((depart, from_lon, from_lat, to_lon, to_lat, "car"))

    return trips
