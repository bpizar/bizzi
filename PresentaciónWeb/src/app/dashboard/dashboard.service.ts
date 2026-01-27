import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DashBoardService {
    constructor(public authHttp: HttpClient, public ConstantService: ConstantService) { }

    // gettasksfordashboard1
    GetTasksForDashboard1() {
        return this.authHttp.get("/api/dashboard/gettasksfordashboard1")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetDashboard2() {
        return this.authHttp.get("/api/dashboard/getdashboard2")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetDashboard3() {
        return this.authHttp.get("/api/dashboard/getdashboard3")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

}




// import { Injectable } from '@angular/core';
// import { Http } from '@angular/http';
// import { Response } from '@angular/http';
// import { Observable } from 'rxjs';

// //FOR DEMO PUSPOSE ONLY.

// @Injectable()
// export class DashBoardService {

//     constructor(private http: Http) { }

//     // private extractData(res: Response) {
//     //     let body = res.json();
//     //     return body || {};
//     // }

//     // private handleError(error: Response | any) {
//     //     let errMsg: string;
//     //     if (error instanceof Response) {
//     //         const body = error.json() || '';
//     //         const err = body.error || JSON.stringify(body);
//     //         errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
//     //     } else {
//     //         errMsg = error.message ? error.message : error.toString();
//     //     }
//     //     console.error(errMsg);
//     //     return Observable.throw(errMsg);
//     // }

// }


