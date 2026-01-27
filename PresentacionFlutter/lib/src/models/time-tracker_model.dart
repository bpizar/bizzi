class TimeTrackers {
  List<TimeTracker> items = [];

  TimeTrackers.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final timeTracker = new TimeTracker.fromJsonMap(item);
      items.add(timeTracker);
    }
  }
}

class TimeTracker {
  int id;
  String start;
  dynamic end;
  String color;
  String projectName;

  TimeTracker({
    this.id,
    this.start,
    this.end,
    this.color,
    this.projectName,
  });

  TimeTracker.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    start = json['start'];
    end = json['end'];
    color = json['color'];
    projectName = json['projectName'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'start': start,
        'end': end,
        'color': color,
        'projectName': projectName,
      };
}
