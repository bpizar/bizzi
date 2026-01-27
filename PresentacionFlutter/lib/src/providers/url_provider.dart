class UrlProvider {
  String url = "52.60.139.58:8010";
  String middle = "/api";

  getDomain() {
    return url;
  }

  getMiddle() {
    return middle;
  }

  Uri getUri(String route) {
    return Uri.http(url, middle + route);
  }
}
