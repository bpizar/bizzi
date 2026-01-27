import 'dart:convert';
import 'package:people_mobile/src/models/client_model.dart' as ClientModels;
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/dailylog_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class ClientProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<ClientModels.Client>> getclients() async {
    final uri = urlProvider.getUri('/clients/getallclients');
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final clients =
        new ClientModels.Clients.fromJsonList(decodedData['clients']);
    return clients.items;
  }

  Future<ClientModels.Client> getClientById(int id) async {
    final uri = urlProvider.getUri('/Clients/getclient/' + id.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final ClientModels.Client client =
        ClientModels.Client.fromJsonMap(decodedData['client']);
    return client;
  }

  Future<List<Dailylog>> getDailylogsByPeriodAndClient(
      int idPeriod, int idClient) async {
    final uri = urlProvider
        .getUri('/clients/getclientdatabyperiodid/$idPeriod/$idClient');
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final dailylogs = new Dailylogs.fromJsonList(decodedData['dailyLogsList']);
    return dailylogs.items;
  }
}
