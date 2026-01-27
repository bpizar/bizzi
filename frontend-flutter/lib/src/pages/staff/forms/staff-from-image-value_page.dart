import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/staff-form-image-value_model.dart';
import 'package:people_mobile/src/models/staff-forms-by-staff_model.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/providers/staff-form_provider.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

StaffFormByStaff staffForm;
final urlProvider = new UrlProvider();
final staffFormProvider = new StaffFormProvider();

class StaffFormImageValuePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    final StaffFormImageValue staffFormImageValue = args['staffFormImageValue'];
    staffForm = args['staffForm'];
    final Staff staff = args['staff'];
    return Scaffold(
      appBar: AppBar(
        title: Text('Staff Form Image Preview'),
      ),
      body: Center(
        child: Padding(
          padding: EdgeInsets.all(10),
          child: Column(
            children: [
              SizedBox(height: 20),
              Text(
                'Staff: ${staff.fullName}',
                style: TextStyle(
                    fontSize: 20, color: Theme.of(context).primaryColor),
              ),
              Text('Form Name: ${staffForm.name}',
                  style: TextStyle(
                      fontSize: 16, color: Theme.of(context).primaryColor)),
              SizedBox(
                height: 15,
              ),
              Expanded(
                child: Card(
                  child: Image.network(urlProvider
                      .getUri(
                          '/image/staffFormValues/' + staffFormImageValue.image)
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
