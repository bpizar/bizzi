class ClientFormsByClient {
  List<ClientFormByClient> items = [];

  ClientFormsByClient.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final clientFormByClient = new ClientFormByClient.fromJsonMap(item);
      items.add(clientFormByClient);
    }
  }
}

class ClientFormByClient {
  int idfClientFormValue;
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

  ClientFormByClient({
    this.idfClientFormValue,
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

  ClientFormByClient.fromJsonMap(Map<String, dynamic> json) {
    idfClientFormValue = json['idfClientFormValue'];
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
        'idfClientFormValue': idfClientFormValue,
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
