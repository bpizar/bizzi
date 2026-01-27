export class TaskModel {
    public Id: number;
    public Subject: string;
    public From: Date;
    public To: Date;
    public ProjectName: string;
    public ProjectColor: string;
    public Hours: number;
    public Group: number;
}

export class StaffModel {
    public Id: number;
    public IdfPosition: number;
    public IdfUser: number;
    public state: string;
    public img: string;

    public availableForManyPrograms: number;
    public cellNumber: string;
    public emergencyPerson: string;
    public emergencyPersonInfo: string;
    public healthInsuranceNumber: string;
    public homeAddress: string;
    public homePhone: string;
    public socialInsuranceNumber: string;
    public spouceName: string;
    public tmpAccreditations: string;
    public workStartDate: string;
    public city: string;
    //public assignedPrograms :string;
}

export class PositionModel {
    public Id: number;
    public Name: string;
    public State: string;
}

export class ProjectPositionCustomEntityModel {
    public Id: number;
    public IdProject: number;
    public ProjectName: string;
    public IdPosition: number;
    public PositionName: string;
    public Group: string;
}

export class UserModel {
    public email: string;
    public firstName: string;
    public id: number;
    public lastName: string;
    public password: string;
    public state: string;
    public face: string;
    public geoTrackingEvery: number;
}

export class PayModel {
    public id: number;
    public IdfStaff: number;
    public HourlyRate: number;
    public PayRollId: string;
    public RegularHours: number;
    public RegularPay: number;
    public OvertimeHours: number;
    public OvertimePay: number;
    public AnnualLeaveHours: number;
    public AnnualLeavePay: number;
    public PeriodPay: string;
    public State: string;
}

export class StaffPeriodSettingModel {
    public id: number;
    public idfStaff: number;
    public idfPeriod: number;
    public workingHours: number;
}