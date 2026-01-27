import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/date-time-convert_lib.dart';
import 'package:people_mobile/src/models/schedule_model.dart';
import 'package:people_mobile/src/providers/mobile_provider.dart';
import 'package:people_mobile/src/providers/period_provider.dart';
import 'package:people_mobile/src/widgets/menu_widget.dart';
import 'package:table_calendar/table_calendar.dart';

class SchedulePage extends StatefulWidget {
  @override
  _SchedulePageState createState() => _SchedulePageState();
}

class _SchedulePageState extends State<SchedulePage> {
  final mobileProvider = new MobileProvider();
  final periodProvider = PeriodProvider();
  CalendarFormat _calendarFormat = CalendarFormat.week;
  DateTime _focusedDay = DateTime.now();
  DateTime _selectedDate;
  int _todayDay;
  DateTime _startScheduleDate = DateTime.now();
  DateTime _endScheduleDate = DateTime.now().add(Duration(days: 14));
  var _schedules = <Schedule>[];
  var _filterSchedules = <Schedule>[];

  @override
  void initState() {
    super.initState();
    _selectedDate = DateTime.now();
    _todayDay = DateTime.now().day;

    mobileProvider.getSchedulebyUser().then((data) {
      setState(() {
        _schedules = data;
        _filterSchedules = _schedules.where((element) {
          var date =
              DateTimeConvert.getStringDateFromDateTimeString(element.from);
          return date == _selectedDate;
        }).toList();
      });
    });
  }

  @override
  void dispose() {
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text('Staff Schedule'),
        ),
        drawer: createMenu(context),
        body: Center(
          child: Column(
            children: [
              TableCalendar(
                firstDay: _startScheduleDate,
                lastDay: _endScheduleDate,
                headerStyle: HeaderStyle(formatButtonVisible: false),
                calendarFormat: _calendarFormat,
                onPageChanged: (focusedDay) =>
                    _onVisibleDaysChanged(focusedDay),
                onDaySelected: (selectedDay, focusedDay) {
                  if (!isSameDay(_selectedDate, selectedDay)) {
                    setState(() {
                      _filterSchedules = _schedules.where((element) {
                        var date =
                            DateTimeConvert.getStringDateFromDateTimeString(
                                element.from);
                        return date == _selectedDate;
                      }).toList();
                    });
                  }
                },
                focusedDay: DateTime.now(),
              ),
              Divider(),
              Stack(
                alignment: Alignment.center,
                children: [
                  _getTitle(),
                  Visibility(
                    child: _buildTodayButton(),
                    visible: _selectedDate != DateTime.now(),
                  ),
                ],
              ),
              SizedBox(height: 20),
              _buildList(context)
            ],
          ),
        ));
  }

  _onVisibleDaysChanged(DateTime focusedDay) {
    if (_selectedDate.isAfter(focusedDay)) {
      if (_selectedDate
          .add(new Duration(days: -7))
          .isBefore(_startScheduleDate))
        _selectedDate = _startScheduleDate;
      else
        _selectedDate = _selectedDate.add(new Duration(days: -7));
    }
    if (_selectedDate.isBefore(focusedDay)) {
      if (_selectedDate.add(new Duration(days: 7)).isAfter(_endScheduleDate))
        _selectedDate = _endScheduleDate;
      else
        _selectedDate = _selectedDate.add(new Duration(days: 7));
    }
    setState(() {
      _filterSchedules = _schedules.where((element) {
        var date =
            DateTimeConvert.getStringDateFromDateTimeString(element.from);
        return date == _selectedDate;
      }).toList();
    });
  }

  Container _buildTodayButton() {
    return Container(
      alignment: Alignment.topRight,
      child: RawMaterialButton(
        onPressed: () {
          _selectedDate = DateTime.now();
          // _calendarController.setSelectedDay(DateTime.now(), runCallback: true);
        },
        elevation: 2.0,
        fillColor: Theme.of(context).primaryColor,
        child: Text(
          _todayDay.toString(),
          style: TextStyle(color: Colors.white),
        ),
        shape: CircleBorder(),
      ),
    );
  }

  Widget _buildList(BuildContext context) {
    return _filterSchedules.length > 0
        ? ListView.builder(
            padding: EdgeInsets.symmetric(horizontal: 30),
            shrinkWrap: true,
            itemCount: _filterSchedules.length,
            itemBuilder: (context, int index) {
              return Card(
                elevation: 1,
                child: ListTile(
                  leading: Icon(
                    Icons.crop_square,
                    color: parseColor(_filterSchedules[index].color),
                  ),
                  title: Text(_filterSchedules[index].projectName),
                  subtitle: Text(
                      'From: ${showHours(_filterSchedules[index].from)} to: ${showHours(_filterSchedules[index].to)}'),
                  onTap: () {},
                ),
              );
            })
        : Text('There are no events');
  }

  Container _getTitle() {
    return Container(
      child: Text(
        'Events for ' + DateTimeConvert.dateTimeToOnlyDateString(_selectedDate),
        style: TextStyle(
            fontWeight: FontWeight.bold,
            fontSize: 16,
            color: Theme.of(context).primaryColor),
      ),
    );
  }

  String showDate(DateTime dateTime) {
    return DateTimeConvert.dateTimeToOnlyDateString(dateTime);
  }

  String showHours(String dateTime) {
    return DateTimeConvert.getStringTimeFromDateTimeString(dateTime);
  }

  Color parseColor(String color) {
    try {
      color = color.toUpperCase().replaceAll("#", "");
      if (color.length == 6) {
        color = "FF" + color;
      }
    } on Exception catch (e) {
      print(e);
      return Colors.white;
    }
    return Color(int.parse(color, radix: 16));
  }
}
