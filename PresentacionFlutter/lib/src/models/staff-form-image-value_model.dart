class StaffFormImageValues {
  List<StaffFormImageValue> items = [];

  StaffFormImageValues.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final period = new StaffFormImageValue.fromJsonMap(item);
      items.add(period);
    }
  }
}

class StaffFormImageValue {
  int id;
  int idfStaff;
  int idfStaffForm;
  String image;
  String formDateTime;
  String dateTime;

  StaffFormImageValue({
    this.id,
    this.idfStaff,
    this.idfStaffForm,
    this.image,
    this.formDateTime,
    this.dateTime,
  });

  StaffFormImageValue.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfStaff = json['idfStaff'];
    idfStaffForm = json['idfStaffForm'];
    image = json['image'];
    formDateTime = json['formDateTime'];
    dateTime = json['dateTime'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfStaff': idfStaff,
        'idfStaffForm': idfStaffForm,
        'image': image,
        'formDateTime': formDateTime,
        'dateTime': dateTime,
      };
}
