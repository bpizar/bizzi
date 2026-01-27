class Dailylogs {
  List<Dailylog> items = [];

  Dailylogs.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final dailyLog = new Dailylog.fromJsonMap(item);
      items.add(dailyLog);
    }
  }
}

class Dailylog {
  int id;
  int idfPeriod;
  int projectId;
  int clientId;
  String date;
  int userId;
  dynamic placement;
  String staffOnShift;
  dynamic generalMood;
  dynamic interactionStaff;
  dynamic interactionPeers;
  dynamic school;
  dynamic attended;
  dynamic inHouseProg;
  dynamic comments;
  dynamic health;
  dynamic contactFamily;
  dynamic seriousOccurrence;
  dynamic other;
  String state;
  dynamic client;
  dynamic idfPeriodNavigation;
  dynamic project;
  List<dynamic> hDailylogInvolvedPeople;

  Dailylog({
    this.id,
    this.idfPeriod,
    this.projectId,
    this.clientId,
    this.date,
    this.userId,
    this.placement,
    this.staffOnShift,
    this.generalMood,
    this.interactionStaff,
    this.interactionPeers,
    this.school,
    this.attended,
    this.inHouseProg,
    this.comments,
    this.health,
    this.contactFamily,
    this.seriousOccurrence,
    this.other,
    this.state,
    this.client,
    this.idfPeriodNavigation,
    this.project,
    this.hDailylogInvolvedPeople,
  });

  Dailylog.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfPeriod = json['idfPeriod'];
    projectId = json['projectId'];
    clientId = json['clientId'];
    date = json['date'];
    userId = json['userId'];
    placement = json['placement'];
    staffOnShift = json['staffOnShift'];
    generalMood = json['generalMood'];
    interactionStaff = json['interactionStaff'];
    interactionPeers = json['interactionPeers'];
    school = json['school'];
    attended = json['attended'];
    inHouseProg = json['inHouseProg'];
    comments = json['comments'];
    health = json['health'];
    contactFamily = json['contactFamily'];
    seriousOccurrence = json['seriousOccurrence'];
    other = json['other'];
    state = json['state'];
    client = json['client'];
    idfPeriodNavigation = json['idfPeriodNavigation'];
    project = json['project'];
    hDailylogInvolvedPeople = json['h_dailylog_involved_people'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfPeriod': idfPeriod,
        'projectId': projectId,
        'clientId': clientId,
        'date': date,
        'userId': userId,
        'placement': placement,
        'StaffOnShift': staffOnShift,
        'generalMood': generalMood,
        'interactionStaff': interactionStaff,
        'interactionPeers': interactionPeers,
        'school': school,
        'attended': attended,
        'inHouseProg': inHouseProg,
        'comments': comments,
        'health': health,
        'contactFamily': contactFamily,
        'seriousOccurrence': seriousOccurrence,
        'other': other,
        'state': state,
        'client': client,
        'idfPeriodNavigation': idfPeriodNavigation,
        'project': project,
        'h_dailylog_involved_people': hDailylogInvolvedPeople,
      };
}
