import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class GeoTrackingService {

    constructor(public authHttp: HttpClient, public constantService: ConstantService) { }

    // GetAllClients() {
    //     return this.authHttp.get("/api/Clients/getallclients/")
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error))); 
    // }

    GeoTracking(getAuto: boolean, idperiod: number, datex: Date) {

        let body = {
            IdPeriod: idperiod,
            Datex: datex,
            GetAuto: getAuto
        }
        // console.clear();

        return this.authHttp.post("/api/geotracking/getgeotracking", body)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
    // SaveClient(client: any) {
    //     return this.authHttp.post("/api/Clients/saveclient", client)
    //     .pipe(map(result=>result),catchError(error=>Observable.throw(error)));
    // }
}
