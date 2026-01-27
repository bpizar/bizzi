import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
import { Scheduling } from './scheduling.component';

const routes: Routes = [
    { path: '', component: Scheduling }
];

export const routing: ModuleWithProviders<Route> = RouterModule.forChild(routes);