import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';

// import { EditClient } from './editclient/editclient.component';

import { Incidents } from './incidents.component';
import { EditIncident } from '../incidents/editincident/editincident.component';

const routes: Routes = [
    { path: '', component: Incidents },
    { path: 'editincident/:idincident/:idperiod', component: EditIncident },
    //{ path: 'editclient/:id', component: EditClient }
];

export const routing: ModuleWithProviders<Route> = RouterModule.forChild(routes);