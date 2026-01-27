import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/client-form-image-value_model.dart';
import 'package:people_mobile/src/models/client-forms-by-client_model.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/providers/client-form_provider.dart';
import 'package:people_mobile/src/providers/url_provider.dart';

ClientFormByClient clientForm;
final urlProvider = new UrlProvider();
final clientFormProvider = new ClientFormProvider();

class ClientFormImageValuePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    final ClientFormImageValue clientFormImageValue =
        args['clientFormImageValue'];
    clientForm = args['clientForm'];
    final Client client = args['client'];
    return Scaffold(
      appBar: AppBar(
        title: Text('Client Form Image Preview'),
      ),
      body: Center(
        child: Padding(
          padding: EdgeInsets.all(10),
          child: Column(
            children: [
              SizedBox(height: 20),
              Text(
                'Client: ${client.fullName}',
                style: TextStyle(
                    fontSize: 20, color: Theme.of(context).primaryColor),
              ),
              Text('Form Name: ${clientForm.name}',
                  style: TextStyle(
                      fontSize: 16, color: Theme.of(context).primaryColor)),
              SizedBox(
                height: 15,
              ),
              Expanded(
                child: Card(
                  child: Image.network(urlProvider
                      .getUri('/image/clientFormValues/' +
                          clientFormImageValue.image)
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
