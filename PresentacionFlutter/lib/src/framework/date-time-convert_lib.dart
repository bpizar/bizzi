import 'package:flutter/material.dart';

import 'package:intl/intl.dart';

class DateTimeConvert {
  static String timeOfdayToString(TimeOfDay timeOfDay) {
    final now = new DateTime.now();
    final dt = DateTime(
        now.year, now.month, now.day, timeOfDay.hour, timeOfDay.minute);
    final dateFormat = DateFormat.Hms();
    return dateFormat.format(dt);
  }

  static String dateTimeToDateString(DateTime dateTime) {
    final _dateTime = DateTime(dateTime.year, dateTime.month, dateTime.day);
    final dateFormat = DateFormat('yyyy-MM-dd HH:mm:ss.SSS');
    String result = dateFormat.format(_dateTime);
    return result;
  }

  static String dateTimeToOnlyDateString(DateTime dateTime) {
    final _dateTime = DateTime(dateTime.year, dateTime.month, dateTime.day);
    final dateFormat = DateFormat('MM/dd/yyyy');
    String result = dateFormat.format(_dateTime);
    return result;
  }

  static String getStringDateFromDateTimeString(String _date) {
    final dt = new DateFormat("MM/dd/yyyy hh:mm:ss").parse(_date);
    final dateFormat = DateFormat('MM/dd/yyyy');
    return dateFormat.format(dt);
  }

  static String getStringTimeFromDateTimeString(String _time) {
    final dt = new DateFormat("MM/dd/yyyy hh:mm:ss").parse(_time);
    final dateFormat = DateFormat.Hms();
    return dateFormat.format(dt);
  }
}
