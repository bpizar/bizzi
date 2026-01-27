import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/toast-message_lib.dart';
import 'package:permission_handler/permission_handler.dart';

class DevicePermissions {
  static final List<Permission> permissionList = [
    Permission.location,
    Permission.camera,
    Permission.microphone,
  ];
  static void requestMultiplePermissions() async {
    await permissionList.request();
  }

  static Future<bool> requestPermission(Permission permission,
      BuildContext context, String permissionName) async {
    if (await permission.request().isGranted) {
      return true;
    } else if (await permission.request().isDenied) {
      //Permisse denied
      ToastMessage.error(
          'I need access to the $permissionName to work properly');
      return false;
    } else {
      //Ask when is permanently denied, only Android
      requestPermanentlyDeniedPermission(permission, permissionName, context);
      return false;
    }
  }

  static void requestPermanentlyDeniedPermission(Permission permission,
      String permissionName, BuildContext context) async {
    if (await permission.isPermanentlyDenied) {
      showAlertDialog(context, permissionName);
    }
  }

  static showAlertDialog(BuildContext context, String permissionName) {
    // Create OK button
    Widget okButton = TextButton(
      child: Text("OK"),
      onPressed: () {
        openAppSettings();
        Navigator.of(context).pop();
      },
    );
    // Create Cancel button
    Widget cancelButton = TextButton(
      child: Text("Cancel"),
      onPressed: () {
        Navigator.of(context).pop();
      },
    );
    // Create AlertDialog
    AlertDialog alert = AlertDialog(
      title: Text("The app needs $permissionName permission"),
      elevation: 10,
      content: Text("¿Open app settings?"),
      actions: [
        okButton,
        cancelButton,
      ],
    );

    // show the dialog
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return alert;
      },
    );
  }
}
