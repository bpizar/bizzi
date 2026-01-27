import { NgModule } from '@angular/core';
import { routing } from './staff.routing';
import { BlockUIModule } from 'primeng/blockui';
import { CommonModule } from '@angular/common';
// import { Editor } from 'primeng/primeng';
// import { ToolbarModule } from 'primeng/primeng';
// import { ButtonModule } from 'primeng/primeng';
import { SharedModuleBizzi } from '../app.module.shared';
import { SharedModuleBizzi1 } from '../app.module.shared.1';
//import { ConfirmDialogModule, ConfirmationService } from 'primeng/primeng';
import { TooltipModule } from 'primeng/tooltip';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { MyFilterPipePeriods } from './periods.pipe';
//import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';
//import { EditProject } from './staff/editstaff.component';
import { Staff } from './staff.component';
import { EditStaff } from './editstaff/editstaff.component'
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
// import { FormsModule } from '@angular/forms';
import { StaffFormsListByStaffComponent } from './staff-forms/staff-forms-list-by-staff.component'
import { StaffFormsComponent } from './staff-forms/staff-forms.component';
import { StaffFormComponent } from './staff-forms/staff-form.component';
import { StaffFormValueComponent } from './staff-forms/staff-form-value.component';
import { StaffFormImageValueComponent } from './staff-forms/staff-form-image-value.component';
import { StaffFormValueViewerComponent } from './staff-forms/staff-form-value-viewer.component';
import { EditorModule } from '@tinymce/tinymce-angular';

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
    imports: [
        routing,
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
        EditorModule,
        CommonModule],
    declarations: [
        Staff,
        EditStaff,
        StaffFormsListByStaffComponent,
        StaffFormsComponent,
        StaffFormComponent,
        StaffFormValueComponent,
        StaffFormImageValueComponent,
        StaffFormValueViewerComponent
    ]
})

export class staffModule { }


