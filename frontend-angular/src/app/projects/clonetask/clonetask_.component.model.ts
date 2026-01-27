export class EventEmiterTaskModel {
    public Id: number;
    public IdfStaff: number;
    public IdfStatus: number;
    public Status: string;
    public TaskName: string;
    public Deadline: Date;
    public Hours: number;
    public IdUser: number;
    public IdfPeriod: number;
    public IdfProject: number;
    public IdfAssignableRol: number;
    public UserFullName: string;
    public Abm: string;
    public type: string;
    public settingReminderTime: any;
    public Description: string;
}

export class tasksRemindersModel {
    public id: number;
    public idfPeriod: number;
    public idfSettingReminderTime: number;
    public idfTask: number;
    public state: string;

}