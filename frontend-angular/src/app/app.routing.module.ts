import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Dashboard } from './dashboard/dashboard.component';
import { Login } from './login/login.component';
import { MyAccount } from './settings/myaccount/myaccount.component';
import { UnAuthorized } from './common/components/unAuthorized/unauthorized.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: 'unauthorized',
        component: UnAuthorized
    },
    {
        path: 'login',
        component: Login
    },
    {
        path: 'dashboard',
        component: Dashboard
    },
    {
        path: 'myaccount',
        component: MyAccount
    },
    {
        path: 'scheduling',
        loadChildren: () => import('src/app/scheduling/scheduling.module').then(m => m.schedulingModule)
    },
    {
        path: 'geotracking',
        loadChildren: () => import('src/app/geotracking/geotracking.module').then(m => m.geotrackingModule)
    },
    {
        path: 'projects',
        loadChildren: () => import('src/app/projects/projects.module').then(m => m.projectsModule)
    },
    {
        path: 'staff',
        loadChildren: () => import('src/app/staff/staff.module').then(m => m.staffModule)
    },
    {
        path: 'clients',
        loadChildren: () => import('src/app/clients/clients.module').then(m => m.clientsModule)
    },
    {
        path: 'finance',
        loadChildren: () => import('src/app/finance/finance.module').then(m => m.financeModule)
    },
    {
        path: 'reports',
        loadChildren: () => import('src/app/reports/reports.module').then(m => m.reportsModule)
    },
    {
        path: 'incidents',
        loadChildren: () => import('src/app/incidents/incidents.module').then(m => m.incidentsModule)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
