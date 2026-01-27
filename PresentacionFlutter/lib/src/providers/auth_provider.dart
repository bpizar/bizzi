import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class AuthProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<bool> verifyTfa(String secret) async {
    final uri = urlProvider.getUri('/auth/verifyTFAToken/' + secret);
    final resp = await http.get(uri);
    final decodedData = resp.body;
    if (decodedData == 'true')
      return true;
    else
      return false;
  }
}
