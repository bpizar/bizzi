import 'package:flutter/material.dart';

// ignore: must_be_immutable
class TimePicker extends StatefulWidget {
  TimeOfDay time = TimeOfDay.now();
  final ValueChanged<TimeOfDay> onChanged;

  TimePicker({this.time, this.onChanged});

  @override
  _TimePickerState createState() => _TimePickerState();
}

class _TimePickerState extends State<TimePicker> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: new EdgeInsets.all(8.0),
      child: GestureDetector(
        onTap: _pickTime,
        child: Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(
                "${widget.time.hour}:${widget.time.minute.toString().padLeft(2, '0')}"),
            SizedBox(width: 20),
            Icon(Icons.keyboard_arrow_down)
          ],
        ),
      ),
    );
  }

  _pickTime() async {
    TimeOfDay chosedTime = await showTimePicker(
      context: context,
      initialTime: widget.time,
    );

    if (chosedTime != null) {
      setState(() {
        widget.time = chosedTime;
        widget.onChanged(chosedTime);
      });
    }
  }
}
