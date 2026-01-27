class ProjectFormFieldValues {
  List<ProjectFormFieldValue> items = [];

  ProjectFormFieldValues.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final projectFormFieldValue = new ProjectFormFieldValue.fromJsonMap(item);
      items.add(projectFormFieldValue);
    }
  }
}

class ProjectFormFieldValue {
  int id;
  int idfProjectFormValue;
  int idfFormField;
  String value;

  ProjectFormFieldValue({
    this.id,
    this.idfProjectFormValue,
    this.idfFormField,
    this.value,
  });

  ProjectFormFieldValue.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfProjectFormValue = json['idfProjectFormValue'];
    idfFormField = json['idfFormField'];
    value = json['value'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfProjectFormValue': idfProjectFormValue,
        'idfFormField': idfFormField,
        'value': value,
      };
}
