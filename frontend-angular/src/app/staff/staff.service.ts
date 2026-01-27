import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class StaffService {

    constructor(public http: HttpClient, public constantService: ConstantService) { }

    GetAllStaff() {
        return this.http.get("/api/staff/getallstaffs/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaff() {
        return this.http.get("/api/staff/getstaff/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffList() {
        return this.http.get("/api/staff/getstafflist/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffForEditById(id: number) {
        return this.http.get("/api/staff/getstaffforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetTasksById(id: number, idperiod: number) {
        return this.http.get("/api/tasks/gettasksbystaff/" + id + "/" + idperiod)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffForScheduling(idperiod: number) {

        alert("uncomment 001  staff.service line 32");

        return null;
        //return this.http.get("/api/staff/getstaffforscheduling/" + idperiod)
        //.pipe(map(result=>result),catchError(error=>Observable.throw(error)));   
    }

    GetStaffForPlanning(groupby: string) {
        return this.http.get("/api/staff/getstaffforplanning/" + groupby)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveStaff(request: any) {
        return this.http.post("/api/staff/savestaff", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SavePositions(request: any) {
        return this.http.post("/api/positions/savepositions", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    EnableDisableAccount(request: any) {
        return this.http.post("/api/staff/enabledisableaccount", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    ResetPassword(id: number) {
        return this.http.get("/api/staff/resetpassword/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    // GetPositionsRoles() {
    //     let myHeader = new Headers();
    //     myHeader.append('Content-Type', 'application/json');
    //     return this.authHttp.get("/api/positions/getpositionsroles/", { headers: myHeader })
    //         .map(this.extractData)
    //         .catch(this.handleError);
    // }

    GetPositions() {
        return this.http.get("/api/positions/getpositions/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllStaffForms() {
        return this.http.get("/api/staffform/getallstaffforms/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffFormForEditById(id: number) {
        return this.http.get("/api/staffform/getstaffformsforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffFormValuesForEditById(id: number) {
        return this.http.get("/api/staffformvalue/getstaffformValuesforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffFormImagenValuesForEditById(id: number) {
        return this.http.get("/api/staffformimagevalue/getstaffformImageValuesforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveStaffForm(request: any) {
        return this.http.post("/api/staffform/savestaffform", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    DeleteStaffForms(idStaffForm: number) {
        return this.http.delete("/api/staffform/deleteStaffForm/" + idStaffForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllStaffFormsByStaff(idStaff: number) {
        return this.http.get("/api/staffform/getallstaffFormsByStaff/" + idStaff)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllStaffFormsByStaffandStaffForm(idStaff: number, idStaffForm: number) {
        return this.http.get("/api/staffform/getallstaffFormsByStaffandStaffForm/" + idStaff + "/" + idStaffForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllFormFieldsByStaffForm(idStaffForm: number) {
        return this.http.get("/api/FormField/getallstaffFormFieldsbystaffform/" + idStaffForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveStaffFormValue(request: any) {
        return this.http.post("/api/StaffFormValue/saveStaffFormValue", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllFormFieldValuesByStaffFormAndStaff(idStaffForm: number, idStaff: number) {
        return this.http.get("/api/StaffFormFieldValue/getallstaffFormFieldValuesByStaffFormAndStaff/" + idStaffForm + "/" + idStaff)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllFormFieldValuesByStaffFormValue(idStaffFormValue: number) {
        return this.http.get("/api/StaffFormFieldValue/getallstaffFormFieldValuesByStaffFormValue/" + idStaffFormValue)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    RemoveStaffFormField(idStaffForm: number, idFormField: number) {
        return this.http.delete("/api/StaffFormField/removeStaffFormField/" + idStaffForm + "/" + idFormField)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    AddStaffFormField(request: any) {
        return this.http.post("/api/StaffFormField/addStaffFormField", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffFormImageValueByStaffFormAndStaff(idStaffForm: number, idStaff: number) {
        return this.http.get("/api/StaffFormImageValue/getStaffFormImageValueByStaffFormAndStaff/" + idStaffForm + "/" + idStaff)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffFormImageValueImageByStaffFormAndStaff(idStaffForm: number, idStaff: number) {

        return this.http.get("/api/StaffFormImageValue/getStaffFormImageValueImageByStaffFormAndStaff/" + idStaffForm + "/" + idStaff)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffFormImageValueImageById(idStaffFormValue: number) {
        return this.http.get("/api/StaffFormImageValue/getStaffFormImageValueImageById/" + idStaffFormValue)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStaffFormRemindersByStaffForm(idStaffForm: number) {
        return this.http.get("/api/StaffFormReminder/getallstaffFormRemindersByStaffForm/" + idStaffForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveStaffFormReminder(request: any) {
        return this.http.post("/api/StaffForm/saveStaffFormReminder", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveStaffFormImageValue(request: any) {
        return this.http.post("/api/StaffFormImageValue/saveStaffFormImageValue", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
}