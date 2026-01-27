class ProjectFormValues {
  List<ProjectFormValue> items = [];

  ProjectFormValues.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final projectFormFieldValue = new ProjectFormValue.fromJsonMap(item);
      items.add(projectFormFieldValue);
    }
  }
}

class ProjectFormValue {
  int id;
  int idfProject;
  int idfProjectForm;
  String formDateTime;
  String dateTime;

  ProjectFormValue(
      {this.id,
      this.idfProject,
      this.idfProjectForm,
      this.formDateTime,
      this.dateTime});

  ProjectFormValue.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfProject = json['idfProject'];
    idfProjectForm = json['idfProjectForm'];
    formDateTime = json['formDateTime'];
    dateTime = json['dateTime'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfProject': idfProject,
        'idfProjectForm': idfProjectForm,
        'formDateTime': formDateTime,
        'dateTime': dateTime
      };
}
