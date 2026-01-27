import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/staff-form-image-value_model.dart';
import 'package:people_mobile/src/models/staff-forms-by-staff_model.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/providers/staff-form_provider.dart';

// ignore: must_be_immutable
class StaffFormHistoryList extends StatelessWidget {
  final staffFormProvider = new StaffFormProvider();
  StaffFormByStaff staffFormByStaff;
  Staff staff;

  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    staffFormByStaff = args['staffFormByStaff'];
    staff = args['staff'];

    return Scaffold(
      appBar: AppBar(
        title: Text('Staff Forms History'),
      ),
      body: Column(
        children: [
          SizedBox(height: 20.0),
          Text(
            'Staff: ${staff.fullName}',
            style: Theme.of(context).textTheme.subtitle1,
          ),
          SizedBox(height: 10.0),
          _futureBuilderStaffForms()
        ],
      ),
    );
  }

  _futureBuilderStaffForms() {
    return Expanded(
      child: FutureBuilder(
        future: staffFormProvider.getStaffFormsByStaffandStaffForm(
            staff.id, staffFormByStaff.id),
        builder: (context, AsyncSnapshot<List<StaffFormByStaff>> snapshot) {
          if (snapshot.hasData) {
            return ListView(
              padding: EdgeInsets.symmetric(horizontal: 30),
              shrinkWrap: true,
              children: _listViewStaffForms(context, snapshot.data),
            );
          }
          return Center(child: CircularProgressIndicator());
        },
      ),
    );
  }

  _listViewStaffForms(BuildContext context, List<StaffFormByStaff> staffForms) {
    final List<Widget> staffFormsWidgets = [];
    staffForms.forEach((staffForm) {
      final widgetTemp = Card(
          elevation: 1,
          child: ListTile(
              title: Text(staffForm.name),
              subtitle: Text(_formatDate(staffForm.formDateTime)),
              trailing: Row(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Visibility(
                    visible: staffForm.idfStaffFormValue > 0,
                    child: IconButton(
                        icon: Icon(Icons.remove_red_eye),
                        onPressed: () {
                          _showPreview(context, staffForm);
                        }),
                  ),
                ],
              )));

      staffFormsWidgets..add(widgetTemp);
    });
    return staffFormsWidgets;
  }

  _showPreview(BuildContext context, StaffFormByStaff staffForm) async {
    if (staffForm.template.startsWith('Image')) {
      final StaffFormImageValue staffFormImageValue = await staffFormProvider
          .getStaffFormImageValueById(staffForm.idfStaffFormValue);
      Navigator.pushNamed(context, 'staff-form-image-value', arguments: {
        'staffFormImageValue': staffFormImageValue,
        'staffForm': staffForm,
        'staff': staff
      });
    } else
      Navigator.pushNamed(context, 'staff-form-value',
          arguments: {'staffFormByStaff': staffForm, 'staff': staff});
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
