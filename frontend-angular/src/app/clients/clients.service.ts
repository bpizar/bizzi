import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { jqxNumberInputComponent } from 'jqwidgets-ng/jqwidgets/jqxnumberinput';

@Injectable()
export class ClientsService {

    constructor(public authHttp: HttpClient, public constantService: ConstantService) { }

    GetAllClients() {
        return this.authHttp.get("/api/Clients/getallclients/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllClientsList() {
        return this.authHttp.get("/api/Clients/getallclientslist/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetClient(id: number) {
        return this.authHttp.get("/api/Clients/getclient/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveClient(client: any) {
        return this.authHttp.post("/api/Clients/saveclient", client)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetClientDataByPeriod(idperiod: number, idclient: number) {
        return this.authHttp.get("/api/Clients/getclientdatabyperiodid/" + idperiod + "/" + idclient)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    // GetIncidentById(idincident:number) {
    //     return this.authHttp.get("/api/incidents/getincidentbyid/" + idincident)
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error))); 
    // }

    GetInjuryById(idinjury: number, idperiod: number, idclient: number, timeDifference: number) {
        return this.authHttp.get("/api/injury/getinjurybyid/" + idinjury + "/" + idperiod + "/" + idclient + "/" + timeDifference)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetDailyLogById(iddailylog: number, idperiod: number, idclient: number, timeDifference: number) {
        //   [HttpGet("getdailylogbyid/{iddailylog}/{idperiod}/{idclient}")]
        return this.authHttp.get("/api/dailylog/getdailylogbyid/" + iddailylog + "/" + idperiod + "/" + idclient + "/" + timeDifference)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveInjury(request: any) {
        return this.authHttp.post("/api/injury/saveinjury", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveDailyLog(request: any) {
        return this.authHttp.post("/api/dailylog/savedailylog", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllClientForms() {
        return this.authHttp.get("/api/clientform/getallclientforms/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetClientFormForEditById(id: number) {
        return this.authHttp.get("/api/clientform/getclientformsforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetClientFormValuesForEditById(id: number) {
        return this.authHttp.get("/api/clientformvalue/getclientformValuesforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveClientForm(request: any) {
        return this.authHttp.post("/api/clientform/saveclientform", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllFormFieldValuesByClientFormValue(idClientFormValue: number) {
        return this.authHttp.get("/api/ClientFormFieldValue/getallclientFormFieldValuesByClientFormValue/" + idClientFormValue)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    DeleteClientForms(idClientForm: number) {
        return this.authHttp.delete("/api/clientform/deleteClientForm/" + idClientForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllClientFormsByClient(idClient: number) {
        return this.authHttp.get("/api/clientform/getallclientFormsByClient/" + idClient)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllClientFormsByClientandClientForm(idClient: number, idClientForm: number) {
        return this.authHttp.get("/api/clientform/getallclientFormsByClientandClientForm/" + idClient + "/" + idClientForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllFormFieldsByClientForm(idClientForm: number) {
        return this.authHttp.get("/api/FormField/getallclientFormFieldsbyclientform/" + idClientForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetClientFormImagenValuesForEditById(id: number) {
        return this.authHttp.get("/api/clientformimagevalue/getclientformImageValuesforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetClientFormImageValueImageById(idClientFormValue: number) {
        return this.authHttp.get("/api/ClientFormImageValue/getClientFormImageValueImageById/" + idClientFormValue)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveClientFormImageValue(request: any) {
        return this.authHttp.post("/api/ClientFormImageValue/saveClientFormImageValue", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveClientFormValue(request: any) {
        return this.authHttp.post("/api/ClientFormValue/saveClientFormValue", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllFormFieldValuesByClientFormAndClient(idClientForm: number, idClient: number) {
        return this.authHttp.get("/api/ClientFormFieldValue/getallclientFormFieldValuesByClientFormAndClient/" + idClientForm + "/" + idClient)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
    RemoveClientFormField(idClientForm: number, idFormField: number) {
        return this.authHttp.delete("/api/ClientFormField/removeClientFormField/" + idClientForm + "/" + idFormField)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
    AddClientFormField(request: any) {
        return this.authHttp.post("/api/ClientFormField/addClientFormField", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
    GetClientFormImageValueByClientFormAndClient(idClientForm: number, idClient: number) {
        return this.authHttp.get("/api/ClientFormImageValue/getClientFormImageValueByClientFormAndClient/" + idClientForm + "/" + idClient)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
    GetClientFormImageValueImageByClientFormAndClient(idClientForm: number, idClient: number) {

        return this.authHttp.get("/api/ClientFormImageValue/getClientFormImageValueImageByClientFormAndClient/" + idClientForm + "/" + idClient)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetClientFormRemindersByClientForm(idClientForm: number) {
        return this.authHttp.get("/api/ClientFormReminder/getallClientFormRemindersByClientForm/" + idClientForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveClientFormReminder(request: any) {
        return this.authHttp.post("/api/ClientForm/saveClientFormReminder", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
}
