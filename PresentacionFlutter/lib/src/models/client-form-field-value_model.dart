class ClientFormFieldValues {
  List<ClientFormFieldValue> items = [];

  ClientFormFieldValues.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final clientFormFieldValue = new ClientFormFieldValue.fromJsonMap(item);
      items.add(clientFormFieldValue);
    }
  }
}

class ClientFormFieldValue {
  int id;
  int idfClientFormValue;
  int idfFormField;
  String value;

  ClientFormFieldValue({
    this.id,
    this.idfClientFormValue,
    this.idfFormField,
    this.value,
  });

  ClientFormFieldValue.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    idfClientFormValue = json['idfClientFormValue'];
    idfFormField = json['idfFormField'];
    value = json['value'];
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'idfClientFormValue': idfClientFormValue,
        'idfFormField': idfFormField,
        'value': value,
      };
}
