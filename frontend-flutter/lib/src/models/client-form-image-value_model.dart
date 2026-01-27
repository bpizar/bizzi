class ClientFormImageValues {
  List<ClientFormImageValue> items = [];

  ClientFormImageValues.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final period = new ClientFormImageValue.fromJsonMap(item);
      items.add(period);
    }
  }
}

class ClientFormImageValue {
  int id;
  int idfClient;
  int idfClientForm;
  String image;
  String formDateTime;
  String dateTime;

  ClientFormImageValue({
    this.id,
    this.idfClient,
    this.idfClientForm,
    this.image,
    this.formDateTime,
    this.dateTime,
  });

  ClientFormImageValue.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfClient = json['idfClient'];
    idfClientForm = json['idfClientForm'];
    image = json['image'];
    formDateTime = json['formDateTime'];
    dateTime = json['dateTime'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfClient': idfClient,
        'idfClientForm': idfClientForm,
        'image': image,
        'formDateTime': formDateTime,
        'dateTime': dateTime,
      };
}
