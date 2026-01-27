class Clients {
  List<Client> items = [];

  Clients.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final client = new Client.fromJsonMap(item);
      items.add(client);
    }
  }
}

class Client {
  String fullName;
  String img;
  dynamic abm;
  int idfClient;
  String programInfo;
  int id;
  String firstName;
  String lastName;
  String birthDate;
  dynamic phoneNumber;
  String email;
  dynamic notes;
  int active;
  String state;
  dynamic idfImg;
  dynamic tmpMotherName;
  dynamic tmpMotherInfo;
  dynamic tmpFatherName;
  dynamic tmpFatherInfo;
  dynamic tmpAgencyWorker;
  dynamic tmpAgencyWorkerInfo;
  dynamic tmpAgency;
  dynamic tmpAgencyInfo;
  dynamic tmpPlacement;
  dynamic tmpSupervisor;
  dynamic tmpSpecialProgram;
  dynamic tmpSchool;
  dynamic tmpSchoolInfo;
  dynamic tmpTeacher;
  dynamic tmpTeacherInfo;
  dynamic tmpDoctorName;
  dynamic tmpDoctorInfo;
  dynamic tmpMedicationNotes;
  List<dynamic> clientsImages;
  List<dynamic> hClientsIncident;
  List<dynamic> hDailylogs;
  List<dynamic> hInjuries;
  List<dynamic> hMedicalReminders;
  List<dynamic> projectsClients;
  String safetyPlan;

  Client({
    this.fullName,
    this.img,
    this.abm,
    this.idfClient,
    this.programInfo,
    this.id,
    this.firstName,
    this.lastName,
    this.birthDate,
    this.phoneNumber,
    this.email,
    this.notes,
    this.active,
    this.state,
    this.idfImg,
    this.tmpMotherName,
    this.tmpMotherInfo,
    this.tmpFatherName,
    this.tmpFatherInfo,
    this.tmpAgencyWorker,
    this.tmpAgencyWorkerInfo,
    this.tmpAgency,
    this.tmpAgencyInfo,
    this.tmpPlacement,
    this.tmpSupervisor,
    this.tmpSpecialProgram,
    this.tmpSchool,
    this.tmpSchoolInfo,
    this.tmpTeacher,
    this.tmpTeacherInfo,
    this.tmpDoctorName,
    this.tmpDoctorInfo,
    this.tmpMedicationNotes,
    this.clientsImages,
    this.hClientsIncident,
    this.hDailylogs,
    this.hInjuries,
    this.hMedicalReminders,
    this.projectsClients,
    this.safetyPlan,
  });

  Client.fromJsonMap(Map<String, dynamic> json) {
    fullName = json['fullName'];
    img = json['img'];
    abm = json['abm'];
    idfClient = json['idfClient'];
    programInfo = json['programInfo'];
    id = json['id'];
    firstName = json['firstName'];
    lastName = json['lastName'];
    birthDate = json['birthDate'];
    phoneNumber = json['phoneNumber'];
    email = json['email'];
    notes = json['notes'];
    active = json['active'];
    state = json['state'];
    idfImg = json['idfImg'];
    tmpMotherName = json['tmpMotherName'];
    tmpMotherInfo = json['tmpMotherInfo'];
    tmpFatherName = json['tmpFatherName'];
    tmpFatherInfo = json['tmpFatherInfo'];
    tmpAgencyWorker = json['tmpAgencyWorker'];
    tmpAgencyWorkerInfo = json['tmpAgencyWorkerInfo'];
    tmpAgency = json['tmpAgency'];
    tmpAgencyInfo = json['tmpAgencyInfo'];
    tmpPlacement = json['tmpPlacement'];
    tmpSupervisor = json['tmpSupervisor'];
    tmpSpecialProgram = json['tmpSpecialProgram'];
    tmpSchool = json['tmpSchool'];
    tmpSchoolInfo = json['tmpSchoolInfo'];
    tmpTeacher = json['tmpTeacher'];
    tmpTeacherInfo = json['tmpTeacherInfo'];
    tmpDoctorName = json['tmpDoctorName'];
    tmpDoctorInfo = json['tmpDoctorInfo'];
    tmpMedicationNotes = json['tmpMedicationNotes'];
    clientsImages = json['clients_images'];
    hClientsIncident = json['h_clients_incident'];
    hDailylogs = json['h_dailylogs'];
    hInjuries = json['h_injuries'];
    projectsClients = json['h_medical_reminders'];
    hMedicalReminders = json['projects_clients'];
    safetyPlan = json['safetyPlan'];
  }
}
