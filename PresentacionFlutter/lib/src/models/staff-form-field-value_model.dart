class StaffFormFieldValues {
  List<StaffFormFieldValue> items = [];

  StaffFormFieldValues.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final staffFormFieldValue = new StaffFormFieldValue.fromJsonMap(item);
      items.add(staffFormFieldValue);
    }
  }
}

class StaffFormFieldValue {
  int id;
  int idfStaffFormValue;
  int idfFormField;
  String value;

  StaffFormFieldValue({
    this.id,
    this.idfStaffFormValue,
    this.idfFormField,
    this.value,
  });

  StaffFormFieldValue.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfStaffFormValue = json['idfStaffFormValue'];
    idfFormField = json['idfFormField'];
    value = json['value'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfStaffFormValue': idfStaffFormValue,
        'idfFormField': idfFormField,
        'value': value,
      };
}
