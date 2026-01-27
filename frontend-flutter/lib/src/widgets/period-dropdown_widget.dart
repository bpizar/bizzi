import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/period_model.dart';
import 'package:people_mobile/src/providers/period_provider.dart';

class PeriodDropDown extends StatefulWidget {
  final ValueChanged<Period> onChanged;

  PeriodDropDown({this.onChanged});

  @override
  _PeriodDropDownState createState() => _PeriodDropDownState();
}

class _PeriodDropDownState extends State<PeriodDropDown> {
  final periodProvider = PeriodProvider();
  var _listPeriods = <Period>[];
  Period _selectedPeriod;

  @override
  void initState() {
    super.initState();
    loadPeriods();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      child: _listPeriods.length > 0
          ? DropdownButton(
              items: _listPeriods
                  .map((period) => DropdownMenuItem(
                        child: Text(period.dateJoin),
                        value: period,
                      ))
                  .toList(),
              onChanged: (chosedPeriod) {
                if (chosedPeriod != null) {
                  setState(() {
                    widget.onChanged(chosedPeriod);
                    _selectedPeriod = chosedPeriod;
                  });
                }
              },
              isExpanded: false,
              hint: Text('Select a period'),
              value: _selectedPeriod,
            )
          : Text('loading periods...'),
    );
  }

  loadPeriods() {
    periodProvider.getPeriods().then((data) {
      setState(() {
        _listPeriods = data;
        _selectedPeriod = _listPeriods.firstWhere((period) =>
            period.from.isBefore(DateTime.now()) &&
            period.to.isAfter(DateTime.now()));
        widget.onChanged(_selectedPeriod);
      });
    });
  }
}
