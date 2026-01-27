import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/toast-message_lib.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/models/dailylogForSave_model.dart';
import 'package:people_mobile/src/models/period_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/dailylog_provider.dart';
import 'package:people_mobile/src/providers/project_provider.dart';
import 'package:people_mobile/src/widgets/date-picker_widget.dart';
import 'package:people_mobile/src/widgets/speech-to-text_widget.dart';
import 'package:people_mobile/src/widgets/time-picker_widget.dart';

final project = <Project>[];
var dailylog = new Dailylog();
var client = new Client();
var period = new Period();

class EditDailylogPage extends StatefulWidget {
  @override
  _EditDailylogPageState createState() => _EditDailylogPageState();
}

class _EditDailylogPageState extends State<EditDailylogPage> {
  DateTime date;
  TimeOfDay time;

  var txtattended = TextEditingController();
  var txtgeneralMood = TextEditingController();
  var txtcomments = TextEditingController();
  var txtinteractionStaff = TextEditingController();
  var txthealth = TextEditingController();
  var txtinteractionPeers = TextEditingController();
  var txtcontactFamily = TextEditingController();
  var txtschool = TextEditingController();
  var txtother = TextEditingController();

  final formKey = new GlobalKey<FormState>();
  final dailylogProvider = new DailylogProvider();
  final projectProvider = ProjectProvider();
  Project _selectedProject;

  @override
  void initState() {
    super.initState();

    date = DateTime.now();
    time = TimeOfDay.now();
  }

  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    client = args['client'];
    period = args['period'];

    return Scaffold(
      appBar: AppBar(
        title: Text('Edit client'),
      ),
      floatingActionButton: FloatingActionButton(
        backgroundColor: Theme.of(context).primaryColor,
        onPressed: _save,
        child: Icon(Icons.save),
      ),
      body: SingleChildScrollView(
        padding: EdgeInsets.symmetric(horizontal: 24),
        child: Form(
          key: formKey,
          child: Column(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              Padding(
                padding: EdgeInsets.only(left: 24),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Date',
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                    DatePicker(
                      date: date,
                      onChanged: (chosedDate) {
                        date = DateTime.parse(chosedDate);
                      },
                    ),
                  ],
                ),
              ),
              Padding(
                padding: EdgeInsets.only(left: 24),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Time',
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                    TimePicker(
                      time: time,
                      onChanged: (chosedTime) {
                        time = chosedTime;
                      },
                    ),
                  ],
                ),
              ),
              Padding(
                padding: EdgeInsets.only(left: 24),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Project',
                      style: Theme.of(context).textTheme.bodyText1,
                    ),
                    Center(
                      child: _projectList(),
                    )
                  ],
                ),
              ),
              SizedBox(height: 20.0),
              TextFormField(
                controller: txtattended,
                onSaved: (value) {
                  dailylog.attended = value;
                },
                validator: (value) {
                  if (value.isEmpty) return "Empty";
                  return null;
                },
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  icon: IconButton(
                      icon: Icon(Icons.mic),
                      onPressed: () async {
                        String result =
                            await SpeechToTextWidget.showModal(context);
                        if (result != '') txtattended.text = result;
                      }),
                  labelText: 'Attended',
                ),
                maxLines: 2,
              ),
              SizedBox(height: 10.0),
              TextFormField(
                controller: txtgeneralMood,
                onSaved: (value) {
                  dailylog.generalMood = value;
                },
                validator: (value) {
                  if (value.isEmpty) return "Empty";
                  return null;
                },
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  icon: IconButton(
                      icon: Icon(Icons.mic),
                      onPressed: () async {
                        String result =
                            await SpeechToTextWidget.showModal(context);
                        if (result != '') txtgeneralMood.text = result;
                      }),
                  labelText: 'General Mood',
                ),
                maxLines: 2,
              ),
              SizedBox(height: 10.0),
              TextFormField(
                controller: txtcomments,
                onSaved: (value) {
                  dailylog.comments = value;
                },
                validator: (value) {
                  if (value.isEmpty) return "Empty";
                  return null;
                },
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  icon: IconButton(
                      icon: Icon(Icons.mic),
                      onPressed: () async {
                        String result =
                            await SpeechToTextWidget.showModal(context);
                        if (result != '') txtcomments.text = result;
                      }),
                  labelText: 'Comments',
                ),
                maxLines: 2,
              ),
              SizedBox(height: 10.0),
              TextFormField(
                controller: txtinteractionStaff,
                onSaved: (value) {
                  dailylog.interactionStaff = value;
                },
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  icon: IconButton(
                      icon: Icon(Icons.mic),
                      onPressed: () async {
                        String result =
                            await SpeechToTextWidget.showModal(context);
                        if (result != '') txtinteractionStaff.text = result;
                      }),
                  labelText: 'Interaction with staff',
                ),
                maxLines: 2,
              ),
              SizedBox(height: 10.0),
              TextFormField(
                controller: txthealth,
                onSaved: (value) {
                  dailylog.health = value;
                },
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  icon: IconButton(
                      icon: Icon(Icons.mic),
                      onPressed: () async {
                        String result =
                            await SpeechToTextWidget.showModal(context);
                        if (result != '') txthealth.text = result;
                      }),
                  labelText: 'General Health',
                ),
                maxLines: 2,
              ),
              SizedBox(height: 10.0),
              TextFormField(
                controller: txtinteractionPeers,
                onSaved: (value) {
                  dailylog.interactionPeers = value;
                },
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  icon: IconButton(
                      icon: Icon(Icons.mic),
                      onPressed: () async {
                        String result =
                            await SpeechToTextWidget.showModal(context);
                        if (result != '') txtinteractionPeers.text = result;
                      }),
                  labelText: 'Interaction with peers',
                ),
                maxLines: 2,
              ),
              SizedBox(height: 10.0),
              TextFormField(
                controller: txtcontactFamily,
                onSaved: (value) {
                  dailylog.contactFamily = value;
                },
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  icon: IconButton(
                      icon: Icon(Icons.mic),
                      onPressed: () async {
                        String result =
                            await SpeechToTextWidget.showModal(context);
                        if (result != '') txtcontactFamily.text = result;
                      }),
                  labelText: 'Contact with Family',
                ),
                maxLines: 2,
              ),
              SizedBox(height: 10.0),
              TextFormField(
                controller: txtschool,
                onSaved: (value) {
                  dailylog.school = value;
                },
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  icon: IconButton(
                      icon: Icon(Icons.mic),
                      onPressed: () async {
                        String result =
                            await SpeechToTextWidget.showModal(context);
                        if (result != '') txtschool.text = result;
                      }),
                  labelText: 'School',
                ),
                maxLines: 2,
              ),
              SizedBox(height: 10.0),
              TextFormField(
                controller: txtother,
                onSaved: (value) {
                  dailylog.other = value;
                },
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  icon: IconButton(
                      icon: Icon(Icons.mic),
                      onPressed: () async {
                        String result =
                            await SpeechToTextWidget.showModal(context);
                        if (result != '') txtother.text = result;
                      }),
                  labelText: 'Other',
                ),
                maxLines: 2,
              ),
              SizedBox(height: 60)
            ],
          ),
        ),
      ),
    );
  }

  _projectList() {
    return FutureBuilder<List<Project>>(
        future: projectProvider.getprojects(),
        builder: (BuildContext context, AsyncSnapshot<List<Project>> snapshot) {
          if (!snapshot.hasData) return CircularProgressIndicator();
          return DropdownButton<Project>(
            items: snapshot.data
                .map((user) => DropdownMenuItem<Project>(
                      child: Text(user.projectName),
                      value: user,
                    ))
                .toList(),
            onChanged: (Project value) {
              setState(() {
                _selectedProject = value;
                dailylog.projectId = _selectedProject.id;
              });
            },
            isExpanded: false,
            hint: _selectedProject == null
                ? Text('Select Project')
                : Text(_selectedProject.projectName),
          );
        });
  }

  _save() {
    final _date =
        new DateTime(date.year, date.month, date.day, time.hour, time.minute);
    if (_selectedProject == null) {
      ToastMessage.error('Error, please select a project');
    } else {
      if (formKey.currentState.validate()) {
        formKey.currentState.save();
        dailylog.id = 0;
        dailylog.idfPeriod = period.id;
        dailylog.clientId = client.id;
        dailylog.userId = 0;
        dailylog.state = "C";
        dailylog.date = _date.toString();
        dailylog.client = null;
        dailylog.idfPeriodNavigation = null;
        dailylog.project = null;
        dailylog.hDailylogInvolvedPeople = [];
        dailylog.staffOnShift = '';

        dailylogProvider.saveDailylog(dailylog).then((result) {
          if (result) {
            ToastMessage.success('Daily Log was saved successfully!');
            Navigator.pop(context);
          } else {
            ToastMessage.error('Error, daily log was not saved.');
          }
        });
      }
    }
  }
}
