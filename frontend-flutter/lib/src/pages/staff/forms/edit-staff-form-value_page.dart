import 'package:flutter/material.dart';
import 'package:flutter_html/flutter_html.dart';
import 'package:people_mobile/src/framework/date-time-convert_lib.dart';
import 'package:people_mobile/src/framework/toast-message_lib.dart';
import 'package:people_mobile/src/models/form-field_model.dart'
    as FormFieldModel;
import 'package:people_mobile/src/models/staff-form-field-value_model.dart';
import 'package:people_mobile/src/models/staff-form-value_model.dart';
import 'package:people_mobile/src/models/staff-forms-by-staff_model.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/providers/validation_provider.dart';
import 'package:people_mobile/src/widgets/date-picker_widget.dart';
import 'package:people_mobile/src/providers/form-field_provider.dart';
import 'package:people_mobile/src/providers/staff-form_provider.dart';
import 'package:people_mobile/src/widgets/form-controls_widget.dart';
import 'package:url_launcher/url_launcher.dart';

StaffFormByStaff staffFormByStaff;
String report;
var formFields = <FormFieldModel.FormField>[];
var staffFormValue = new StaffFormValue();
var staffFormFieldValues = <StaffFormFieldValue>[];
var staff = new Staff();
List<Widget> widgetList = [];

// ignore: must_be_immutable
class EditStaffFormValuePage extends StatefulWidget {
  @override
  _EditStaffFormValuePageState createState() => _EditStaffFormValuePageState();
}

class _EditStaffFormValuePageState extends State<EditStaffFormValuePage> {
  final formFieldProvider = new FormFieldProvider();
  final staffFormProvider = new StaffFormProvider();
  Future<List<FormFieldModel.FormField>> futureFormField;
  final List<Widget> formFieldWidgets = [];
  final validationProvider = new ValidationProvider();
  bool showinformation = false;
  String informationText = "+ Information";

  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    staffFormByStaff = args['staffFormByStaff'];
    staff = args['staff'];
    staffFormValue.formDateTime = DateTime.now().toString();
    futureFormField =
        formFieldProvider.getFormFieldsByStaffForm(staffFormByStaff.id);
    _loadFormFieldValues(staffFormByStaff.idfStaffFormValue);

    return WillPopScope(
      onWillPop: () async {
        clearArrays();
        return true;
      },
      child: Scaffold(
        appBar: AppBar(
          title: Text('Staff Form Preview'),
        ),
        body: Padding(
          padding: EdgeInsets.all(20),
          child: Column(
            children: [
              Text(
                'Staff: ${staff.fullName}',
                style: TextStyle(
                    fontSize: 20, color: Theme.of(context).primaryColor),
              ),
              Text('Form Name: ${staffFormByStaff.name}',
                  style: TextStyle(
                      fontSize: 16, color: Theme.of(context).primaryColor)),
              DatePicker(
                  date: DateTime.now(),
                  onChanged: (value) {
                    staffFormValue.formDateTime = value;
                  }),
              SizedBox(
                height: 15,
              ),
              if ((staffFormByStaff.information ?? "") != "")
                new GestureDetector(
                  onTap: () {
                    setState(() {
                      showinformation = !showinformation;
                      informationText =
                          showinformation ? "- Information" : "+ Information";
                    });
                  },
                  child: Container(
                    color: Theme.of(context).primaryColor,
                    width: double.maxFinite,
                    alignment: Alignment.topLeft,
                    child: new SizedBox(
                      child: Text(
                        informationText,
                        style: TextStyle(
                            backgroundColor: Theme.of(context).primaryColor,
                            color: Colors.white,
                            height: 2),
                      ),
                    ),
                  ),
                ),
              if (showinformation)
                Expanded(
                  child: Container(
                    color: Theme.of(context).dialogBackgroundColor,
                    child: SingleChildScrollView(
                      child: Column(children: [
                        SizedBox(
                          height: 15,
                        ),
                        Html(
                          data: staffFormByStaff.information ?? "",
                          onLinkTap: (String url, RenderContext context,
                              Map<String, String> attributes, element) {
                            Uri uri = Uri.parse(url);
                            launchUrl(uri);
                          },
                        ),
                        SizedBox(
                          height: 15,
                        )
                      ]),
                    ),
                  ),
                ),
              loadFormFields(),
              // Buttons
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  ElevatedButton(
                    onPressed: () {
                      saveData(context);
                    },
                    child: Text('Save'),
                  ),
                  SizedBox(
                    width: 20,
                  ),
                  ElevatedButton(
                    onPressed: () {
                      clearArrays();
                      Navigator.pop(context);
                    },
                    child: Text('Cancel'),
                  ),
                ],
              )
            ],
          ),
        ),
      ),
    );
  }

  loadFormFields() {
    return Expanded(
      child: FutureBuilder(
        future: futureFormField,
        builder: (BuildContext context, AsyncSnapshot snapshot) {
          if (snapshot.hasData) {
            return ListView(
              padding: EdgeInsets.symmetric(horizontal: 30),
              shrinkWrap: true,
              children: _loadWidget(context, snapshot.data),
            );
          }
          return Center(child: CircularProgressIndicator());
        },
      ),
    );
  }

  _loadWidget(
      BuildContext context, List<FormFieldModel.FormField> _formFields) {
    formFields = _formFields;
    _formFields.forEach((formField) {
      if (staffFormFieldValues.length > 0)
        formField.value = staffFormFieldValues
            .firstWhere((element) => element.idfFormField == formField.id,
                orElse: () => new StaffFormFieldValue())
            .value;
      switch (formField.datatype) {
        case 'number':
          formFieldWidgets.add(NumberType(formField));
          break;
        case 'decimal number':
          formFieldWidgets.add(NumberType(formField));
          break;
        case 'note':
          formFieldWidgets.add(TextAreaType(formField));
          break;
        case 'date':
          formFieldWidgets.add(DateType(formField));
          break;
        case 'time':
          formFieldWidgets.add(TimeType(formField));
          break;
        case 'check/no check':
          formFieldWidgets.add(CheckUncheckType(formField));
          break;
        case 'yes/no':
          formFieldWidgets.add(YesNoType(formField));
          break;
        default:
          formFieldWidgets.add(TextType(formField));
      }
    });
    return formFieldWidgets;
  }

  saveData(BuildContext context) async {
    for (var i = 0; i < formFields.length; i++) {
      var widget;
      switch (formFields[i].datatype) {
        case 'number':
          widget = formFieldWidgets[i] as NumberType;
          break;
        case 'decimal number':
          widget = formFieldWidgets[i] as NumberType;
          break;
        case 'note':
          widget = formFieldWidgets[i] as TextAreaType;
          break;
        case 'date':
          widget = formFieldWidgets[i] as DateType;
          break;
        case 'time':
          widget = formFieldWidgets[i] as TimeType;
          break;
        case 'check/no check':
          widget = formFieldWidgets[i] as CheckUncheckType;
          break;
        case 'yes/no':
          widget = formFieldWidgets[i] as YesNoType;
          break;
        default:
          widget = formFieldWidgets[i] as TextType;
      }
      var staffFormFieldValue = new StaffFormFieldValue();
      staffFormFieldValue.id = 0;
      staffFormFieldValue.idfFormField = formFields[i].id;
      staffFormFieldValue.idfStaffFormValue = 0;
      staffFormFieldValue.value = widget.controller.text;
      staffFormFieldValues.add(staffFormFieldValue);
      validationProvider.setFormField(formFields[i], widget.controller.text);
    }
    if (validationProvider.arrayErrors.length > 0)
      ToastMessage.error(validationProvider.getBuildedMessage());
    else {
      final today = DateTimeConvert.dateTimeToDateString(DateTime.now());
      staffFormValue.id = 0;
      staffFormValue.formDateTime = staffFormValue.formDateTime ?? today;
      staffFormValue.idfStaff = staff.id;
      staffFormValue.idfStaffForm = staffFormByStaff.id;
      staffFormValue.dateTime = today;

      var result = await staffFormProvider.saveStaffFormValue(
          staffFormValue, staffFormFieldValues);
      if (result) {
        ToastMessage.success('Form saved successfully!');
      } else
        ToastMessage.error('Error, the info was not saved.');
      clearArrays();
      Navigator.pop(context);
    }
  }

  _loadFormFieldValues(int idfStaffFormValue) async {
    if (idfStaffFormValue > 0 && staffFormFieldValues.length == 0) {
      staffFormFieldValues = await staffFormProvider
          .getStaffFormFieldValueByStaffFormValue(idfStaffFormValue);
      setState(() {
        staffFormFieldValues = staffFormFieldValues;
      });
    }
  }

  clearArrays() {
    formFields.clear();
    staffFormFieldValues.clear();
  }
}
