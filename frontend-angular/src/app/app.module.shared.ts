import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import {BrowserModule} from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
//import { GlowMessages } from './common/components/glowmessages/glowmessages.component';
//import { GrowlModule } from 'primeng/primeng';
//import { MessagesModule } from 'primeng/primeng';
// import { SplitButtonModule } from 'primeng/primeng';
// import { jqxTreeGridComponent } from 'jqwidgets-ng/jqxtreegrid';
// import { xx } from 'jqwidgets-ng/jqwidgets/jqxcore';
// import { jqxMaskedInputComponent } from 'jqwidgets-ng/jqwidgets/jqxmaskedinput';
import { jqxDropDownListModule } from 'jqwidgets-ng/jqxdropdownlist';
//import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { jqxComboBoxModule } from 'jqwidgets-ng/jqxcombobox';
import { jqxTabsModule } from 'jqwidgets-ng/jqxtabs';
import { jqxButtonModule } from 'jqwidgets-ng/jqxbuttons';
import { jqxBarGaugeModule } from 'jqwidgets-ng/jqxbargauge';
import { jqxGaugeModule } from 'jqwidgets-ng/jqxgauge';
//import { jqxDrawComponent } from 'jqwidgets-ng/jqwidgets/jqxdraw';
// se quita
import { jqxListBoxModule } from 'jqwidgets-ng/jqxlistbox';
import { ImageCropperModule } from 'ngx-image-cropper';
import { jqxCheckBoxModule } from 'jqwidgets-ng/jqxcheckbox';
import { jqxEditorModule } from 'jqwidgets-ng/jqxeditor';
import { jqxColorPickerModule } from 'jqwidgets-ng/jqxcolorpicker';
import { jqxDropDownButtonModule } from 'jqwidgets-ng/jqxdropdownbutton';
import { jqxScrollViewModule } from 'jqwidgets-ng/jqxscrollview';
import { jqxDrawModule } from 'jqwidgets-ng/jqxdraw';
import { jqxGridModule } from 'jqwidgets-ng/jqxgrid';
// quita
import { jqxWindowModule } from 'jqwidgets-ng/jqxwindow';
import { jqxScrollBarModule } from 'jqwidgets-ng/jqxscrollbar';
import { jqxButtonGroupModule } from 'jqwidgets-ng/jqxbuttongroup';
import { jqxMenuModule } from 'jqwidgets-ng/jqxmenu';
import { jqxCalendarModule } from 'jqwidgets-ng/jqxcalendar';
import { jqxSchedulerModule } from 'jqwidgets-ng/jqxscheduler';
import { jqxDateTimeInputModule } from 'jqwidgets-ng/jqxdatetimeinput';
//import {  jqxTooltipComponent} from  'jqwidgets-ng/jqwidgets/jqxtooltip';
import { jqxPasswordInputModule } from 'jqwidgets-ng/jqxpasswordinput';
import { jqxNumberInputModule } from 'jqwidgets-ng/jqxnumberinput';
import { jqxRadioButtonModule } from 'jqwidgets-ng/jqxradiobutton';
import { jqxDragDropModule } from 'jqwidgets-ng/jqxdragdrop';
//se quita
import { jqxLoaderModule } from 'jqwidgets-ng/jqxloader';
import { jqxMaskedInputModule } from 'jqwidgets-ng/jqxmaskedinput';
import { jqxNotificationModule } from 'jqwidgets-ng/jqxnotification';
import { jqxTextAreaModule } from 'jqwidgets-ng/jqxtextarea';
import { jqxTreeGridModule } from 'jqwidgets-ng/jqxtreegrid';
import { SaveActionDisplay } from './common/components/saveActionDisplay/saveactiondisplay.component';
//import {  jqxNumberInputComponent} from  'jqwidgets-ng/jqwidgets/jqxmaskedinput';
//import { Menu } from './menu/menu.component';

//import { AuthHelper } from './common/helpers/app.auth.helper';


//import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';

//import { Menu } from './menu/menu.component';

//import { BrowserModule } from '@angular/platform-browser';

//import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

//import { TranslateModule} from '@ngx-translate/core';

// se quita
//import { SelectClient } from './clients/selectclients/selectclient.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
//import { CommonModule} from '@angular/common';

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

//import { BrowserModule } from '@angular/platform-browser';
@NgModule({
    imports: [
        CommonModule,
        //BrowserModule,
        //BrowserModule,
        FormsModule,
        // GrowlModule,
        // MessagesModule,
        ImageCropperModule,
        jqxTreeGridModule,
        jqxDropDownListModule,
        jqxComboBoxModule,
        jqxTabsModule,
        jqxButtonModule,
        jqxBarGaugeModule,
        jqxGaugeModule,
        jqxCheckBoxModule,
        jqxColorPickerModule,
        jqxDropDownButtonModule,
        jqxScrollViewModule,
        jqxDrawModule,
        jqxGridModule,
        jqxScrollBarModule,
        jqxButtonGroupModule,
        jqxMenuModule,
        jqxCalendarModule,
        jqxSchedulerModule,
        jqxDateTimeInputModule,
        jqxPasswordInputModule,
        jqxNumberInputModule,
        jqxRadioButtonModule,
        jqxDragDropModule,
        jqxMaskedInputModule,
        jqxNotificationModule,
        jqxTextAreaModule,
        jqxEditorModule,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            },
            isolate: true
        })
        // TranslateService
        // RouterModule
        // Menu
        // SplitButtonModule                      
    ],
    declarations: [
        // Menu,
        // Menu,
        // AuthHelper,
        //GlowMessages,
        // jqxTreeGridComponent,
        // jqxDropDownListComponent,
        //jqxMaskedInputComponent,
        //quitar
        //jqxInputComponent,        


        // quita
        // se quita

        //jqxTooltipComponent,
        // se quita

        //se quita
        //SelectClient,
        SaveActionDisplay,
        // TranslateService
    ],
    providers: [

    ],
    exports: [
        //AuthHelper,
        //Menu,
        ImageCropperModule,
        jqxTreeGridModule,
        jqxDropDownListModule,
        jqxComboBoxModule,
        jqxTabsModule,
        jqxButtonModule,
        jqxBarGaugeModule,
        jqxGaugeModule,
        jqxCheckBoxModule,
        jqxColorPickerModule,
        jqxDropDownButtonModule,
        jqxScrollViewModule,
        jqxDrawModule,
        jqxGridModule,
        jqxScrollBarModule,
        jqxButtonGroupModule,
        jqxMenuModule,
        jqxCalendarModule,
        jqxSchedulerModule,
        jqxDateTimeInputModule,
        jqxPasswordInputModule,
        jqxNumberInputModule,
        jqxRadioButtonModule,
        jqxDragDropModule,
        jqxMaskedInputModule,
        jqxNotificationModule,
        jqxTextAreaModule,
        jqxEditorModule,
        //GlowMessages,  
        //quitar
        //jqxInputComponent,
        // quita
        //se quita
        //jqxTooltipComponent,
        // sequite
        // seq uita
        //SelectClient,
        SaveActionDisplay,
        // TranslateService
    ]
})
export class SharedModuleBizzi { }