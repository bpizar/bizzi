import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/toast-message_lib.dart';
import 'package:people_mobile/src/providers/auth_provider.dart';

// ignore: must_be_immutable
class TfaVerification extends StatefulWidget {
  BuildContext context;
  final ValueChanged<bool> onChanged;

  TfaVerification({this.context, this.onChanged});
  @override
  _TfaVerificationState createState() => _TfaVerificationState();

  static Future<bool> showModal(BuildContext context) async {
    bool result;

    await showModalBottomSheet(
      context: context,
      isDismissible: false,
      isScrollControlled: true,
      enableDrag: false,
      builder: (context) => SingleChildScrollView(
        child: Container(
          padding:
              EdgeInsets.only(bottom: MediaQuery.of(context).viewInsets.bottom),
          child: TfaVerification(
              context: context,
              onChanged: (value) {
                result = value;
              }),
        ),
      ),
    );
    return result;
  }
}

class _TfaVerificationState extends State<TfaVerification> {
  TextEditingController editController = new TextEditingController();
  final formKey = new GlobalKey<FormState>();
  String secret;

  @override
  Widget build(BuildContext context) {
    return Container(
      color: Color(0xff757575),
      child: Container(
        padding: EdgeInsets.all(20.0),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.only(
            topLeft: Radius.circular(20.0),
            topRight: Radius.circular(20.0),
          ),
        ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Text(
              'Enter the Secret',
              textAlign: TextAlign.center,
              style: Theme.of(context).textTheme.bodyText1,
            ),
            SizedBox(height: 10.0),
            Image(
              image: AssetImage('assets/images/authenticator.png'),
              height: 60,
              width: 60,
            ),
            SizedBox(height: 10.0),
            Form(
              key: formKey,
              child: TextFormField(
                autofocus: true,
                textAlign: TextAlign.center,
                keyboardType: TextInputType.number,
                decoration: new InputDecoration(
                  hintText: 'Secret',
                  border: OutlineInputBorder(),
                  labelText: 'Secret',
                ),
                onSaved: (value) {
                  secret = value;
                },
                validator: (value) {
                  if (value.isEmpty) return "The Secret must not be empty!";
                  if (value.length != 6) return "Invalid Secret!";
                  return null;
                },
              ),
            ),
            SizedBox(height: 10.0),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                ElevatedButton(
                  child: Text('Aceptar'),
                  onPressed: () {
                    _verify();
                  },
                ),
                SizedBox(width: 10.0),
                ElevatedButton(
                  child: Text('Cancelar'),
                  onPressed: () {
                    widget.onChanged(false);
                    Navigator.pop(context);
                  },
                ),
              ],
            )
          ],
        ),
      ),
    );
  }

  _verify() async {
    if (formKey.currentState.validate()) {
      formKey.currentState.save();
      final authProvider = new AuthProvider();
      final result = await authProvider.verifyTfa(secret);
      if (result) {
        widget.onChanged(true);
        Navigator.pop(context);
      } else {
        ToastMessage.error('Invalid Secret, please try again.');
        formKey.currentState.reset();
      }
    }
  }
}
