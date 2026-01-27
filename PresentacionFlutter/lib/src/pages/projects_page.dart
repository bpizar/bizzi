import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/project_provider.dart';
import 'package:people_mobile/src/providers/url_provider.dart';
import 'package:people_mobile/src/widgets/menu_widget.dart';

final urlProvider = new UrlProvider();

class ProjectsPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text('Programs'),
        ),
        drawer: createMenu(context),
        body: Center(
          child: _listProjects(context),
        ));
  }

  Widget _listProjects(BuildContext context) {
    final projectProvider = new ProjectProvider();
    return FutureBuilder(
      future: projectProvider.getprojects(),
      builder: (context, AsyncSnapshot<List<Project>> snapshot) {
        if (snapshot.hasData) {
          return ListView(
            children: _projectListView(context, snapshot.data),
          );
        }
        return CircularProgressIndicator();
      },
    );
  }

  List<Widget> _projectListView(BuildContext context, List<Project> projects) {
    final List<Widget> projectWidgets = [];
    projects.forEach((project) {
      final widgetTemp = ListTile(
        title: Text(project.projectName),
        leading: Icon(
          Icons.radio_button_unchecked,
          color: parseColor(project.color),
        ),
        subtitle: Text(
          "${project.description}\n${project.address}\n${project.phone1}\n${project.phone2}",
          overflow: TextOverflow.fade,
        ),
        trailing: Icon(Icons.keyboard_arrow_right,
            color: Theme.of(context).iconTheme.color),
        onTap: () {
          Navigator.pushNamed(context, 'program-detail', arguments: project);
        },
      );
      projectWidgets
        ..add(widgetTemp)
        ..add(Divider());
    });
    return projectWidgets;
  }

  Color parseColor(String color) {
    try {
      color = color.toUpperCase().replaceAll("#", "");
      if (color.length == 6) {
        color = "FF" + color;
      }
    } on Exception catch (e) {
      print(e);
      return Colors.white;
    }
    return Color(int.parse(color, radix: 16));
  }
}
