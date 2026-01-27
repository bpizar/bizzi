import 'package:people_mobile/src/models/user_model.dart';

class Staffs {
  List<Staff> items = [];

  //Staff();

  Staffs.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final pelicula = new Staff.fromJsonMap(item);
      items.add(pelicula);
    }
  }
}

class Staff {
  String positionName;
  String fullName;
  String email;
  dynamic group;
  double hours;
  int idfPosition;
  int idfStaff;
  dynamic abm;
  String img;
  int geoTrackingEvery;
  String projectInfo;
  dynamic projectColor;
  int id;
  int idfUser;
  dynamic state;
  dynamic color;
  dynamic workStartDate;
  dynamic socialInsuranceNumber;
  dynamic healthInsuranceNumber;
  dynamic homeAddress;
  dynamic city;
  dynamic homePhone;
  dynamic cellNumber;
  dynamic spouceName;
  dynamic emergencyPerson;
  dynamic emergencyPersonInfo;
  dynamic availableForManyPrograms;
  dynamic tmpAccreditations;
  User idfUserNavigation;
  List<dynamic> projectOwners;
  List<dynamic> staffPeriodSettings;
  List<dynamic> staffProjectPosition;

  Staff({
    this.positionName,
    this.fullName,
    this.email,
    this.group,
    this.hours,
    this.idfPosition,
    this.idfStaff,
    this.abm,
    this.img,
    this.geoTrackingEvery,
    this.projectInfo,
    this.projectColor,
    this.id,
    this.idfUser,
    this.state,
    this.color,
    this.workStartDate,
    this.socialInsuranceNumber,
    this.healthInsuranceNumber,
    this.homeAddress,
    this.city,
    this.homePhone,
    this.cellNumber,
    this.spouceName,
    this.emergencyPerson,
    this.emergencyPersonInfo,
    this.availableForManyPrograms,
    this.tmpAccreditations,
    this.idfUserNavigation,
    this.projectOwners,
    this.staffPeriodSettings,
    this.staffProjectPosition,
  });

  Staff.fromJsonMap(Map<String, dynamic> json) {
    positionName = json['positionName'];
    fullName = json['fullName'];
    email = json['email'];
    group = json['group'];
    hours = json['hours'].toDouble();
    idfPosition = json['idfPosition'];
    idfStaff = json['idfStaff'];
    abm = json['abm'];
    img = json['img'];
    geoTrackingEvery = json['geoTrackingEvery'];
    projectInfo = json['projectInfo'];
    projectColor = json['projectColor'];
    id = json['id'];
    idfUser = json['idfUser'];
    state = json['state'];
    color = json['color'];
    workStartDate = json['workStartDate'];
    socialInsuranceNumber = json['socialInsuranceNumber'];
    healthInsuranceNumber = json['healthInsuranceNumber'];
    homeAddress = json['homeAddress'];
    city = json['city'];
    homePhone = json['homePhone'];
    cellNumber = json['cellNumber'];
    spouceName = json['spouceName'];
    emergencyPerson = json['emergencyPerson'];
    emergencyPersonInfo = json['emergencyPersonInfo'];
    availableForManyPrograms = json['availableForManyPrograms'];
    tmpAccreditations = json['tmpAccreditations'];
    idfUserNavigation = User.fromJsonMap(
        json['idfUserNavigation'] ?? new Map<String, dynamic>());
    projectOwners = json['project_owners'];
    staffPeriodSettings = json['staff_period_settings'];
    staffProjectPosition = json['staff_project_position'];
  }
}
