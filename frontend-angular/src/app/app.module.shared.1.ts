import { NgModule } from '@angular/core';
//import { CommonModule } from '@angular/common';
//import { FormsModule } from '@angular/forms';
//import { GlowMessages } from './common/components/glowmessages/glowmessages.component';
//import { GrowlModule } from 'primeng/primeng';
//import { MessagesModule } from 'primeng/primeng';
//import {JwtInterceptor} from './common/helpers/app.http.interceptor';
import { Menu } from './menu/menu.component';
import { RouterModule } from '@angular/router';
//import { TranslateModule} from '@ngx-translate/core';
//import { SelectClient } from './clients/selectclients/selectclient.component';
import { GlowMessages } from './common/components/glowmessages/glowmessages.component';
import { ToastModule } from 'primeng/toast';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { SidebarModule } from 'primeng/sidebar';
//import {MessageService} from 'primeng/components/common/messageservice';
//import {MessagesModule} from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { VerifyTfaComponent } from "./common/components/verify-tfa/verify-tfa.component";

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

//import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
//import { TooltipModule } from 'primeng/primeng';
import { jqxTooltipModule } from 'jqwidgets-ng/jqxtooltip';
//import {  jqxResponseComponent} from  'jqwidgets-ng/jqwidgets/jqxresponse';
import { jqxListBoxModule } from 'jqwidgets-ng/jqxlistbox';
// TODO: check reference
import { SelectClient } from './clients/selectclients/selectclient.component';
//agrega
import { jqxLoaderModule } from 'jqwidgets-ng/jqxloader';
import { jqxWindowModule } from 'jqwidgets-ng/jqxwindow';
import { jqxInputModule } from 'jqwidgets-ng/jqxinput';
//nuev
import { FormsModule } from '@angular/forms';
import { PipesModule } from './common/pipes/pipes.module';

@NgModule({
    imports: [
        CommonModule,
        jqxTooltipModule,
        FormsModule,
        //TooltipModule,
        //Menu  
        //BrowserModule,
        RouterModule,
        //TranslateModule  ,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            },
            isolate: true
        }),
        ToastModule,
        SidebarModule,
        MessageModule,
        PipesModule,
        jqxTooltipModule,
        jqxListBoxModule,
        jqxLoaderModule,
        jqxWindowModule,
        jqxInputModule
        // MessageService       
    ],
    declarations: [
        Menu,
        GlowMessages,
        VerifyTfaComponent,
        // agrega
        SelectClient,
        //se agrega
        // agrega
        // agrega
        //SidebarModule
        //SaveActionDisplay
        //SelectClient   
    ],
    providers: [
        //JwtInterceptor
    ],
    exports: [
        Menu,
        GlowMessages,
        //se agrega
        // agrega
        SelectClient,
        jqxTooltipModule,
        jqxListBoxModule,
        jqxLoaderModule,
        jqxWindowModule,
        jqxInputModule,
        VerifyTfaComponent
        // agega
        // agrega
        //SidebarModule
        //SaveActionDisplay
        //SelectClient
    ]
})
export class SharedModuleBizzi1 { }