import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class LoginProvider {
  static final LoginProvider _loginProvider = new LoginProvider._internal();
  LoginProvider._internal();
  String token = "";
  int idLoggedUser;
  String userName;

  factory LoginProvider() {
    return _loginProvider;
  }

  Future<bool> login(String username, String password) async {
    final uri = urlProvider.getUri('/auth/login');
    final body = jsonEncode(<String, String>{
      'username': username,
      'password': password,
      'onesignalid': '',
    });
    final resp = await http.post(uri,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: body);
    final decodedData = resp.body;
    if (json.decode(decodedData)['login_failure'] != null) {
      return false;
    } else {
      final _token = json.decode(decodedData)["auth_token"];
      idLoggedUser = int.parse(json.decode(decodedData)["id"]);
      userName = username;
      bool isTokenExpired = JwtDecoder.isExpired(_token);

      if (!isTokenExpired) {
        token = _token;
        return true;
      }
    }
    return false;
  }

  getToken() {
    return token;
  }

  getIdLoggedUser() {
    return idLoggedUser;
  }

  getUserName() {
    return userName;
  }
}
