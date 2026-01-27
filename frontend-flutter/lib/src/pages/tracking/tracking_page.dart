import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/device-permissions_lib.dart';
import 'package:people_mobile/src/models/time-tracker_model.dart';
import 'package:people_mobile/src/providers/mobile_provider.dart';
import 'package:people_mobile/src/widgets/speech-to-text_widget.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:geolocator/geolocator.dart';
import 'package:people_mobile/src/framework/toast-message_lib.dart';
import 'package:people_mobile/src/widgets/tfa-verification_widget.dart';

class TrackingPage extends StatefulWidget {
  @override
  _TrackingPageState createState() => _TrackingPageState();
}

class _TrackingPageState extends State<TrackingPage> {
  final formKey = new GlobalKey<FormState>();
  var txtNote = TextEditingController();
  var timeTracking = new TimeTracker();
  bool isSaving = false;
  bool isTracking = false;
  String note;
  Position position;
  int idUserProjectProfile;

  @override
  Widget build(BuildContext context) {
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    isTracking = args['isTracking'];
    if (isTracking)
      timeTracking = args['timeTracker'];
    else
      idUserProjectProfile = args['idUserProjectProfile'];

    DevicePermissions.requestPermission(
        Permission.location, context, 'location');
    return Scaffold(
      appBar: AppBar(
        title: Text('Geotracking'),
      ),
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          isTracking
              ? Text(
                  'Tab the button to stop trackinkg',
                  style: Theme.of(context).textTheme.bodyText1,
                )
              : Text(
                  'Tab the button to start trackinkg',
                  style: Theme.of(context).textTheme.bodyText1,
                ),
          SizedBox(height: 80),
          _buildPadding(context),
          SizedBox(height: 50),
          Center(
            child: isSaving
                ? CircularProgressIndicator()
                : isTracking
                    ? _stopTrackingButton(context)
                    : _startTrackingButton(context),
          ),
        ],
      ),
    );
  }

  _buildPadding(BuildContext context) {
    return Form(
      key: formKey,
      child: Padding(
        padding: EdgeInsets.only(right: 40.0),
        child: TextFormField(
          controller: txtNote,
          onSaved: (value) {
            note = value;
          },
          validator: (value) {
            if (value.isEmpty) return "You have to write a note";
            return null;
          },
          decoration: InputDecoration(
            border: OutlineInputBorder(),
            icon: IconButton(
                icon: Icon(Icons.mic),
                onPressed: () async {
                  String result = await SpeechToTextWidget.showModal(context);
                  if (result != '') txtNote.text = result;
                }),
            labelText: 'Note',
          ),
          maxLines: 2,
        ),
      ),
    );
  }

  Container _startTrackingButton(BuildContext context) {
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
    if (formKey.currentState.validate()) {
      setState(() {
        isSaving = true;
      });
      formKey.currentState.save();
      final mobileProvider = new MobileProvider();
      position = await Geolocator.getCurrentPosition(
          desiredAccuracy: LocationAccuracy.high);
      if (!position.latitude.isNaN)
        mobileProvider
            .startTimeTracker(
              idUserProjectProfile,
              note,
              position.longitude,
              position.latitude,
            )
            .then((value) => {
                  ToastMessage.info(
                      'Tracking started, your position has saved!'),
                  Navigator.pop(context),
                });
    }
  }

  Container _stopTrackingButton(BuildContext context) {
    return Container(
      alignment: Alignment.center,
      child: RawMaterialButton(
        onPressed: () async {
          bool result = await TfaVerification.showModal(context);
          if (result) _stopTracking();
        },
        elevation: 15.0,
        fillColor: Theme.of(context).primaryColor,
        child: Padding(
          padding: EdgeInsets.all(20),
          child: Icon(
            Icons.location_off,
            color: Colors.white,
            size: 100.0,
          ),
        ),
        shape: CircleBorder(),
      ),
    );
  }

  _stopTracking() async {
    if (formKey.currentState.validate()) {
      setState(() {
        isSaving = true;
      });
      formKey.currentState.save();
      final mobileProvider = new MobileProvider();
      position = await Geolocator.getCurrentPosition(
          desiredAccuracy: LocationAccuracy.high);
      if (!position.latitude.isNaN)
        mobileProvider
            .stopTimeTracker(
              timeTracking.id,
              note,
              position.longitude,
              position.latitude,
            )
            .then((value) => {
                  ToastMessage.info(
                      'Tracking stopped, your position has saved!'),
                  Navigator.pop(context),
                });
    }
  }
}
