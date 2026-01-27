import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class IncidentsService {

    constructor(public authHttp: HttpClient, public ConstantService: ConstantService) { }

    GetIncidentsListByPeriod(period: number) {
        return this.authHttp.get("/api/incidents/getincidentslistbyperiod/" + period)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));

    }

    SaveIncident(request: any) {
        return this.authHttp.post("/api/incidents/saveincident", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetIncidentById(idincident: number, idperiod: number) {
        return this.authHttp.get("/api/incidents/getincidentbyid/" + idincident + "/" + idperiod)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    // ///{idperiod}/{idproject}
    // GetTimeTrackingReviewByProjectAndPeriod(period:number,project:number) {
    //     return this.authHttp.get("/api/finance/gettimetrackingreviewbyprojectandperiod/" + period + "/" + project)
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error)));

    // }

    // //
    // SaveTimeTrackingReview(body: any) {
    //     return this.authHttp.post("/api/finance/savetimetrackingreview", body)
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error)));

    // }

}