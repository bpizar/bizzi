import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/client_model.dart';
import 'package:people_mobile/src/models/dailylog_model.dart';
import 'package:people_mobile/src/models/period_model.dart';
import 'package:people_mobile/src/providers/client_provider.dart';
import 'package:people_mobile/src/widgets/period-dropdown_widget.dart';

// ignore: must_be_immutable
class DailyLogPage extends StatefulWidget {
  Client client = new Client();
  Period period = new Period();
  DailyLogPage(this.period, this.client);

  @override
  _DailyLogPageState createState() => _DailyLogPageState();
}

class _DailyLogPageState extends State<DailyLogPage> {
  final clientProvider = new ClientProvider();
  Future<List<Dailylog>> _futureDailyLogs;

  @override
  void initState() {
    super.initState();
    _loadDailyLogs(0);
    widget.period.id = null;
  }

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        FutureBuilder(
          future: _futureDailyLogs,
          builder: (context, AsyncSnapshot<List<Dailylog>> snapshot) {
            if (snapshot.hasData) {
              return Column(
                children: [
                  SizedBox(height: 10),
                  Text(
                    'Select a Period',
                    style: Theme.of(context).textTheme.bodyText1,
                  ),
                  PeriodDropDown(
                    onChanged: (chosedPeriod) {
                      setState(() {
                        widget.period = chosedPeriod;
                      });
                      _loadDailyLogs(chosedPeriod.id);
                    },
                  ),
                  Divider(),
                  Expanded(
                    child: ListView(
                      padding: EdgeInsets.symmetric(horizontal: 15),
                      shrinkWrap: true,
                      children: _dailylogListView(context, snapshot.data),
                    ),
                  ),
                ],
              );
            }
            return Center(child: CircularProgressIndicator());
          },
        ),
        Padding(
          padding: EdgeInsets.all(20),
          child: Align(
            alignment: Alignment.bottomRight,
            child: Visibility(
              visible: widget.period.id != null,
              child: FloatingActionButton(
                elevation: 5,
                backgroundColor: Theme.of(context).primaryColor,
                onPressed: () {
                  Navigator.pushNamed(context, 'edit-dailylog', arguments: {
                    'client': widget.client,
                    'period': widget.period,
                  }).then((value) => _loadDailyLogs(widget.period.id));
                },
                child: Icon(Icons.add),
              ),
            ),
          ),
        ),
      ],
    );
  }

  List<Widget> _dailylogListView(
      BuildContext context, List<Dailylog> dailylogs) {
    final List<Widget> dailylogWidgets = [];
    dailylogs.forEach((dailylog) {
      final widgetTemp = ListTile(
        title: Text(dailylog.projectName),
        subtitle: Row(
          children: [
            Text(dailylog.description),
            Text(dailylog.dateDailyLog ?? ""),
          ],
        ),
      );
      dailylogWidgets..add(widgetTemp)..add(Divider());
    });
    return dailylogWidgets;
  }

  _loadDailyLogs(int idPeriod) {
    setState(() {
      _futureDailyLogs = clientProvider.getDailylogsByPeriodAndClient(
          idPeriod, widget.client.id);
    });
  }
}
