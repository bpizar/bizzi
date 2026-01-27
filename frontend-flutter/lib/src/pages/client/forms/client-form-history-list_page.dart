import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/client-form-image-value_model.dart';
import 'package:people_mobile/src/models/client-forms-by-client_model.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/providers/client-form_provider.dart';

// ignore: must_be_immutable
class ClientFormHistoryList extends StatelessWidget {
  final clientFormProvider = new ClientFormProvider();
  ClientFormByClient clientFormByClient;
  Client client;

  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    clientFormByClient = args['clientFormByClient'];
    client = args['client'];

    return Scaffold(
      appBar: AppBar(
        title: Text('Client Forms History'),
      ),
      body: Column(
        children: [
          SizedBox(height: 20.0),
          Text(
            'Client: ${client.fullName}',
            style: Theme.of(context).textTheme.subtitle1,
          ),
          SizedBox(height: 10.0),
          _futureBuilderClientForms()
        ],
      ),
    );
  }

  _futureBuilderClientForms() {
    return Expanded(
      child: FutureBuilder(
        future: clientFormProvider.getClientFormsByClientandClientForm(
            client.id, clientFormByClient.id),
        builder: (context, AsyncSnapshot<List<ClientFormByClient>> snapshot) {
          if (snapshot.hasData) {
            return ListView(
              padding: EdgeInsets.symmetric(horizontal: 30),
              shrinkWrap: true,
              children: _listViewClientForms(context, snapshot.data),
            );
          }
          return Center(child: CircularProgressIndicator());
        },
      ),
    );
  }

  _listViewClientForms(
      BuildContext context, List<ClientFormByClient> clientForms) {
    final List<Widget> clientFormsWidgets = [];
    clientForms.forEach((clientForm) {
      final widgetTemp = Card(
          elevation: 1,
          child: ListTile(
              title: Text(clientForm.name),
              subtitle: Text(_formatDate(clientForm.formDateTime)),
              trailing: Row(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Visibility(
                    visible: clientForm.idfClientFormValue > 0,
                    child: IconButton(
                        icon: Icon(Icons.remove_red_eye),
                        onPressed: () {
                          _showPreview(context, clientForm);
                        }),
                  ),
                ],
              )));

      clientFormsWidgets..add(widgetTemp);
    });
    return clientFormsWidgets;
  }

  _showPreview(BuildContext context, ClientFormByClient clientForm) async {
    if (clientForm.template.startsWith('Image')) {
      final ClientFormImageValue clientFormImageValue = await clientFormProvider
          .getClientFormImageValueById(clientForm.idfClientFormValue);
      Navigator.pushNamed(context, 'client-form-image-value', arguments: {
        'clientFormImageValue': clientFormImageValue,
        'clientForm': clientForm,
        'client': client
      });
    } else
      Navigator.pushNamed(context, 'client-form-value',
          arguments: {'clientFormByClient': clientForm, 'client': client});
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
