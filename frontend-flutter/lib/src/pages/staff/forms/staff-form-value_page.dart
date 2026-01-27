import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/staff-forms-by-staff_model.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/providers/form-field_provider.dart';
import 'package:people_mobile/src/providers/staff-form_provider.dart';
import 'package:people_mobile/src/widgets/template_widget.dart';

class StaffFormValuePage extends StatelessWidget {
  final formFieldProvider = new FormFieldProvider();
  final staffFormProvider = new StaffFormProvider();
  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    final StaffFormByStaff staffFormByStaff = args['staffFormByStaff'];
    final Staff staff = args['staff'];
    return Scaffold(
        appBar: AppBar(
          title: Text('Staff Form Preview'),
        ),
        body: Padding(
          padding: EdgeInsets.all(20),
          child: Center(
            child: Column(
              children: [
                Text(
                  'Staff: ${staff.fullName}',
                  style: TextStyle(
                      fontSize: 20, color: Theme.of(context).primaryColor),
                ),
                Text('Form Name: ${staffFormByStaff.name}',
                    style: TextStyle(
                        fontSize: 16, color: Theme.of(context).primaryColor)),
                SizedBox(
                  height: 15,
                ),
                BuildTemplate.staffForm(staffFormByStaff),
              ],
            ),
          ),
        ));
  }
}
