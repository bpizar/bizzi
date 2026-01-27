class Schedules {
  List<Schedule> items = [];

  Schedules.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final schedule = new Schedule.fromJsonMap(item);
      items.add(schedule);
    }
  }
}

class Schedule {
  int id;
  String from;
  String to;
  String projectName;
  String color;

  Schedule({
    this.id,
    this.from,
    this.to,
    this.projectName,
    this.color,
  });

  Schedule.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    from = json['from'];
    to = json['to'];
    projectName = json['projectName'];
    color = json['color'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'from': from,
        'to': to,
        'projectName': projectName,
        'color': color,
      };
}
