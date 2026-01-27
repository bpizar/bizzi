import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/models/period_model.dart';
import 'package:people_mobile/src/pages/client/dailylogs/dailylog-list_page.dart';
import 'package:people_mobile/src/pages/client/forms/client-form-list_page.dart';
import 'package:people_mobile/src/providers/client_provider.dart';

class ClientDetailPage extends StatefulWidget {
  @override
  _ClientDetailPageState createState() => _ClientDetailPageState();
}

Period period = new Period();
DateFormat formatter = DateFormat('yyyy-MM-dd');

class _ClientDetailPageState extends State<ClientDetailPage> {
  Client client;
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    if (client == null) {
      client = ModalRoute.of(context).settings.arguments;
      _loadClient(client.id);
    }
    return DefaultTabController(
      length: 6,
      child: Scaffold(
        appBar: AppBar(
          title: Text('Client Detail'),
          bottom: TabBar(
            tabs: [
              Tab(text: 'Info'),
              Tab(text: 'Safety Plan'),
              Tab(text: 'More Info'),
              Tab(text: 'Education'),
              Tab(text: 'Health'),
              Tab(text: 'Forms'),
            ],
          ),
        ),
        body: TabBarView(children: [
          _info(context),
          _safetyPlan(context),
          _moreInfo(),
          _education(),
          _health(),
          ClientFormListPage(context, client),
          // DailyLogPage(period, client),
        ]),
      ),
    );
  }

  CustomScrollView _info(BuildContext context) {
    return CustomScrollView(
      slivers: <Widget>[
        SliverList(
          delegate: SliverChildListDelegate([
            SizedBox(height: 10.0),
            Row(
              children: <Widget>[
                Hero(
                  tag: "client" + client.id.toString() ?? "",
                  child: ClipRRect(
                    borderRadius: BorderRadius.circular(20.0),
                    child: FadeInImage(
                      image: NetworkImage(urlProvider
                          .getUri('/image/clients/' + client.img)
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
                    Text(client.fullName ?? '',
                        style: Theme.of(context).textTheme.headline3,
                        overflow: TextOverflow.ellipsis),
                    Text('Email: ' + (client.email ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text(
                        'Birth Date: ' +
                            (formatter
                                    .format(DateTime.parse(client.birthDate)) ??
                                ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text('Phone: ' + (client.phoneNumber ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text('Notes: ' + (client.notes ?? ''),
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
                    Text("Mother: " + (client.tmpMotherName ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Contact Number: " + (client.tmpMotherInfo ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Father: " + (client.tmpFatherName ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Contact Number: " + (client.tmpFatherInfo ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Agency: " + (client.tmpAgency ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Contact Number: " + (client.tmpAgencyInfo ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Agency Worker: " + (client.tmpAgencyWorker ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text(
                        "Contact Number: " + (client.tmpAgencyWorkerInfo ?? ''),
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

  CustomScrollView _education() {
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
                    Text("School/Education: " + (client.tmpSchool ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Number: " + (client.tmpSchoolInfo ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Teacher: " + (client.tmpTeacher ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Contact Number: " + (client.tmpTeacherInfo ?? ''),
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

  CustomScrollView _health() {
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
                    Text("Doctor Name: " + (client.tmpDoctorName ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("More Info: " + (client.tmpDoctorInfo ?? ''),
                        style: Theme.of(context).textTheme.bodyText1,
                        overflow: TextOverflow.ellipsis,
                        maxLines: 2),
                    Text("Medical Notes: " + (client.tmpMedicationNotes ?? ''),
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

  CustomScrollView _safetyPlan(BuildContext context) {
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
                    Text((client.safetyPlan ?? ''),
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

  _loadClient(int id) {
    final clientProvider = new ClientProvider();
    clientProvider.getClientById(id).then((data) {
      if (this.mounted) {
        setState(() {
          client.safetyPlan = data.safetyPlan;
          client.tmpMotherName = data.tmpMotherName;
          client.tmpMotherInfo = data.tmpMotherInfo;
          client.tmpFatherName = data.tmpFatherName;
          client.tmpFatherInfo = data.tmpFatherInfo;
          client.tmpAgency = data.tmpAgency;
          client.tmpAgencyInfo = data.tmpAgencyInfo;
          client.tmpAgencyWorker = data.tmpAgencyWorker;
          client.tmpSchool = data.tmpSchool;
          client.tmpSchoolInfo = data.tmpSchoolInfo;
          client.tmpTeacher = data.tmpTeacher;
          client.tmpTeacherInfo = data.tmpTeacherInfo;
        });
      }
    });
  }
}
