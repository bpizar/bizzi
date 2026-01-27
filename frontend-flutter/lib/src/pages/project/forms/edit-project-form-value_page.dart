import 'package:flutter/material.dart';
import 'package:flutter_html/flutter_html.dart';
import 'package:people_mobile/src/framework/date-time-convert_lib.dart';
import 'package:people_mobile/src/framework/toast-message_lib.dart';
import 'package:people_mobile/src/models/form-field_model.dart'
    as FormFieldModel;
import 'package:people_mobile/src/models/project-form-field-value_model.dart';
import 'package:people_mobile/src/models/project-form-value_model.dart';
import 'package:people_mobile/src/models/project-forms-by-project_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/validation_provider.dart';
import 'package:people_mobile/src/widgets/date-picker_widget.dart';
import 'package:people_mobile/src/providers/form-field_provider.dart';
import 'package:people_mobile/src/providers/project-form_provider.dart';
import 'package:people_mobile/src/widgets/form-controls_widget.dart';
import 'package:url_launcher/url_launcher.dart';

ProjectFormByProject projectFormByProject;
String report;
var formFields = <FormFieldModel.FormField>[];
var projectFormValue = new ProjectFormValue();
var projectFormFieldValues = <ProjectFormFieldValue>[];
var project = new Project();
List<Widget> widgetList = [];

// ignore: must_be_immutable
class EditProjectFormValuePage extends StatefulWidget {
  @override
  _EditProjectFormValuePageState createState() =>
      _EditProjectFormValuePageState();
}

class _EditProjectFormValuePageState extends State<EditProjectFormValuePage> {
  final formFieldProvider = new FormFieldProvider();
  final projectFormProvider = new ProjectFormProvider();
  Future<List<FormFieldModel.FormField>> futureFormField;
  final validationProvider = new ValidationProvider();
  bool showinformation = false;
  String informationText = "+ Information";

  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    projectFormByProject = args['projectFormByProject'];
    project = args['project'];
    projectFormValue.formDateTime = DateTime.now().toString();
    futureFormField =
        formFieldProvider.getFormFieldsByProjectForm(projectFormByProject.id);
    _loadFormFieldValues(projectFormByProject.idfProjectFormValue);

    return WillPopScope(
      onWillPop: () async {
        clearArrays();
        return true;
      },
      child: Scaffold(
        appBar: AppBar(
          title: Text('Program Form Preview'),
        ),
        body: Padding(
          padding: EdgeInsets.all(20),
          child: Column(
            children: [
              Text(
                'Project: ${project.projectName}',
                style: TextStyle(
                    fontSize: 20, color: Theme.of(context).primaryColor),
              ),
              Text('Form Name: ${projectFormByProject.name}',
                  style: TextStyle(
                      fontSize: 16, color: Theme.of(context).primaryColor)),
              DatePicker(
                  date: DateTime.now(),
                  onChanged: (value) {
                    projectFormValue.formDateTime = value;
                  }),
              SizedBox(
                height: 15,
              ),
              if ((projectFormByProject.information ?? "") != "")
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
                          data: projectFormByProject.information ?? "",
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

  final List<Widget> formFieldWidgets = [];

  _loadWidget(
      BuildContext context, List<FormFieldModel.FormField> _formFields) {
    formFields = _formFields;
    _formFields.forEach((formField) {
      if (projectFormFieldValues.length > 0)
        formField.value = projectFormFieldValues
            .firstWhere((element) => element.idfFormField == formField.id,
                orElse: () => new ProjectFormFieldValue())
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
      var projectFormFieldValue = new ProjectFormFieldValue();
      projectFormFieldValue.id = 0;
      projectFormFieldValue.idfFormField = formFields[i].id;
      projectFormFieldValue.idfProjectFormValue = 0;
      projectFormFieldValue.value = widget.controller.text;
      projectFormFieldValues.add(projectFormFieldValue);
      validationProvider.setFormField(formFields[i], widget.controller.text);
    }
    if (validationProvider.arrayErrors.length > 0)
      ToastMessage.error(validationProvider.getBuildedMessage());
    else {
      final today = DateTimeConvert.dateTimeToDateString(DateTime.now());
      projectFormValue.id = 0;
      projectFormValue.formDateTime = projectFormValue.formDateTime ?? today;
      projectFormValue.idfProject = project.id;
      projectFormValue.idfProjectForm = projectFormByProject.id;
      projectFormValue.dateTime = today;

      var result = await projectFormProvider.saveProjectFormValue(
          projectFormValue, projectFormFieldValues);
      if (result) {
        ToastMessage.success('Form saved successfully!');
      } else
        ToastMessage.error('Error, the info was not saved.');
      clearArrays();
      Navigator.pop(context);
    }
  }

  _loadFormFieldValues(int idfProjectFormValue) async {
    if (idfProjectFormValue > 0 && projectFormFieldValues.length == 0) {
      projectFormFieldValues = await projectFormProvider
          .getProjectFormFieldValueByProjectFormValue(idfProjectFormValue);
      setState(() {
        projectFormFieldValues = projectFormFieldValues;
      });
    }
  }

  clearArrays() {
    formFields.clear();
    projectFormFieldValues.clear();
  }
}
