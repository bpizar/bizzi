class Dailylogs {
  List<Dailylog> items = [];

  Dailylogs.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final dailylog = new Dailylog.fromJsonMap(item);
      items.add(dailylog);
    }
  }
}

class Dailylog {
  int id;
  String dateDailyLog;
  String description;
  String color;
  String abm;
  String projectName;

  Dailylog({
    this.id,
    this.dateDailyLog,
    this.description,
    this.color,
    this.abm,
    this.projectName,
  });

  Dailylog.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    dateDailyLog = json['dateDailyLog'];
    description = json['description'];
    color = json['color'];
    abm = json['abm'];
    projectName = json['projectName'];
  }
}
