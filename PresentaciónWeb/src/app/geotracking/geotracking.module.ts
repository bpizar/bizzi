import { NgModule } from '@angular/core';
import { routing } from './geotracking.routing';
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
// import { EditClient } from './editclient/editclient.component';
// import { Clients } from './clients.component';
import { GeoTracking } from './geotracking.component';
import { GMapModule } from 'primeng/gmap';
import { GeoTrackingPipe } from './GeoTracking.pipe';
import { GeoTrackingPipe2 } from './GeoTracking.pipe2';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { PipesModule } from '../common/pipes/pipes.module';

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
    imports: [
        routing,
        FormsModule,
        BlockUIModule,
        SharedModuleBizzi,
        SharedModuleBizzi1,
        TooltipModule,
        GMapModule,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            },
            isolate: true
        }),
        // GlowMessages,
        PipesModule,
        CommonModule],
    declarations: [
        GeoTracking,
        GeoTrackingPipe,
        GeoTrackingPipe2
        //EditClient,
        //Clients
    ]
})

export class geotrackingModule { }


