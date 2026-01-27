import 'dart:convert';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_html/flutter_html.dart';
import 'package:image_picker/image_picker.dart';
import 'package:people_mobile/src/framework/device-permissions_lib.dart';
import 'package:people_mobile/src/framework/toast-message_lib.dart';
import 'package:people_mobile/src/models/project-form-image-value_model.dart';
import 'package:people_mobile/src/models/project-forms-by-project_model.dart';
import 'package:people_mobile/src/models/project_model.dart';
import 'package:people_mobile/src/providers/project-form_provider.dart';
import 'package:people_mobile/src/providers/url_provider.dart';
import 'package:people_mobile/src/widgets/date-picker_widget.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:url_launcher/url_launcher.dart';

ProjectFormByProject projectFormByProject;
Project project;
final urlProvider = new UrlProvider();
final projectFormProvider = new ProjectFormProvider();
final picker = ImagePicker();
File image;
String selectedDate;

class EditProjectFormImageValuePage extends StatefulWidget {
  @override
  _EditProjectFormImageValuePageState createState() =>
      _EditProjectFormImageValuePageState();
}

class _EditProjectFormImageValuePageState
    extends State<EditProjectFormImageValuePage> {
  bool showinformation = false;
  String informationText = "+ Information";

  @override
  Widget build(BuildContext context) {
    DevicePermissions.requestPermission(Permission.camera, context, 'camera');
    final Map args = ModalRoute.of(context).settings.arguments as Map;
    projectFormByProject = args['projectFormByProject'];
    project = args['project'];
    selectedDate = DateTime.now().toString();

    return WillPopScope(
      onWillPop: () async {
        image = null;
        return true;
      },
      child: Scaffold(
        appBar: AppBar(
          title: Text('Edit Program Form Image'),
          actions: <Widget>[
            IconButton(
              icon: Icon(Icons.photo_size_select_actual),
              onPressed: _selectImage,
            ),
            IconButton(
              icon: Icon(Icons.camera_alt),
              onPressed: _takePhoto,
            ),
          ],
        ),
        body: Center(
          child: Padding(
            padding: EdgeInsets.all(10),
            child: Column(
              children: [
                SizedBox(height: 20),
                Text(
                  'Project: ${project.projectName}',
                  style: TextStyle(
                      fontSize: 20, color: Theme.of(context).primaryColor),
                ),
                Text('Form Name: ${projectFormByProject.name}',
                    style: TextStyle(
                        fontSize: 16, color: Theme.of(context).primaryColor)),
                DatePicker(
                    date: DateTime.now(),
                    onChanged: (value) {
                      selectedDate = value;
                    }),
                SizedBox(
                  height: 15,
                ),
                if ((projectFormByProject.information ?? "") != "")
                  new GestureDetector(
                    onTap: () {
                      setState(() {
                        showinformation = !showinformation;
                        informationText =
                            showinformation ? "- Information" : "+ Information";
                      });
                    },
                    child: Container(
                      color: Theme.of(context).primaryColor,
                      width: double.maxFinite,
                      alignment: Alignment.topLeft,
                      child: new SizedBox(
                        child: Text(
                          informationText,
                          style: TextStyle(
                              backgroundColor: Theme.of(context).primaryColor,
                              color: Colors.white,
                              height: 2),
                        ),
                      ),
                    ),
                  ),
                if (showinformation)
                  Expanded(
                    child: Container(
                      color: Theme.of(context).dialogBackgroundColor,
                      child: SingleChildScrollView(
                        child: Column(children: [
                          SizedBox(
                            height: 15,
                          ),
                          Html(
                            data: projectFormByProject.information ?? "",
                            onLinkTap: (String url, RenderContext context,
                                Map<String, String> attributes, element) {
                              Uri uri = Uri.parse(url);
                              launchUrl(uri);
                            },
                          ),
                          SizedBox(
                            height: 15,
                          )
                        ]),
                      ),
                    ),
                  ),
                Expanded(
                  child: Card(child: _showImage()),
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    ElevatedButton(
                      onPressed: () {
                        //image = null;
                        _save();
                      },
                      child: Text('Save'),
                    ),
                    SizedBox(
                      width: 20,
                    ),
                    ElevatedButton(
                      onPressed: () {
                        image = null;
                        Navigator.pop(context);
                      },
                      child: Text('Cancel'),
                    ),
                  ],
                )
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _showImage() {
    var img;
    if (image != null) {
      img = FileImage(image);
    } else {
      img = AssetImage('assets/images/no-image.png');
    }
    return Image(
      image: img,
    );
  }

  _save() {
    if (image != null) {
      final bytes = File(image.path).readAsBytesSync();
      String stringImage = base64Encode(bytes);
      ProjectFormImageValue projectFormImageValue = new ProjectFormImageValue();
      projectFormImageValue.id = -1;
      projectFormImageValue.idfProject = project.id;
      projectFormImageValue.idfProjectForm = projectFormByProject.id;
      projectFormImageValue.image = stringImage;
      projectFormImageValue.formDateTime = selectedDate;
      projectFormImageValue.dateTime = selectedDate;
      projectFormProvider
          .saveProjectFormImageValue(projectFormImageValue)
          .then((result) {
        if (result) {
          {
            image = null;
            ToastMessage.success('Form Image saved successfully!');
          }
        } else
          ToastMessage.error('Error, image was not saved.');
        Navigator.pop(context);
      });
    } else
      ToastMessage.error('You didn`t choose any image');
  }

  _selectImage() async {
    _processImage(ImageSource.gallery);
  }

  _takePhoto() {
    _processImage(ImageSource.camera);
  }

  _processImage(ImageSource imageSource) async {
    final pickedFile = await picker.getImage(source: imageSource);
    setState(() {
      if (pickedFile != null) {
        image = File(pickedFile.path);
      } else {
        ToastMessage.error('You didn`t choose any image');
      }
    });
  }
}
