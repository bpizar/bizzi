import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/date-time-convert_lib.dart';
import 'package:people_mobile/src/framework/toast-message_lib.dart';
import 'package:people_mobile/src/models/time-tracker_model.dart';
import 'package:people_mobile/src/models/user-project-profile_model.dart';
import 'package:people_mobile/src/providers/mobile_provider.dart';
import 'package:people_mobile/src/widgets/menu_widget.dart';

class TimetrackerPage extends StatefulWidget {
  @override
  _TimetrackerPageState createState() => _TimetrackerPageState();
}

class _TimetrackerPageState extends State<TimetrackerPage> {
  final _scaffoldKey = GlobalKey<ScaffoldState>();
  UserProjectProfile _userProjectProfile;
  Future<List<TimeTracker>> _futureTimeTrackers;
  final mobileProvider = new MobileProvider();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          if (_userProjectProfile != null) {
            Navigator.pushNamed(context, 'tracking', arguments: {
              'isTracking': false,
              'idUserProjectProfile': _userProjectProfile.idStaffProjectPosition
            }).then((value) =>
                _loadTimeTracking(_userProjectProfile.idStaffProjectPosition));
          } else
            ToastMessage.info('Please select a program first');
        },
        backgroundColor: Theme.of(context).primaryColor,
        child: Icon(Icons.play_arrow),
      ),
      key: _scaffoldKey,
      appBar: AppBar(
        title: Text('Time Tracking'),
      ),
      drawer: createMenu(context),
      body: Center(
        child: Column(
          children: <Widget>[
            SizedBox(height: 10.0),
            _userProjectProfileList(),
            SizedBox(height: 10.0),
            _buildTimetrackerFutureBuilder(),
          ],
        ),
      ),
    );
  }

  FutureBuilder<List<TimeTracker>> _buildTimetrackerFutureBuilder() {
    return FutureBuilder(
      future: _futureTimeTrackers,
      builder: (context, AsyncSnapshot<List<TimeTracker>> snapshot) {
        if (snapshot.hasData) {
          return Expanded(
            child: Column(
              children: [
                ListView(
                  padding: EdgeInsets.symmetric(horizontal: 15),
                  shrinkWrap: true,
                  children: _timeTrackerListView(context, snapshot.data),
                ),
              ],
            ),
          );
        }
        return _userProjectProfile != null
            ? Center(child: CircularProgressIndicator())
            : Container();
      },
    );
  }

  _userProjectProfileList() {
    final mobileProfile = new MobileProvider();
    return FutureBuilder<List<UserProjectProfile>>(
        future: mobileProfile.getMyUserProjectProfile(),
        builder: (BuildContext context,
            AsyncSnapshot<List<UserProjectProfile>> snapshot) {
          if (!snapshot.hasData) return CircularProgressIndicator();
          return DropdownButton<UserProjectProfile>(
            items: snapshot.data
                .map((user) => DropdownMenuItem<UserProjectProfile>(
                      child: Text(user.projectName),
                      value: user,
                    ))
                .toList(),
            onChanged: (UserProjectProfile value) {
              setState(() {
                _userProjectProfile = value;
                _loadTimeTracking(_userProjectProfile.idStaffProjectPosition);
              });
            },
            isExpanded: false,
            hint: _userProjectProfile == null
                ? Text('Select Project')
                : Text(_userProjectProfile.projectName),
          );
        });
  }

  _loadTimeTracking(int idUserProjectProfile) {
    setState(() {
      _futureTimeTrackers =
          mobileProvider.getTimeTrackerbyIdFromToday(idUserProjectProfile);
    });
  }

  List<Widget> _timeTrackerListView(
      BuildContext context, List<TimeTracker> timeTrackers) {
    final List<Widget> timeTrackerWidgets = [];
    timeTrackers.forEach((timeTracker) {
      final widgetTemp = ListTile(
        title: Text(timeTracker.projectName),
        subtitle: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
                "start at: ${DateTimeConvert.getStringTimeFromDateTimeString(timeTracker.start)}"),
            timeTracker.end != null
                ? Text(
                    "stop at: ${DateTimeConvert.getStringTimeFromDateTimeString(timeTracker.end)}")
                : Text("Not stopped yet"),
          ],
        ),
        trailing: timeTracker.end == null
            ? _stopButton(context, timeTracker)
            : Text(''),
      );
      timeTrackerWidgets..add(widgetTemp)..add(Divider());
    });
    return timeTrackerWidgets;
  }

  Container _stopButton(BuildContext context, TimeTracker timeTracker) {
    return Container(
      child: RawMaterialButton(
        onPressed: () async {
          Navigator.pushNamed(context, 'tracking', arguments: {
            'isTracking': true,
            'timeTracker': timeTracker
          }).then((value) =>
              _loadTimeTracking(_userProjectProfile.idStaffProjectPosition));
        },
        elevation: 5.0,
        fillColor: Theme.of(context).primaryColor,
        child: Padding(
          padding: EdgeInsets.all(0),
          child: Icon(
            Icons.stop,
            color: Colors.white,
            size: 25.0,
          ),
        ),
        shape: CircleBorder(),
      ),
    );
  }
}
