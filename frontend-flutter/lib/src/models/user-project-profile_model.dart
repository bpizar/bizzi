class UsersProjectProfile {
  List<UserProjectProfile> items = [];

  UsersProjectProfile.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final userProjectProfile = new UserProjectProfile.fromJsonMap(item);
      items.add(userProjectProfile);
    }
  }
}

class UserProjectProfile {
  String projectName;
  int projectId;
  String name;
  int positionId;
  int idStaffProjectPosition;

  UserProjectProfile({
    this.projectName,
    this.projectId,
    this.name,
    this.positionId,
    this.idStaffProjectPosition,
  });

  UserProjectProfile.fromJsonMap(Map<String, dynamic> json) {
    projectName = json['projectName'];
    projectId = json['projectId'];
    name = json['name'];
    positionId = json['positionId'];
    idStaffProjectPosition = json['idStaffProjectPosition'];
  }

  Map<String, dynamic> toJson() => {
        'projectName': projectName,
        'projectId': projectId,
        'name': name,
        'positionId': positionId,
        'idStaffProjectPosition': idStaffProjectPosition,
      };
}
