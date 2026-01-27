import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/project-form-image-value_model.dart';
import 'package:people_mobile/src/models/project-forms-by-project_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/project-form_provider.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

ProjectFormByProject projectForm;
final urlProvider = new UrlProvider();
final projectFormProvider = new ProjectFormProvider();

class ProjectFormImageValuePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    final ProjectFormImageValue projectFormImageValue =
        args['projectFormImageValue'];
    projectForm = args['projectForm'];
    final Project project = args['project'];
    return Scaffold(
      appBar: AppBar(
        title: Text('Program Form Image Preview'),
      ),
      body: Center(
        child: Padding(
          padding: EdgeInsets.all(10),
          child: Column(
            children: [
              SizedBox(height: 20),
              Text(
                'Program: ${project.projectName}',
                style: TextStyle(
                    fontSize: 20, color: Theme.of(context).primaryColor),
              ),
              Text('Form Name: ${projectForm.name}',
                  style: TextStyle(
                      fontSize: 16, color: Theme.of(context).primaryColor)),
              SizedBox(
                height: 15,
              ),
              Expanded(
                child: Card(
                  child: Image.network(urlProvider
                      .getUri('/image/projectFormValues/' +
                          projectFormImageValue.image)
                      .toString()),
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}
