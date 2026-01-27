import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class StaffProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<Staff>> getStaffList() async {
    final uri = urlProvider.getUri('/staff/getallstaffs');
    final resp = await http.get(uri);

    final decodedData = json.decode(resp.body);

    final staffList = new Staffs.fromJsonList(decodedData['staffs']);
    return staffList.items;
  }

  Future<Staff> getEditStaffForId(int id) async {
    final uri =
        urlProvider.getUri('/staff/getstaffforeditbyid/' + id.toString());
    final resp = await http.get(uri);

    final decodedData = json.decode(resp.body);

    final Staff staff = Staff.fromJsonMap(decodedData['staff']);
    return staff;
  }
}
