import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
import { EditClient } from './editclient/editclient.component';
import { Clients } from './clients.component';
import { EditDailyLog } from './editclient/editdailylog/editdailylog.component';
import { EditInjury } from './editclient/editinjury/editinjury.component';
import { PrintDailyLog } from './editclient/editdailylog/print/printdailylog.component';
import { ClientFormsComponent } from './client-forms/client-forms.component';
import { ClientFormComponent } from './client-forms/client-form.component';
import { ClientFormValueComponent } from './client-forms/client-form-value.component';
import { ClientFormImageValueComponent } from './client-forms/client-form-image-value.component';
import { ClientFormValueViewerComponent } from './client-forms/client-form-value-viewer.component';

const routes: Routes = [
    { path: '', component: Clients },
    { path: 'editclient/:id', component: EditClient },
    //{ path: 'editclient/:idclient/editdailylog/:id/:idperiod/:print', component: EditDailyLog },
    { path: 'editclient/:idclient/editdailylog/:id/:idperiod', component: EditDailyLog },
    { path: 'editclient/:idclient/editdailylog/print/:id/:idperiod', component: PrintDailyLog },
    { path: 'editclient/:idclient/editinjury/:id/:idperiod', component: EditInjury },
    //{ path: 'editclient/:idclient/editinjury/:id/:idperiod', component: EditInjury },
    { path: 'client-forms', component: ClientFormsComponent },
    { path: 'client-form/:id', component: ClientFormComponent },
    { path: 'client-form-value/:id', component: ClientFormValueComponent },
    { path: 'client-form-value/:idClientForm/:idClient', component: ClientFormValueComponent },
    { path: 'client-form-image-value/:idClientForm/:idClient', component: ClientFormImageValueComponent },
    { path: 'client-form-image-value/:id', component: ClientFormImageValueComponent },
    { path: 'client-form-value-viewer/:id', component: ClientFormValueViewerComponent },

];

export const routing: ModuleWithProviders<Route> = RouterModule.forChild(routes);



