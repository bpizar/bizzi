import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/pages/project/forms/project-form-list_page.dart';
import 'package:people_mobile/src/pages/project/project-clients-list_page.dart';
import 'package:people_mobile/src/pages/project/project-staff-list_page.dart';

class ProjectDetailPage extends StatefulWidget {
  @override
  _ProjectDetailPageState createState() => _ProjectDetailPageState();
}

Project project;

class _ProjectDetailPageState extends State<ProjectDetailPage> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    project = ModalRoute.of(context).settings.arguments;

    return DefaultTabController(
      length: 4,
      child: Scaffold(
        appBar: AppBar(
          title: Text('Program Detail'),
          bottom: TabBar(
            tabs: [
              Tab(text: 'Info'),
              Tab(text: 'Staff'),
              Tab(text: 'Clients'),
              Tab(text: 'Forms')
            ],
          ),
        ),
        body: TabBarView(children: [
          _info(context),
          ProjectStaffListPage(context, project),
          ProjectClientsListPage(context, project),
          ProjectFormListPage(context, project),
        ]),
      ),
    );
  }

  CustomScrollView _info(BuildContext context) {
    return CustomScrollView(
      slivers: <Widget>[
        SliverList(
          delegate: SliverChildListDelegate([
            SizedBox(height: 10.0),
            Row(
              children: <Widget>[
                Expanded(
                    child: ListTile(
                        leading: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Icon(
                              Icons.radio_button_unchecked,
                              color: parseColor(project.color),
                            ),
                          ],
                        ),
                        title: Text(
                          project.projectName ?? '',
                          style: Theme.of(context).textTheme.headline3,
                        ),
                        subtitle: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text('Description: ' + (project.description ?? ''),
                                style: Theme.of(context).textTheme.bodyText1,
                                overflow: TextOverflow.ellipsis,
                                maxLines: 2),
                            Text('Address: ' + (project.address ?? ''),
                                style: Theme.of(context).textTheme.bodyText1,
                                overflow: TextOverflow.ellipsis,
                                maxLines: 2),
                          ],
                        )))
              ],
            ),
          ]),
        )
      ],
    );
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
