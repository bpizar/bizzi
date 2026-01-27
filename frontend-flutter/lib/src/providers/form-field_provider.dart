import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/form-field_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class FormFieldProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<FormField>> getFormFieldsByStaffForm(int idStaffForm) async {
    final uri = urlProvider.getUri(
        '/FormField/getallstaffFormFieldsbystaffform/' +
            idStaffForm.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final formFields = new FormFields.fromJsonList(decodedData['formFields']);
    formFields.items.removeWhere((formfield) => !formfield.isEnabled);
    return formFields.items;
  }

  Future<List<FormField>> getFormFieldsByClientForm(int idClientForm) async {
    final uri = urlProvider.getUri(
        '/FormField/getallclientFormFieldsbyclientform/' +
            idClientForm.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final formFields = new FormFields.fromJsonList(decodedData['formFields']);
    formFields.items.removeWhere((formfield) => !formfield.isEnabled);
    return formFields.items;
  }

  Future<List<FormField>> getFormFieldsByProjectForm(int idProjectForm) async {
    final uri = urlProvider.getUri(
        '/FormField/getallprojectFormFieldsbyprojectform/' +
            idProjectForm.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final formFields = new FormFields.fromJsonList(decodedData['formFields']);
    formFields.items.removeWhere((formfield) => !formfield.isEnabled);
    return formFields.items;
  }
}
