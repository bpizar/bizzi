import { NgModule } from '@angular/core';
import { routing } from './finance.routing';
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
import { Finance } from './finance.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { PipesModule } from '../common/pipes/pipes.module';

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
        PipesModule,
        CommonModule],
    declarations: [
        Finance
    ]
})

export class financeModule { }


