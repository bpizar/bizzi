import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
//import { EditClient } from './editclient/editclient.component';
import { GeoTracking } from './geotracking.component';

const routes: Routes = [
    { path: '', component: GeoTracking }
    //{ path: 'editclient/:id', component: EditClient }
];

export const routing: ModuleWithProviders<Route> = RouterModule.forChild(routes);