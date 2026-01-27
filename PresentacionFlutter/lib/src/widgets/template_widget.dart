import 'package:flutter/material.dart';
import 'package:flutter_html/flutter_html.dart';
import 'package:people_mobile/src/models/client-forms-by-client_model.dart';
import 'package:people_mobile/src/models/project-forms-by-project_model.dart';
import 'package:people_mobile/src/models/staff-forms-by-staff_model.dart';
import 'package:people_mobile/src/providers/client-form_provider.dart';
import 'package:people_mobile/src/providers/form-field_provider.dart';
import 'package:people_mobile/src/providers/project-form_provider.dart';
import 'package:people_mobile/src/providers/staff-form_provider.dart';

String report;
final formFieldProvider = new FormFieldProvider();
final staffFormProvider = new StaffFormProvider();
final clientFormProvider = new ClientFormProvider();
final projectFormProvider = new ProjectFormProvider();
String template;

// ignore: must_be_immutable
class BuildTemplate extends StatefulWidget {
  StaffFormByStaff staffFormByStaff = new StaffFormByStaff();
  ClientFormByClient clientFormByClient = new ClientFormByClient();
  ProjectFormByProject projectFormByProject = new ProjectFormByProject();
  BuildTemplate.staffForm(this.staffFormByStaff);
  BuildTemplate.clientForm(this.clientFormByClient);
  BuildTemplate.projectForm(this.projectFormByProject);

  @override
  _BuildTemplateState createState() => _BuildTemplateState();
}

class _BuildTemplateState extends State<BuildTemplate> {
  @override
  void initState() {
    loadData();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return template == null
        ? CircularProgressIndicator()
        : Expanded(
            child: Card(
              elevation: 3,
              child: SingleChildScrollView(
                child: Html(
                  data: template == null ? 'Loading data' : template,
                ),
              ),
            ),
          );
  }

  loadData() {
    if (widget.staffFormByStaff.id != null) {
      formFieldProvider
          .getFormFieldsByStaffForm(widget.staffFormByStaff.id)
          .then((data) {
        var formFields = data;
        template = widget.staffFormByStaff.template;
        formFields.forEach((element) {
          if ((element.name != null) && (element.isEnabled)) {
            template = template.replaceAll('\$' + element.name.trim() + '\$',
                "\$" + element.id.toString() + "\$");
          }
        });
        staffFormProvider
            .getStaffFormFieldValueByStaffFormValue(
                widget.staffFormByStaff.idfStaffFormValue)
            .then((data) {
          setState(() {
            var staffFormFieldValues = data;
            staffFormFieldValues.forEach((element) {
              template = template.replaceAll(
                  '\$' + element.idfFormField.toString() + '\$', element.value);
            });
          });
        });
      });
    }
    if (widget.clientFormByClient.id != null) {
      print(widget.clientFormByClient.id);
      formFieldProvider
          .getFormFieldsByClientForm(widget.clientFormByClient.id)
          .then((data) {
        //setState(() {
        var formFields = data;
        template = widget.clientFormByClient.template;
        formFields.forEach((element) {
          template = template.replaceAll(
              '\$' + element.name + '\$', "\$" + element.id.toString() + "\$");
        });
        //});
        clientFormProvider
            .getClientFormFieldValueByClientFormValue(
                widget.clientFormByClient.idfClientFormValue)
            .then((data) {
          setState(() {
            var clientFormFieldValues = data;
            clientFormFieldValues.forEach((element) {
              template = template.replaceAll(
                  '\$' + element.idfFormField.toString() + '\$', element.value);
            });
          });
        });
      });
    }
    if (widget.projectFormByProject.id != null) {
      print(widget.projectFormByProject.id);
      formFieldProvider
          .getFormFieldsByProjectForm(widget.projectFormByProject.id)
          .then((data) {
        //setState(() {
        var formFields = data;
        template = widget.projectFormByProject.template;
        formFields.forEach((element) {
          template = template.replaceAll(
              '\$' + element.name + '\$', "\$" + element.id.toString() + "\$");
        });
        //});
        projectFormProvider
            .getProjectFormFieldValueByProjectFormValue(
                widget.projectFormByProject.idfProjectFormValue)
            .then((data) {
          setState(() {
            var projectFormFieldValues = data;
            projectFormFieldValues.forEach((element) {
              template = template.replaceAll(
                  '\$' + element.idfFormField.toString() + '\$', element.value);
            });
          });
        });
      });
    }
  }
}
