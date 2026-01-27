class Projects {
  List<Project> items = [];

  Projects.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final project = new Project.fromJsonMap(item);
      items.add(project);
    }
  }
}

class Project {
  double totalHours;
  dynamic abm;
  dynamic idSpp;
  int id;
  String projectName;
  String description;
  String beginDate;
  String endDate;
  String color;
  String state;
  int visible;
  String creationDate;
  String address;
  dynamic city;
  String phone1;
  String phone2;
  List<dynamic> hDailylogs;
  List<dynamic> hInjuries;
  List<dynamic> projectOwners;
  List<dynamic> projectPettyCash;
  List<dynamic> projectsClients;
  List<dynamic> scheduling;
  List<dynamic> staffProjectPosition;
  List<dynamic> tasks;

  Project({
    this.totalHours,
    this.abm,
    this.idSpp,
    this.id,
    this.projectName,
    this.description,
    this.beginDate,
    this.endDate,
    this.color,
    this.state,
    this.visible,
    this.creationDate,
    this.address,
    this.city,
    this.phone1,
    this.phone2,
    this.hDailylogs,
    this.hInjuries,
    this.projectOwners,
    this.projectPettyCash,
    this.projectsClients,
    this.scheduling,
    this.staffProjectPosition,
    this.tasks,
  });

  Project.fromJsonMap(Map<String, dynamic> json) {
    totalHours = json['totalHours'].toDouble();
    abm = json['abm'];
    idSpp = json['idSpp'];
    id = json['id'];
    projectName = json['projectName'];
    description = json['description'];
    beginDate = json['beginDate'];
    endDate = json['endDate'];
    color = json['color'];
    state = json['state'];
    visible = json['visible'];
    creationDate = json['creationDate'];
    address = json['address'];
    city = json['city'];
    phone1 = json['phone1'];
    phone2 = json['phone2'];
    hDailylogs = json['h_dailylogs'];
    hInjuries = json['h_injuries'];
    projectOwners = json['project_owners'];
    projectPettyCash = json['project_petty_cash'];
    projectsClients = json['projects_clients'];
    scheduling = json['scheduling'];
    staffProjectPosition = json['staff_project_position'];
    tasks = json['tasks'];
  }
}
