import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/project-form-field-value_model.dart';
import 'package:people_mobile/src/models/project-form-image-value_model.dart';
import 'package:people_mobile/src/models/project-form-value_model.dart';
import 'package:people_mobile/src/models/project-forms-by-project_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class ProjectFormProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<ProjectFormByProject>> getProjectFormByProject(
      int idProject) async {
    final uri = urlProvider.getUri(
        '/projectform/getallprojectFormsByProject/' + idProject.toString());
    final resp = await http.get(uri);

    final decodedData = json.decode(resp.body);

    final projectFormsList = new ProjectFormsByProject.fromJsonList(
        decodedData['projectFormsbyProject']);
    return projectFormsList.items;
  }

  Future<List<ProjectFormByProject>> getProjectFormByProjectSearch(
      int idProject, String query) async {
    final uri = urlProvider.getUri(
        '/projectform/getallprojectFormsByProject/' + idProject.toString());
    final resp = await http.get(uri);

    final decodedData = json.decode(resp.body);

    final projectFormsList = new ProjectFormsByProject.fromJsonList(
        decodedData['projectFormsbyProject']);
    return projectFormsList.items.where((filter) {
      return filter.name.toLowerCase().contains(query.toLowerCase());
    }).toList();
  }

  Future<ProjectFormImageValue> getProjectFormImageValueById(int id) async {
    final uri = urlProvider.getUri(
        '/projectformimagevalue/getprojectformImageValuesforeditbyid/' +
            id.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final ProjectFormImageValue projectFormImageValue =
        ProjectFormImageValue.fromJsonMap(decodedData['projectFormImageValue']);
    return projectFormImageValue;
  }

  Future<List<ProjectFormFieldValue>>
      getProjectFormFieldValueByProjectFormValue(int idProjectFormValue) async {
    final uri = urlProvider.getUri(
        '/ProjectFormFieldValue/getallprojectFormFieldValuesByProjectFormValue/' +
            idProjectFormValue.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final projectFormFieldValues = new ProjectFormFieldValues.fromJsonList(
        decodedData['projectFormFieldValues']);
    return projectFormFieldValues.items;
  }

  Future<bool> saveProjectFormValue(ProjectFormValue projectFormValue,
      List<ProjectFormFieldValue> projectFormFieldValues) async {
    List<Map> mapedList = projectFormFieldValues != null
        ? projectFormFieldValues.map((i) => i.toJson()).toList()
        : null;

    final _body = {
      "ProjectFormValue": projectFormValue.toJson(),
      "ProjectFormFieldValues": mapedList
    };

    final uri = urlProvider.getUri('/ProjectFormValue/saveProjectFormValue/');
    final response = await http.post(uri,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode(_body));
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }

  Future<bool> saveProjectFormImageValue(
      ProjectFormImageValue projectFormImageValue) async {
    final _body = {"projectFormImageValue": projectFormImageValue.toJson()};
    final uri =
        urlProvider.getUri('/ProjectFormImageValue/saveProjectFormImageValue/');
    final response = await http.post(uri,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode(_body));
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }

  Future<List<ProjectFormByProject>> getProjectFormsByProjectandProjectForm(
      int idProject, int idProjectForm) async {
    final uri = urlProvider.getUri(
        '/projectform/getallprojectFormsByProjectandProjectForm/' +
            idProject.toString() +
            '/' +
            idProjectForm.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final projectFormsList = new ProjectFormsByProject.fromJsonList(
        decodedData['projectFormsbyProject']);
    return projectFormsList.items;
  }
}
