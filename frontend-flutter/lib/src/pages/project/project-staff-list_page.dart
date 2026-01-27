import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/period_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/providers/project_provider.dart';
import 'package:people_mobile/src/providers/url_provider.dart';
import 'package:people_mobile/src/widgets/period-dropdown_widget.dart';

class ProjectStaffListPage extends StatefulWidget {
  final BuildContext context;
  final Project project;

  ProjectStaffListPage(this.context, this.project);

  @override
  _ProjectStaffListPageState createState() => _ProjectStaffListPageState();
}

class _ProjectStaffListPageState extends State<ProjectStaffListPage> {
  final projectProvider = new ProjectProvider();
  final urlProvider = new UrlProvider();
  Future<List<Staff>> futureStaff;
  Period period = new Period();

  @override
  void initState() {
    super.initState();
    _loadStaff(0);
    period.id = null;
  }

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        FutureBuilder(
          future: futureStaff,
          builder: (context, AsyncSnapshot<List<Staff>> snapshot) {
            if (snapshot.hasData) {
              return Column(
                children: [
                  SizedBox(height: 10),
                  Text(
                    'Select a Period',
                    style: Theme.of(context).textTheme.bodyText1,
                  ),
                  PeriodDropDown(
                    onChanged: (chosedPeriod) {
                      setState(() {
                        period = chosedPeriod;
                      });
                      _loadStaff(chosedPeriod.id);
                    },
                  ),
                  Divider(),
                  Expanded(
                    child: ListView(
                      padding: EdgeInsets.symmetric(horizontal: 15),
                      shrinkWrap: true,
                      children: _staffListView(context, snapshot.data),
                    ),
                  ),
                ],
              );
            }
            return Center(child: CircularProgressIndicator());
          },
        ),
      ],
    );
  }

  _loadStaff(int idPeriod) {
    setState(() {
      futureStaff =
          projectProvider.getProjectStaff(widget.project.id, idPeriod);
    });
  }

  List<Widget> _staffListView(BuildContext context, List<Staff> staffList) {
    final List<Widget> staffWidgets = [];
    staffList.forEach((staff) {
      final widgetTemp = ListTile(
        title: Text(staff.fullName),
        leading: Hero(
          tag: "client" + staff.id.toString() ?? "",
          child: ClipRRect(
            borderRadius: BorderRadius.circular(80.0),
            child: FadeInImage(
              image: NetworkImage(
                  urlProvider.getUri('/image/users/' + staff.img).toString()),
              placeholder: AssetImage('assets/images/users/default.png'),
              fit: BoxFit.cover,
              height: 160.0,
            ),
          ),
        ),
        subtitle: Text(
          "${staff.email}\n${staff.homePhone ?? ""}\n${staff.cellNumber ?? ""}",
          overflow: TextOverflow.fade,
        ),
        trailing: Icon(Icons.keyboard_arrow_right,
            color: Theme.of(context).iconTheme.color),
        onTap: () {
          Navigator.pushNamed(context, 'staff-detail', arguments: staff);
        },
      );
      staffWidgets
        ..add(widgetTemp)
        ..add(Divider());
    });
    return staffWidgets;
  }
}
