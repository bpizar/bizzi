import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app.routing.module';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SharedModuleBizzi } from './app.module.shared';
import { SharedModuleBizzi1 } from './app.module.shared.1';

import { jqxPasswordInputModule } from 'jqwidgets-ng/jqxpasswordinput'
import { jqxTabsModule } from 'jqwidgets-ng/jqxtabs';
import { jqxLoaderModule } from 'jqwidgets-ng/jqxloader';
import { jqxChartModule } from 'jqwidgets-ng/jqxchart';

import { AppComponent } from './app.component';
import { Login } from './login/login.component';
import { Dashboard } from './dashboard/dashboard.component';
import { MyAccount } from './settings/myaccount/myaccount.component';
import { UnAuthorized } from './common/components/unAuthorized/unauthorized.component';
import { JwtInterceptor } from './common/helpers/app.http.interceptor';

export function createTranslateLoader(http: HttpClient) // HttpClient era solo Http
{
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    Login,
    Dashboard,
    MyAccount,
    UnAuthorized
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    TranslateModule.forRoot(
      {
        loader: {
          provide: TranslateLoader,
          useFactory: (createTranslateLoader),
          deps: [HttpClient]
        },
        //isolate:true
      }
    ),
    SharedModuleBizzi,
    SharedModuleBizzi1,
    jqxPasswordInputModule,
    jqxTabsModule,
    jqxLoaderModule,
    jqxChartModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
