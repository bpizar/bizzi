import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/client-forms-by-client_model.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/providers/form-field_provider.dart';
import 'package:people_mobile/src/providers/client-form_provider.dart';
import 'package:people_mobile/src/widgets/template_widget.dart';

class ClientFormValuePage extends StatelessWidget {
  final formFieldProvider = new FormFieldProvider();
  final clientFormProvider = new ClientFormProvider();
  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    final ClientFormByClient clientFormByClient = args['clientFormByClient'];
    final Client client = args['client'];
    return Scaffold(
        appBar: AppBar(
          title: Text('Client Form Preview'),
        ),
        body: Padding(
          padding: EdgeInsets.all(20),
          child: Center(
            child: Column(
              children: [
                Text(
                  'Client: ${client.fullName}',
                  style: TextStyle(
                      fontSize: 20, color: Theme.of(context).primaryColor),
                ),
                Text('Form Name: ${clientFormByClient.name}',
                    style: TextStyle(
                        fontSize: 16, color: Theme.of(context).primaryColor)),
                SizedBox(
                  height: 15,
                ),
                BuildTemplate.clientForm(clientFormByClient),
              ],
            ),
          ),
        ));
  }
}
