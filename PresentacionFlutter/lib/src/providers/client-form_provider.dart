import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/client-form-field-value_model.dart';
import 'package:people_mobile/src/models/client-form-image-value_model.dart';
import 'package:people_mobile/src/models/client-form-value_model.dart';
import 'package:people_mobile/src/models/client-forms-by-client_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class ClientFormProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<ClientFormByClient>> getClientFormByClient(int idClient) async {
    final uri = urlProvider
        .getUri('/clientform/getallclientFormsByClient/' + idClient.toString());
    final resp = await http.get(uri);

    final decodedData = json.decode(resp.body);

    final clientFormsList = new ClientFormsByClient.fromJsonList(
        decodedData['clientFormsbyClient']);
    return clientFormsList.items;
  }

  Future<List<ClientFormByClient>> getClientFormByClientSearch(
      int idClient, String query) async {
    final uri = urlProvider
        .getUri('/clientform/getallclientFormsByClient/' + idClient.toString());
    final resp = await http.get(uri);

    final decodedData = json.decode(resp.body);

    final clientFormsList = new ClientFormsByClient.fromJsonList(
        decodedData['clientFormsbyClient']);
    return clientFormsList.items.where((filter) {
      return filter.name.toLowerCase().contains(query.toLowerCase());
    }).toList();
  }

  Future<ClientFormImageValue> getClientFormImageValueById(int id) async {
    final uri = urlProvider.getUri(
        '/clientformimagevalue/getclientformImageValuesforeditbyid/' +
            id.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final ClientFormImageValue clientFormImageValue =
        ClientFormImageValue.fromJsonMap(decodedData['clientFormImageValue']);
    return clientFormImageValue;
  }

  Future<List<ClientFormFieldValue>> getClientFormFieldValueByClientFormValue(
      int idClientFormValue) async {
    final uri = urlProvider.getUri(
        '/ClientFormFieldValue/getallclientFormFieldValuesByClientFormValue/' +
            idClientFormValue.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final clientFormFieldValues = new ClientFormFieldValues.fromJsonList(
        decodedData['clientFormFieldValues']);
    return clientFormFieldValues.items;
  }

  Future<bool> saveClientFormValue(ClientFormValue clientFormValue,
      List<ClientFormFieldValue> clientFormFieldValues) async {
    List<Map> mapedList = clientFormFieldValues != null
        ? clientFormFieldValues.map((i) => i.toJson()).toList()
        : null;

    final _body = {
      "ClientFormValue": clientFormValue.toJson(),
      "ClientFormFieldValues": mapedList
    };

    final uri = urlProvider.getUri('/ClientFormValue/saveClientFormValue/');
    final response = await http.post(uri,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode(_body));
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }

  Future<bool> saveClientFormImageValue(
      ClientFormImageValue clientFormImageValue) async {
    final _body = {"clientFormImageValue": clientFormImageValue.toJson()};
    final uri =
        urlProvider.getUri('/ClientFormImageValue/saveClientFormImageValue/');
    final response = await http.post(uri,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode(_body));
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }

  Future<List<ClientFormByClient>> getClientFormsByClientandClientForm(
      int idClient, int idClientForm) async {
    final uri = urlProvider.getUri(
        '/clientform/getallclientFormsByClientandClientForm/' +
            idClient.toString() +
            '/' +
            idClientForm.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final clientFormsList = new ClientFormsByClient.fromJsonList(
        decodedData['clientFormsbyClient']);
    return clientFormsList.items;
  }
}
