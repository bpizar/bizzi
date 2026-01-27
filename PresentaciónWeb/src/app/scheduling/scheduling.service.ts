import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable, observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SchedulingService {

    constructor(//public authHttp: AuthHttp,
        public http: HttpClient,
        public ConstantService: ConstantService) { }

    DeleteSelectedSchedules(request: any) {
        return this.http.post("/api/scheduling/deleteselectedschedules", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetScheduling(period: number) {
        return this.http.get("/api/scheduling/getscheduling/" + period)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetSchedulingByProject(project: number, period: number) {
        return this.http.get("/api/scheduling/getschedulingbyproject/" + project + "/" + period)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetSchedulingByStaff(staff: number) {
        return this.http.get("/api/scheduling/getschedulingbystaff/" + staff)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetPeriods() {
        return this.http.get("/api/periods/getperiods")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SavePeriods(request: any) {
        return this.http.post("/api/periods/saveperiods", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveScheduling(request: any) {
        return this.http.post("/api/scheduling/savescheduling", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    UpdateScheduling(request: any) {
        return this.http.post("/api/scheduling/updatescheduling", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    DeleteScheduling(request: any) {
        return this.http.post("/api/scheduling/deletescheduling", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
}