import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SettingsService {

    constructor(public authHttp: HttpClient, public ConstantService: ConstantService) { }

    GetMyAccount() {
        return this.authHttp.get("/api/identity/getmyaccount")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    ChangeMyPassword(request: any) {
        return this.authHttp.post("/api/identity/changemypassword", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    generateTFASecret() {
        return this.authHttp.get("/api/auth/generateTFASecret/", { responseType: "blob" });
    }

    verifyTFAToken(secret: string) {
        return this.authHttp.get("/api/auth/verifyTFAToken/" + secret)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
}