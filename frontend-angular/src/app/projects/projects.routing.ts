import { ModuleWithProviders } from '@angular/core';
import { Projects } from './projects.component';
import { Routes, RouterModule, Route } from '@angular/router';
import { EditProject } from './editproject/editproject_.component';
import { ProjectFormsComponent } from './project-forms/project-forms.component';
import { ProjectFormComponent } from './project-forms/project-form.component';
import { ProjectFormValueComponent } from './project-forms/project-form-value.component';
import { ProjectFormImageValueComponent } from './project-forms/project-form-image-value.component';
import { ProjectFormValueViewerComponent } from './project-forms/project-form-value-viewer.component';

const routes: Routes = [
    { path: '', component: Projects },
    { path: 'editproject/:id', component: EditProject },
    { path: 'project-forms', component: ProjectFormsComponent },
    { path: 'project-form/:id', component: ProjectFormComponent },
    { path: 'project-form-value/:id', component: ProjectFormValueComponent },
    { path: 'project-form-value/:idProjectForm/:idProject', component: ProjectFormValueComponent },
    { path: 'project-form-image-value/:idProjectForm/:idProject', component: ProjectFormImageValueComponent },
    { path: 'project-form-image-value/:id', component: ProjectFormImageValueComponent },
    { path: 'project-form-value-viewer/:id', component: ProjectFormValueViewerComponent },
];

export const routing: ModuleWithProviders<Route> = RouterModule.forChild(routes);