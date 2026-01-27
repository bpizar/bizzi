import { NgModule } from '@angular/core';
import { Projects } from './projects.component';
import { routing } from './projects.routing';
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
// Users/napilex/TFS/JAYGOR/PEOPLE_V2/FRONTEND/BROWSER/PeopleAngular/src/app/projects/editproject/editproject.component.ts
import { EditProject } from './editproject/editproject_.component';
import { CloneTask } from './clonetask/clonetask_.component';
import { EditTask } from './edittask/edittask_.component';
import { PettyCash } from './pettycash/pettycash_.component';
// import { SelectClient } from '../clients/selectclients/selectclient.component';
import { SelectStaff } from '../staff/selectstaff/selectstaff.component';
//import {  jqxEditorComponent} from  'jqwidgets-ng/jqxeditor';
import { NotesTask } from './notes/notes.component';
// import { FormsModule } from '@angular/forms';
// import { EditorModule } from '../../modules/editor.module';
// import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
// import { TranslateHttpLoader } from '@ngx-translate/http-loader';
// import { HttpClientModule, HttpClient } from '@angular/common/http';
// import { SelectClient } from '../clients/selectclients/selectclient.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ProjectFormsListByProjectComponent } from './project-forms/project-forms-list-by-project.component';
import { ProjectFormsComponent } from './project-forms/project-forms.component';
import { ProjectFormComponent } from './project-forms/project-form.component';
import { ProjectFormValueComponent } from './project-forms/project-form-value.component';
import { ProjectFormImageValueComponent } from './project-forms/project-form-image-value.component'
import { ProjectFormValueViewerComponent } from './project-forms/project-form-value-viewer.component';
import { EditorModule } from '@tinymce/tinymce-angular';
import { jqxGridModule } from 'jqwidgets-ng/jqxgrid';

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

// export function createTranslateLoader(http:HttpClient) // HttpClient era solo Http
// {
//   return new TranslateHttpLoader(http,'./assets/i18n/','.json');
// }

@NgModule({
    imports: [routing,
        FormsModule,
        ReactiveFormsModule,
        BlockUIModule,
        SharedModuleBizzi,
        SharedModuleBizzi1,
        TooltipModule,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            },
            isolate: true
        }),
        // SelectClient,
        // TranslateModule.forRoot(
        //     {
        //        loader:{
        //          provide:TranslateLoader,
        //          useFactory:(createTranslateLoader),
        //          deps:[HttpClient]
        //        }
        //     }
        //   ),
        EditorModule,
        CommonModule,
        jqxGridModule
    ],
    declarations: [Projects,
        EditProject,
        CloneTask,
        EditTask,
        NotesTask,
        //SelectClient,
        SelectStaff,
        PettyCash,
        ProjectFormsListByProjectComponent,
        ProjectFormsComponent,
        ProjectFormComponent,
        ProjectFormValueComponent,
        ProjectFormImageValueComponent,
        ProjectFormValueViewerComponent
        //jqxEditorComponent
    ] //,
    //providers:[JwtInterceptor]
})

export class projectsModule { }


