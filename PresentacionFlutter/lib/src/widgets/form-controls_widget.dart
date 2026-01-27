import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/date-time-convert_lib.dart';
import 'package:people_mobile/src/models/form-field_model.dart'
    as FormFieldModel;
import 'package:intl/intl.dart';

// ignore: must_be_immutable
class TextType extends StatelessWidget {
  FormFieldModel.FormField formField = new FormFieldModel.FormField();
  TextEditingController controller = new TextEditingController();
  TextType(this.formField);
  @override
  Widget build(BuildContext context) {
    controller.text = this.formField.value;
    return Container(
      margin: new EdgeInsets.all(8.0),
      child: new TextField(
        controller: controller,
        decoration: new InputDecoration(
          hintText: formField.placeholder,
          border: OutlineInputBorder(),
          labelText: formField.name.replaceAll("_", " "),
        ),
      ),
    );
  }
}

// ignore: must_be_immutable
class NumberType extends StatelessWidget {
  FormFieldModel.FormField formField = new FormFieldModel.FormField();
  TextEditingController controller = new TextEditingController();
  NumberType(this.formField);
  @override
  Widget build(BuildContext context) {
    controller.text = this.formField.value;
    return Container(
      margin: new EdgeInsets.all(8.0),
      child: new TextField(
        controller: controller,
        keyboardType: TextInputType.number,
        decoration: new InputDecoration(
          hintText: formField.placeholder,
          border: OutlineInputBorder(),
          labelText: formField.name.replaceAll("_", " "),
        ),
      ),
    );
  }
}

// ignore: must_be_immutable
class TextAreaType extends StatelessWidget {
  FormFieldModel.FormField formField = new FormFieldModel.FormField();
  TextEditingController controller = new TextEditingController();
  TextAreaType(this.formField);
  @override
  Widget build(BuildContext context) {
    controller.text = this.formField.value;
    return Container(
      margin: new EdgeInsets.all(8.0),
      child: new TextField(
        maxLines: 2,
        controller: controller,
        decoration: new InputDecoration(
          hintText: formField.placeholder,
          border: OutlineInputBorder(),
          labelText: formField.name.replaceAll("_", " "),
        ),
      ),
    );
  }
}

// ignore: must_be_immutable
class DateType extends StatefulWidget {
  FormFieldModel.FormField formField = new FormFieldModel.FormField();
  TextEditingController controller = new TextEditingController();
  DateType(this.formField);

  @override
  _DateTypeState createState() => _DateTypeState();
}

class _DateTypeState extends State<DateType> {
  var date = DateTime.now();
  @override
  void initState() {
    date = (widget.formField.value != null || widget.formField.value == '')
        ? new DateFormat("MM/dd/yyyy").parse(widget.formField.value)
        : DateTime.now();
    widget.controller.text = date.toString();
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
            Text('${widget.formField.name.replaceAll("_", " ")}:'),
            SizedBox(width: 20),
            Text("${date.month}/${date.day}/${date.year}"),
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
        initialDate: date,
        firstDate: DateTime(DateTime.now().year - 1),
        lastDate: DateTime(DateTime.now().year + 1));

    if (chosedDate != null) {
      setState(() {
        date = chosedDate;
        widget.controller.text = chosedDate.toString();
      });
    }
  }
}

// ignore: must_be_immutable
class CheckUncheckType extends StatefulWidget {
  FormFieldModel.FormField formField = new FormFieldModel.FormField();
  TextEditingController controller = new TextEditingController();
  CheckUncheckType(this.formField);
  @override
  _CheckUncheckTypeState createState() => _CheckUncheckTypeState();
}

class _CheckUncheckTypeState extends State<CheckUncheckType> {
  var checkBoxValue = false;
  @override
  void initState() {
    super.initState();
    checkBoxValue = widget.formField.value == 'true';
    widget.controller.text = widget.formField.value;
  }

  @override
  Widget build(BuildContext context) {
    return Container(
        margin: new EdgeInsets.all(8.0),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            new Checkbox(
                value: checkBoxValue,
                onChanged: (bool value) {
                  setState(() {
                    widget.controller.text = value.toString();
                    checkBoxValue = value;
                  });
                }),
            Text(widget.formField.name.replaceAll("_", " ")),
          ],
        ));
  }
}

// ignore: must_be_immutable
class YesNoType extends StatefulWidget {
  FormFieldModel.FormField formField = new FormFieldModel.FormField();
  TextEditingController controller = new TextEditingController();
  YesNoType(this.formField);
  @override
  _YesNoTypeTypeState createState() => _YesNoTypeTypeState();
}

class _YesNoTypeTypeState extends State<YesNoType> {
  var checkBoxValue = false;
  @override
  void initState() {
    super.initState();
    checkBoxValue = widget.formField.value == 'true';
    widget.controller.text = widget.formField.value;
  }

  @override
  Widget build(BuildContext context) {
    return Container(
        margin: new EdgeInsets.all(8.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(widget.formField.name.replaceAll("_", " ")),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text("Yes"),
                new Checkbox(
                    value: checkBoxValue,
                    onChanged: (bool value) {
                      setState(() {
                        widget.controller.text = value.toString();
                        checkBoxValue = value;
                      });
                    }),
                Text("No"),
                new Checkbox(
                    value: !checkBoxValue,
                    onChanged: (bool value) {
                      setState(() {
                        widget.controller.text = (!value).toString();
                        checkBoxValue = !value;
                      });
                    }),
              ],
            )
          ],
        ));
  }
}

// ignore: must_be_immutable
class TimeType extends StatefulWidget {
  FormFieldModel.FormField formField = new FormFieldModel.FormField();
  TextEditingController controller = new TextEditingController();
  TimeType(this.formField);

  @override
  _TimeTypeState createState() => _TimeTypeState();
}

class _TimeTypeState extends State<TimeType> {
  var time = TimeOfDay.now();
  @override
  void initState() {
    time = TimeOfDay(
        hour: int.parse(widget.formField.value.split(":")[0]),
        minute: int.parse(widget.formField.value.split(":")[1]));
    widget.controller.text = DateTimeConvert.timeOfdayToString(time);
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
            Text(widget.formField.name.replaceAll("_", " ")),
            SizedBox(width: 20),
            Text("${time.hour}:${time.minute.toString().padLeft(2, '0')}"),
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
      initialTime: time,
    );

    if (chosedTime != null) {
      setState(() {
        time = chosedTime;
        widget.controller.text = DateTimeConvert.timeOfdayToString(chosedTime);
      });
    }
  }
}
