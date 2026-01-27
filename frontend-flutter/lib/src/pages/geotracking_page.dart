import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/device-permissions_lib.dart';
import 'package:people_mobile/src/providers/login_provider.dart';
import 'package:people_mobile/src/providers/mobile_provider.dart';
import 'package:people_mobile/src/widgets/menu_widget.dart';
import 'package:people_mobile/src/widgets/tfa-verification_widget.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:geolocator/geolocator.dart';
import 'package:people_mobile/src/framework/toast-message_lib.dart';

class GeotrackingPage extends StatefulWidget {
  @override
  _GeotrackingPageState createState() => _GeotrackingPageState();
}

class _GeotrackingPageState extends State<GeotrackingPage> {
  bool isSaving = false;
  Position position;
  int idUser;

  @override
  @override
  void initState() {
    super.initState();
    final loginProvider = new LoginProvider();
    idUser = loginProvider.getIdLoggedUser();
  }

  Widget build(BuildContext context) {
    DevicePermissions.requestPermission(
        Permission.location, context, 'location');
    return Scaffold(
        appBar: AppBar(
          title: Text('Geotracking'),
        ),
        drawer: createMenu(context),
        body: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            isSaving
                ? Text(
                    'Getting position, please wait!',
                    style: Theme.of(context).textTheme.bodyText1,
                  )
                : Text(
                    'Tab the button to start trackinkg',
                    style: Theme.of(context).textTheme.bodyText1,
                  ),
            SizedBox(height: 80),
            Center(
              child: isSaving
                  ? CircularProgressIndicator()
                  : _trackingButton(context),
            ),
          ],
        ));
  }

  Container _trackingButton(BuildContext context) {
    return Container(
      alignment: Alignment.center,
      child: RawMaterialButton(
        onPressed: () async {
          bool result = await TfaVerification.showModal(context);
          if (result) _startTracking();
        },
        elevation: 15.0,
        fillColor: Theme.of(context).primaryColor,
        child: Padding(
          padding: EdgeInsets.all(20),
          child: Icon(
            Icons.location_on,
            color: Colors.white,
            size: 100.0,
          ),
        ),
        shape: CircleBorder(),
      ),
    );
  }

  _startTracking() async {
    setState(() {
      isSaving = true;
    });
    final mobileProvider = new MobileProvider();
    position = await Geolocator.getCurrentPosition(
        desiredAccuracy: LocationAccuracy.high);
    if (!position.latitude.isNaN)
      mobileProvider
          .autoTimeTracker(
            idUser,
            position.longitude,
            position.latitude,
          )
          .then((value) => {
                ToastMessage.info(
                    'Auto Tracking registered, your position has saved!'),
                FocusScope.of(context).unfocus(),
                setState(() {
                  isSaving = false;
                })
              });
  }
}
