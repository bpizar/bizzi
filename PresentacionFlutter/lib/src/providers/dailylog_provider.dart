import 'dart:convert';

import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/dailylogForSave_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class DailylogProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<bool> saveDailylog(Dailylog dailylog) async {
    final uri = urlProvider.getUri('/dailylog/savedailylog/');

    final _body = {
      "DailyLog": dailylog.toJson(),
      /* "InvolvedPeople": [
          {
            "identifierGroup": "s1",
            "idfDailyLog": -1,
            "idfSPP": 1,
            "state": "C"
          }
        ], */
      "TimeDifference": -4
    };

    final response = await http.post(uri,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode(_body));
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }
}
