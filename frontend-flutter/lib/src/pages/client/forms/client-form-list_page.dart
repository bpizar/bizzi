import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/client-form-image-value_model.dart';
import 'package:people_mobile/src/models/client-forms-by-client_model.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/providers/client-form_provider.dart';
import 'package:people_mobile/src/search/client-form_delegate.dart';

class ClientFormListPage extends StatefulWidget {
  final BuildContext context;
  final Client client;

  ClientFormListPage(this.context, this.client);

  @override
  _ClientFormListPageState createState() => _ClientFormListPageState();
}

class _ClientFormListPageState extends State<ClientFormListPage> {
  final clientFormProvider = new ClientFormProvider();
  Future<List<ClientFormByClient>> futureClientFormByClient;

  @override
  void initState() {
    super.initState();
    _loadClientFormByClient(widget.client.id);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          showSearch(
              context: context,
              delegate: ClientFormDelegate(
                  client: widget.client,
                  onCreateisChosen: (clientFormByClient) {
                    _createForm(clientFormByClient);
                  }));
        },
        child: Icon(Icons.search),
      ),
      body: Column(
          children: [SizedBox(height: 20.0), _futureBuilderClientForms()]),
    );
  }

  _loadClientFormByClient(int idClient) {
    setState(() {
      futureClientFormByClient =
          clientFormProvider.getClientFormByClient(idClient);
    });
  }

  _futureBuilderClientForms() {
    return Expanded(
      child: FutureBuilder(
        future: futureClientFormByClient,
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
                    visible: (!(clientForm.idfClientFormValue > 0)) ||
                        (clientForm.idfRecurrence > 0),
                    child: IconButton(
                        icon: Icon(Icons.add),
                        onPressed: () {
                          _createForm(clientForm);
                        }),
                  ),
                  Visibility(
                    visible: ((clientForm.idfClientFormValue > 0)),
                    child: IconButton(
                        icon: Icon(Icons.edit),
                        onPressed: () {
                          _editForm(clientForm);
                        }),
                  ),
                  Visibility(
                    visible: clientForm.idfClientFormValue > 0,
                    child: IconButton(
                        icon: Icon(Icons.remove_red_eye),
                        onPressed: () {
                          _showPreview(clientForm);
                        }),
                  ),
                  Visibility(
                    visible: clientForm.quantity > 1,
                    child: IconButton(
                        icon: Icon(Icons.list),
                        onPressed: () {
                          _showHistoryForm(clientForm);
                        }),
                  ),
                ],
              )));

      clientFormsWidgets..add(widgetTemp);
    });
    return clientFormsWidgets;
  }

  _createForm(ClientFormByClient clientFormByClient) async {
    clientFormByClient.idfClientFormValue = 0;
    if (clientFormByClient.template.startsWith('Image')) {
      Navigator.pushNamed(widget.context, 'edit-client-form-image-value',
          arguments: {
            'clientFormByClient': clientFormByClient,
            'client': widget.client
          }).then((value) => _loadClientFormByClient(widget.client.id));
    } else
      Navigator.pushNamed(widget.context, 'edit-client-form-value', arguments: {
        'clientFormByClient': clientFormByClient,
        'client': widget.client
      }).then((value) => _loadClientFormByClient(widget.client.id));
  }

  _editForm(ClientFormByClient clientFormByClient) async {
    if (clientFormByClient.template.startsWith('Image')) {
      Navigator.pushNamed(widget.context, 'edit-client-form-image-value',
          arguments: {
            'clientFormByClient': clientFormByClient,
            'client': widget.client
          }).then((value) => _loadClientFormByClient(widget.client.id));
    } else
      Navigator.pushNamed(widget.context, 'edit-client-form-value', arguments: {
        'clientFormByClient': clientFormByClient,
        'client': widget.client
      }).then((value) => _loadClientFormByClient(widget.client.id));
  }

  _showPreview(ClientFormByClient clientForm) async {
    if (clientForm.template.startsWith('Image')) {
      final ClientFormImageValue clientFormImageValue = await clientFormProvider
          .getClientFormImageValueById(clientForm.idfClientFormValue);
      Navigator.pushNamed(widget.context, 'client-form-image-value',
          arguments: {
            'clientFormImageValue': clientFormImageValue,
            'clientForm': clientForm,
            'client': widget.client
          });
    } else
      Navigator.pushNamed(widget.context, 'client-form-value', arguments: {
        'clientFormByClient': clientForm,
        'client': widget.client
      });
  }

  _showHistoryForm(ClientFormByClient clientForm) {
    Navigator.pushNamed(widget.context, 'client-form-history-list',
        arguments: {'clientFormByClient': clientForm, 'client': widget.client});
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
