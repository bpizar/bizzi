import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/scheduling_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class SchedulingProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<Scheduling>> getScheduling(int idPeriod) async {
    final uri =
        urlProvider.getUri('/scheduling/getscheduling/' + idPeriod.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);

    final schedulingList =
        new SchedulingList.fromJsonList(decodedData['scheduling']);
    return schedulingList.items;
  }
}
