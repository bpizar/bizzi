export class EditDuplicatesModel {
    public id: number;
    public duplicateValue: string;
    public repeatEvery: number;
    public weekly_Su: boolean;
    public weekly_Mo: boolean;
    public weekly_Tu: boolean;
    public weekly_We: boolean;
    public weekly_Th: boolean;
    public weekly_Fr: boolean;
    public weekly_Sa: boolean;
    public monthly_Day: number;
    public yearly_Month: number;
    public yearly_MonthDay: number;
    public endAfter: number;
    public endOn: Date;
}