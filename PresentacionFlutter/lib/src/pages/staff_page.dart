import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/providers/staff_provider.dart';
import 'package:people_mobile/src/widgets/menu_widget.dart';

class StaffPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text('Staff'),
        ),
        drawer: createMenu(context),
        body: Center(
          child: _listStaff(context),
        ));
  }

  Widget _listStaff(BuildContext context) {
    final staffProvider = new StaffProvider();
    return FutureBuilder(
      future: staffProvider.getStaffList(),
      builder: (context, AsyncSnapshot<List<Staff>> snapshot) {
        if (snapshot.hasData) {
          return ListView(
            children: _staffListView(context, snapshot.data),
          );
        }
        return CircularProgressIndicator();
      },
    );
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
        subtitle: Row(
          children: [
            Text(staff.email),
          ],
        ),
        trailing: Icon(Icons.keyboard_arrow_right,
            color: Theme.of(context).iconTheme.color),
        onTap: () {
          Navigator.pushNamed(context, 'staff-detail', arguments: staff);
        },
      );
      staffWidgets..add(widgetTemp)..add(Divider());
    });
    return staffWidgets;
  }
}
