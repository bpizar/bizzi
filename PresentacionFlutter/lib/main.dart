import 'package:flutter/material.dart';
import 'package:people_mobile/src/mainTheme.dart';
import 'package:people_mobile/src/pages/client/client-detail_page.dart';
import 'package:people_mobile/src/pages/client/dailylogs/edit-dailylog_page.dart';
import 'package:people_mobile/src/pages/client/forms/client-form-history-list_page.dart';
import 'package:people_mobile/src/pages/client/forms/client-form-value_page.dart';
import 'package:people_mobile/src/pages/client/forms/client-from-image-value_page.dart';
import 'package:people_mobile/src/pages/client/forms/edit-client-form-image-value_page.dart';
import 'package:people_mobile/src/pages/client/forms/edit-client-form-value_page.dart';
import 'package:people_mobile/src/pages/client_page.dart';
import 'package:people_mobile/src/pages/login_page.dart';
import 'package:people_mobile/src/pages/dashboard_page.dart';
import 'package:people_mobile/src/pages/geotracking_page.dart';
import 'package:people_mobile/src/pages/project/forms/edit-project-form-image-value_page.dart';
import 'package:people_mobile/src/pages/project/forms/edit-project-form-value_page.dart';
import 'package:people_mobile/src/pages/project/forms/project-form-history-list_page.dart';
import 'package:people_mobile/src/pages/project/forms/project-form-value_page.dart';
import 'package:people_mobile/src/pages/project/forms/project-from-image-value_page.dart';
import 'package:people_mobile/src/pages/project/project-detail_page.dart';
import 'package:people_mobile/src/pages/projects_page.dart';
import 'package:people_mobile/src/pages/schedule_page.dart';
import 'package:people_mobile/src/pages/staff/forms/edit-staff-form-value_page.dart';
import 'package:people_mobile/src/pages/staff/forms/edit-staff-form-image-value_page.dart';
import 'package:people_mobile/src/pages/staff/forms/staff-form-history-list_page.dart';
import 'package:people_mobile/src/pages/staff/forms/staff-form-value_page.dart';
import 'package:people_mobile/src/pages/staff/forms/staff-from-image-value_page.dart';
import 'package:people_mobile/src/pages/staff/staff-detail_page.dart';
import 'package:people_mobile/src/pages/staff_page.dart';
import 'package:people_mobile/src/pages/timetracker_page.dart';
import 'package:people_mobile/src/pages/tracking/tracking_page.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Bizzi',
      initialRoute: 'login',
      theme: mainTheme(),
      routes: {
        'login': (BuildContext context) => LoginPage(),
        'staff': (BuildContext context) => StaffPage(),
        'staff-detail': (BuildContext context) => StaffDetailPage(),
        'clients': (BuildContext context) => ClientsPage(),
        'client-detail': (BuildContext context) => ClientDetailPage(),
        'edit-dailylog': (BuildContext context) => EditDailylogPage(),
        'programs': (BuildContext context) => ProjectsPage(),
        'program-detail': (BuildContext context) => ProjectDetailPage(),
        'dashboard': (BuildContext context) => DashboardPage(),
        'geotracking': (BuildContext context) => GeotrackingPage(),
        'staff-schedule': (BuildContext context) => SchedulePage(),
        'timetracker': (BuildContext context) => TimetrackerPage(),
        'tracking': (BuildContext context) => TrackingPage(),
        'staff-form-value': (BuildContext context) => StaffFormValuePage(),
        'staff-form-image-value': (BuildContext context) =>
            StaffFormImageValuePage(),
        'edit-staff-form-value': (BuildContext context) =>
            EditStaffFormValuePage(),
        'edit-staff-form-image-value': (BuildContext context) =>
            EditStaffFormImageValuePage(),
        'staff-form-history-list': (BuildContext context) =>
            StaffFormHistoryList(),
        'client-form-value': (BuildContext context) => ClientFormValuePage(),
        'client-form-image-value': (BuildContext context) =>
            ClientFormImageValuePage(),
        'edit-client-form-value': (BuildContext context) =>
            EditClientFormValuePage(),
        'edit-client-form-image-value': (BuildContext context) =>
            EditClientFormImageValuePage(),
        'client-form-history-list': (BuildContext context) =>
            ClientFormHistoryList(),
        'program-form-value': (BuildContext context) => ProjectFormValuePage(),
        'program-form-image-value': (BuildContext context) =>
            ProjectFormImageValuePage(),
        'edit-program-form-value': (BuildContext context) =>
            EditProjectFormValuePage(),
        'edit-program-form-image-value': (BuildContext context) =>
            EditProjectFormImageValuePage(),
        'program-form-history-list': (BuildContext context) =>
            ProjectFormHistoryList(),
      },
    );
  }
}
