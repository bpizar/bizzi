class StaffFormValues {
  List<StaffFormValue> items = [];

  StaffFormValues.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final staffFormFieldValue = new StaffFormValue.fromJsonMap(item);
      items.add(staffFormFieldValue);
    }
  }
}

class StaffFormValue {
  int id;
  int idfStaff;
  int idfStaffForm;
  String formDateTime;
  String dateTime;

  StaffFormValue(
      {this.id,
      this.idfStaff,
      this.idfStaffForm,
      this.formDateTime,
      this.dateTime});

  StaffFormValue.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfStaff = json['idfStaff'];
    idfStaffForm = json['idfStaffForm'];
    formDateTime = json['formDateTime'];
    dateTime = json['dateTime'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfStaff': idfStaff,
        'idfStaffForm': idfStaffForm,
        'formDateTime': formDateTime,
        'dateTime': dateTime
      };
}
