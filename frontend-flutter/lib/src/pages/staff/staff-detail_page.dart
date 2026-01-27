import 'package:flutter/material.dart';
import 'package:flutter_html/flutter_html.dart';
import 'package:intl/intl.dart';
import 'package:people_mobile/src/models/period_model.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/models/tasks-by-staff_model.dart';
import 'package:people_mobile/src/pages/staff/forms/staff-form-list_page.dart';
import 'package:people_mobile/src/providers/staff-form_provider.dart';
import 'package:people_mobile/src/providers/staff_provider.dart';
import 'package:people_mobile/src/providers/tasks_provider.dart';
import 'package:people_mobile/src/providers/url_provider.dart';
import 'package:people_mobile/src/widgets/period-dropdown_widget.dart';
import 'package:url_launcher/url_launcher.dart';

final staffFormProvider = new StaffFormProvider();
DateFormat formatter = DateFormat('yyyy-MM-dd');

class StaffDetailPage extends StatefulWidget {
  @override
  _StaffDetailPageState createState() => _StaffDetailPageState();
}

class _StaffDetailPageState extends State<StaffDetailPage> {
  Staff staff;
  final urlProvider = new UrlProvider();
  Period period = new Period();
  TaskByStaff taskByStaff;

  @override
  Widget build(BuildContext context) {
    if (staff == null) {
      staff = ModalRoute.of(context).settings.arguments;
      _loadStaff(staff.id);
    }
    return DefaultTabController(
        length: 5,
        child: Scaffold(
          appBar: AppBar(
            bottom: TabBar(
              tabs: [
                Tab(text: 'Info'),
                Tab(text: 'Work Info'),
                Tab(text: 'More Info'),
                Tab(text: 'Accreditations'),
                Tab(text: 'Forms')
              ],
            ),
            title: Text('Staff Detail'),
          ),
          body: TabBarView(children: [
            _info(),
            _workInfo(),
            _moreInfo(),
            _accreditations(),
            StaffFormListPage(context, staff)
          ]),
        ));
  }

  CustomScrollView _info() {
    return CustomScrollView(
      slivers: <Widget>[
        SliverList(
          delegate: SliverChildListDelegate([
            SizedBox(height: 10.0),
            Row(
              children: <Widget>[
                Hero(
                  tag: "client" + staff.id.toString() ?? "",
                  child: ClipRRect(
                    borderRadius: BorderRadius.circular(20.0),
                    child: FadeInImage(
                      image: NetworkImage(urlProvider
                          .getUri('/image/users/' + staff.img)
                          .toString()),
                      placeholder:
                          AssetImage('assets/images/users/default.png'),
                      fit: BoxFit.cover,
                      height: 150.0,
                    ),
                  ),
                ),
                SizedBox(width: 20.0),
                Flexible(
                    child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Text(staff.fullName ?? '',
                        style: Theme.of(context).textTheme.headline3,
                        overflow: TextOverflow.ellipsis),
                    Text("Email: " + (staff.email ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Projects: " + (staff.projectInfo ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                  ],
                ))
              ],
            ),
          ]),
        )
      ],
    );
  }

  CustomScrollView _workInfo() {
    return CustomScrollView(
      slivers: <Widget>[
        SliverList(
          delegate: SliverChildListDelegate([
            SizedBox(height: 10.0),
            SizedBox(height: 10),
            Text(
              'Select a Period',
              style: Theme.of(context).textTheme.bodyText1,
            ),
            PeriodDropDown(
              onChanged: (chosedPeriod) {
                setState(() {
                  period = chosedPeriod;
                  _loadTaskByStaff(staff.id, chosedPeriod.id);
                });
              },
            ),
            Divider(),
            Row(
              children: <Widget>[
                Flexible(
                    child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Text(
                        "Working hours per perid: " +
                            (taskByStaff?.assignedHoursOnPeriod.toString()),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text(
                        "Program Assigned per period: " +
                            (taskByStaff?.assignedPrograms ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text(
                        "Scheduled Hours per period: " +
                            (taskByStaff?.assignedHoursOnPeriod.toString()),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text(
                        "Available hours per period: " +
                            (taskByStaff?.availableHoursOnPeriod.toString()),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text(
                        "Assigned tasks per period: " +
                            (taskByStaff?.assignedTasks.toString() ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                  ],
                ))
              ],
            ),
          ]),
        )
      ],
    );
  }

  CustomScrollView _moreInfo() {
    return CustomScrollView(
      slivers: <Widget>[
        SliverList(
          delegate: SliverChildListDelegate([
            SizedBox(height: 10.0),
            Row(
              children: <Widget>[
                Flexible(
                    child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Text(
                        "Work anniversary: " +
                            ((staff.workStartDate == null)
                                ? "N/A"
                                : (formatter.format(
                                    DateTime.parse(staff.workStartDate)))),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis),
                    Text(
                        "Social Insurance Number: " +
                            (staff.socialInsuranceNumber ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text(
                        "Health Insurance Number: " +
                            (staff.healthInsuranceNumber ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Home Address: " + (staff.homeAddress ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("City: " + (staff.city ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Home Phone: " + (staff.homePhone ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Cell Number: " + (staff.cellNumber ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Spouse Name: " + (staff.spouceName ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Emergency Person: " + (staff.emergencyPerson ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text(
                        "Emergency Number: " +
                            (staff.emergencyPersonInfo ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                  ],
                ))
              ],
            ),
          ]),
        )
      ],
    );
  }

  CustomScrollView _accreditations() {
    return CustomScrollView(
      slivers: <Widget>[
        SliverList(
          delegate: SliverChildListDelegate([
            SizedBox(height: 10.0),
            Html(
              data: staff.tmpAccreditations ?? "",
              onLinkTap: (String url, RenderContext context,
                  Map<String, String> attributes, element) {
                Uri uri = Uri.parse(url);
                launchUrl(uri);
              },
            )
          ]),
        )
      ],
    );
  }

  _loadStaff(int id) {
    final staffProvider = new StaffProvider();
    staffProvider.getEditStaffForId(id).then((data) {
      if (this.mounted) {
        setState(() {
          staff.tmpAccreditations = data.tmpAccreditations;
          staff.workStartDate = data.workStartDate;
          staff.socialInsuranceNumber = data.socialInsuranceNumber;
          staff.healthInsuranceNumber = data.healthInsuranceNumber;
          staff.homeAddress = data.homeAddress;
          staff.city = data.city;
          staff.homePhone = data.homePhone;
          staff.cellNumber = data.cellNumber;
          staff.spouceName = data.spouceName;
          staff.emergencyPerson = data.emergencyPerson;
          staff.emergencyPersonInfo = data.emergencyPersonInfo;
          staff.availableForManyPrograms = data.availableForManyPrograms;
        });
      }
    });
  }

  _loadTaskByStaff(int idStaff, int idPeriod) {
    final taskProvider = new TaskProvider();
    taskProvider.getTaskByStaff(idStaff, idPeriod).then((data) {
      if (this.mounted) {
        setState(() {
          taskByStaff = data;
        });
      }
    });
  }
}
