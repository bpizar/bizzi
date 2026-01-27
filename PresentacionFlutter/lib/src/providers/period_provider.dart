import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/period_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class PeriodProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<Period>> getPeriods() async {
    final uri = urlProvider.getUri('/periods/getperiods');
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final period = new Periods.fromJsonList(decodedData['periodsList']);
    return period.items;
  }
}
