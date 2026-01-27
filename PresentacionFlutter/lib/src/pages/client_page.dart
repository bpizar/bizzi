import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/providers/client_provider.dart';
import 'package:people_mobile/src/providers/url_provider.dart';
import 'package:people_mobile/src/widgets/menu_widget.dart';

final urlProvider = new UrlProvider();

class ClientsPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text('Clients'),
        ),
        drawer: createMenu(context),
        body: Center(
          child: _listClient(context),
        ));
  }

  Widget _listClient(BuildContext context) {
    final clientProvider = new ClientProvider();
    return FutureBuilder(
      future: clientProvider.getclients(),
      builder: (context, AsyncSnapshot<List<Client>> snapshot) {
        if (snapshot.hasData) {
          return ListView(
            children: _clientListView(context, snapshot.data),
          );
        }
        return CircularProgressIndicator();
      },
    );
  }

  List<Widget> _clientListView(BuildContext context, List<Client> clients) {
    final List<Widget> clientWidgets = [];
    clients.forEach((client) {
      final widgetTemp = ListTile(
        title: Text(client.fullName),
        leading: Hero(
          tag: "client" + client.id.toString() ?? "",
          child: ClipRRect(
            borderRadius: BorderRadius.circular(20.0),
            child: FadeInImage(
              image: NetworkImage(urlProvider
                  .getUri('/image/clients/' + client.img)
                  .toString()),
              placeholder: AssetImage('assets/images/users/default.png'),
              fit: BoxFit.cover,
              height: 160.0,
            ),
          ),
        ),
        subtitle: Text(
          client.email ?? "",
          overflow: TextOverflow.fade,
        ),
        trailing: Icon(Icons.keyboard_arrow_right,
            color: Theme.of(context).iconTheme.color),
        onTap: () {
          Navigator.pushNamed(context, 'client-detail', arguments: client);
        },
      );
      clientWidgets
        ..add(widgetTemp)
        ..add(Divider());
    });
    return clientWidgets;
  }
}
