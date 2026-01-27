import 'package:flutter/material.dart';
import 'package:people_mobile/src/models/staff-form-image-value_model.dart';
import 'package:people_mobile/src/models/staff-forms-by-staff_model.dart';
import 'package:people_mobile/src/models/staff_model.dart';
import 'package:people_mobile/src/providers/staff-form_provider.dart';
import 'package:people_mobile/src/search/staff-form_delegate.dart';

class StaffFormListPage extends StatefulWidget {
  final BuildContext context;
  final Staff staff;

  StaffFormListPage(this.context, this.staff);

  @override
  _StaffFormListPageState createState() => _StaffFormListPageState();
}

class _StaffFormListPageState extends State<StaffFormListPage> {
  final staffFormProvider = new StaffFormProvider();
  Future<List<StaffFormByStaff>> futureStaffFormByStaff;

  @override
  void initState() {
    super.initState();
    _loadStaffFormByStaff(widget.staff.id);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          showSearch(
              context: context,
              delegate: StaffFormDelegate(
                  staff: widget.staff,
                  onCreateisChosen: (staffFormByStaff) {
                    if (staffFormByStaff.id != null)
                      _createForm(staffFormByStaff);
                  }));
        },
        child: Icon(Icons.search),
      ),
      body: Column(
          children: [SizedBox(height: 20.0), _futureBuilderStaffForms()]),
    );
  }

  _loadStaffFormByStaff(int idStaff) {
    setState(() {
      futureStaffFormByStaff = staffFormProvider.getStaffFormByStaff(idStaff);
    });
  }

  _futureBuilderStaffForms() {
    return Expanded(
      child: FutureBuilder(
        future: futureStaffFormByStaff,
        builder: (context, AsyncSnapshot<List<StaffFormByStaff>> snapshot) {
          if (snapshot.hasData) {
            return ListView(
              padding: EdgeInsets.symmetric(horizontal: 30),
              shrinkWrap: true,
              children: _listViewStaffForms(context, snapshot.data),
            );
          }
          return Center(child: CircularProgressIndicator());
        },
      ),
    );
  }

  _listViewStaffForms(BuildContext context, List<StaffFormByStaff> staffForms) {
    final List<Widget> staffFormsWidgets = [];
    staffForms.forEach((staffForm) {
      final widgetTemp = Card(
          elevation: 1,
          child: ListTile(
              title: Text(staffForm.name),
              subtitle: Text(_formatDate(staffForm.formDateTime)),
              trailing: Row(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Visibility(
                    visible: (!(staffForm.idfStaffFormValue > 0)) ||
                        (staffForm.idfRecurrence > 0),
                    child: IconButton(
                        icon: Icon(Icons.add),
                        onPressed: () {
                          _createForm(staffForm);
                        }),
                  ),
                  Visibility(
                    visible: ((staffForm.idfStaffFormValue > 0)),
                    child: IconButton(
                        icon: Icon(Icons.edit),
                        onPressed: () {
                          _editForm(staffForm);
                        }),
                  ),
                  Visibility(
                    visible: staffForm.idfStaffFormValue > 0,
                    child: IconButton(
                        icon: Icon(Icons.remove_red_eye),
                        onPressed: () {
                          _showPreview(staffForm);
                        }),
                  ),
                  Visibility(
                    visible: staffForm.quantity > 1,
                    child: IconButton(
                        icon: Icon(Icons.list),
                        onPressed: () {
                          _showHistoryForm(staffForm);
                        }),
                  ),
                ],
              )));

      staffFormsWidgets..add(widgetTemp);
    });
    return staffFormsWidgets;
  }

  _createForm(StaffFormByStaff staffFormByStaff) async {
    staffFormByStaff.idfStaffFormValue = 0;
    if (staffFormByStaff.template.startsWith('Image')) {
      Navigator.pushNamed(widget.context, 'edit-staff-form-image-value',
          arguments: {
            'staffFormByStaff': staffFormByStaff,
            'staff': widget.staff
          }).then((value) => _loadStaffFormByStaff(widget.staff.id));
    } else
      Navigator.pushNamed(widget.context, 'edit-staff-form-value', arguments: {
        'staffFormByStaff': staffFormByStaff,
        'staff': widget.staff
      }).then((value) => _loadStaffFormByStaff(widget.staff.id));
  }

  _editForm(StaffFormByStaff staffFormByStaff) async {
    if (staffFormByStaff.template.startsWith('Image')) {
      Navigator.pushNamed(widget.context, 'edit-staff-form-image-value',
          arguments: {
            'staffFormByStaff': staffFormByStaff,
            'staff': widget.staff
          }).then((value) => _loadStaffFormByStaff(widget.staff.id));
    } else
      Navigator.pushNamed(widget.context, 'edit-staff-form-value', arguments: {
        'staffFormByStaff': staffFormByStaff,
        'staff': widget.staff
      }).then((value) => _loadStaffFormByStaff(widget.staff.id));
  }

  _showPreview(StaffFormByStaff staffForm) async {
    if (staffForm.template.startsWith('Image')) {
      final StaffFormImageValue staffFormImageValue = await staffFormProvider
          .getStaffFormImageValueById(staffForm.idfStaffFormValue);
      Navigator.pushNamed(widget.context, 'staff-form-image-value', arguments: {
        'staffFormImageValue': staffFormImageValue,
        'staffForm': staffForm,
        'staff': widget.staff
      });
    } else
      Navigator.pushNamed(widget.context, 'staff-form-value',
          arguments: {'staffFormByStaff': staffForm, 'staff': widget.staff});
  }

  _showHistoryForm(StaffFormByStaff staffForm) {
    Navigator.pushNamed(widget.context, 'staff-form-history-list',
        arguments: {'staffFormByStaff': staffForm, 'staff': widget.staff});
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
