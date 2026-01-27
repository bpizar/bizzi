export class StaffForm {
    public id: number;
    public name: string;
    public description: string;
    public information: string;
    public template: string;
    public templateFile: string;
    public idfRecurrence: number;
    public idfRecurrenceDetail: number;
}

export class StaffFormByStaff {
    public id: number;
    public name: string;
    public description: string;
    public template: string;
    public IdfRecurrence: number;
    public idfRecurrenceDetail: number;
    public idfStaffFormValue: number;
    public formDateTime: string;
    public quantity: number;
}

export class FormField {
    public id: number;
    public name: string;
    public placeholder: string;
    public datatype: string;
    public constraints: string;
    public description: string;
    public isChecked: boolean;
    public isEnabled: boolean;
    public position: number;
}

export class StaffFormValue {
    public id: number;
    public idfStaff: number;
    public idfStaffForm: number;
    public formDateTime: string;
}

export class StaffFormFieldValue {
    public id: number;
    public idfStaffFormValue: number;
    public idfFormField: number;
    public value: string;
}

export class StaffFormImageValue {
    public id: number;
    public idfStaff: number;
    public idfStaffForm: number;
    public image: string;
    public formDateTime: string;
}

export class StaffFormReminders {
    public id: number;
    public idfStaffForm: number;
    public idfReminderLevel: number;
    public idfPeriodType: number;
    public idfPeriodValue: number;
    public idfUsers: number[];
}

export class StaffFormReminderUsers {
    public id: number;
    public idfStaffFormReminder: number;
    public idfUser: number;
}

export class Staff {
    public id: number;
    public name: string;
}
// export class positionsrolesmodel {
//     public id: number;
//     public idfPosition: number;
//     public idfRol: number;
//     public abm: string;
// }

// export class positionsrolesmodel {
//     public id: number;
//     public idfPosition: number;
//     public idfRol: number;
//     public abm: string;
// }
