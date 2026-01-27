class StaffFormsByStaff {
  List<StaffFormByStaff> items = [];

  StaffFormsByStaff.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final staffFormByStaff = new StaffFormByStaff.fromJsonMap(item);
      items.add(staffFormByStaff);
    }
  }
}

class StaffFormByStaff {
  int idfStaffFormValue;
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

  StaffFormByStaff({
    this.idfStaffFormValue,
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

  StaffFormByStaff.fromJsonMap(Map<String, dynamic> json) {
    idfStaffFormValue = json['idfStaffFormValue'];
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
        'idfStaffFormValue': idfStaffFormValue,
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
