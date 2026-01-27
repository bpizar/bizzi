export class ClientForm {
    public id: number;
    public name: string;
    public description: string;
    public information: string;
    public template: string;
    public templateFile: string;
    public idfRecurrence: number;
    public idfRecurrenceDetail: number;
}

export class ClientFormByClient {
    public id: number;
    public name: string;
    public description: string;
    public template: string;
    public idfClientFormValue: number;
    public idfRecurrence: number;
    public idfRecurrenceDetail: number;
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

export class ClientFormValue {
    public id: number;
    public idfClient: number;
    public idfClientForm: number;
    public formDateTime: string;
}

export class ClientFormFieldValue {
    public id: number;
    public idfClientFormValue: number;
    public idfFormField: number;
    public value: string;
}

export class ClientFormImageValue {
    public id: number;
    public idfClient: number;
    public idfClientForm: number;
    public image: string;
    public formDateTime: string;
}

export class ClientFormReminders {
    public id: number;
    public idfClientForm: number;
    public idfReminderLevel: number;
    public idfPeriodType: number;
    public idfPeriodValue: number;
    public idfUsers: number[];
}

export class ClientFormReminderUsers {
    public id: number;
    public idfClientFormReminder: number;
    public idfUser: number;
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
