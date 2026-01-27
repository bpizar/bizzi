import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/staff-form-image-value_model.dart';
import 'package:people_mobile/src/models/staff-forms-by-staff_model.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/providers/staff-form_provider.dart';

class StaffFormDelegate extends SearchDelegate {
  final staffFormProvider = new StaffFormProvider();
  final ValueChanged<StaffFormByStaff> onCreateisChosen;
  String selected = '';
  Staff staff;

  StaffFormDelegate({this.staff, this.onCreateisChosen});

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
        future: staffFormProvider.getStaffFormByStaffSearch(staff.id, query),
        builder: (BuildContext context,
            AsyncSnapshot<List<StaffFormByStaff>> snapshot) {
          if (snapshot.hasData) {
            final staffForms = snapshot.data;

            return ListView(
                children: staffForms.map((staffFormByStaff) {
              return Card(
                elevation: 1,
                child: ListTile(
                  title: Text(staffFormByStaff.name),
                  subtitle: Text(_formatDate(staffFormByStaff.formDateTime)),
                  trailing: Row(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      Visibility(
                        visible: (!(staffFormByStaff.idfStaffFormValue > 0)) ||
                            (staffFormByStaff.idfRecurrence > 0),
                        child: IconButton(
                            icon: Icon(Icons.add),
                            onPressed: () {
                              close(context, null);
                              _createForm(context, staffFormByStaff);
                            }),
                      ),
                      Visibility(
                        visible: staffFormByStaff.idfStaffFormValue > 0,
                        child: IconButton(
                            icon: Icon(Icons.remove_red_eye),
                            onPressed: () {
                              close(context, null);
                              _showPreview(context, staffFormByStaff);
                            }),
                      ),
                      Visibility(
                        visible: staffFormByStaff.quantity > 1,
                        child: IconButton(
                            icon: Icon(Icons.list),
                            onPressed: () {
                              close(context, null);
                              _showHistoryForm(context, staffFormByStaff);
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

  _createForm(BuildContext context, StaffFormByStaff staffFormByStaff) {
    onCreateisChosen(staffFormByStaff);
  }

  _showPreview(BuildContext context, StaffFormByStaff staffFormByStaff) async {
    if (staffFormByStaff.template.startsWith('Image')) {
      final StaffFormImageValue staffFormImageValue = await staffFormProvider
          .getStaffFormImageValueById(staffFormByStaff.idfStaffFormValue);
      Navigator.pushNamed(context, 'staff-form-image-value', arguments: {
        'staffFormImageValue': staffFormImageValue,
        'staffForm': staffFormByStaff,
        'staff': staff
      });
    } else
      Navigator.pushNamed(context, 'staff-form-value',
          arguments: {'staffFormByStaff': staffFormByStaff, 'staff': staff});
  }

  _showHistoryForm(BuildContext context, StaffFormByStaff staffForm) {
    Navigator.pushNamed(context, 'staff-form-history-list',
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
