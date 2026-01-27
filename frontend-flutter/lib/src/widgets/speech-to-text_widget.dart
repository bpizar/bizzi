import 'package:flutter/material.dart';
import 'package:people_mobile/src/framework/device-permissions_lib.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:speech_to_text/speech_to_text.dart' as speechTotext;

// ignore: must_be_immutable
class SpeechToTextWidget extends StatefulWidget {
  bool autocompleteTask;
  BuildContext context;
  final ValueChanged<String> onChanged;

  SpeechToTextWidget({this.context, this.onChanged, this.autocompleteTask});

  @override
  _SpeechToTextWidgetState createState() => _SpeechToTextWidgetState();

  static Future<String> showModal(BuildContext context,
      [bool autocompleteTask = false]) async {
    String result;

    await showModalBottomSheet(
      context: context,
      isDismissible: false,
      isScrollControlled: true,
      enableDrag: false,
      builder: (context) => SingleChildScrollView(
        child: Container(
          padding:
              EdgeInsets.only(bottom: MediaQuery.of(context).viewInsets.bottom),
          child: SpeechToTextWidget(
              context: context,
              autocompleteTask: autocompleteTask,
              onChanged: (value) {
                result = value;
              }),
        ),
      ),
    );
    return result;
  }
}

class _SpeechToTextWidgetState extends State<SpeechToTextWidget> {
  speechTotext.SpeechToText _speech;
  bool isAvaiable = false;
  bool isListening = false;
  String textResult = '';
  bool noAcceptedFlag = true;
  bool noCancelFlag = true;
  bool noNavigatorPopCalled = true;

  @override
  void initState() {
    super.initState();
    _speech = speechTotext.SpeechToText();
    Future.delayed(Duration(milliseconds: 100), () async {
      if (await DevicePermissions.requestPermission(
          Permission.microphone, context, 'microphone'))
        _startListen();
      else
        _cancel();
    });
  }

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
              isListening ? 'Recording...' : 'Tab and start talking please!',
              textAlign: TextAlign.center,
              style: Theme.of(context).textTheme.bodyText1,
            ),
            SizedBox(height: 10.0),
            isListening
                ? IconButton(
                    icon: Icon(Icons.record_voice_over),
                    color: Theme.of(context).primaryColor,
                    iconSize: 50,
                    onPressed: () {},
                  )
                : IconButton(
                    icon: Icon(Icons.mic),
                    color: Theme.of(context).primaryColor,
                    iconSize: 50,
                    onPressed: () {
                      _startListen();
                    },
                  ),
            SizedBox(height: 10.0),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 40.0),
              child: Text(
                textResult,
                textAlign: TextAlign.center,
                style: TextStyle(color: Theme.of(context).primaryColor),
              ),
            ),
            SizedBox(height: 20.0),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                ElevatedButton(
                  child: Text('Accept'),
                  onPressed: () {
                    _acceptText();
                  },
                ),
                SizedBox(width: 10.0),
                ElevatedButton(
                  child: Text('Cancel'),
                  onPressed: () {
                    _cancel();
                  },
                ),
              ],
            )
          ],
        ),
      ),
    );
  }

  _acceptText() {
    if (noNavigatorPopCalled) {
      _speech.stop();
      widget.onChanged(textResult);
      Navigator.pop(context);
      noNavigatorPopCalled = false;
    }
  }

  _cancel() {
    if (noNavigatorPopCalled) {
      _speech.stop();
      widget.onChanged('');
      Navigator.pop(context);
      noNavigatorPopCalled = false;
    }
  }

  _startListen() async {
    if (!isListening) {
      bool available = await _speech.initialize(
        onError: (val) => print('onError: $val'),
      );
      if (available) {
        setState(() => isListening = true);
        _speech.listen(
            onResult: (val) => {
                  setState(() {
                    textResult = val.recognizedWords;
                    if (val.finalResult) {
                      isListening = false;
                      if (widget.autocompleteTask) {
                        _acceptText();
                      }
                    }
                  }),
                });
      }
    } else {
      setState(() => isListening = false);
      _speech.stop();
    }
  }
}
