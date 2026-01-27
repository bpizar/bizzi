class TaskByStaffList {
  List<TaskByStaff> items = [];

  TaskByStaffList.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;
    for (var item in jsonList) {
      final period = new TaskByStaff.fromJsonMap(item);
      items.add(period);
    }
  }
}

class TaskByStaff {
  int assignedHoursOnPeriod;
  String assignedPrograms;
  List<dynamic> assignedTasks;
  int availableHoursOnPeriod;

  TaskByStaff({
    this.assignedHoursOnPeriod,
    this.assignedPrograms,
    this.assignedTasks,
    this.availableHoursOnPeriod,
  });

  TaskByStaff.fromJsonMap(Map<String, dynamic> json) {
    assignedHoursOnPeriod = json['assignedHoursOnPeriod'];
    assignedPrograms = json['assignedPrograms'];
    assignedTasks = json['assignedTasks'];
    availableHoursOnPeriod = json['availableHoursOnPeriod'];
  }

  Map<String, dynamic> toJson() => {
        'assignedHoursOnPeriod': assignedHoursOnPeriod,
        'assignedPrograms': assignedPrograms,
        'assignedTasks': assignedTasks,
        'availableHoursOnPeriod': availableHoursOnPeriod,
      };
}
