import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';

class ToastMessage {
  static success(String successMessage) {
    return Fluttertoast.showToast(
        msg: successMessage,
        toastLength: Toast.LENGTH_LONG,
        gravity: ToastGravity.TOP,
        timeInSecForIosWeb: 1,
        backgroundColor: Color.fromRGBO(89, 202, 118, 0.8),
        textColor: Colors.white,
        fontSize: 16.0);
  }

  static error(String errorMessage) {
    return Fluttertoast.showToast(
        msg: errorMessage,
        toastLength: Toast.LENGTH_LONG,
        gravity: ToastGravity.TOP,
        timeInSecForIosWeb: 1,
        backgroundColor: Color.fromRGBO(237, 119, 119, 0.8),
        textColor: Colors.white,
        fontSize: 16.0);
  }

  static warning(String warningMessage) {
    return Fluttertoast.showToast(
        msg: warningMessage,
        toastLength: Toast.LENGTH_LONG,
        gravity: ToastGravity.TOP,
        timeInSecForIosWeb: 1,
        backgroundColor: Color.fromRGBO(243, 199, 117, 0.8),
        textColor: Colors.white,
        fontSize: 16.0);
  }

  static info(String infoMessage) {
    return Fluttertoast.showToast(
        msg: infoMessage,
        toastLength: Toast.LENGTH_LONG,
        gravity: ToastGravity.TOP,
        timeInSecForIosWeb: 1,
        backgroundColor: Color.fromRGBO(80, 167, 217, 0.8),
        textColor: Colors.white,
        fontSize: 16.0);
  }
}
