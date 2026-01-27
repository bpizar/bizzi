class FormFields {
  List<FormField> items = [];

  FormFields.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final formField = new FormField.fromJsonMap(item);
      items.add(formField);
    }
  }
}

class FormField {
  bool isEnabled;
  int id;
  String name;
  String placeholder;
  String datatype;
  String constraints;
  String value;

  FormField({
    this.isEnabled,
    this.id,
    this.name,
    this.placeholder,
    this.datatype,
    this.constraints,
    this.value,
  });

  FormField.fromJsonMap(Map<String, dynamic> json) {
    isEnabled = json['isEnabled'];
    id = json['id'];
    name = json['name'];
    placeholder = json['placeholder'];
    datatype = json['datatype'];
    constraints = json['constraints'];
    value = json['value'];
  }

  Map<String, dynamic> toJson() => {
        'isEnabled': isEnabled,
        'id': id,
        'name': name,
        'placeholder': placeholder,
        'datatype': datatype,
        'constraints': constraints,
        'value': value,
      };
}
