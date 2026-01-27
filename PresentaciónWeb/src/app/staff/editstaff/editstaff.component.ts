import { Component, ViewChild, AfterViewInit, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserModel, StaffPeriodSettingModel } from './editstaff.component.model';
import { StaffModel } from './editstaff.component.model';
import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { jqxListBoxComponent } from 'jqwidgets-ng/jqwidgets/jqxlistbox';
import { ConstantService } from '../../common/services/app.constant.service';
import { UploadService } from '../../common/services/app.upload.service';
import { StaffService } from '../staff.service';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import { SchedulingService } from '../../scheduling/scheduling.service';
import { AuthHelper } from '../../common/helpers/app.auth.helper';
import { CommonHelper } from '../../common/helpers/app.common.helper';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
import { jqxCheckBoxComponent } from "jqwidgets-ng/jqwidgets/jqxcheckbox";
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { JqxHelper } from '../../common/helpers/app.jqx.helper'
import { jqxTabsComponent } from 'jqwidgets-ng/jqwidgets/jqxtabs';
import { SaveActionDisplay } from '../../common/components/saveActionDisplay/saveactiondisplay.component';
import { Router } from '@angular/router';
import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqwidgets/jqxdatetimeinput';
import { FormsModule } from '@angular/forms';
import { jqxNumberInputComponent } from 'jqwidgets-ng/jqwidgets/jqxnumberinput';
import { jqxEditorComponent } from 'jqwidgets-ng/jqwidgets/jqxeditor';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';

@Component({
    selector: 'editstaff',
    templateUrl: '../../staff/editstaff/editstaff.component.template.html',
    providers: [SchedulingService, StaffService, ConstantService, UploadService, AuthHelper, CommonHelper, JqxHelper],
    styleUrls: ['../../staff/editstaff/editstaff.component.css'],
})

export class EditStaff implements OnInit, OnDestroy, AfterViewInit {

    @ViewChild('checkBoxUsePicture') checkBoxUsePicture: jqxCheckBoxComponent;
    @ViewChild("fileInput") fileInput;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('txtFirstName') txtFirstName: jqxInputComponent;
    @ViewChild('txtLastName') txtLastName: jqxInputComponent;
    @ViewChild('txtemail') txtemail: jqxInputComponent;
    @ViewChild('periodsDropDown') periodsDropDown: jqxListBoxComponent;
    @ViewChild('listBoxRoles') listBoxRoles: jqxListBoxComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('tabsReference') tabsReference: jqxTabsComponent;
    @ViewChild(SaveActionDisplay) saveActionDisplay: SaveActionDisplay;
    @ViewChild('txtDate') txtDate: jqxDateTimeInputComponent;
    @ViewChild('numberInputWorkingHours') numberInputWorkingHours: jqxNumberInputComponent;
    @ViewChild('txtsocialinsurance') txtsocialinsurance: jqxInputComponent;
    @ViewChild('txthealtinsurance') txthealtinsurance: jqxInputComponent;
    @ViewChild('txthomeaddress') txthomeaddress: jqxInputComponent;
    @ViewChild('txtCity') txtCity: jqxInputComponent;
    @ViewChild('txtHomePhone') txtHomePhone: jqxInputComponent;
    @ViewChild('txtCellNumber') txtCellNumber: jqxInputComponent;
    @ViewChild('txtSpouseName') txtSpouseName: jqxInputComponent;
    @ViewChild('txtEmergencyPerson') txtEmergencyPerson: jqxInputComponent;
    @ViewChild('txtEmergencyNumber') txtEmergencyNumber: jqxInputComponent;
    @ViewChild('myEditor') jqxEditor: jqxEditorComponent;

    imagePipe = new ImagePipe();
    availableForManyPrograms: boolean;
    PlaceHolderLookingFor: string = "";
    placeHolderEmail: string = "";
    placeHolderFirstName: string = "";
    placeHolderLastName: string = "";
    placeHolderPeriod: string = "";
    placeHolderWorkingHours: string = "";
    loadedControl: boolean[] = [false, false, true];
    msgError: string = "";
    msgSuccess: string = "";
    autoGeoTracking: boolean = false;
    staffPeriodSetting: StaffPeriodSettingModel;
    workingHours: number = 0;
    isInrolAdmin: boolean = false;

    constructor(private jqxHelper: JqxHelper,
        private translate: TranslateService,
        private staffServiceService: StaffService,
        private constantService: ConstantService,
        private schedulingService: SchedulingService,
        private authHelper: AuthHelper,
        public CommonHelper: CommonHelper,
        private route: ActivatedRoute,
        private router: Router,
        private uploadService: UploadService) {
        this.translate.setDefaultLang('en');
    }

    createNew(event: any): void {
        this.loadedControl[2] = false;
        this.staff.Id = -1;
        this.CurrentItem = -1;
        this.ngAfterViewInit();
        this.txtemail.val("");
        this.txtFirstName.val("");
        this.txtLastName.val("");
        this.autoGeoTracking = false;
        this.assignedPrograms = "";
        // this.numberInputWorkingHours.val("0");
        this.txtDate.val(new Date());
        this.txtsocialinsurance.val("");
        this.txthealtinsurance.val("");
        this.txthomeaddress.val("");
        this.txtCity.val("");
        this.txtHomePhone.val("");
        this.txtCellNumber.val("");
        this.txtSpouseName.val("");
        this.txtEmergencyPerson.val("");
        this.txtEmergencyNumber.val("");
        this.jqxEditor.val("");
        setTimeout(() => {
            this.loadedControl[2] = true;
        });

    }

    tools: string = 'bold italic underline | format font size | color background | left center right | outdent indent | ul ol | image | link';
    canDeleteOrSave: boolean; // = (): boolean => {
    // let disabled = this.projectClosed;
    //return (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor")) && !disabled;
    //return (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor"));
    //}

    //NG DISABLED ISSUE IN JQWIDGETS
    EnabledDisableForm = (): void => {
        let disabled = !this.canDeleteOrSave;
        this.txtFirstName.setOptions({ disabled: disabled });
        this.txtLastName.setOptions({ disabled: disabled });
        //this.listBoxRoles.setOptions({ disabled: disabled }); no aqui
        this.numberInputWorkingHours.setOptions({ disabled: disabled });
        this.txtDate.setOptions({ disabled: disabled });
        this.txtsocialinsurance.setOptions({ disabled: disabled });
        this.txthealtinsurance.setOptions({ disabled: disabled });
        this.txthomeaddress.setOptions({ disabled: disabled });
        this.txtCity.setOptions({ disabled: disabled });
        this.txtHomePhone.setOptions({ disabled: disabled });
        this.txtCellNumber.setOptions({ disabled: disabled });
        this.txtSpouseName.setOptions({ disabled: disabled });
        this.txtEmergencyPerson.setOptions({ disabled: disabled });
        this.txtEmergencyNumber.setOptions({ disabled: disabled });
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.isInrolAdmin = this.authHelper.IsInRol("admin");
            this.translate.use('en');

            this.loadPeriods();
            this.LoadStaff();

            this.translate.get('global_lookingfor').subscribe((res: string) => {
                this.PlaceHolderLookingFor = res;
            });

            this.translate.get('glow_staff_email_placeholder').subscribe((res: string) => {
                this.placeHolderEmail = res;
            });
            this.translate.get('glow_staff_firstname_placeholder').subscribe((res: string) => {
                this.placeHolderFirstName = res;
            });
            this.translate.get('glow_staff_lastname_placeholder').subscribe((res: string) => {
                this.placeHolderLastName = res;
            });
            this.translate.get('glow_staff_period_placeholder').subscribe((res: string) => {
                this.placeHolderPeriod = res;
            });
            this.translate.get('glow_staff_working_hours').subscribe((res: string) => {
                this.placeHolderWorkingHours = res;
            });

            this.translate.get('staff_assigned_tasks_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(0, res); });
            this.translate.get('staff_permissions_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(1, res); });
        });
    }

    HiddeLoaderWhenEnd = (): void => {
        let control = true;
        this.loadedControl.forEach(element => {
            control = control && element;
        });

        if (control) {
            this.myLoader.close();
        }
    }

    GetAvailableHours = (): any => {
        return this.CommonHelper.convertMinsToHrsMins(this.assignedHours - this.availableHours);
    }

    btnEnableDisbleAccountText: string = "";
    btnHiddenEnableDisableAccount: boolean = true;

    // VARIABLES
    private sub: any;
    currentPeriod: number = 0;
    CurrentItem: number = -1;
    staff: StaffModel = new StaffModel();
    user: UserModel = new UserModel();
    ImagePath: string = '';
    idNewPosition: number = -1;
    assignedHours: number;
    availableHours: number;
    assignedPrograms: string;

    // Crop image
    imgSet: boolean = false;
    imageChangedEvent: any = '';
    croppedImage: any = '';
    cropperReady = false;
    selectAtLeastOne: boolean = false;

    checkBoxUseThisPictureChange(event: any): void {
        let checked = event.args.checked;
        if (checked) {
            this.imgSet = true;
        }
        else {
            this.imgSet = false;
        }
    }

    fileChangeEvent(event: any): void {
        if (this.canDeleteOrSave) {
            this.imgSet = event.srcElement.files.length > 0;
            this.selectAtLeastOne = this.selectAtLeastOne || this.imgSet;
            this.imageChangedEvent = event;
            this.checkBoxUsePicture.val(true);
        }
    }

    imageCroppedBase64(image: string) {
        if (this.canDeleteOrSave) {
            this.croppedImage = image;
            this.onAnyChange(null, 'other');
        }
    }

    imageLoaded() {
        this.cropperReady = true;
    }

    imageLoadFailed() {
        this.glowMessage.ShowGlow("error", "glow_error", "glow_staff_load_image_failed");
    }

    isEditing = (): boolean => {
        return this.CurrentItem >= 0;
        // return true;
    }

    validateStaff = (): boolean => {
        let result = true;
        let txtFirstName: string = this.txtFirstName.val();
        let txtLastName: string = this.txtLastName.val();

        if (!this.emailValidator(this.user.email)) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_staff_is_not_valid_email");
            result = false;
        }

        if (txtFirstName.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_staff_type_firstname");
            result = false;
        }

        if (txtLastName.trim().length <= 0) {

            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_staff_type_lastname");
            result = false;
        }

        return result;
    }

    ResetPassword = (event: any): void => {
        this.myLoader.open();
        this.staffServiceService.ResetPassword(this.CurrentItem)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.glowMessage.ShowGlow("success", "glow_success", "glow_staff_reset_password_success");
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


    changeGeoTracking = (event: any): void => {
        // this.project.abm = "U";
        // this.project.state = event.target.checked ? "CL" : "C";        
        this.autoGeoTracking = event.target.checked;
        // this.minsDropDown.val(this.user.geoTrackingEvery);
    }

    sourceMinutes: string[] = ["5", "10", "15", "20", "30", "45", "50", "60"];

    manageError = (data: any): void => {
        // if (data.status != undefined && data.status == 403) {
        //     this.msgError = "Unauthorized";
        // }
        // else {
        //     this.msgError = data.messages != undefined ? data.messages[0].description : this.authHelper.loggedIn() ? "Connection error" : "Your session expired";
        // }

        // //this.chRef.detectChanges();
        // this.msgNotificationError.elementRef.nativeElement.childNodes[0].innerText = this.msgError;
        // this.msgNotificationError.open();
        this.glowMessage.ShowGlowByError(data);
    }

    onMustSave = (event: any): void => {
        this.Save(null);
    }

    // canSave:boolean = true;

    onAnyChange = (event: any, type: string = "-"): void => {
        var args = event ? event.args : null;
        if (args || type == "check" || type == "other") {
            let control = true;
            this.loadedControl.forEach(element => {
                control = control && element;
            });

            if (control && this.canDeleteOrSave) {
                this.saveActionDisplay.setDirty();
            }
        }
        else {
        }
    }

    calling: boolean = false;

    Save = (event: any): void => {
        if (this.validateStaff()) {
            let rolesChecked: number[] = [];
            //this.listBoxRoles.render();
            this.listBoxRoles.getCheckedItems().forEach(element => {
                rolesChecked.push(element.originalItem.id);
            });

            this.user.geoTrackingEvery = 0; //this.autoGeoTracking ? this.minsDropDown.val() : 0;            

            if (this.staffPeriodSetting && this.staffPeriodSetting.workingHours) {
                this.staffPeriodSetting.workingHours = Number(this.workingHours);
            }

            this.staff.availableForManyPrograms = this.availableForManyPrograms ? 1 : 0;
            this.staff.workStartDate = this.txtDate.getDate();
            this.user.email = this.txtemail.val().trim();
            this.staff.tmpAccreditations = this.jqxEditor.val();

            var saveStaffRequest = {
                staff: this.staff,
                user: this.user,
                roles: this.rolesTabWasVisible ? rolesChecked : null, // ,
                staffPeriodSettings: this.staffPeriodSetting ? this.staffPeriodSetting : null,
                idfPeriod: this.currentPeriod
            };

            if (this.calling && this.staff.Id <= 0) {
                return;
            }

            this.calling = true;

            this.staffServiceService.SaveStaff(saveStaffRequest)
                .subscribe(
                    (data: any) => {
                        this.calling = false;
                        if (data.result) {
                            var iduser = !this.isEditing() ? data.tagInfo.split('-')[0] : this.user.id;
                            var idstaff = !this.isEditing() ? data.tagInfo.split('-')[1] : this.CurrentItem;
                            this.staff.Id = idstaff;
                            if (this.cropperReady && this.checkBoxUsePicture.val()) {
                                let fileToUpload = this.croppedImage;  //fi;// fi.files[0];
                                this.uploadService.upload(fileToUpload, 'users', iduser)
                                    .subscribe(
                                        (dataUploaded: any) => {
                                            if (dataUploaded.result && !this.isEditing) {
                                                if (this.CurrentItem == -1) {
                                                    this.CurrentItem = idstaff;
                                                    this.loadedControl = [true, false, false];
                                                    this.LoadStaff(true);
                                                    this.glowMessage.ShowGlow("success", "glow_success", "glow_staff_saved_successfully");
                                                }
                                            }

                                            if (this.CurrentItem == -1) {
                                                this.CurrentItem = idstaff;
                                                this.loadedControl = [true, false, false];
                                                this.LoadStaff(true);
                                                this.glowMessage.ShowGlow("success", "glow_success", "glow_staff_saved_successfully");
                                            }
                                            // this.router.navigate(['editstaff2', idstaff]);
                                            this.saveActionDisplay.saved(true);
                                        },
                                        error => {
                                            this.manageError(error);
                                            this.saveActionDisplay.saved(false);
                                        });
                            }
                            else {
                                if (this.CurrentItem == -1) {
                                    //if(this.currentPeriod>0)
                                    //{

                                    // }
                                    this.CurrentItem = idstaff;
                                    this.loadedControl = [true, false, false];
                                    //this.loadPeriods();
                                    this.LoadStaff(true);
                                    this.loadTasksByPeriod();
                                    this.glowMessage.ShowGlow("success", "glow_success", "glow_staff_saved_successfully");
                                }

                                this.saveActionDisplay.saved(true);
                                //this.router.navigate(['editstaff2', idstaff]);
                            }
                        }
                        else {
                            this.manageError(data);
                            this.saveActionDisplay.saved(false);
                        }
                    },
                    error => {
                        this.calling = false;
                        //this.myLoader.close();
                        this.manageError(error);
                        this.saveActionDisplay.saved(false);
                    });
        }
        else {
            this.saveActionDisplay.saved(false);
        }
    }

    sourceTasksListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'subject', type: 'string' },
                { name: 'from', type: 'Date' },
                { name: 'to', type: 'Date' },
                { name: 'projectName', type: 'string' },
                { name: 'projectColor', type: 'string' },
                { name: 'hours', type: 'string' },
                { name: 'group', type: 'string' }
            ],
            id: 'idfStaffProjectPosition',
            async: true
        }

    dataAdapterTasksListBox: any = new jqx.dataAdapter(this.sourceTasksListBox);

    sourceRolesListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'displayShortName', type: 'string' },
                { name: 'rolDescription', type: 'string' },
                { name: 'abm', type: 'string' },
                { name: 'group', type: 'string' },
                { name: 'isInrole', type: 'boolean' },
            ],
            id: 'id',
            async: true
        };

    dataAdapterRolesListBox: any = new jqx.dataAdapter(this.sourceRolesListBox);

    emailValidator(email: string): boolean {
        var EMAIL_REGEXP = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        if (!EMAIL_REGEXP.test(email)) {
            return false;
        }
        return true;
    };

    rendererListBoxTasks = (index, label, value): string => {
        if (this.sourceTasksListBox.localData == undefined) {
            return null;
        }

        var datarecord = this.sourceTasksListBox.localData[index];

        if (datarecord != undefined) {
            let date = datarecord.deadLine == null ? "" : ", Deadline " + new Date(datarecord.deadLine).toLocaleDateString();
            var table = '<table style="min-width: 120px;"><tr><td style="font-size:12px;">' + datarecord.assignedToPosition + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:12px;">' + datarecord.subject + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + this.CommonHelper.convertMinsToHrsMins(datarecord.hours) + ' ' + date + '</td></tr></table>';
            return table;
        }
        else {
            if (label != undefined) {
                var values = label.split("|");
                var divSquareProject = "<span style=' border:1px solid var(--thirteenth-color); height:15px; width:15px !important; background-color:" + values[0] + ";'>&nbsp;&nbsp;</span>";

                return divSquareProject + values[1];
            }
            else {
                return label;
            }
        }
    };

    periodsBindingComplete = (event: any) => {
        let found = false;
        if (this.sourcePeriodsListBox.localData != undefined) {
            if (this.sourcePeriodsListBox.localData.length > 0) {
                let i = 0;
                this.sourcePeriodsListBox.localData.forEach(element => {
                    let dtf = new Date(element.from);
                    let dtt = new Date(element.to);
                    if (this.serverDateTime > dtf && this.serverDateTime < dtt) {
                        this.periodsDropDown.selectedIndex(i);
                        found = true;
                    }
                    i++;
                });

                if (!found) {
                    this.periodsDropDown.selectedIndex(0);
                }
            }
        }
    }

    loadTasksByPeriod = () => {
        if (this.CurrentItem == -1) {
            this.loadedControl[2] = true;
            this.HiddeLoaderWhenEnd();
            this.staffPeriodSetting = new StaffPeriodSettingModel();
            return;
        }

        this.myLoader.open();
        this.staffServiceService.GetTasksById(this.CurrentItem, this.currentPeriod)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.sourceTasksListBox.localData = data.assignedTasks;
                        this.dataAdapterTasksListBox = new jqx.dataAdapter(this.sourceTasksListBox);
                        this.assignedHours = data.assignedHoursOnPeriod;
                        this.availableHours = data.availableHoursOnPeriod;
                        this.assignedPrograms = data.assignedPrograms;
                        this.workingHours = data.staffPeriodSettings.workingHours;
                        this.staffPeriodSetting = data.staffPeriodSettings;
                    }
                    else {
                        this.manageError(data);
                    }

                    //this.myLoader.close();
                    this.loadedControl[2] = true;
                    this.HiddeLoaderWhenEnd();
                },
                error => {
                    //this.myLoader.close();
                    this.manageError(error);
                });
    }

    PeriodSelectDrowDown = (event: any): void => {
        if (event.args.item.value == this.currentPeriod || event.args.item.value < 0) {
            return;
        }

        this.loadedControl[2] = false;
        this.currentPeriod = event.args.item.value;

        //this.EnabledDisableForm();
        this.loadTasksByPeriod();

        // setTimeout(() => {
        //     this.EnabledDisableForm();   
        // },2000);
    }

    //SOURCE PERIODS
    sourcePeriodsListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'dateJoin', type: 'string' },
                { name: 'from', type: 'date' },
                { name: 'to', type: 'date' },
            ],
            id: 'idfStaffProjectPosition',
            async: true
        }

    dataAdapterPeriod: any = new jqx.dataAdapter(this.sourcePeriodsListBox);
    serverDateTime: Date;

    loadPeriods = (): void => {
        this.myLoader.open();
        this.schedulingService.GetPeriods()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.serverDateTime = new Date(data.currentDateTime);
                        this.sourcePeriodsListBox.localData = data.periodsList;
                        this.dataAdapterPeriod = new jqx.dataAdapter(this.sourcePeriodsListBox);
                        setTimeout(() => {
                            if (data.periodsList.length > 0) {
                                // this.periodsDropDown.selectIndex(0);   
                            }
                        });
                    }
                    else {
                        this.manageError(data);
                    }

                    this.loadedControl[0] = true;
                    this.HiddeLoaderWhenEnd();
                },
                error => {
                    this.manageError(error);
                }
            );
    }

    EnableDisableAccont = (event: any) => {
        let request: any = {
            IdUser: this.user.id,
            State: this.user.state == "D" ? "A" : "D"
        }
        this.myLoader.open();
        this.staffServiceService.EnableDisableAccount(request)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.glowMessage.ShowGlow("success", "glow_success", "glow_staff_state_account_changed");
                        this.LoadStaff();
                    } else {
                        this.manageError(data);
                    }

                    this.myLoader.close();
                },
                error => {
                    this.myLoader.close();
                    this.manageError(error);
                });
    }

    rolesTabWasVisible: boolean;

    initWidgets = (tab: any): void => {
        switch (tab) {
            case 0:
                break;
            case 1:
                this.loadRoles();
                break;
        }
    }

    hiddenWhenIsNew = (): boolean => {
        return !this.isEditing();
    }

    loadRoles() {
        this.dataAdapterRolesListBox = new jqx.dataAdapter(this.sourceRolesListBox);
        setTimeout(() => {
            if (this.sourceRolesListBox.localData) {
                this.sourceRolesListBox.localData.forEach(element => {
                    if (element.isInrole == true) {
                        this.listBoxRoles.checkItem(this.listBoxRoles.getItemByValue(element.id));
                    }
                });
            }
            //this.listBoxRoles.render();
        });
        // this.listBoxRoles.render();

        setTimeout(() => {
            //this.EnabledDisableForm();   
            let disabled = !this.canDeleteOrSave;
            this.listBoxRoles.setOptions({ disabled: disabled });
            this.rolesTabWasVisible = true;

            //   this.listBoxRoles.render();
        });
    }

    LoadStaff = (mustLoadTaks: boolean = false) => {
        this.myLoader.open();
        this.staffServiceService.GetStaffForEditById(this.CurrentItem)
            .subscribe(
                (data: any) => {
                    //alert(data.result);
                    if (data.result) {
                        if (this.CurrentItem >= 0) {
                            this.EnabledDisableForm();

                            this.btnEnableDisbleAccountText = data.staff.idfUserNavigation.state == "D" ? "Enable Account" : "Disable Account";
                            this.btnHiddenEnableDisableAccount = false;

                            this.staff = <StaffModel>data.staff;
                            this.user = <UserModel>data.staff.idfUserNavigation;

                            this.sourceRolesListBox.localData = data.roles;
                            this.loadRoles();
                            this.ImagePath = this.imagePipe.transform(this.staff.img, 'users');
                            this.autoGeoTracking = data.staff.idfUserNavigation.geoTrackingEvery > 0;
                            this.txtDate.setDate(new Date(data.staff.workStartDate));
                            setTimeout(() => {
                                this.jqxEditor.val(data.staff.tmpAccreditations ? data.staff.tmpAccreditations : "");
                                this.availableForManyPrograms = data.staff.availableForManyPrograms == 1 ? true : false;
                                if (mustLoadTaks) {
                                    this.loadTasksByPeriod();
                                }
                                // this.tabsReference.enableAt(0);
                                // this.tabsReference.enableAt(1);
                                // this.tabsReference.enableAt(2);
                                // this.tabsReference.enableAt(3);
                                // this.tabsReference.select(0);

                                this.tabsReference.enable();
                                // listbox permissions
                                // this.dataAdapterRolesListBox = new jqx.dataAdapter(this.sourceRolesListBox);  

                                // setTimeout(() => {

                                //     if(this.sourceRolesListBox.localData)
                                //     {
                                //         this.sourceRolesListBox.localData.forEach(element => {
                                //             if(element.isInrole==true)
                                //             {
                                //                     this.listBoxRoles.checkItem(this.listBoxRoles.getItemByValue(element.id));
                                //             }
                                //         });  
                                //     }

                                //   //});

                                //   //this.listBoxRoles.render();

                                //  listbox permissions
                            });
                        }
                        else {
                            this.staff.Id = -1;
                            this.ImagePath = this.imagePipe.transform('generic', 'users');
                            this.tabsReference.disable();
                            // this.tabsReference.disableAt(0);
                            // this.tabsReference.disableAt(1);
                            // this.tabsReference.disableAt(2);
                            // this.tabsReference.disableAt(3);
                            // this.tabsReference.select(0);
                        }

                        this.checkBoxUsePicture.val(false);
                        this.loadedControl[1] = true;
                        this.HiddeLoaderWhenEnd();
                    } else {
                        this.manageError(data);
                    }

                    //this.myLoader.close();
                },
                error => {
                    //this.myLoader.close();
                    this.manageError(error);
                });
    }

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(params => {
            this.CurrentItem = +params['id'];
            this.canDeleteOrSave = (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor"));
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}