import 'dart:convert';
import 'package:http/http.dart';
import 'package:http_interceptor/http/intercepted_client.dart';
import 'package:people_mobile/src/interceptors/auth_interceptor.dart';
import 'package:people_mobile/src/models/schedule_model.dart';
import 'package:people_mobile/src/models/time-tracker_model.dart';
import 'package:people_mobile/src/models/user-project-profile_model.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

final urlProvider = new UrlProvider();

class MobileProvider {
  Client http = InterceptedClient.build(interceptors: [
    AuthInterceptor(),
  ]);

  Future<List<UserProjectProfile>> getMyUserProjectProfile() async {
    final uri = urlProvider.getUri('/mobile/getMyProjectsPositions/');
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final usersProjectProfile = new UsersProjectProfile.fromJsonList(
        decodedData['staffProjectPositions']);
    return usersProjectProfile.items;
  }

  Future<List<TimeTracker>> getTimeTrackerbyIdFromToday(
      int idUserProjectProfile) async {
    final uri = urlProvider.getUri('/mobile/getTimeTrackerbyIdFromToday/' +
        idUserProjectProfile.toString());
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);
    final timeTrackers =
        new TimeTrackers.fromJsonList(decodedData['timeTracker']);
    return timeTrackers.items;
  }

  Future<bool> autoTimeTracker(
      int idUser, double longitude, double latitude) async {
    var map = new Map<String, dynamic>();
    map['idfUser'] = idUser.toString();
    map['longitude'] = longitude.toStringAsFixed(7).replaceAll('.', ',');
    map['latitude'] = latitude.toStringAsFixed(7).replaceAll('.', ',');

    final uri = urlProvider.getUri('/mobile/saveAutoGeoTracking/');
    final response = await http.post(uri, body: map);
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }

  Future<bool> startTimeTracker(int idfStaffProjectPosition, String startNote,
      double longitude, double latitude) async {
    var map = new Map<String, dynamic>();
    map['IdfStaffProjectPosition'] = idfStaffProjectPosition.toString();
    map['note'] = startNote;
    map['longitude'] = longitude.toStringAsFixed(7).replaceAll('.', ',');
    map['latitude'] = latitude.toStringAsFixed(7).replaceAll('.', ',');

    final uri = urlProvider.getUri('/mobile/StartTimeTracker/');
    final response = await http.post(uri, body: map);
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }

  Future<bool> stopTimeTracker(
      int idUser, String startNote, double longitude, double latitude) async {
    var map = new Map<String, dynamic>();
    map['id'] = idUser.toString();
    map['note'] = startNote;
    map['longitude'] = longitude.toStringAsFixed(7).replaceAll('.', ',');
    map['latitude'] = latitude.toStringAsFixed(7).replaceAll('.', ',');

    final uri = urlProvider.getUri('/mobile/StopTimeTracker/');
    final response = await http.post(uri, body: map);
    final decodedData = response.body;
    final result = json.decode(decodedData)["result"];
    return result;
  }

  Future<List<Schedule>> getSchedulebyUser() async {
    final uri = urlProvider.getUri('/mobile/getMySchedule/');
    final resp = await http.get(uri);
    final decodedData = json.decode(resp.body);

    final schedules = new Schedules.fromJsonList(decodedData['schedule']);
    return schedules.items;
  }
}
