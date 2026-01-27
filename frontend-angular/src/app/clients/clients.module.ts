import { NgModule } from '@angular/core';
import { routing } from './clients.routing';
import { BlockUIModule } from 'primeng/blockui';
import { CommonModule } from '@angular/common';
// import { Editor } from 'primeng/primeng';
// import { ToolbarModule } from 'primeng/primeng';
// import { ButtonModule } from 'primeng/primeng';
import { SharedModuleBizzi } from '../app.module.shared';
import { SharedModuleBizzi1 } from '../app.module.shared.1';
// import { ConfirmDialogModule, ConfirmationService } from 'primeng/primeng';
import { TooltipModule } from 'primeng/tooltip';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// import { MyFilterPipePeriods } from './periods.pipe';
// import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';
// import { EditClient } from './staff/editstaff.component';
import { EditClient } from './editclient/editclient.component';
import { EditDailyLog } from './editclient/editdailylog/editdailylog.component';
import { PrintDailyLog } from './editclient/editdailylog/print/printdailylog.component';
// import { EditIncident } from './editclient/editincident/editincident.component';
// import { EditIncident } from '../incidents/editincident/editincident.component';
import { EditInjury } from './editclient/editinjury/editinjury.component';
import { Clients } from './clients.component';
// import { AngularSignaturePadModule } from 'angular-signature-pad';
import { SignaturePadModule } from 'angular2-signaturepad';
// import {  jqxTextAreaComponent} from  'jqwidgets-ng/jqxtextarea';
// import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
// import { SelectClient } from './selectclients/selectclient.component';
//import { TranslateModule} from '@ngx-translate/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ClientFormsListByClientComponent } from './client-forms/client-forms-list-by-client.component';
import { ClientFormsComponent } from './client-forms/client-forms.component';
import { ClientFormComponent } from './client-forms/client-form.component';
import { ClientFormValueComponent } from './client-forms/client-form-value.component';
import { ClientFormImageValueComponent } from './client-forms/client-form-image-value.component'
import { ClientFormValueViewerComponent } from './client-forms/client-form-value-viewer.component';
import { EditorModule } from '@tinymce/tinymce-angular';
import { PipesModule } from '../common/pipes/pipes.module';

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
        FormsModule,
        ReactiveFormsModule,
        BlockUIModule,
        SharedModuleBizzi,
        SharedModuleBizzi1,
        TooltipModule,
        CommonModule,
        SignaturePadModule,
        PipesModule,
        EditorModule,
        // SelectClient
    ],
    declarations: [
        EditClient,
        EditDailyLog,
        PrintDailyLog,
        EditInjury,
        // EditIncident,
        Clients,
        // SelectClient
        // jqxLoaderComponent
        // jqxTextAreaComponent
        ClientFormsListByClientComponent,
        ClientFormsComponent,
        ClientFormComponent,
        ClientFormValueComponent,
        ClientFormImageValueComponent,
        ClientFormValueViewerComponent
    ]
})

export class clientsModule { }


