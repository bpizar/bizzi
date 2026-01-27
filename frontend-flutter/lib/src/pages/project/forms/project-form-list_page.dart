import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/project-form-image-value_model.dart';
import 'package:people_mobile/src/models/project-forms-by-project_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/project-form_provider.dart';
import 'package:people_mobile/src/search/project-form_delegate.dart';

class ProjectFormListPage extends StatefulWidget {
  final BuildContext context;
  final Project project;

  ProjectFormListPage(this.context, this.project);

  @override
  _ProjectFormListPageState createState() => _ProjectFormListPageState();
}

class _ProjectFormListPageState extends State<ProjectFormListPage> {
  final projectFormProvider = new ProjectFormProvider();
  Future<List<ProjectFormByProject>> futureProjectFormByProject;

  @override
  void initState() {
    super.initState();
    _loadProjectFormByProject(widget.project.id);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          showSearch(
              context: context,
              delegate: ProjectFormDelegate(
                  project: widget.project,
                  onCreateisChosen: (projectFormByProject) {
                    if (projectFormByProject.id != null)
                      _createForm(projectFormByProject);
                  }));
        },
        child: Icon(Icons.search),
      ),
      body: Column(
          children: [SizedBox(height: 20.0), _futureBuilderProjectForms()]),
    );
  }

  _loadProjectFormByProject(int idProject) {
    setState(() {
      futureProjectFormByProject =
          projectFormProvider.getProjectFormByProject(idProject);
    });
  }

  _futureBuilderProjectForms() {
    return Expanded(
      child: FutureBuilder(
        future: futureProjectFormByProject,
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
                    visible: (!(projectForm.idfProjectFormValue > 0)) ||
                        (projectForm.idfRecurrence > 0),
                    child: IconButton(
                        icon: Icon(Icons.add),
                        onPressed: () {
                          _createForm(projectForm);
                        }),
                  ),
                  Visibility(
                    visible: ((projectForm.idfProjectFormValue > 0)),
                    child: IconButton(
                        icon: Icon(Icons.edit),
                        onPressed: () {
                          _editForm(projectForm);
                        }),
                  ),
                  Visibility(
                    visible: projectForm.idfProjectFormValue > 0,
                    child: IconButton(
                        icon: Icon(Icons.remove_red_eye),
                        onPressed: () {
                          _showPreview(projectForm);
                        }),
                  ),
                  Visibility(
                    visible: projectForm.quantity > 1,
                    child: IconButton(
                        icon: Icon(Icons.list),
                        onPressed: () {
                          _showHistoryForm(projectForm);
                        }),
                  ),
                ],
              )));

      projectFormsWidgets..add(widgetTemp);
    });
    return projectFormsWidgets;
  }

  _createForm(ProjectFormByProject projectFormByProject) async {
    projectFormByProject.idfProjectFormValue = 0;
    if (projectFormByProject.template.startsWith('Image')) {
      Navigator.pushNamed(widget.context, 'edit-project-form-image-value',
          arguments: {
            'projectFormByProject': projectFormByProject,
            'project': widget.project
          }).then((value) => _loadProjectFormByProject(widget.project.id));
    } else
      Navigator.pushNamed(widget.context, 'edit-program-form-value',
          arguments: {
            'projectFormByProject': projectFormByProject,
            'project': widget.project
          }).then((value) => _loadProjectFormByProject(widget.project.id));
  }

  _editForm(ProjectFormByProject projectFormByProject) async {
    if (projectFormByProject.template.startsWith('Image')) {
      Navigator.pushNamed(widget.context, 'edit-project-form-image-value',
          arguments: {
            'projectFormByProject': projectFormByProject,
            'project': widget.project
          }).then((value) => _loadProjectFormByProject(widget.project.id));
    } else
      Navigator.pushNamed(widget.context, 'edit-program-form-value',
          arguments: {
            'projectFormByProject': projectFormByProject,
            'project': widget.project
          }).then((value) => _loadProjectFormByProject(widget.project.id));
  }

  _showPreview(ProjectFormByProject projectForm) async {
    if (projectForm.template.startsWith('Image')) {
      final ProjectFormImageValue projectFormImageValue =
          await projectFormProvider
              .getProjectFormImageValueById(projectForm.idfProjectFormValue);
      Navigator.pushNamed(widget.context, 'project-form-image-value',
          arguments: {
            'projectFormImageValue': projectFormImageValue,
            'projectForm': projectForm,
            'project': widget.project
          });
    } else
      Navigator.pushNamed(widget.context, 'program-form-value', arguments: {
        'projectFormByProject': projectForm,
        'project': widget.project
      });
  }

  _showHistoryForm(ProjectFormByProject projectForm) {
    Navigator.pushNamed(widget.context, 'program-form-history-list',
        arguments: {
          'projectFormByProject': projectForm,
          'project': widget.project
        });
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
