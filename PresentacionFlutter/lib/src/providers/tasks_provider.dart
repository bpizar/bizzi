import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/tasks-by-staff_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class TaskProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);
// tasksbystaff
  Future<TaskByStaff> getTaskByStaff(int idStaff, int idPeriod) async {
    final uri = urlProvider.getUri('/tasks/gettasksbystaff/' +
        idStaff.toString() +
        '/' +
        idPeriod.toString() +
        '/');
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);

    final taskByStaff = new TaskByStaff.fromJsonMap(decodedData);
    return taskByStaff;
  }
}
