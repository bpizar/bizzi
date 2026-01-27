import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/project-form-image-value_model.dart';
import 'package:people_mobile/src/models/project-forms-by-project_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/project-form_provider.dart';

// ignore: must_be_immutable
class ProjectFormHistoryList extends StatelessWidget {
  final projectFormProvider = new ProjectFormProvider();
  ProjectFormByProject projectFormByProject;
  Project project;

  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    projectFormByProject = args['projectFormByProject'];
    project = args['project'];

    return Scaffold(
      appBar: AppBar(
        title: Text('Program Forms History'),
      ),
      body: Column(
        children: [
          SizedBox(height: 20.0),
          Text(
            'Program: ${project.projectName}',
            style: Theme.of(context).textTheme.subtitle1,
          ),
          SizedBox(height: 10.0),
          _futureBuilderProjectForms()
        ],
      ),
    );
  }

  _futureBuilderProjectForms() {
    return Expanded(
      child: FutureBuilder(
        future: projectFormProvider.getProjectFormsByProjectandProjectForm(
            project.id, projectFormByProject.id),
        builder: (context, AsyncSnapshot<List<ProjectFormByProject>> snapshot) {
          if (snapshot.hasData) {
            return ListView(
              padding: EdgeInsets.symmetric(horizontal: 30),
              shrinkWrap: true,
              children: _listViewProjectForms(context, snapshot.data),
            );
          }
          return Center(child: CircularProgressIndicator());
        },
      ),
    );
  }

  _listViewProjectForms(
      BuildContext context, List<ProjectFormByProject> projectForms) {
    final List<Widget> projectFormsWidgets = [];
    projectForms.forEach((projectForm) {
      final widgetTemp = Card(
          elevation: 1,
          child: ListTile(
              title: Text(projectForm.name),
              subtitle: Text(_formatDate(projectForm.formDateTime)),
              trailing: Row(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Visibility(
                    visible: projectForm.idfProjectFormValue > 0,
                    child: IconButton(
                        icon: Icon(Icons.remove_red_eye),
                        onPressed: () {
                          _showPreview(context, projectForm);
                        }),
                  ),
                ],
              )));

      projectFormsWidgets..add(widgetTemp);
    });
    return projectFormsWidgets;
  }

  _showPreview(BuildContext context, ProjectFormByProject projectForm) async {
    if (projectForm.template.startsWith('Image')) {
      final ProjectFormImageValue projectFormImageValue =
          await projectFormProvider
              .getProjectFormImageValueById(projectForm.idfProjectFormValue);
      Navigator.pushNamed(context, 'project-form-image-value', arguments: {
        'projectFormImageValue': projectFormImageValue,
        'projectForm': projectForm,
        'project': project
      });
    } else
      Navigator.pushNamed(context, 'project-form-value',
          arguments: {'projectFormByProject': projectForm, 'project': project});
  }

  String _formatDate(String date) {
    var temp = date.split(' ')[0].split('-');
    var res = temp[1] + '-' + temp[2] + '-' + temp[0];
    if (res == '01-01-0001')
      return 'No date';
    else
      return res;
  }
}
