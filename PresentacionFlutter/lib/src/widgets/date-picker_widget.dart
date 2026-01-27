import 'package:flutter/material.dart';

// ignore: must_be_immutable
class DatePicker extends StatefulWidget {
  DateTime date = DateTime.now();
  final ValueChanged<String> onChanged;

  DatePicker({this.date, this.onChanged});

  @override
  _DatePickerState createState() => _DatePickerState();
}

class _DatePickerState extends State<DatePicker> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: new EdgeInsets.all(8.0),
      child: GestureDetector(
        onTap: _pickDate,
        child: Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text("${widget.date.month}/${widget.date.day}/${widget.date.year}"),
            SizedBox(width: 20),
            Icon(Icons.keyboard_arrow_down)
          ],
        ),
      ),
    );
  }

  _pickDate() async {
    DateTime chosedDate = await showDatePicker(
        context: context,
        initialDate: widget.date,
        firstDate: DateTime(DateTime.now().year - 1),
        lastDate: DateTime(DateTime.now().year + 1));

    if (chosedDate != null) {
      setState(() {
        widget.date = chosedDate;
        widget.onChanged(chosedDate.toString());
      });
    }
  }
}
