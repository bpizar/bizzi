import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/client-form-image-value_model.dart';
import 'package:people_mobile/src/models/client-forms-by-client_model.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/providers/client-form_provider.dart';

class ClientFormDelegate extends SearchDelegate {
  final clientFormProvider = new ClientFormProvider();
  final ValueChanged<ClientFormByClient> onCreateisChosen;
  String selected = '';
  Client client;

  ClientFormDelegate({this.client, this.onCreateisChosen});

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
        future:
            clientFormProvider.getClientFormByClientSearch(client.id, query),
        builder: (BuildContext context,
            AsyncSnapshot<List<ClientFormByClient>> snapshot) {
          if (snapshot.hasData) {
            final clientForms = snapshot.data;

            return ListView(
                children: clientForms.map((clientFormByClient) {
              return Card(
                elevation: 1,
                child: ListTile(
                  title: Text(clientFormByClient.name),
                  subtitle: Text(_formatDate(clientFormByClient.formDateTime)),
                  trailing: Row(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      Visibility(
                        visible:
                            (!(clientFormByClient.idfClientFormValue > 0)) ||
                                (clientFormByClient.idfRecurrence > 0),
                        child: IconButton(
                            icon: Icon(Icons.add),
                            onPressed: () {
                              close(context, null);
                              _createForm(context, clientFormByClient);
                            }),
                      ),
                      Visibility(
                        visible: clientFormByClient.idfClientFormValue > 0,
                        child: IconButton(
                            icon: Icon(Icons.remove_red_eye),
                            onPressed: () {
                              close(context, null);
                              _showPreview(context, clientFormByClient);
                            }),
                      ),
                      Visibility(
                        visible: clientFormByClient.quantity > 0,
                        child: IconButton(
                            icon: Icon(Icons.list),
                            onPressed: () {
                              close(context, null);
                              _showHistoryForm(context, clientFormByClient);
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

  _createForm(BuildContext context, ClientFormByClient clientFormByClient) {
    onCreateisChosen(clientFormByClient);
  }

  _showPreview(
      BuildContext context, ClientFormByClient clientFormByClient) async {
    if (clientFormByClient.template.startsWith('Image')) {
      final ClientFormImageValue clientFormImageValue = await clientFormProvider
          .getClientFormImageValueById(clientFormByClient.idfClientFormValue);
      Navigator.pushNamed(context, 'client-form-image-value', arguments: {
        'clientFormImageValue': clientFormImageValue,
        'clientForm': clientFormByClient,
        'client': client
      });
    } else
      Navigator.pushNamed(context, 'client-form-value', arguments: {
        'clientFormByClient': clientFormByClient,
        'client': client
      });
  }

  _showHistoryForm(BuildContext context, ClientFormByClient clientForm) {
    Navigator.pushNamed(context, 'client-form-history-list',
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
