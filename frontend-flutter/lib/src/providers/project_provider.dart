import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/models/client_model.dart' as ClientModel;
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class ProjectProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<Project>> getprojects() async {
    final uri = urlProvider.getUri('/projects/getprojects');
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);

    final projects = new Projects.fromJsonList(decodedData['projects']);
    return projects.items;
  }

  Future<Project> getProjectById(int id) async {
    final uri = urlProvider.getUri('/projects/getproject/' + id.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final Project project = Project.fromJsonMap(decodedData['project']);
    return project;
  }

  Future<List<ClientModel.Client>> getClientsByProjectId(int id) async {
    final uri = urlProvider.getUri('/projects/getproject/' + id.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final clients =
        new ClientModel.Clients.fromJsonList(decodedData['clientsAllPeriods']);
    return clients.items;
  }

  Future<List<Staff>> getProjectStaff(int id, int periodId) async {
    final uri = urlProvider.getUri(
        '/projects/getproject2/' + id.toString() + '/' + periodId.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final staff = new Staffs.fromJsonList(decodedData['staffs']);
    return staff.items;
  }
}
