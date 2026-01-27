import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(
    public http: HttpClient
  ) { }
  GetClientFormTemplate(Filename: string) {
    return this.http.get("/api/file/clientFormTemplates/" + Filename, { responseType: 'blob' });
  }
  GetStaffFormTemplate(Filename: string) {
    return this.http.get("/api/file/staffFormTemplates/" + Filename, { responseType: 'blob' });
  }
  GetProjectFormTemplate(Filename: string) {
    return this.http.get("/api/file/projectFormTemplates/" + Filename, { responseType: 'blob' });
  }
}
