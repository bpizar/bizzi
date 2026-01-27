class ClientFormValues {
  List<ClientFormValue> items = [];

  ClientFormValues.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final clientFormFieldValue = new ClientFormValue.fromJsonMap(item);
      items.add(clientFormFieldValue);
    }
  }
}

class ClientFormValue {
  int id;
  int idfClient;
  int idfClientForm;
  String formDateTime;
  String dateTime;

  ClientFormValue(
      {this.id,
      this.idfClient,
      this.idfClientForm,
      this.formDateTime,
      this.dateTime});

  ClientFormValue.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfClient = json['idfClient'];
    idfClientForm = json['idfClientForm'];
    formDateTime = json['formDateTime'];
    dateTime = json['dateTime'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfClient': idfClient,
        'idfClientForm': idfClientForm,
        'formDateTime': formDateTime,
        'dateTime': dateTime
      };
}
