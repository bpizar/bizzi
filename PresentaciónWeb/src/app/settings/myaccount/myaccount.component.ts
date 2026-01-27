import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { ConstantService } from '../../common/services/app.constant.service';
import { Router } from '@angular/router';

import { jqxPasswordInputComponent } from 'jqwidgets-ng/jqxpasswordinput';
import { jqxTabsComponent } from 'jqwidgets-ng/jqxtabs';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { jqxWindowComponent } from 'jqwidgets-ng/jqwidgets/jqxwindow';

import { jqxNotificationComponent } from 'jqwidgets-ng/jqwidgets/jqxnotification';
import { AuthHelper } from '../../common/helpers/app.auth.helper';
import { SettingsService } from '../settings.service';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'myaccount',
    templateUrl: '../../settings/myaccount/myaccount.component.template.html',
    providers: [ConstantService, SettingsService, AuthHelper]
})

export class MyAccount implements OnInit, AfterViewInit {

    constructor(private settingsService: SettingsService,
        private authHelper: AuthHelper,
        private translate: TranslateService,
        private constantService: ConstantService,
        private router: Router,
        private sanitizer: DomSanitizer) {
        this.translate.setDefaultLang('en');
    }

    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('txtCurrentPass') txtCurrentPass: jqxPasswordInputComponent;
    @ViewChild('txtNewPass') txtNewPass: jqxPasswordInputComponent;
    @ViewChild('txtNewPassConfirm') txtNewPassConfirm: jqxPasswordInputComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('tabsReference') tabsReference: jqxTabsComponent;
    @ViewChild('renewWindow') renewWindow: jqxWindowComponent;

    imagePipe = new ImagePipe();
    msgSuccess: string = "";
    msgError: string = "";
    ImagePath: string = '';
    UserEmail: string = "";
    UserLastName: string = "";
    UserFirstName: string = "";
    currentPass: string = "";
    currentNewPass: string = "";
    currentPassConfirm: string = "";
    EffectivePermissions: string[] = [];
    generatedQRImage: any;
    isSecretShowing: boolean = false;

    placeHolderEnterYourPassword: string = "";
    placeHolderEnterNewPassword: string = "";
    placeHolderReWritePassword: string = "";

    validateEqualsPasswords = (): boolean => {
        let result = true;
        let currentpass: string = this.txtCurrentPass.val();
        let newpass: string = this.txtNewPass.val();
        let newPassConfirm: string = this.txtNewPassConfirm.val();

        if (currentpass.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "set_glow_enter_current_pass");
            result = false;
        }

        if (newpass.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "set_glow_enter_new_pass");
            result = false;
        }

        if (newPassConfirm.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "set_glow_rewrite_pass");
            result = false;
        }

        if (result) {
            if (newpass.trim() != newPassConfirm.trim()) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "set_glow_unmatch_pass");
                result = false;
            }
        }

        return result;
    }

    ChangePassword = (event: any) => {
        if (this.validateEqualsPasswords()) {
            let currentpass: string = this.txtCurrentPass.val();
            let newpass: string = this.txtNewPass.val();
            let newPassConfirm: string = this.txtNewPassConfirm.val();

            let request = {
                CurrentPassword: currentpass.trim(),
                NewPassword: newpass.trim(),
                ConfirmNewPassword: newPassConfirm.trim()
            };

            this.myLoader.open();
            this.settingsService.ChangeMyPassword(request)
                .subscribe(
                    (data: any) => {
                        if (data.result) {
                            this.myLoader.close();
                            this.glowMessage.ShowGlow("success", "glow_success", "set_glow_saved_succesfully");
                        }
                        else {
                            this.manageError(data);
                        }

                        this.myLoader.close();
                    },
                    error => {
                        this.myLoader.close();
                        this.manageError(error);
                    });
        }
    };

    logOut = (event: any) => {
        this.authHelper.LogOut();
        this.router.navigate(['login']);
    };

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
            this.LoadMyAccount();
            this.translate.get('set_account_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(0, res); });
            // this.translate.get('set_company_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(1,res); });    

            // this.translate.get('set_change_pass_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(1,res); });                
            // this.translate.get('set_permission_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(2,res); });                                       
            this.translate.get('set_enter_you_pass').subscribe((res: string) => { this.placeHolderEnterYourPassword = res; });
            this.translate.get('set_enter_new_pass').subscribe((res: string) => { this.placeHolderEnterNewPassword = res; });
            this.translate.get('set_enter_new_pass2').subscribe((res: string) => { this.placeHolderReWritePassword = res; });
        });
    };

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    LoadMyAccount = () => {
        this.myLoader.open();
        this.settingsService.GetMyAccount()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.UserEmail = data.user.email;
                        this.UserLastName = data.user.lastName;
                        this.UserFirstName = data.user.firstName;
                        for (var i = 0; i < data.user.identity_users_rol.length; i++) {
                            this.EffectivePermissions.push(data.user.identity_users_rol[i].idfRolNavigation.displayShortName);
                        }
                        this.ImagePath = this.imagePipe.transform(data.user.img, 'users');
                    }
                    else {
                        this.manageError(data);
                    }
                    this.myLoader.close();
                },
                error => {
                    this.myLoader.close();
                    this.manageError(error);
                });
    }

    renewSecret() {
        this.renewWindow.open();
    }

    showSecret() {
        this.isSecretShowing = true;
        this.settingsService.generateTFASecret()
            .subscribe(data => {
                if (data) {
                    let unsafeImageUrl = URL.createObjectURL(data);
                    this.generatedQRImage = this.sanitizer.bypassSecurityTrustUrl(unsafeImageUrl);
                }
                else {
                    this.closeRenewWindow();
                    this.glowMessage.ShowGlow("error", "Error on generating QR", "Please check your connection");
                }
            });
    }

    closeRenewWindow() {
        this.isSecretShowing = false;
        this.renewWindow.close();
    }

    ngOnInit(): void {

    }
}