import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/models/dailylog_model.dart';
import 'package:people_mobile/src/providers/client_provider.dart';

Widget listDailylogs(BuildContext context, int idPeriod, Client client) {
  final clientProvider = new ClientProvider();
  return FutureBuilder(
    future: clientProvider.getDailylogsByPeriodAndClient(idPeriod, client.id),
    builder: (context, AsyncSnapshot<List<Dailylog>> snapshot) {
      if (snapshot.hasData) {
        return ListView(
          shrinkWrap: true,
          children: _dailylogListView(context, snapshot.data),
        );
      }
      return CircularProgressIndicator();
    },
  );
}

List<Widget> _dailylogListView(BuildContext context, List<Dailylog> dailylogs) {
  final List<Widget> dailylogWidgets = [];
  dailylogs.forEach((dailylog) {
    final widgetTemp = ListTile(
      title: Text(dailylog.description),
      subtitle: Row(
        children: [
          Text(dailylog.dateDailyLog ?? ""),
        ],
      ),
    );
    dailylogWidgets..add(widgetTemp)..add(Divider());
  });
  return dailylogWidgets;
}
