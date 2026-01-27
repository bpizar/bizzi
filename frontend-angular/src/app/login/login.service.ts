import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
// import { HttpClient } from '@angular/common/http';

@Injectable()
export class LoginService {

    constructor(public http: HttpClient,
        public constantService: ConstantService) { }

    Login(username: string, password: string, onesignalid: string) {
        var body = { username, password, onesignalid };
        return this.http.post("/api/auth/login", body)
            .pipe(map(result => result), catchError(error => throwError(error)));
    }

    SaveWebOneSignalId(idonesignal: string) {
        return this.http.get("/api/identity/savewebonesignalid/" + idonesignal)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));

    }
}


