import { Injectable } from '@angular/core';
import { ConstantService } from './app.constant.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ChatService {

    constructor(public http: HttpClient, public constantService: ConstantService) { }

    // var completeURL = string.Format("{0}chat/getlastupdatechat/{1}/{2}/{3}/{4}", Common.ApiURL, Common.userID, Common.LastChatRoomVersion, Common.LastChatMessagesVersion, Common.LastChatParticipantsVersion);
    GetLastUpdateChat() {
        return this.http.get("/api/chat/getlastupdatechat/0/0/0/0")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    // GetAllStaff() {
    //     return this.http.get("/api/staff/getallstaffs/")
    //     .pipe(map(result=>result), catchError(error=>Observable.throw(error)));   
    // }

    // GetStaff() {
    //     return this.http.get("/api/staff/getstaff/")
    //     .pipe(map(result=>result), catchError(error=>Observable.throw(error)));   
    // }

    // GetStaffForEditById(id: number) {
    //     return this.http.get("/api/staff/getstaffforeditbyid/" + id)            
    //     .pipe(map(result=>result), catchError(error=>Observable.throw(error)));   
    // }

    // GetTasksById(id: number, idperiod: number) {
    //     return this.http.get("/api/tasks/gettasksbystaff/" + id + "/" + idperiod)
    //     .pipe(map(result=>result), catchError(error=>Observable.throw(error)));   
    // }

    // GetStaffForScheduling(idperiod: number) {

    //     alert("uncomment 001  staff.service line 32");

    //     return null;
    //     //return this.http.get("/api/staff/getstaffforscheduling/" + idperiod)
    //     //.pipe(map(result=>result),catchError(error=>Observable.throw(error)));   
    // }

    // GetStaffForPlanning(groupby: string)
    // {
    //     return this.http.get("/api/staff/getstaffforplanning/" + groupby)
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error)));   
    // }

    // SaveStaff(request: any) {     
    //     return this.http.post("/api/staff/savestaff", request)
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error)));   
    // }

    // SavePositions(request: any) {
    //     return this.http.post("/api/positions/savepositions", request)
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error)));   
    // }

    // EnableDisableAccount(request: any) {
    //      return this.http.post("/api/staff/enabledisableaccount", request)
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error)));   
    // }

    // ResetPassword(id: number) {
    //     return this.http.get("/api/staff/resetpassword/" + id)
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error)));   
    // }

    // // GetPositionsRoles() {
    // //     let myHeader = new Headers();
    // //     myHeader.append('Content-Type', 'application/json');
    // //     return this.authHttp.get("/api/positions/getpositionsroles/", { headers: myHeader })
    // //         .map(this.extractData)
    // //         .catch(this.handleError);
    // // }

    // GetPositions () {
    //      return this.http.get("/api/positions/getpositions/")
    //      .pipe(map(result=>result),catchError(error=>Observable.throw(error)));   
    //  }
}