export class ProjectForm {
    public id: number;
    public name: string;
    public description: string;
    public information: string;
    public template: string;
    public templateFile: string;
    public idfRecurrence: number;
    public idfRecurrenceDetail: number;
}

export class ProjectFormByProject {
    public id: number;
    public name: string;
    public description: string;
    public template: string;
    public idfProjectFormValue: number;
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

export class ProjectFormValue {
    public id: number;
    public idfProject: number;
    public idfProjectForm: number;
    public formDateTime: string;
}

export class ProjectFormFieldValue {
    public id: number;
    public idfProjectFormValue: number;
    public idfFormField: number;
    public value: string;
}

export class ProjectFormImageValue {
    public id: number;
    public idfProject: number;
    public idfProjectForm: number;
    public image: string;
    public formDateTime: string;
}

export class ProjectFormReminders {
    public id: number;
    public idfProjectForm: number;
    public idfReminderLevel: number;
    public idfPeriodType: number;
    public idfPeriodValue: number;
    public idfUsers: number[];
}

export class ProjectFormReminderUsers {
    public id: number;
    public idfProjectFormReminder: number;
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
