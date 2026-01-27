import { NgModule } from '@angular/core';
import { routing } from './incidents.routing';
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
//import { MyFilterPipePeriods } from './periods.pipe';
//import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';
//import { EditProject } from './staff/editstaff.component';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
//import { SelectClient } from '../clients/selectclients/selectclient.component';
import { Incidents } from './incidents.component';
import { EditIncident } from '../incidents/editincident/editincident.component';
//import {  jqxTextAreaComponent} from  'jqwidgets-ng/jqxtextarea';
// import { TranslatePipe} from '@ngx-translate/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { PipesModule } from '../common/pipes/pipes.module';
//import { TranslateHttpLoader } from '@ngx-translate/http-loader';
export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
    imports: [routing,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            },
            isolate: true
        }),
        // TranslateModule,
        FormsModule,
        BlockUIModule,
        SharedModuleBizzi,
        SharedModuleBizzi1,
        TooltipModule,
        PipesModule,
        CommonModule],
    declarations: [
        Incidents,
        EditIncident,
        //SelectClient,
        // jqxTextAreaComponent
    ]
})

export class incidentsModule { }