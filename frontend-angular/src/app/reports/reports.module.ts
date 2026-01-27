
import { NgModule } from '@angular/core';
//import { Projects } from './projects.component';
import { routing } from './reports.routing';
import { BlockUIModule } from 'primeng/blockui';
import { CommonModule } from '@angular/common';
// import { Editor } from 'primeng/primeng';
// import { ToolbarModule } from 'primeng/primeng';
// import { ButtonModule } from 'primeng/primeng';
import { SharedModuleBizzi } from '../app.module.shared';
import { SharedModuleBizzi1 } from '../app.module.shared.1';
//import { ConfirmDialogModule, ConfirmationService } from 'primeng/primeng';
import { TooltipModule } from 'primeng/tooltip';
import { FormsModule } from '@angular/forms';
import { ReportPettyCashByProject } from './reportpettycashbyproject/reportpettycashbyproject.component';
import { ReportScheduleByProject } from './reportschedulebyproject/reportschedulebyproject.component';
import { ReportScheduleByStaff } from './reportschedulebystaff/reportschedulebystaff.component';
import { ReportTaskByProject } from './reporttaskbyproject/reporttaskbyproject.component';
import { ReportMenu } from './reportmenu/reportmenu.component';
import { ReportFinance } from './reportsfinance/reportsfinance.component';
import { ReportHistoryTask } from './reporthistorytask/reporthistorytask.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
    imports: [routing,
        FormsModule,
        BlockUIModule,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            },
            isolate: true
        }),
        SharedModuleBizzi,
        SharedModuleBizzi1,
        TooltipModule,
        CommonModule],
    declarations: [
        ReportPettyCashByProject,
        ReportScheduleByProject,
        ReportScheduleByStaff,
        ReportTaskByProject,
        ReportMenu,
        ReportFinance,
        ReportHistoryTask
    ]
})

export class reportsModule { }