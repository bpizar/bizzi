import { Component, ViewChild, OnInit, AfterViewInit, Input, OnDestroy } from '@angular/core';
import { AuthHelper } from '../common/helpers/app.auth.helper';
import { ProjectsGuard } from '../common/services/projects.guard.service';
import { StaffGuard } from '../common/services/staff.guard.service';
import { FinanceGuard } from '../common/services/finance.guard.service';
import { ClientGuard } from '../common/services/client.guard.service'
import { GeoTrackingGuard } from '../common/services/geotracking.guard.service'
import { ReportsGuard } from '../common/services/reports.guard.service'
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
// import { ModuleWithProviders } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { SidebarModule } from 'primeng/sidebar';
import { Message } from 'primeng/api';
import { MessageService } from 'primeng/api';
import { FormsModule } from '@angular/forms';
import { NgModule, Output, Injectable } from '@angular/core';
import { ProjectsService } from '../projects/projects.service';
import { LoginService } from '../login/login.service';
import { CommonHelper } from '../common/helpers/app.common.helper';
import { ChatService } from '../common/services/chat.service';
import { ConstantService } from '../common/services/app.constant.service';
//import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';

@Component({
    selector: 'menu',
    templateUrl: '../menu/menu.component.template.html',
    styleUrls: ['../menu/menu.component.css'],
    providers: [CommonHelper, ConstantService, LoginService, ProjectsService, ChatService, ProjectsGuard, StaffGuard, FinanceGuard, ClientGuard, AuthHelper, GeoTrackingGuard, ReportsGuard]
})

export class Menu implements OnInit, OnDestroy, AfterViewInit {
    @Input('loaderReference') myLoader: jqxLoaderComponent;
    //@ViewChild('txtSend') txtSend: jqxInputComponent;
    imagePipe = new ImagePipe();
    hiddenProjects: boolean = false;
    hiddenStaff: boolean = false;
    hiddenFinance: boolean = false;
    hiddenClients: boolean = false;
    hiddenGeoTracking: boolean = false;
    hiddenReports: boolean = false;
    currentUserForMenu: string = "";
    rooms: any[] = [];
    roomfrom: number = 0;
    roomto: number = 4;
    currentChat: number = 0;
    displayChatPanel: boolean = false;
    displayGlobalParticipants: boolean = false;
    sendText: string = ".";
    sourceGlobalParticipants: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'name', type: 'string' },
                { name: 'imgUser', type: 'string' },
                { name: 'check', type: 'boolean' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterGlobalParticipants: any = new jqx.dataAdapter(this.sourceGlobalParticipants);

    // clickMenu=():void=>
    // {

    //     setTimeout(() => {
    //         if(this.myLoader)
    //         {
    //             this.myLoader.open();
    //         }  
    //     });

    // }

    isSelected = (sel: string): boolean => {
        // return   sel === this.router.url;
        return this.router.url.includes(sel);
    }

    disabledToolTip: boolean = true;

    OnWindowResize = (event: any): void => {
        this.disabledToolTip = event.target.outerWidth > 1224;
        // setTimeout(() => {
        //     this.gridReference.render();      
        // }, 2000);
    }

    constructor(private authHelper: AuthHelper,
        private translate: TranslateService,
        private commonHelper: CommonHelper,
        private projectsGuard: ProjectsGuard,
        private constantService: ConstantService,
        private staffGuard: StaffGuard,
        private router: Router,
        private projectsService: ProjectsService,
        private ChatService: ChatService,
        private clientGuard: ClientGuard,
        private reportsGuard: ReportsGuard,
        private loginService: LoginService,
        private geoTrackingGuard: GeoTrackingGuard,
        private financeGuard: FinanceGuard) {
        // private messageService: MessageService
        // this.translate.setDefaultLang('en');
        this.hiddenProjects = !this.projectsGuard.canActivateWithOuthRedirecting();
        this.hiddenStaff = !this.staffGuard.canActivateWithOuthRedirecting();
        this.hiddenFinance = !this.financeGuard.canActivateWithOuthRedirecting();
        this.hiddenClients = !this.clientGuard.canActivateWithOuthRedirecting();
        this.hiddenGeoTracking = !this.geoTrackingGuard.canActivateWithOuthRedirecting();
        this.hiddenReports = !this.reportsGuard.canActivateWithOuthRedirecting();
    }

    // public cosa:number[]=[3,4,5];

    msgNotReminder: Message[] = [];
    displayNotificationPanel: boolean;

    ngOnInit() {
        this.currentUserForMenu = this.authHelper.GetEmail();

        this.msgNotReminder = [];
        // this.msgs.push({severity:'success', summary:'Success Message 2', detail:'Order submitted 3'});
    }

    //cosa2:number[]=[3,2,3];

    notificationEnabled: boolean = false;
    remindersForToday: any[] = [];
    remindersForTomorrow: any[] = [];
    remindersOthers: any[] = [];
    remindersMedicals: any[] = [];

    getRemindersForPanel = () => {
        // mostrar ruedita
        this.projectsService.GetReminderForPanel()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.remindersForToday = data.reminderToday;
                        this.remindersForTomorrow = data.remindersTomorrow;
                        this.remindersOthers = data.remindersOthers;
                        this.remindersMedicals = data.remindersMedicals;
                        // this.source.localData = data.projects;
                        // this.dataAdapter = new jqx.dataAdapter(this.source);
                    }
                    else {
                        // mensaje no se pudo obtner reminders
                    }

                    // this.myLoader.close();
                },
                error => {
                    // mensaje no se pudo obtner reminders
                    // ocultar ruedita
                });
    }

    ngAfterViewInit() {
        // if(localStorage.getItem("os").length>2)
        // {
        this.getRemindersForPanel();
        var OneSignal = window['OneSignal'] || [];
        setTimeout(() => {
            OneSignal.on('notificationDisplay', (event: any) => {
                this.displayNotificationPanel = event.data ? event.data.TypeMessage == "notification" : false;
                if (this.displayNotificationPanel) {
                    this.commonHelper.playAudio();
                }
            });
        });

        OneSignal.on('subscriptionChange', (isSubscribed: any) => {
            this.notificationEnabled = isSubscribed;
            this.msgNotReminder = [];
            if (!this.notificationEnabled) {
                this.msgNotReminder.push({ severity: 'warn', summary: 'Notification is disabled', detail: ', for enable use the bell icon on botton right of screen' });
            }
            OneSignal.getUserId().then((userId: any) => {
                this.loginService.SaveWebOneSignalId(userId);
            });
        });

        OneSignal.isPushNotificationsEnabled().then((isEnabled: any) => {
            //       if (isEnabled)
            //         else

            this.notificationEnabled = isEnabled;
            this.msgNotReminder = [];
            if (!this.notificationEnabled) {
                this.msgNotReminder.push({ severity: 'warn', summary: 'Notification is disabled', detail: ', for enable use the bell icon on botton right of screen' });
            }

            OneSignal.getUserId().then((userId: any) => {
                // this.onesignalid = userId;
                // llamar al METODO WEB.
                this.loginService.SaveWebOneSignalId(userId);
            });
        });



        // CHAT        
        // this.ChatService.GetLastUpdateChat()
        // .subscribe(
        // (data:any) => {
        //     if (data.result) {
        //         this.sourceGlobalParticipants.localData = data.globalParticipants;
        //         this.dataAdapterGlobalParticipants = new jqx.dataAdapter(this.sourceGlobalParticipants);

        //         this.rooms = data.rooms;
        //     }
        //     else
        //     {
        //         // mensaje no se pudo obtner reminders
        //     }

        //     // this.myLoader.close();
        // },
        // error => {                
        //     // mensaje no se pudo obtner reminders
        //     // ocultar ruedita
        // });    
        // }
        // else
        // {


        // }
    }

    clickChatRoom = (i: number): void => {
        this.currentChat = i;
    }

    clickRightChat = (): void => {
        // rooms:any[]=[];
        // roomfrom:number=0;
        // roomto:number=4;
        // currentChat:number=0;
        if (this.roomto + 1 <= this.rooms.length) {
            this.roomto = this.roomto + 1;
            this.roomfrom = this.roomto - 4;
        }
    }


    clickLeftChat = (): void => {
        // rooms:any[]=[];
        // roomfrom:number=0;
        // roomto:number=4;
        // currentChat:number=0;
        if (this.roomfrom - 1 >= 0) {
            this.roomfrom = this.roomfrom - 1;
            this.roomto = this.roomfrom + 4;
        }
    }

    rendererListBoxChatGlobalWindow = (index, label, value): string => {
        if (this.sourceGlobalParticipants.localData == undefined) {
            return null;
        }
        var datarecord = this.sourceGlobalParticipants.localData[index];
        if (datarecord != undefined) {
            var imgurl = this.imagePipe.transform(datarecord.imgUser, 'users');
            var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';
            // var table = '<ul class="ultable"><li style="width:40px;">' + img + '</li><li style="width:270px;">' + datarecord.name + '</li><li style="width:70px;">' + datarecord.positionName + '</li></ul>';
            var table = '<ul class="ultable"><li style="width:40px;">' + img + '</li><li style="width:270px;">' + datarecord.name + '</li><li style="width:70px;">' + "" + '</li></ul>';
            return table;
        }
        // else {
        //     if (label != undefined) {
        //         var values = label.split("|");
        //         var divSquareProject = "<span style=' border:1px solid white; height:15px; width:15px !important; background-color:" + values[0] + ";'>&nbsp;&nbsp;</span>";

        //         return divSquareProject + values[1];
        //     }
        //     else {
        //         return label;
        //     }
        // }
    };

    public ngOnDestroy() {
        if (this.myLoader) {
            this.myLoader.close();
        }
    }
}








 //  OneSignal.getUserId().then( (userId:any)=> {
        //     //this.notificationEnabled = userId ? String(userId).length > 0 : false;

        //     // if(!this.notificationEnabled)
        //     // {
        //     //     this.msgNotReminder.push({severity:'warn', summary:'Reminder is not enabled', detail:'The automatic reminder service is disabled, for enable, logout and click to, Add Device to notification service.'});
        //     // }

        // });


        // OneSignal.push(function () {
        //     // Occurs when the user's subscription changes to a new value.
        //       OneSignal.on('subscriptionChange',  (isSubscribed:any) => {

        //               this.notificationEnabled = isSubscribed;

        //               OneSignal.getUserId().then(function (userId) {
        //                   // this.onesignalid = userId;

        //                   // llamar al METODO WEB.
        //               });
        //       });
        //   });





        //this.displayNotificationPanel = true;




  //ShowPromtOneSignal ()
    //{

        //var OneSignal = window['OneSignal'] || [];

        //var OneSignal = window['OneSignal'] || [];


        // OneSignal.push(["init", {
        // appId: "216ebdba-0d63-47c7-af2d-8b3b1c604d23",
        // autoRegister: false,
        // allowLocalhostAsSecureOrigin: true,
        // notifyButton: {
        //     enable: false
        // }
        // }]);


        //OneSignal.push(function () {
            //OneSignal.push(["registerForPushNotifications"])
           // OneSignal.showSlidedownPrompt();
        //});

        // OneSignal.push(function () {
        // // Occurs when the user's subscription changes to a new value.
        // OneSignal.on('subscriptionChange', function (isSubscribed) {
        //         OneSignal.getUserId().then(function (userId) {
        //             this.onesignalid = userId;
        //         });
        // });
        // });

        // OneSignal.getUserId().then(function (userId) {
        //     this.onesignalid = userId;
        // });


        //OneSignal.getUserId().then(cosax);

      //  this.cosax = "";
       // this.cosax = OneSignal.getUserId();
        //OneSignal.getUserId().then(this.cosax);

    //    OneSignal .getUserId().then( (userId)=> {
    //         this.onesignalid = userId;
    //     });

    //       OneSignal.getUserId(function(userId) {
    //         // (Output) OneSignal User ID: 270a35cd-4dda-4b3f-b04e-41d7463a2316    
    //       });
   // }


    // OneSignal.push(function () {
        //     //OneSignal.push(["registerForPushNotifications"])
        //     OneSignal.on('notificationDisplay', function(event) {
        //         this.displayNotificationPanel = true;
        //     });
        // });