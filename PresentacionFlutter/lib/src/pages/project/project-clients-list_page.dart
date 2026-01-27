import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/project_provider.dart';

import '../../providers/url_provider.dart';

class ProjectClientsListPage extends StatefulWidget {
  final BuildContext context;
  final Project project;

  ProjectClientsListPage(this.context, this.project);

  @override
  _ProjectClientsListPageState createState() => _ProjectClientsListPageState();
}

class _ProjectClientsListPageState extends State<ProjectClientsListPage> {
  final projectProvider = new ProjectProvider();
  final urlProvider = new UrlProvider();
  Future<List<Client>> futureClientByProject;

  @override
  void initState() {
    super.initState();
    _loadProjectFormByProject(widget.project.id);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
          children: [SizedBox(height: 20.0), _futureBuilderProjectForms()]),
    );
  }

  _loadProjectFormByProject(int idProject) {
    setState(() {
      futureClientByProject = projectProvider.getClientsByProjectId(idProject);
    });
  }

  _futureBuilderProjectForms() {
    return Expanded(
      child: FutureBuilder(
        future: futureClientByProject,
        builder: (context, AsyncSnapshot<List<Client>> snapshot) {
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

  _listViewProjectForms(BuildContext context, List<Client> clients) {
    final List<Widget> projectFormsWidgets = [];
    clients.forEach((client) {
      final widgetTemp = Card(
          elevation: 1,
          child: ListTile(
            title: Text(client.fullName),
            leading: Hero(
              tag: "client" + client.id.toString() ?? "",
              child: ClipRRect(
                borderRadius: BorderRadius.circular(20.0),
                child: FadeInImage(
                  image: NetworkImage(urlProvider
                      .getUri('/image/clients/' + client.img)
                      .toString()),
                  placeholder: AssetImage('assets/images/users/default.png'),
                  fit: BoxFit.cover,
                  height: 160.0,
                ),
              ),
            ),
            subtitle: Text(
              client.email ?? "",
              overflow: TextOverflow.fade,
            ),
            trailing: Icon(Icons.keyboard_arrow_right,
                color: Theme.of(context).iconTheme.color),
            onTap: () {
              Navigator.pushNamed(context, 'client-detail', arguments: client);
            },
          ));
      projectFormsWidgets..add(widgetTemp);
    });
    return projectFormsWidgets;
  }
}
