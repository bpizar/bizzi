import 'package:flutter/material.dart';
import 'package:people_mobile/src/providers/login_provider.dart';

class LoginPage extends StatefulWidget {
  LoginPage({Key key, this.title}) : super(key: key);

  final String title;

  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  TextEditingController userController = new TextEditingController();
  TextEditingController passwordController = new TextEditingController();
  bool isLoading = false;
  Widget _entryField(
      TextEditingController controller, IconData icon, String title,
      {bool isPassword = false}) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 10),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          SizedBox(
            height: 10,
          ),
          TextField(
              controller: controller,
              obscureText: isPassword,
              decoration: InputDecoration(
                  icon: Icon(
                    icon,
                    color: Theme.of(context).primaryColor,
                  ),
                  hintText: title,
                  labelText: title,
                  // border: InputBorder.none,
                  // fillColor: Color(0xfff3f3f4),
                  filled: true))
        ],
      ),
    );
  }

  Widget _submitButton(BuildContext context) {
    return TextButton(
        onPressed: () async {
          setState(() {
            isLoading = true;
          });
          var result = await LoginProvider()
              .login(userController.value.text, passwordController.value.text);
          if (result) {
            setState(() {
              isLoading = false;
            });
            Navigator.pushReplacementNamed(context, "dashboard");
          } else {
            setState(() {
              isLoading = false;
            });
            showDialog(
              context: context,
              barrierDismissible: true,
              builder: (context) {
                return AlertDialog(
                  title: Text('Authentication Error'),
                  content: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      Text(
                          'There was not possible to login, please check the information')
                    ],
                  ),
                  actions: <Widget>[
                    TextButton(
                      child: Text('Ok'),
                      onPressed: () => Navigator.of(context).pop(),
                    )
                  ],
                );
              },
            );
          }
        },
        // color: Colors.white,
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(
              'Login',
              style: TextStyle(
                  fontSize: 20, color: Theme.of(context).primaryColor),
            ),
            Icon(
              Icons.apps,
              color: Theme.of(context).primaryColor,
              size: 30.0,
            ),
          ],
        ));
  }

  Widget _title() {
    return Column(children: <Widget>[
      Center(
        child: Image.asset(
          'assets/images/logo01.png',
          width: 150,
        ),
      )
    ]);
  }

  Widget _emailPasswordWidget() {
    return Column(
      children: <Widget>[
        _entryField(userController, Icons.email, "Username"),
        _entryField(passwordController, Icons.lock, "Password",
            isPassword: true),
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    final height = MediaQuery.of(context).size.height;
    return Scaffold(
        body: Container(
      height: height,
      decoration: BoxDecoration(
          gradient: LinearGradient(
              begin: Alignment.centerRight,
              end: Alignment.centerLeft,
              colors: [
            Color(0xffB5B2B2),
            Color(0xffF1EAEA),
            Color(0xffB5B2B2)
          ])),
      child: isLoading
          ? Center(child: showLoading())
          : Stack(
              children: <Widget>[
                Container(
                  padding: EdgeInsets.symmetric(horizontal: 10),
                  child: SingleChildScrollView(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.center,
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: <Widget>[
                        SizedBox(height: height * .1),
                        _title(),
                        SizedBox(height: 50),
                        _emailPasswordWidget(),
                        SizedBox(height: 20),
                        _submitButton(context),
                      ],
                    ),
                  ),
                ),
              ],
            ),
    ));
  }

  Widget showLoading() {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        CircularProgressIndicator(),
        SizedBox(height: 20),
        Text('Please wait...')
      ],
    );
  }
}
