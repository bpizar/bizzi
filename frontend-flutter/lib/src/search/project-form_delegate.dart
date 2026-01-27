import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/project-form-image-value_model.dart';
import 'package:people_mobile/src/models/project-forms-by-project_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/project-form_provider.dart';

class ProjectFormDelegate extends SearchDelegate {
  final projectFormProvider = new ProjectFormProvider();
  final ValueChanged<ProjectFormByProject> onCreateisChosen;
  String selected = '';
  Project project;

  ProjectFormDelegate({this.project, this.onCreateisChosen});

  @override
  List<Widget> buildActions(BuildContext context) {
    return [
      IconButton(
        icon: Icon(Icons.clear),
        onPressed: () {
          query = '';
        },
      )
    ];
  }

  @override
  Widget buildLeading(BuildContext context) {
    return IconButton(
      icon: AnimatedIcon(
        icon: AnimatedIcons.menu_arrow,
        progress: transitionAnimation,
      ),
      onPressed: () {
        close(context, null);
      },
    );
  }

  @override
  Widget buildResults(BuildContext context) {
    return Center(
      child: Container(
        height: 100.0,
        width: 100.0,
        color: Colors.blueAccent,
        child: Text(selected),
      ),
    );
  }

  @override
  Widget buildSuggestions(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 30),
      child: FutureBuilder(
        future: projectFormProvider.getProjectFormByProjectSearch(
            project.id, query),
        builder: (BuildContext context,
            AsyncSnapshot<List<ProjectFormByProject>> snapshot) {
          if (snapshot.hasData) {
            final projectForms = snapshot.data;

            return ListView(
                children: projectForms.map((projectFormByProject) {
              return Card(
                elevation: 1,
                child: ListTile(
                  title: Text(projectFormByProject.name),
                  subtitle:
                      Text(_formatDate(projectFormByProject.formDateTime)),
                  trailing: Row(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      Visibility(
                        visible:
                            (!(projectFormByProject.idfProjectFormValue > 0)) ||
                                (projectFormByProject.idfRecurrence > 0),
                        child: IconButton(
                            icon: Icon(Icons.add),
                            onPressed: () {
                              close(context, null);
                              _createForm(context, projectFormByProject);
                            }),
                      ),
                      Visibility(
                        visible: projectFormByProject.idfProjectFormValue > 0,
                        child: IconButton(
                            icon: Icon(Icons.remove_red_eye),
                            onPressed: () {
                              close(context, null);
                              _showPreview(context, projectFormByProject);
                            }),
                      ),
                      Visibility(
                        visible: projectFormByProject.quantity > 0,
                        child: IconButton(
                            icon: Icon(Icons.list),
                            onPressed: () {
                              close(context, null);
                              _showHistoryForm(context, projectFormByProject);
                            }),
                      ),
                    ],
                  ),
                ),
              );
            }).toList());
          } else {
            return Center(child: CircularProgressIndicator());
          }
        },
      ),
    );
  }

  _createForm(
      BuildContext context, ProjectFormByProject projectFormByProject) async {
    onCreateisChosen(projectFormByProject);
  }

  _showPreview(
      BuildContext context, ProjectFormByProject projectFormByProject) async {
    if (projectFormByProject.template.startsWith('Image')) {
      final ProjectFormImageValue projectFormImageValue =
          await projectFormProvider.getProjectFormImageValueById(
              projectFormByProject.idfProjectFormValue);
      Navigator.pushNamed(context, 'project-form-image-value', arguments: {
        'projectFormImageValue': projectFormImageValue,
        'projectForm': projectFormByProject,
        'project': project
      });
    } else
      Navigator.pushNamed(context, 'project-form-value', arguments: {
        'projectFormByProject': projectFormByProject,
        'project': project
      });
  }

  _showHistoryForm(BuildContext context, ProjectFormByProject projectForm) {
    Navigator.pushNamed(context, 'project-form-history-list',
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
