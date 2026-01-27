class ProjectFormsByProject {
  List<ProjectFormByProject> items = [];

  ProjectFormsByProject.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final projectFormByProject = new ProjectFormByProject.fromJsonMap(item);
      items.add(projectFormByProject);
    }
  }
}

class ProjectFormByProject {
  int idfProjectFormValue;
  String formDateTime;
  dynamic nextDate;
  int quantity;
  int id;
  String name;
  String description;
  String template;
  String information;
  int idfRecurrence;
  int idfRecurrenceDetail;

  ProjectFormByProject({
    this.idfProjectFormValue,
    this.formDateTime,
    this.nextDate,
    this.quantity,
    this.id,
    this.name,
    this.description,
    this.template,
    this.information,
    this.idfRecurrence,
    this.idfRecurrenceDetail,
  });

  ProjectFormByProject.fromJsonMap(Map<String, dynamic> json) {
    idfProjectFormValue = json['idfProjectFormValue'];
    formDateTime = json['formDateTime'];
    nextDate = json['nextDate'];
    quantity = json['quantity'];
    id = json['id'];
    name = json['name'];
    description = json['description'];
    template = json['template'];
    information = json['information'];
    idfRecurrence = json['idfRecurrence'];
    idfRecurrenceDetail = json['idfRecurrenceDetail'];
  }

  Map<String, dynamic> toJson() => {
        'idfProjectFormValue': idfProjectFormValue,
        'formDateTime': formDateTime,
        'nextDate': nextDate,
        'quantity': quantity,
        'id': id,
        'name': name,
        'description': description,
        'template': template,
        'information': information,
        'idfRecurrence': idfRecurrence,
        'idfRecurrenceDetail': idfRecurrenceDetail,
      };
}
