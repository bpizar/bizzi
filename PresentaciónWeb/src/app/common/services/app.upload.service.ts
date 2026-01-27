import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConstantService } from './app.constant.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class UploadService {

    constructor(private http: HttpClient, public constantService: ConstantService) { };

    upload(fileToUpload: any, location: string, realfilename: string) {
        let input = new FormData();
        input.append("file64", fileToUpload);
        input.append("location", location);
        input.append("realfilename", realfilename);
        // return this.http.post("/api/common/upload",input)
        //     .map(this.extractData)
        //     .catch(this.handleError);
        return this.http.post("api/common/upload", input)
            //.pipe(map(this.extractData))
            .pipe(map(result => result));
        //.catch(this.handleError);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body || {};
    }

    private handleError(error: Response | any) {
        return Observable.throw(error);
    }
}