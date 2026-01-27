import 'package:http_interceptor/http_interceptor.dart';
import 'package:people_mobile/src/providers/login_provider.dart';

class AuthInterceptor implements InterceptorContract {
  final loginProvider = new LoginProvider();

  @override
  Future<RequestData> interceptRequest({RequestData data}) async {
    data.headers["Authorization"] = "Bearer " + loginProvider.getToken();
    return data;
  }

  @override
  Future<ResponseData> interceptResponse({ResponseData data}) async {
    return data;
  }
}
