export class TaskModel {
    public id: number;
    public idfTaskParent: number;
    public subject: string;
    public idfStatus: number;
    public status: string;
    public description: string;
    public deadline: Date;
    public idfAssignedTo: number;
    public RecurrencePattern: string;
    public RecurrenceException: string;
    public AllDay: boolean;
    public IdfCreatedBy: number;
    public CreationDate: Date;
    public Lat: string;
    public Lon: string;
    public Address: string;
    public IdDuplicate: number;
    public idUser: number;
    public hours: number;
    public assignedToFullName: string;
    public abm: string;
    public idfPeriod: number;
    public idfProject: number;
    public idfAssignableRol: number;
    public type: string;
    public assignedToPosition: string;
    public img: string;
    public notes: string;
}

export class SubtaskModel {
    public id: number;
    public subject: string;
    public idfStatus: number;
    public status: string;
    public description: string;
    public idfAssignedTo: number;
    public IdfCreatedBy: number;
    public CreationDate: Date;
    public hours: number;
    public assignedToFullName: string;
    //public abm: string;
}

export class ProjectModel {
    public id: number;
    public projectName: string;
    public beginDate: Date = new Date();
    public endDate: Date;
    public color: string;
    public creationDate: Date;
    public description: string;
    public totalHours: number;
    public state: string;
    public abm: string = "-";
    public address: string = "";
    public city: string = "";
    public phone1: string = "";
    public phone2: string = "";
}

export class OwnerModel {
    public id: number;
    public idfProject: number;
    public idfOwner: number;
    public state: string;
    public fullName: string;
    public idfStaff: number;
}

export class settingReminderTimeModel {
    public id: number;
    public minutesBefore: number;
    public state: string;
}