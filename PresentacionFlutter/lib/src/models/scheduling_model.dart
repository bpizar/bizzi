class SchedulingList {
  List<Scheduling> items = [];

  SchedulingList.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final period = new Scheduling.fromJsonMap(item);
      items.add(period);
    }
  }
}

class Scheduling {
  String assignedToFullName;
  int idUser;
  String assignedToPosition;
  String projectName;
  String projectColor;
  int idfStaff;
  dynamic abm;
  bool draggable;
  bool resizable;
  int hours;
  String img;
  String subject;
  bool isDirty;
  int id;
  String state;
  String from;
  String to;
  int idfAssignedTo;
  dynamic allDay;
  dynamic idfCreatedBy;
  String creationDate;
  dynamic idDuplicate;
  int idfProject;
  dynamic idfPeriod;
  dynamic idDuplicateNavigation;
  dynamic idfAssignedToNavigation;
  dynamic idfPeriodNavigation;
  dynamic idfProjectNavigation;

  Scheduling({
    this.assignedToFullName,
    this.idUser,
    this.assignedToPosition,
    this.projectName,
    this.projectColor,
    this.idfStaff,
    this.abm,
    this.draggable,
    this.resizable,
    this.hours,
    this.img,
    this.subject,
    this.isDirty,
    this.id,
    this.state,
    this.from,
    this.to,
    this.idfAssignedTo,
    this.allDay,
    this.idfCreatedBy,
    this.creationDate,
    this.idDuplicate,
    this.idfProject,
    this.idfPeriod,
    this.idDuplicateNavigation,
    this.idfAssignedToNavigation,
    this.idfPeriodNavigation,
    this.idfProjectNavigation,
  });

  Scheduling.fromJsonMap(Map<String, dynamic> json) {
    assignedToFullName = json['assignedToFullName'];
    idUser = json['idUser'];
    assignedToPosition = json['assignedToPosition'];
    projectName = json['projectName'];
    projectColor = json['projectColor'];
    idfStaff = json['idfStaff'];
    abm = json['abm'];
    draggable = json['draggable'];
    resizable = json['resizable'];
    hours = json['hours'];
    img = json['img'];
    subject = json['subject'];
    isDirty = json['isDirty'];
    id = json['id'];
    state = json['state'];
    from = json['from'];
    to = json['to'];
    idfAssignedTo = json['idfAssignedTo'];
    allDay = json['allDay'];
    idfCreatedBy = json['idfCreatedBy'];
    creationDate = json['creationDate'];
    idDuplicate = json['idDuplicate'];
    idfProject = json['idfProject'];
    idfPeriod = json['idfPeriod'];
    idDuplicateNavigation = json['idDuplicateNavigation'];
    idfAssignedToNavigation = json['idfAssignedToNavigation'];
    idfPeriodNavigation = json['idfPeriodNavigation'];
    idfProjectNavigation = json['idfProjectNavigation'];
  }

  Map<String, dynamic> toJson() => {
        'assignedToFullName': assignedToFullName,
        'idUser': idUser,
        'assignedToPosition': assignedToPosition,
        'projectName': projectName,
        'projectColor': projectColor,
        'idfStaff': idfStaff,
        'abm': abm,
        'draggable': draggable,
        'resizable': resizable,
        'hours': hours,
        'img': img,
        'subject': subject,
        'isDirty': isDirty,
        'id': id,
        'state': state,
        'from': from,
        'to': to,
        'idfAssignedTo': idfAssignedTo,
        'allDay': allDay,
        'idfCreatedBy': idfCreatedBy,
        'creationDate': creationDate,
        'idDuplicate': idDuplicate,
        'idfProject': idfProject,
        'idfPeriod': idfPeriod,
        'idDuplicateNavigation': idDuplicateNavigation,
        'idfAssignedToNavigation': idfAssignedToNavigation,
        'idfPeriodNavigation': idfPeriodNavigation,
        'idfProjectNavigation': idfProjectNavigation,
      };
}
