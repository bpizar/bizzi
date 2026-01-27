import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/project-forms-by-project_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/form-field_provider.dart';
import 'package:people_mobile/src/providers/project-form_provider.dart';
import 'package:people_mobile/src/widgets/template_widget.dart';

class ProjectFormValuePage extends StatelessWidget {
  final formFieldProvider = new FormFieldProvider();
  final projectFormProvider = new ProjectFormProvider();
  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    final ProjectFormByProject projectFormByProject =
        args['projectFormByProject'];
    final Project project = args['project'];
    return Scaffold(
        appBar: AppBar(
          title: Text('Program Form Preview'),
        ),
        body: Padding(
          padding: EdgeInsets.all(20),
          child: Center(
            child: Column(
              children: [
                Text(
                  'Program: ${project.projectName}',
                  style: TextStyle(
                      fontSize: 20, color: Theme.of(context).primaryColor),
                ),
                Text('Form Name: ${projectFormByProject.name}',
                    style: TextStyle(
                        fontSize: 16, color: Theme.of(context).primaryColor)),
                SizedBox(
                  height: 15,
                ),
                BuildTemplate.projectForm(projectFormByProject),
              ],
            ),
          ),
        ));
  }
}
