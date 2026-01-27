export class EventEmiterClientModel {
    public ClientList: ClientModel[]
}

export class ClientModel {
    public id: number;
    public fullName: string;
    public email: string;
    public birthDate: Date;
    public notes: string;
    public img: string;
    public phoneNumber: string;
    public abm: string;
    public idfClient: number
}
