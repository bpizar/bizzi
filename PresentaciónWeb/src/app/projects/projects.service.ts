import { Injectable } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class ProjectsService {

    constructor(public authHttp: HttpClient, public ConstantService: ConstantService) { }

    //gettasksfordashboard1
    GetTasksForDashboard1() {
        return this.authHttp.get("/api/tasks/gettasksfordashboard1")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProjects() {
        return this.authHttp.get("/api/projects/getprojects")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProjectsList() {
        return this.authHttp.get("/api/projects/getprojectslist")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetTasksByProject(idProject: number, idPeriod: number) {
        return this.authHttp.get("/api/tasks/gettasksbyproject/" + idProject + "/" + idPeriod)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetOverdueTasks() {
        return this.authHttp.get("/api/tasks/getoverduetasks/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetStatuses() {
        let myHeader = new Headers();
        myHeader.append('Content-Type', 'application/json');

        return this.authHttp.get("/api/tasks/getstatuses")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProject(id: number) {
        return this.authHttp.get("/api/projects/getproject/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetPettycashCategories() {
        return this.authHttp.get("/api/projects/getpettycashcategories")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProject2(id: number, idperiod: number) {
        return this.authHttp.get("/api/projects/getproject2/" + id + "/" + idperiod)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetPettyCash(id: number, idperiod: number, idcategory: number) {
        return this.authHttp.get("/api/projects/getpettycash/" + id + "/" + idperiod + "/" + idcategory)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SavePettyCash(body: any) {
        return this.authHttp.post("/api/projects/savepettycash", body)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveProject(project: any) {
        return this.authHttp.post("/api/projects/saveproject", project)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    DeleteProject(project: any) {
        return this.authHttp.post("/api/projects/deleteproject", project)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    CloneTask(request: any) {
        return this.authHttp.post("/api/tasks/movecopytask", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
    MoveTask(task: any) {
        return this.authHttp.post("/api/tasks/movetask", task)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetReminderForPanel() {
        return this.authHttp.get("/api/tasks/getremindersforpanel")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    // getstaffandpositionsforcopywindow
    GetStaffAndPositionsForCopyWindow(idProject: number, idPeriod: number) {
        return this.authHttp.get("/api/projects/getstaffandpositionsforcopywindow/" + idProject + "/" + idPeriod)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllProjectForms() {
        return this.authHttp.get("/api/projectform/getallprojectforms/")
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProjectFormForEditById(id: number) {
        return this.authHttp.get("/api/projectform/getprojectformsforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProjectFormValuesForEditById(id: number) {
        return this.authHttp.get("/api/projectformvalue/getprojectformValuesforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveProjectForm(request: any) {
        return this.authHttp.post("/api/projectform/saveprojectform", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllFormFieldValuesByProjectFormValue(idProjectFormValue: number) {
        return this.authHttp.get("/api/ProjectFormFieldValue/getallprojectFormFieldValuesByProjectFormValue/" + idProjectFormValue)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    DeleteProjectForms(idProjectForm: number) {
        return this.authHttp.delete("/api/projectform/deleteProjectForm/" + idProjectForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllProjectFormsByProject(idProject: number) {
        return this.authHttp.get("/api/projectform/getallprojectFormsByProject/" + idProject)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllProjectFormsByProjectandProjectForm(idProject: number, idProjectForm: number) {
        return this.authHttp.get("/api/projectform/getallprojectFormsByProjectandProjectForm/" + idProject + "/" + idProjectForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllFormFieldsByProjectForm(idProjectForm: number) {
        return this.authHttp.get("/api/FormField/getallprojectFormFieldsbyprojectform/" + idProjectForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProjectFormImagenValuesForEditById(id: number) {
        return this.authHttp.get("/api/projectformimagevalue/getprojectformImageValuesforeditbyid/" + id)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProjectFormImageValueImageById(idProjectFormValue: number) {
        return this.authHttp.get("/api/ProjectFormImageValue/getProjectFormImageValueImageById/" + idProjectFormValue)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveProjectFormImageValue(request: any) {
        return this.authHttp.post("/api/ProjectFormImageValue/saveProjectFormImageValue", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveProjectFormValue(request: any) {
        return this.authHttp.post("/api/ProjectFormValue/saveProjectFormValue", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetAllFormFieldValuesByProjectFormAndProject(idProjectForm: number, idProject: number) {
        return this.authHttp.get("/api/ProjectFormFieldValue/getallprojectFormFieldValuesByProjectFormAndProject/" + idProjectForm + "/" + idProject)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    RemoveProjectFormField(idProjectForm: number, idFormField: number) {
        return this.authHttp.delete("/api/ProjectFormField/removeProjectFormField/" + idProjectForm + "/" + idFormField)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    AddProjectFormField(request: any) {
        return this.authHttp.post("/api/ProjectFormField/addProjectFormField", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProjectFormImageValueByProjectFormAndProject(idProjectForm: number, idProject: number) {
        return this.authHttp.get("/api/ProjectFormImageValue/getProjectFormImageValueByProjectFormAndProject/" + idProjectForm + "/" + idProject)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProjectFormImageValueImageByProjectFormAndProject(idProjectForm: number, idProject: number) {
        return this.authHttp.get("/api/ProjectFormImageValue/getProjectFormImageValueImageByProjectFormAndProject/" + idProjectForm + "/" + idProject)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    GetProjectFormRemindersByProjectForm(idProjectForm: number) {
        return this.authHttp.get("/api/ProjectFormReminder/getallProjectFormRemindersByProjectForm/" + idProjectForm)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }

    SaveProjectFormReminder(request: any) {
        return this.authHttp.post("/api/ProjectForm/saveProjectFormReminder", request)
            .pipe(map(result => result), catchError(error => Observable.throw(error)));
    }
}
