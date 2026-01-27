class ProjectFormImageValues {
  List<ProjectFormImageValue> items = [];

  ProjectFormImageValues.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final period = new ProjectFormImageValue.fromJsonMap(item);
      items.add(period);
    }
  }
}

class ProjectFormImageValue {
  int id;
  int idfProject;
  int idfProjectForm;
  String image;
  String formDateTime;
  String dateTime;

  ProjectFormImageValue({
    this.id,
    this.idfProject,
    this.idfProjectForm,
    this.image,
    this.formDateTime,
    this.dateTime,
  });

  ProjectFormImageValue.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfProject = json['idfProject'];
    idfProjectForm = json['idfProjectForm'];
    image = json['image'];
    formDateTime = json['formDateTime'];
    dateTime = json['dateTime'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfProject': idfProject,
        'idfProjectForm': idfProjectForm,
        'image': image,
        'formDateTime': formDateTime,
        'dateTime': dateTime,
      };
}
