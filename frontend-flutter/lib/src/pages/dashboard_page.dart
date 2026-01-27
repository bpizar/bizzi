import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/device-permissions_lib.dart';
import 'package:people_mobile/src/providers/staff_provider.dart';
import 'package:people_mobile/src/providers/login_provider.dart';
import 'package:people_mobile/src/providers/voice_commands_provider.dart';
import 'package:people_mobile/src/widgets/menu_widget.dart';
import 'package:people_mobile/src/widgets/speech-to-text_widget.dart';

class DashboardPage extends StatefulWidget {
  @override
  _DashboardPageState createState() => _DashboardPageState();
}

class _DashboardPageState extends State<DashboardPage> {
  @override
  void initState() {
    super.initState();
    DevicePermissions.requestMultiplePermissions();
    final loginProvider = new LoginProvider();
    _title = "Welcome,${loginProvider.getUserName()},";
  }

  String _title = "Welcome,";

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text('Dashboard'),
        ),
        drawer: createMenu(context),
        floatingActionButton: FloatingActionButton(
          backgroundColor: Theme.of(context).primaryColor,
          onPressed: () async {
            String result = await SpeechToTextWidget.showModal(context, true);
            if (result != '')
              VoiceCommandsProvider.executeCommand(result, context);
          },
          child: Icon(Icons.mic),
        ),
        body: Center(
          child: Column(
            children: getContent(),
          ),
        ));
  }

  getContent() {
    return <Widget>[
      Text(_title),
    ];
  }
}
