class Periods {
  List<Period> items = [];

  Periods.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final period = new Period.fromJsonMap(item);
      items.add(period);
    }
  }
}

class Period {
  String dateJoin;
  String abm;
  int id;
  String state;
  String description;
  DateTime from;
  DateTime to;
  int idfCreatedBy;
  String creationDate;
  List<dynamic> hDailylogs;
  List<dynamic> hIncidents;
  List<dynamic> hInjuries;
  List<dynamic> projectOwners;
  List<dynamic> projectPettyCash;
  List<dynamic> projectsClients;
  List<dynamic> scheduling;
  List<dynamic> staffPeriodSettings;
  List<dynamic> staffProjectPosition;
  List<dynamic> tasks;
  List<dynamic> timeTrackingReview;

  Period({
    this.dateJoin,
    this.abm,
    this.id,
    this.state,
    this.description,
    this.from,
    this.to,
    this.idfCreatedBy,
    this.creationDate,
    this.hDailylogs,
    this.hIncidents,
    this.hInjuries,
    this.projectOwners,
    this.projectPettyCash,
    this.projectsClients,
    this.scheduling,
    this.staffPeriodSettings,
    this.staffProjectPosition,
    this.tasks,
    this.timeTrackingReview,
  });

  Period.fromJsonMap(Map<String, dynamic> json) {
    dateJoin = json['dateJoin'];
    abm = json['abm'];
    id = json['id'];
    state = json['state'];
    description = json['description'];
    from = DateTime.parse(json['from']);
    to = DateTime.parse(json['to']);
    idfCreatedBy = json['idfCreatedBy'];
    creationDate = json['creationDate'];
    hDailylogs = json['hDailylogs'];
    hIncidents = json['hIncidents'];
    hInjuries = json['hInjuries'];
    projectOwners = json['projectOwners'];
    projectPettyCash = json['projectPettyCash'];
    projectsClients = json['projectsClients'];
    scheduling = json['scheduling'];
    staffPeriodSettings = json['staffPeriodSettings'];
    staffProjectPosition = json['staffProjectPosition'];
    tasks = json['tasks'];
    timeTrackingReview = json['timeTrackingReview'];
  }

  Map<String, dynamic> toJson() => {
        'dateJoin': dateJoin,
        'abm': abm,
        'id': id,
        'state': state,
        'description': description,
        'from': from,
        'to': to,
        'idfCreatedBy': idfCreatedBy,
        'creationDate': creationDate,
        'hDailylogs': hDailylogs,
        'hIncidents': hIncidents,
        'hInjuries': hInjuries,
        'projectOwners': projectOwners,
        'projectPettyCash': projectPettyCash,
        'projectsClients': projectsClients,
        'scheduling': scheduling,
        'staffPeriodSettings': staffPeriodSettings,
        'staffProjectPosition': staffProjectPosition,
        'tasks': tasks,
        'timeTrackingReview': timeTrackingReview,
      };
}
