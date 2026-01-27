import { Injectable } from '@angular/core';
//import { LocalStorageService } from 'ng2-webstorage';

@Injectable()
export class ConstantService {

    REPORT_FILE: string;

    TIMEZONE: string;

    // private storage: LocalStorageService
    constructor() {
        this.REPORT_FILE = "/media/files/styleReport.component9.css";
        this.TIMEZONE = "-04:00";
    }
}