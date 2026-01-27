import 'package:flutter/material.dart';

ThemeData mainTheme() {
  return ThemeData(
      primaryColor: _primaryColor,
      primaryColorDark: _primaryColorDark,
      dialogBackgroundColor: _dialogBackgroundColor,
      iconTheme: IconThemeData(
        color: _primaryColor,
      ),
      textTheme: _textTheme);
}

// Colors
final _primaryColor = Color(0xff46568f);
final _primaryColorDark = Color.fromRGBO(12, 9, 28, 1);
final _dialogBackgroundColor = Color(0xfff1f1f1);

// Text Style
final TextTheme _textTheme = TextTheme(
  headline1: _headingStyle1,
  headline2: _headingStyle2,
  headline3: _headingStyle3,
  headline4: _headingStyle4,
  headline5: _headingStyle5,
  headline6: _headingStyle6,
  bodyText1: _bodyText1,
  bodyText2: _bodyText2,
  subtitle1: _subtitle1,
);

final TextStyle _headingStyle1 =
    TextStyle(fontSize: 26, color: _primaryColorDark);
final TextStyle _headingStyle2 =
    TextStyle(fontSize: 24, color: _primaryColorDark);
final TextStyle _headingStyle3 =
    TextStyle(fontSize: 22, color: _primaryColorDark);
final TextStyle _headingStyle4 =
    TextStyle(fontSize: 20, color: _primaryColorDark);
final TextStyle _headingStyle5 =
    TextStyle(fontSize: 18, color: _primaryColorDark);
final TextStyle _headingStyle6 =
    TextStyle(fontSize: 16, color: _primaryColorDark);

final TextStyle _bodyText1 = TextStyle(fontSize: 20.0, color: Colors.grey);
final TextStyle _bodyText2 = TextStyle(fontSize: 16.0, color: Colors.grey);

final TextStyle _subtitle1 =
    TextStyle(fontSize: 22.0, color: _primaryColorDark);
