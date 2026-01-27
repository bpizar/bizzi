import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ReportsService {

    constructor(public authHttp: HttpClient, public ConstantService: ConstantService) { }

    GetReport1(projectsIds: any[], from: Date, to: Date) {
        var body = {
            ProjectIds: projectsIds,
            From: from,
            To: to
        };

        return this.authHttp.post("/api/reports/getreport1", body)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetReportPrettyCashPerProject(projectId: any) {
        var body = {
            ProjectId: projectId
        };

        return this.authHttp.post("/api/projects/getpettycash/" + projectId, body)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }


    GetReportProjects() {
        return this.authHttp.get("/api/reports/getreportprojects")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetReportTaskHistory(idproject: number, idperiod: number) {
        return this.authHttp.get("/api/reports/gettaskhistoryreport/" + idproject + "/" + idperiod)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    //[HttpGet("gettaskhistoryreport/{idproject}/{idperiod}")]
}
