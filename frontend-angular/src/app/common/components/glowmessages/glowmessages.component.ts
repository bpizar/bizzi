import { Component, OnInit, AfterViewInit, NgModule, Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

//import { Injectable } from '@angular/core';
import { AuthHelper } from '../../helpers/app.auth.helper';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';

@Component({
    selector: 'glowmessages',
    template: '<p-toast></p-toast>',
    providers: [AuthHelper, MessageService]
})

@Injectable()
export class GlowMessages implements OnInit, AfterViewInit {

    // msgs: Message[] = [];
    // msgs2: Message[] = [];
    sticky: boolean = false;

    constructor(private router: Router, private authHelper: AuthHelper, private translate: TranslateService, private messageService: MessageService) {
        this.translate.setDefaultLang('en');
    }

    ngOnInit() {
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
        })
    };

    ShowGlow = (severity: string, summary: string, detail: string, sticky: boolean = false): void => {
        this.translate.get(summary).subscribe((resSummary: string) => {
            this.sticky = false;
            this.translate.get(detail).subscribe((resDetail: string) => {
                this.messageService.add({ severity: severity, summary: resSummary, detail: resDetail });
            });
        });
    }

    ShowFlat = (severity: string, summary: string, detail: string[], sticky: boolean = false): void => {
        this.sticky = sticky;
        for (var i = 0; i < detail.length; i++) {
            if (detail[i].length > 0) {
                this.messageService.add({ severity: severity, summary: summary, detail: detail[i] });
            }
        }
    }

    ShowTechnicalProblems = (): void => {
        this.messageService.add({ severity: "error", summary: "Technical problems", detail: "It is possible that the problem is corrected by reloading the page again" });
    }

    ShowGlowByError = (data: any): void => {

        if (data.status != undefined && data.status == 403) {
            this.messageService.add({ severity: "warn", summary: "Not authorized", detail: "Not authorized" });
        }
        else {
            if (data.messages != undefined && data.result == false) {
                this.messageService.add({ severity: "error", summary: "Server Error", detail: data.messages.length > 0 ? data.messages[0].description : "Unknow Error" });
            }
            else if (!this.authHelper.loggedIn()) {
                this.messageService.add({ severity: "warn", summary: "Session Expired", detail: "Your session expired" });

                setTimeout(() => {
                    this.router.navigate(['login']);
                }, 2000);

            }
            else if (data.type == 3) {
                this.messageService.add({ severity: "error", summary: "Connection Error", detail: "Problems connecting to the remote server" });
            }
            else {
                // this.msgs.push({ severity: "error", summary: "Unknown error", detail: "An unexpected error occurred" });
            }
        }
    };
}