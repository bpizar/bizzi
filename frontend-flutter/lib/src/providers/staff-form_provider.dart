import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/staff-form-field-value_model.dart';
import 'package:people_mobile/src/models/staff-form-image-value_model.dart';
import 'package:people_mobile/src/models/staff-form-value_model.dart';
import 'package:people_mobile/src/models/staff-forms-by-staff_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class StaffFormProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<StaffFormByStaff>> getStaffFormByStaff(int idStaff) async {
    final uri = urlProvider
        .getUri('/staffform/getallstaffFormsByStaff/' + idStaff.toString());
    final resp = await http.get(uri);

    final decodedData = json.decode(resp.body);

    final staffFormsList =
        new StaffFormsByStaff.fromJsonList(decodedData['staffFormsbyStaff']);
    return staffFormsList.items;
  }

  Future<List<StaffFormByStaff>> getStaffFormByStaffSearch(
      int idStaff, String query) async {
    final uri = urlProvider
        .getUri('/staffform/getallstaffFormsByStaff/' + idStaff.toString());
    final resp = await http.get(uri);

    final decodedData = json.decode(resp.body);

    final staffFormsList =
        new StaffFormsByStaff.fromJsonList(decodedData['staffFormsbyStaff']);
    return staffFormsList.items.where((filter) {
      return filter.name.toLowerCase().contains(query.toLowerCase());
    }).toList();
  }

  Future<StaffFormImageValue> getStaffFormImageValueById(int id) async {
    final uri = urlProvider.getUri(
        '/staffformimagevalue/getstaffformImageValuesforeditbyid/' +
            id.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final StaffFormImageValue staffFormImageValue =
        StaffFormImageValue.fromJsonMap(decodedData['staffFormImageValue']);
    return staffFormImageValue;
  }

  Future<List<StaffFormFieldValue>> getStaffFormFieldValueByStaffFormValue(
      int idStaffFormValue) async {
    final uri = urlProvider.getUri(
        '/StaffFormFieldValue/getallstaffFormFieldValuesByStaffFormValue/' +
            idStaffFormValue.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final staffFormFieldValues = new StaffFormFieldValues.fromJsonList(
        decodedData['staffFormFieldValues']);
    return staffFormFieldValues.items;
  }

  Future<bool> saveStaffFormValue(StaffFormValue staffFormValue,
      List<StaffFormFieldValue> staffFormFieldValues) async {
    List<Map> mapedList = staffFormFieldValues != null
        ? staffFormFieldValues.map((i) => i.toJson()).toList()
        : null;

    final _body = {
      "StaffFormValue": staffFormValue.toJson(),
      "StaffFormFieldValues": mapedList
    };

    final uri = urlProvider.getUri('/StaffFormValue/saveStaffFormValue/');
    final response = await http.post(uri,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode(_body));
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }

  Future<bool> saveStaffFormImageValue(
      StaffFormImageValue staffFormImageValue) async {
    final _body = {"staffFormImageValue": staffFormImageValue.toJson()};
    final uri =
        urlProvider.getUri('/StaffFormImageValue/saveStaffFormImageValue/');
    final response = await http.post(uri,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode(_body));
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }

  Future<List<StaffFormByStaff>> getStaffFormsByStaffandStaffForm(
      int idStaff, int idStaffForm) async {
    final uri = urlProvider.getUri(
        '/staffform/getallstaffFormsByStaffandStaffForm/' +
            idStaff.toString() +
            '/' +
            idStaffForm.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final staffFormsList =
        new StaffFormsByStaff.fromJsonList(decodedData['staffFormsbyStaff']);
    return staffFormsList.items;
  }
}
