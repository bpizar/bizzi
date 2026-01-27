import { ModuleWithProviders } from '@angular/core';
import { Staff } from './staff.component';
import { Routes, RouterModule, Route } from '@angular/router';
import { EditStaff } from './editstaff/editstaff.component';
import { StaffFormsComponent } from './staff-forms/staff-forms.component';
import { StaffFormComponent } from './staff-forms/staff-form.component';
import { StaffFormValueComponent } from './staff-forms/staff-form-value.component';
import { StaffFormImageValueComponent } from './staff-forms/staff-form-image-value.component';
import { StaffFormValueViewerComponent } from './staff-forms/staff-form-value-viewer.component';

const routes: Routes = [
    { path: '', component: Staff },
    { path: 'editstaff/:id', component: EditStaff },
    { path: 'staff-forms', component: StaffFormsComponent },
    { path: 'staff-form/:id', component: StaffFormComponent },
    { path: 'staff-form-value/:id', component: StaffFormValueComponent },
    { path: 'staff-form-value/:idStaffForm/:idStaff', component: StaffFormValueComponent },
    { path: 'staff-form-image-value/:idStaffForm/:idStaff', component: StaffFormImageValueComponent },
    { path: 'staff-form-image-value/:id', component: StaffFormImageValueComponent },
    { path: 'staff-form-value-viewer/:id', component: StaffFormValueViewerComponent },
];

export const routing: ModuleWithProviders<Route> = RouterModule.forChild(routes);