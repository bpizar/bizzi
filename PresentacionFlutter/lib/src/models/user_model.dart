class Users {
  List<User> items = [];

  //Staff();

  Users.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final user = new User.fromJsonMap(item);
      items.add(user);
    }
  }
}

class User {
  int id;
  String email;
  String password;
  String state;
  String registrationDate;
  String lastName;
  String firstName;
  String face;
  int idfImg;
  dynamic idOneSignal;
  dynamic idOneSignalWeb;
  int geoTrackingEvery;
  String faceStamp;
  List<dynamic> chatIdentityUsersTimestamp;
  List<dynamic> chatMessages;
  List<dynamic> chatRoomParticipants;
  List<dynamic> identityImages;
  List<dynamic> identityUsersRol;
  dynamic staff;
  List<dynamic> tasksStateHistory;
  List<dynamic> timeTrackingAuto;

  User({
    this.id,
    this.email,
    this.password,
    this.state,
    this.registrationDate,
    this.lastName,
    this.firstName,
    this.face,
    this.idfImg,
    this.idOneSignal,
    this.idOneSignalWeb,
    this.geoTrackingEvery,
    this.faceStamp,
    this.chatIdentityUsersTimestamp,
    this.chatMessages,
    this.chatRoomParticipants,
    this.identityImages,
    this.identityUsersRol,
    this.staff,
    this.tasksStateHistory,
    this.timeTrackingAuto,
  });

  User.fromJsonMap(Map<String, dynamic> json) {
    id = json['id'];
    email = json['email'];
    password = json['password'];
    state = json['state'];
    registrationDate = json['registrationDate'];
    lastName = json['lastName'];
    firstName = json['firstName'];
    face = json['face'];
    idfImg = json['idfImg'];
    idOneSignal = json['idOneSignal'];
    idOneSignalWeb = json['idOneSignalWeb'];
    geoTrackingEvery = json['geoTrackingEvery'];
    faceStamp = json['faceStamp'];
    chatIdentityUsersTimestamp = json['chatIdentityUsersTimestamp'];
    chatMessages = json['chatMessages'];
    chatRoomParticipants = json['chatRoomParticipants'];
    identityImages = json['identityImages'];
    identityUsersRol = json['identityUsersRol'];
    staff = json['staff'];
    tasksStateHistory = json['tasksStateHistory'];
    timeTrackingAuto = json['timeTrackingAuto'];
  }
}
