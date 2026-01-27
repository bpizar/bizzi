import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
import { ReportPettyCashByProject } from './reportpettycashbyproject/reportpettycashbyproject.component';
import { ReportScheduleByProject } from './reportschedulebyproject/reportschedulebyproject.component';
import { ReportScheduleByStaff } from './reportschedulebystaff/reportschedulebystaff.component';
import { ReportTaskByProject } from './reporttaskbyproject/reporttaskbyproject.component';
import { ReportFinance } from './reportsfinance/reportsfinance.component';
import { ReportHistoryTask } from './reporthistorytask/reporthistorytask.component';

const routes: Routes = [
    { path: '', component: ReportPettyCashByProject },
    { path: 'reportpettycashbyproject', component: ReportPettyCashByProject },
    { path: 'reportschedulebyproject', component: ReportScheduleByProject },
    { path: 'reportschedulebystaff', component: ReportScheduleByStaff },
    { path: 'reporttaskbyproject', component: ReportTaskByProject },
    { path: 'reportsfinance', component: ReportFinance },
    { path: 'reporthistorytask', component: ReportHistoryTask },
];

export const routing: ModuleWithProviders<Route> = RouterModule.forChild(routes);