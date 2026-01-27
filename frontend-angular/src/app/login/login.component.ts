import { Component, ViewChild, OnInit, AfterViewInit, ChangeDetectorRef, isDevMode } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { LoginService } from './login.service';
import { Router } from '@angular/router';
import { jqxButtonComponent } from 'jqwidgets-ng/jqxbuttons';
import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';

@Component({
    selector: 'loginwindow',
    templateUrl: '../login/login.component.template.html',
    providers: [LoginService, ConstantService],
    styleUrls: ['../login/login.component.css'],
})

export class Login implements OnInit, AfterViewInit {

    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('loginButton') loginButton: jqxButtonComponent;

    constructor(private loginService: LoginService,
        private router: Router) { }

    onesignalid: string = "";
    msgError: string = "";
    msgWarning: string = "";
    private idStorage: string = "token";

    // username: string = "admin@people.com";
    // password: string = "!People12345";
    // password: string = "1";

    username: string = "";
    password: string = "";
    authenticating: boolean = false;
    ready: boolean = true;

    // ngAfterViewInit() {
    //     //if(!isDevMode()) {
    //     var OneSignal = window['OneSignal'] || [];
    //     OneSignal.push(() => {
    //         /* These examples are all valid */
    //         OneSignal.isPushNotificationsEnabled((isEnabled: any) => {
    //             if (isEnabled) {
    //                 OneSignal.getUserId().then((userId: any) => {
    //                     setTimeout(() => {
    //                         this.onesignalid = userId ? userId : "";
    //                         this.ready = true;
    //                         this.saveOneSignal(this.onesignalid);
    //                     });
    //                 });
    //             }
    //             else {
    //                 setTimeout(() => {
    //                     this.onesignalid = "";
    //                     this.ready = true;
    //                     this.saveOneSignal(this.onesignalid);
    //                 });
    //             }
    //         });
    //
    //         // Occurs when the user's subscription changes to a new value.
    //         OneSignal.on('subscriptionChange', (isSubscribed: any) => {
    //             if (!isSubscribed) {
    //                 setTimeout(() => {
    //                     this.onesignalid = "";
    //                     this.ready = true;
    //                     this.saveOneSignal(this.onesignalid);
    //                 });
    //             }
    //             else {
    //                 OneSignal.getUserId().then((userId: any) => {
    //                     setTimeout(() => {
    //                         this.onesignalid = userId ? userId : "";
    //                         this.ready = true;
    //                         this.saveOneSignal(this.onesignalid);
    //                     });
    //                 });
    //             }
    //         });
    //
    //         OneSignal.getUserId().then((userId: any) => {
    //             setTimeout(() => {
    //                 this.onesignalid = userId;
    //                 this.ready = true;
    //                 this.saveOneSignal(userId);
    //             });
    //         });
    //     });
    //     // }
    //     // else
    //     // {
    //     //     this.onesignalid = "";
    //     //     this.ready = true;
    //     // }
    //
    //     OneSignal.getUserId().then((userId) => {
    //         this.onesignalid = userId;
    //     });
    //
    //     setTimeout(() => {
    //         if (!this.ready) {
    //             //alert("ABC");
    //             this.ready = true;
    //             this.saveOneSignal("");
    //         }
    //     }, 5000);
    // }

  ngAfterViewInit() {
    // 1) Never block the UI on OneSignal
    this.ready = true;

    // Optional: mark OneSignal as empty by default
    this.onesignalid = '';
    this.saveOneSignal('');

    // 2) Best-effort OneSignal init (non-blocking)
    try {
      const OneSignal: any = (window as any).OneSignal;

      // If SDK not present or blocked, just log and exit safely
      if (!OneSignal || typeof OneSignal.push !== 'function') {
        if (isDevMode() && this.glowMessage) {
          this.glowMessage.ShowGlow('warn', 'OneSignal', 'OneSignal failed / blocked on localhost');
        }
        console.warn('[Login] OneSignal SDK not available (blocked or not loaded).');
        return;
      }

      OneSignal.push(() => {
        try {
          // subscriptionChange handler (guarded)
          if (typeof OneSignal.on === 'function') {
            OneSignal.on('subscriptionChange', (isSubscribed: any) => {
              if (!isSubscribed) {
                this.onesignalid = '';
                this.saveOneSignal('');
              } else if (typeof OneSignal.getUserId === 'function') {
                OneSignal.getUserId()
                  .then((id: any) => {
                    this.onesignalid = id || '';
                    this.saveOneSignal(this.onesignalid);
                  })
                  .catch((e: any) => console.warn('[Login] OneSignal.getUserId failed:', e));
              }
            });
          }

          // initial state
          if (typeof OneSignal.isPushNotificationsEnabled === 'function') {
            OneSignal.isPushNotificationsEnabled((isEnabled: any) => {
              if (!isEnabled) { return; }

              if (typeof OneSignal.getUserId === 'function') {
                OneSignal.getUserId()
                  .then((id: any) => {
                    this.onesignalid = id || "";
                    this.saveOneSignal(this.onesignalid);
                  })
                  .catch((e: any) => console.warn('[Login] OneSignal.getUserId failed:', e));
              } else {
                console.warn('[Login] OneSignal.getUserId is not a function.');
              }
            });
          } else {
            console.warn('[Login] OneSignal.isPushNotificationsEnabled is not a function.');
          }
        } catch (e) {
          console.warn('[Login] OneSignal init threw:', e);
        }
      });
    } catch (e) {
      console.warn('[Login] OneSignal outer error:', e);
    }
  }

  saveOneSignal(d: any) {
        localStorage.setItem('os', this.onesignalid);
    }

    ngOnInit() {
        sessionStorage.setItem(this.idStorage, '');
    }

    Login = (): void => {
        this.authenticating = true;
        this.glowMessage.ShowGlow('info', 'login_glow_authenticating', "glow_globalpleasewait");
        this.loginService.Login(this.username, this.password, this.onesignalid)
            .subscribe(
                (data: any) => {
                    localStorage.setItem(this.idStorage, data.auth_token);
                    this.router.navigate(['./dashboard']);
                    this.authenticating = false;
                },
                error => {
                    if (error.error.type === 'error') {
                        this.glowMessage.ShowGlow('error', 'glow_globalconexionerror', 'glow_gloablcantconnecttoserver');
                    }
                    else {
                        this.glowMessage.ShowGlow('warn', 'login_glow_authenticatingfailed', 'login_glow_notvalidcretendials');
                    }
                    this.authenticating = false;
                    // FIX TWO WAY BINDING PROPERTIES (JQWIDGETS ISSUE).
                    this.loginButton.setOptions({ disabled: false });
                });
    }
}





//     OneSignal = window['OneSignal'] || [];


    //     OneSignal.push(["init", {
    //     appId: "216ebdba-0d63-47c7-af2d-8b3b1c604d23",
    //     autoRegister: false,
    //     allowLocalhostAsSecureOrigin: true,
    //     // notifyButton: {
    //     //     enable: false
    //     // }
    //     }]);


    //     OneSignal.push(function () {
    //         OneSignal.push(["registerForPushNotifications"])
    //     });


    // OneSignalx:any;// = window['OneSignal'] || [];

    //    cosax=(userId:any)=>
    //    {
    //         this.onesignalid = userId;
    //    }
    //cosax:string="";


                    // this.onesignalid = "";
                    // this.ready = true;





    //     // OneSignal.getUserId().then(function (userId) {
    //     //     this.onesignalid = userId;
    //     // });


    //     //OneSignal.getUserId().then(cosax);

    //   //  this.cosax = "";
    //    // this.cosax = OneSignal.getUserId();
    //     //OneSignal.getUserId().then(this.cosax);

    //    OneSignal.getUserId().then( (userId)=> {
    //         this.onesignalid = userId;
    //     });

    //     OneSignal.on('notificationDisplay', function(event) {
    //         this.displayNotificationPanel = true;
    //     });


    // //       OneSignal.getUserId(function(userId) {
    // //         // (Output) OneSignal User ID: 270a35cd-4dda-4b3f-b04e-41d7463a2316
    // //       });

        // setTimeout(() => {


        //     OneSignal.getUserId().then( (userId:any)=> {
        //         this.onesignalid = userId;

        //     });
        // });
