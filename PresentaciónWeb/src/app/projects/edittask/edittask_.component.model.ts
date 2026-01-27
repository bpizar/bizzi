export class EventEmiterTaskModel {
    public Id: number;
    public IdfTaskParent: number;
    public IdfStatus: number;
    public Status: string;
    public TaskName: string;
    public Deadline: Date;
    public Hours: number;
    public IdUser: number;
    public IdfPeriod: number;
    public Abm: string;
    public type: string;
    public settingReminderTime: any;
    public IdfProject: number;
    public Description: string;
    public IdfStaff: number;
    public IdfAssignableRol: number;
    public UserFullName: string;
    public AssignedToPosition: string;
    public Img: string;
    //public Notes:string;
}

export class tasksRemindersModel {
    public id: number;
    public idfPeriod: number;
    public idfSettingReminderTime: number;
    public idfTask: number;
    public state: string;

}
