import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
//import { EditClient } from './editclient/editclient.component';
import { Finance } from './finance.component';

const routes: Routes = [
    { path: '', component: Finance },
    //{ path: 'editclient/:id', component: EditClient }
];

export const routing: ModuleWithProviders<Route> = RouterModule.forChild(routes);