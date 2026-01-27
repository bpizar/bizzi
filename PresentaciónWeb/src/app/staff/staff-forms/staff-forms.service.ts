import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ConstantService } from '../../common/services/app.constant.service';

@Injectable({
  providedIn: 'root'
})
export class StaffFormsService {

  constructor(public http: HttpClient, public constantService: ConstantService) { }

  // GetAllStaffForms() {
  //     return this.http.get("/api/staffforms/getallstaffforms/")
  //     .pipe(map(result=>result), catchError(error=>Observable.throw(error)));   
  // }

  // GetStaffFormForEditById(id: number) {
  //     return this.http.get("/api/staffforms/getstaffformsforeditbyid/" + id)            
  //     .pipe(map(result=>result), catchError(error=>Observable.throw(error)));   
  // }

  // SaveStaffForm(request: any) {     
  //     return this.http.post("/api/staffforms/savestaffforms", request)
  //     .pipe(map(result=>result),catchError(error=>Observable.throw(error)));   
  // }

  // DeleteStaffForms(idStaffForm:number) {       
  //   return this.http.delete("/api/staffforms/deleteStaffForm"+idStaffForm)
  //   .pipe(map(result=>result),catchError(error=>Observable.throw(error)));
  // }
  // GetAllStaffFormsByStaff() {
  //   return this.http.get("/api/staffform/getallstaffFormsByStaff/")
  //   .pipe(map(result=>result), catchError(error=>Observable.throw(error)));   
  // }
}
