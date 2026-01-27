import 'package:flutter/material.dart';

class VoiceCommandsProvider {
  static executeCommand(String command, BuildContext context) {
    if (command.toLowerCase().contains("staff")) {
      Navigator.pushReplacementNamed(context, 'staff');
    }
    if (command.toLowerCase().contains("client")) {
      Navigator.pushReplacementNamed(context, 'clients');
    }
    if (command.toLowerCase().contains("project")) {
      Navigator.pushReplacementNamed(context, 'projects');
    }
    if (command.toLowerCase().contains("schedule")) {
      Navigator.pushReplacementNamed(context, 'schedule');
    }
    if (command.toLowerCase().contains("geo tracking")) {
      Navigator.pushReplacementNamed(context, 'geotracking');
    }
    if (command.toLowerCase().contains("time tracker")) {
      Navigator.pushReplacementNamed(context, 'timetracker');
    }
  }
}
