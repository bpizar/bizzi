import { Component, ViewChild, ViewChildren, AfterViewInit, OnInit, OnDestroy, ChangeDetectorRef, QueryList, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClientModel } from './editclient.component.model';
import { Router } from '@angular/router';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqwidgets/jqxdropdownlist';
import { jqxComboBoxComponent } from 'jqwidgets-ng/jqwidgets/jqxcombobox';
import { jqxGridComponent } from 'jqwidgets-ng/jqwidgets/jqxgrid';
import { jqxWindowComponent } from 'jqwidgets-ng/jqwidgets/jqxwindow';
import { jqxButtonComponent } from 'jqwidgets-ng/jqwidgets/jqxbuttons';
import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { jqxListBoxComponent } from 'jqwidgets-ng/jqwidgets/jqxlistbox';
import { jqxTabsComponent } from 'jqwidgets-ng/jqwidgets/jqxtabs';
import { ConstantService } from '../../common/services/app.constant.service';
import { UploadService } from '../../common/services/app.upload.service';
import { ClientsService } from '../clients.service';
import { jqxColorPickerComponent } from 'jqwidgets-ng/jqwidgets/jqxcolorpicker';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqwidgets/jqxdatetimeinput';
import { SchedulingService } from '../../scheduling/scheduling.service';
import { AuthHelper } from '../../common/helpers/app.auth.helper';
import { ProjectsService } from '../../projects/projects.service';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { jqxCheckBoxComponent } from "jqwidgets-ng/jqwidgets/jqxcheckbox";
import { HttpUrlEncodingCodec } from '@angular/common/http';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { SaveActionDisplay } from '../../common/components/saveActionDisplay/saveactiondisplay.component';
import { jqxTextAreaComponent } from 'jqwidgets-ng/jqwidgets/jqxtextarea';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';
//import { MessageDirective } from './message.directive';

@Component({
    selector: 'project',
    templateUrl: '../../clients/editclient/editclient.component.template.html',
    providers: [SchedulingService, ProjectsService, ClientsService, ConstantService, UploadService, AuthHelper],
    styleUrls: ['../../clients/editclient/editclient.component.css'],
})

export class EditClient implements OnInit, OnDestroy, AfterViewInit {

    loadedControl: boolean[] = [false, false, true]; // 0  client   1  period  2 clientdata
    @ViewChild('checkBoxUsePicture') checkBoxUsePicture: jqxCheckBoxComponent;
    @ViewChild("fileInput") fileInput;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('txtFirstName') txtFirstName: jqxInputComponent;
    @ViewChild('txtLastName') txtLastName: jqxInputComponent;
    @ViewChild('txtemail') txtemail: jqxInputComponent;
    @ViewChild('txtMedication') txtMedication: jqxInputComponent;
    @ViewChild('txtBirthDate') txtBirthDate: jqxDateTimeInputComponent;
    @ViewChild('txtTime') txtTime: jqxDateTimeInputComponent;
    @ViewChild('txtFrom') txtFrom: jqxDateTimeInputComponent;
    @ViewChild('txtTo') txtTo: jqxDateTimeInputComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('periodsDropDown') periodsDropDown: jqxListBoxComponent;
    @ViewChild('gridDailyLog') gridDailyLog: jqxGridComponent;
    @ViewChild('gridMedication') gridMedication: jqxGridComponent;
    @ViewChild('gridIncidents') gridIncidets: jqxGridComponent;
    @ViewChild(SaveActionDisplay) saveActionDisplay: SaveActionDisplay;
    @ViewChild('txtPhone') txtPhone: jqxInputComponent;
    @ViewChild('txtNotes') txtNotes: jqxInputComponent;
    @ViewChild('txtSafetyPlan') txtSafetyPlan: jqxTextAreaComponent;
    @ViewChild('tabsReference') tabsReference: jqxTabsComponent;
    @ViewChild('editGenStaff') editGenStaff: jqxDropDownListComponent;
    @ViewChild('txtMotherName') txtMotherName: jqxTextAreaComponent;
    @ViewChild('txtMotherInfo') txtMotherInfo: jqxTextAreaComponent;
    @ViewChild('txtFatherName') txtFatherName: jqxTextAreaComponent;
    @ViewChild('txtFatherInfo') txtFatherInfo: jqxTextAreaComponent;
    @ViewChild('txtAgencyWorker') txtAgencyWorker: jqxTextAreaComponent;
    @ViewChild('txtAgencyInfo') txtAgencyInfo: jqxTextAreaComponent;
    @ViewChild('txtPlacement') txtPlacement: jqxTextAreaComponent;
    @ViewChild('txtTmpSupervisor') txtTmpSupervisor: jqxTextAreaComponent;
    @ViewChild('txtTmpSpecialProgram') txtTmpSpecialProgram: jqxTextAreaComponent;
    @ViewChild('txttmpAdditionalInformation') txttmpAdditionalInformation: jqxTextAreaComponent;
    @ViewChild('txtTmpShool') txtTmpShool: jqxTextAreaComponent;
    @ViewChild('txtSchoolInfo') txtSchoolInfo: jqxTextAreaComponent;
    @ViewChild('txtTeacher') txtTeacher: jqxTextAreaComponent;
    @ViewChild('txtTeacherInfo') txtTeacherInfo: jqxTextAreaComponent;
    @ViewChild('txtTmpDoctorName') txtTmpDoctorName: jqxTextAreaComponent;
    @ViewChild('txtTmpDoctorInfo') txtTmpDoctorInfo: jqxTextAreaComponent;
    @ViewChild('txtTmpMedication') txtTmpMedication: jqxTextAreaComponent;
    //@ViewChild('txtTmpMedSchedule') txtTmpMedSchedule: jqxTextAreaComponent;
    @ViewChild('medicalWindow') medicalWindow: jqxWindowComponent;
    @ViewChild('checkBoxReminder') checkBoxReminder: jqxCheckBoxComponent;
    @ViewChildren(jqxDropDownListComponent) supervisorcmb: QueryList<jqxDropDownListComponent>;
    @ViewChild('msgTemp')
    private msgTempRef: TemplateRef<jqxDropDownListComponent>
    imagePipe = new ImagePipe();
    currentPeriod: number = 0;
    periodClosed: boolean = false;
    msgError: string = "";
    msgSuccess: string = "";
    ImagePath: String = "";
    placeHolderEmail: string = "";
    placeHolderFirstName: string = "";
    placeHolderLastName: string = "";
    placeHolderBirthDate: string = "";
    placeHolderPhoneNumber: string = "";
    placeHolderNotes: string = "";
    // Crop image
    imgSet: boolean = false;
    imageChangedEvent: any = '';
    croppedImage: any = '';
    cropperReady = false;
    selectAtLeastOne: boolean = false;
    hiddenWhenIsNew: boolean; //=():boolean=>
    // {
    // return !this.isEditing();
    // }
    canDeleteOrSave: boolean; // => {
    //  return (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("clienteditor"));
    //}
    btnEnableDisbleAccountText: string = "";
    btnHiddenEnableDisableAccount: boolean = true;

    //VARIABLES
    private sub: any;
    //currentProject: number = 0;
    CurrentItem: number = -1;
    Client: ClientModel = new ClientModel();
    idNewPosition: number = -1;
    isEditing: boolean;
    calling: boolean = false;
    // {        
    //     return this.CurrentItem >= 0;
    // }

    constructor(private translate: TranslateService,
        private ClientsServiceService: ClientsService,
        private constantService: ConstantService,
        private projectsService: ProjectsService,
        private schedulingService: SchedulingService,
        private authHelper: AuthHelper,
        private route: ActivatedRoute,
        private router: Router,
        private uploadService: UploadService,
        private chRef: ChangeDetectorRef) {
        this.translate.setDefaultLang('en');
    }

    onMustSave = (event: any): void => {
        this.Save(null);
    }

    onAnyChange = (event: any, type: string = "-"): void => {
        var args = event ? event.args : null;
        if (args || type == "check" || type == "other") {
            let control = true;
            this.loadedControl.forEach(element => {
                control = control && element;
            });

            if (control) {

                this.saveActionDisplay.setDirty();
            }
        }
        else {
        }
    }

    checkBoxUseThisPictureChange(event: any): void {
        let checked = event.args.checked;
        if (checked) {
            this.imgSet = true;
        }
        else {
            this.imgSet = false;
        }
    }

    cancelWindowMedical = (event: any): void => {
        this.medicalWindow.close();
    }

    RemoveWindowMedical = (event: any): void => {
        let record = this.sourceMedication.localData.filter(c => c.id == this.idmedication)[0];
        record.abm = "D";
        this.onAnyChange(null, 'other');
        this.medicalWindow.close();
    }

    cmbSupervidorOnchange = (event: any, i: number) => {
        this.projectClient[i].idSPP = event.args.item.value;
        this.onAnyChange(null, 'other');
        //);
    }

    cmbSupervisorsBindingComplete = (event: any, i: number) => {
        //     if(this.supervisorcmb)
        //     {
        //         // alert(this.supervisorcmb.length);

        //         let auxCosa:any=this.supervisorcmb.toArray()[i];

        //         // setTimeout(() => {
        //         //     auxCosa.selectItem(auxCosa.getItems()[0]);
        //         // });




        //         // this.supervisorcmb.forEach((messageDirective:any) => 
        //         //     {
        //         //         messageDirective.selectIndex(0);
        //         //         //auxCosa = messageDirective.viewContainerRef.createEmbeddedView(this.msgTempRef);
        //         //     }
        //         // );


        //         auxCosa.selectIndex(0);



        //         // if(this.projectClient[i].idSPP>0)
        //         // {


        //         //     //let elementX = this.supervisorcmb.toArray(); // this.supervisorcmb.filter((element, index) => index === i);


        //         //     var item = auxCosa.getItemByValue(String(this.projectClient[i].idSPP));


        //         //     auxCosa.selectIndex(1);

        //         //     // if(item)
        //         //     // {
        //         //     //     auxCosa.selectItem(item);
        //         //     // }
        //         //     // else{
        //         //     //    //this.supervisorcmb.toArray()[i].clearSelection();
        //         //     // }
        //         // }
        //         // else
        //         // {
        //         //     //this.supervisorcmb.toArray()[i].clearSelection();
        //         //     auxCosa.clearSelection();
        //         // }
        //     }

        //     //this.supervisorcmb[i].selectItem(this.supervisorcmb[i].getItemByValue(String(this.projectClient[i])));

        //     // setTimeout(() => {
        //     //     if(this.dailyLog){
        //     //         this.cmbProjects.selectItem(this.cmbProjects.getItemByValue(String(this.dailyLog.projectId)));
        //     //     }

        //     //     setTimeout(() => {                           
        //     //         this.loadedControl[1] = true;                         
        //     //         this.HiddeLoaderWhenEnd();                      
        //     //      });

        //     // });                
    }

    DoneWindowMedical = (event: any): void => {
        // validate
        //this.txtMedication
        let time1: Date = this.txtTime.getDate();
        let date1: Date = this.txtFrom.getDate();
        let date2: Date = this.txtTo.getDate();
        let editGenStaffValue: number = Number(this.editGenStaff.val());
        let hasError = false;
        if (this.medication.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_write_medication");
            hasError = true;
        }

        if (date1 > date2) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_time_from_must_smaller_than_date");
            hasError = true;
        }

        if (editGenStaffValue <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "projects_select_participant");
            hasError = true;
        }

        if (hasError) {
            return;
        }
        // end validate
        //let editGenStaffValue: number = this.editGenStaff.val();
        if (this.isAddingMedication) {
            this.sourceMedication.localData.push(
                {
                    'id': -1,
                    'idfClient': this.CurrentItem,
                    'idfAssignedTo': editGenStaffValue,
                    'description': this.medication,
                    'datetime': time1,
                    'from': date1,
                    'to': date2,
                    'reminder': 1,
                    'state': 'C',
                    'projectName': '',
                    'color': '',
                    'sppDescription': '',
                    'abm': 'I'
                }
            );
        }
        else {
            let record = this.sourceMedication.localData.filter(c => c.id == this.idmedication)[0];
            record.abm = "U";
            record.idfAssignedTo = editGenStaffValue;
            record.description = this.medication;
            record.datetime = time1;
            record.from = date1;
            record.to = date2;
            record.reminder = this.checkBoxReminder.val() ? 1 : 0;
        }

        this.onAnyChange(null, 'other');
        this.medicalWindow.close();
    }

    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    fileChangeEvent(event: any): void {
        this.imgSet = event.srcElement.files.length > 0;
        this.selectAtLeastOne = this.selectAtLeastOne || this.imgSet;
        this.imageChangedEvent = event;
        this.checkBoxUsePicture.val(true);
    }

    imageCroppedBase64(image: string) {
        this.croppedImage = image;
        this.onAnyChange(null, 'other');
    }

    imageLoaded() {
        this.cropperReady = true;
    }

    imageLoadFailed() {
        this.glowMessage.ShowGlow("error", "Load failed", "Load failed, please select another image format");
    }

    // end crop image

    // periodsBindingComplete = (event: any) => {
    //     if (this.sourcePeriodsListBox.localData != undefined)
    //     {
    //         if (this.sourcePeriodsListBox.localData.length > 0) {
    //             this.periodsDropDown.selectedIndex(0);
    //         }
    //     }
    // }

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

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
            this.loadPeriods();
            if (this.CurrentItem >= 0) {
                this.LoadClient();
                // return;
            }
            else {
                this.loadedControl[0] = true;
                this.tabsReference.disable();
                /// this.Client.id = -1;
            }
            this.ImagePath = this.imagePipe.transform('default', 'clients');

            this.translate.get('clients_edit_email_placeholder').subscribe((res: string) => {
                this.placeHolderEmail = res;
            });
            this.translate.get('clients_edit_fist_name_placeholder').subscribe((res: string) => {
                this.placeHolderFirstName = res;
            });
            this.translate.get('clients_edit_lastname_placeholder').subscribe((res: string) => {
                this.placeHolderLastName = res;
            });
            this.translate.get('clients_edit_birthdate_placeholder').subscribe((res: string) => {
                this.placeHolderBirthDate = res;
            });
            this.translate.get('clients_phone_placeholder').subscribe((res: string) => {
                this.placeHolderPhoneNumber = res;
            });
            this.translate.get('clients_notes_placeholder').subscribe((res: string) => {
                this.placeHolderNotes = res;
            });
            //this.tabsReference.disable();
        });
    }

    validateClient = (): boolean => {
        let result = true;
        let txtFirstName: string = this.txtFirstName.val();
        let txtLastName: string = this.txtLastName.val();
        let txtemail: string = this.txtemail.val();
        let txtdate: string = this.txtBirthDate.val();
        if (txtFirstName.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_clients_fistname");
            result = false;
        }

        if (txtLastName.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_clients_lastname");
            result = false;
        }

        if (txtemail.trim().length <= 0) {
            // this.glowMessage.ShowGlow("warn","glow_invalid","glow_clients_valid_email");
            // result = false;
        }
        else {
            if (!this.emailValidator(txtemail)) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_clients_valid_email");
                result = false;
            }
        }

        if (txtdate.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_clients_birthdate");
            result = false;
        }

        return result;
    }

    manageError = (data: any): void => {
        this.myLoader.close();
    }

    createNew(event: any): void {
        //        this.loadedControl[2]= false;
        this.Client.id = -1;
        this.CurrentItem = -1;
        // this.router.navigate(['staff/editstaff', this.CurrentItem]); 
        this.ngAfterViewInit();
        this.loadedControl[2] = false;
        this.txtemail.val("");
        this.txtFirstName.val("");
        this.txtLastName.val("");
        this.txtBirthDate.clearString();
        this.txtPhone.val("");
        this.txtNotes.val("");
        this.txtSafetyPlan.val("");
        this.sourceDailyLogs = [];
        this.sourceInjuries = [];
        this.sourceMedication = [];
        this.dataAdapterDailyLogs = new jqx.dataAdapter(this.sourceDailyLogs);
        this.gridDailyLog.render();
        this.dataAdapterInjuries = new jqx.dataAdapter(this.sourceInjuries);
        this.gridIncidets.render();
        this.dataAdapterMedication = jqx.dataAdapter(this.sourceMedication);
        this.gridMedication.render();
        this.txtMotherName.val("");
        this.txtMotherInfo.val("");
        this.txtFatherName.val("");
        this.txtFatherInfo.val("");
        this.txtAgencyWorker.val("");
        this.txtAgencyInfo.val("");
        this.txtPlacement.val("");
        this.txtTmpSupervisor.val("");
        this.txtTmpSpecialProgram.val("");
        this.txttmpAdditionalInformation.val("");
        this.txtTmpShool.val("");
        this.txtSchoolInfo.val("");
        this.txtTeacher.val("");
        this.txtTeacherInfo.val("");
        this.txtTmpDoctorName.val("");
        this.txtTmpDoctorInfo.val("");
        this.txtTmpMedication.val("");
        //this.txtTmpMedSchedule.val("");
        this.isEditing = false;
        this.hiddenWhenIsNew = !this.isEditing;

        setTimeout(() => {
            this.loadedControl[2] = true;
        });
    }

    Save(event: any): void {
        if (this.validateClient()) {

            this.Client.id = this.CurrentItem;

            let saveClientRequestx = {
                Client: this.Client,
                UserCallName: "-",
                Reminders: this.sourceMedication.localData ? this.sourceMedication.localData.filter(c => c.abm != "") : [],
                ProjectClient: this.projectClient
            };
            saveClientRequestx.Client.birthDate = new Date(this.txtBirthDate.val());
            // this.myLoader.open();
            if (this.calling && this.Client.id <= 0) {
                return;
            }

            this.calling = true;
            this.ClientsServiceService.SaveClient(saveClientRequestx)
                .subscribe(
                    (data: any) => {
                        this.calling = false;
                        if (data.result) {
                            this.hiddenWhenIsNew = false;
                            this.isEditing = this.CurrentItem >= 0
                            this.hiddenWhenIsNew = !this.isEditing;
                            var idclient = !this.isEditing ? data.tagInfo : this.Client.id;
                            let fi = this.croppedImage;//  this.fileInput.nativeElement;
                            this.Client.id = idclient;
                            this.sourceMedication.localData = data.reminders;
                            this.dataAdapterMedication = new jqx.dataAdapter(this.sourceMedication);
                            if (this.cropperReady && this.checkBoxUsePicture.val()) {
                                let fileToUpload = fi;// fi.files[0];
                                this.uploadService.upload(fileToUpload, 'clients', idclient)
                                    .subscribe(
                                        (dataUploaded: any) => {
                                            if (dataUploaded.result && !this.isEditing) {
                                                if (this.CurrentItem == -1) {
                                                    this.CurrentItem = idclient;
                                                    this.loadedControl = [true, true, false];
                                                    this.LoadClient(true);
                                                    this.loadClientData();
                                                    this.EnabledDisableForm();
                                                    this.glowMessage.ShowGlow("success", "glow_success", "glow_clients_saved_successfully");
                                                }
                                            }

                                            // this.glowMessage.msgs = [];
                                            // this.glowMessage.ShowGlow("success","glow_success","glow_clients_saved_successfully");
                                            // this.myLoader.close();                                
                                            // this.CurrentItem = idclient;                                
                                            // this.LoadClient();
                                            if (this.CurrentItem == -1) {
                                                this.CurrentItem = idclient;
                                                this.loadedControl = [true, true, false];
                                                this.LoadClient(true);
                                                this.loadClientData();
                                                this.EnabledDisableForm();
                                                this.glowMessage.ShowGlow("success", "glow_success", "glow_clients_saved_successfully");
                                            }
                                            this.saveActionDisplay.saved(true);
                                        },
                                        error => {
                                            // this.myLoader.close();
                                            // this.manageError(error);
                                            this.manageError(error);
                                            this.saveActionDisplay.saved(false);
                                        });
                            }
                            else {
                                if (this.CurrentItem == -1) {
                                    this.CurrentItem = idclient;
                                    this.loadedControl = [true, true, false];
                                    this.LoadClient(true);
                                    this.loadClientData();
                                    this.EnabledDisableForm();
                                    this.glowMessage.ShowGlow("success", "glow_success", "glow_clients_saved_successfully");
                                }

                                this.saveActionDisplay.saved(true);
                                // this.glowMessage.msgs = [];
                                // this.glowMessage.ShowGlow("success","glow_success","glow_clients_saved_successfully")

                                // this.myLoader.close();

                                // if (this.CurrentItem == -1) {
                                //   d  //this.user.email = "";
                                //     //this.txtFirstName.val("");
                                //     //this.txtLastName.val("");
                                // }

                                // this.CurrentItem = iduser;
                                // //this.loadPeriods();
                                // //this.LoadStaff();

                                // this.LoadClient();
                                // //this.router.navigate(['clientes']);
                            }
                        }
                        else {
                            this.manageError(data);
                            this.saveActionDisplay.saved(false);
                            // this.myLoader.close();
                            // this.manageError(data);
                        }
                    },
                    error => {
                        // this.myLoader.close();
                        // this.manageError(error);
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

    emailValidator(email: string): boolean {
        var EMAIL_REGEXP = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!EMAIL_REGEXP.test(email)) {
            return false;
        }
        return true;
    }

    sourceStaff: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'projectColor', type: 'string' },
                { name: 'projectInfo', type: 'string' },
                { name: 'email', type: 'string' },
                { name: 'fullName', type: 'string' },
                { name: 'idfUser', type: 'number' },
                { name: 'positionName', type: 'string' },
                { name: 'group', type: 'string' },
                { name: 'hours', type: 'number' },
                { name: 'idfPosition', type: 'number' },
                { name: 'idfStaff', type: 'number' },
                { name: 'abm', type: 'string' },
                { name: 'img', type: 'string' },
            ],
            id: 'id',
            async: false
        }

    dataAdapterStaffListBox: any = new jqx.dataAdapter(this.sourceStaff);
    sourceStaffListList: any[] = [];
    dataAdapterStaffListBoxList: any[] = []; // new jqx.dataAdapter(this.sourceStaff);

    rendererListBoxStaff = (index, label, value): string => {
        if (this.sourceStaff.localData == undefined) {
            return null;
        }
        var datarecord = this.sourceStaff.localData[index];
        if (datarecord != undefined) {
            var imgurl = this.imagePipe.transform(datarecord.img, 'users');
            var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';
            var divSquareProject = "<span style='border:1px solid var(--thirteenth-color); height:35px; width:15px !important; background-color:" + datarecord.projectColor + ";'>&nbsp;&nbsp;</span>";
            var table = '<table border=0 style="min-width: 120px;"><tr>  <td style="width: 30px;" rowspan="3">' + img + '</td>     <td style="color:rgb(51, 51, 51); font-size:12px;">' + datarecord.fullName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + datarecord.positionName + '</td></tr>    <tr> <td style="color:rgb(51, 51, 51); font-size:10px;">' + divSquareProject + datarecord.projectInfo + '</td> </tr>       </table>';
            return table;
        }
        else {
            return label;
        }
    };

    //rendererListBoxStaff2 = (index, label, value): string => {


    //         if (index<0 || !label || this.sourceStaffListList[index].localData == undefined) {
    //             return null;
    //         }

    //         var datarecord = this.sourceStaffListList[index].localData;



    // return;

    //         if (datarecord != undefined) {



    //             var imgurl = '/media/images/users/' + datarecord.img + '.png';
    //             var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';

    //             var divSquareProject = "<span style='border:1px solid white; height:35px; width:15px !important; background-color:" + datarecord.projectColor + ";'>&nbsp;&nbsp;</span>";

    //             var table = '<table border=0 style="min-width: 120px;"><tr>  <td style="width: 30px;" rowspan="3">' + img + '</td>     <td style="color:rgb(51, 51, 51); font-size:12px;">' + datarecord.fullName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + datarecord.positionName + '</td></tr>    <tr> <td style="color:rgb(51, 51, 51); font-size:10px;">' + divSquareProject + datarecord.projectInfo + '</td> </tr>       </table>';
    //             return table;
    //         }
    //         else {
    //             return "";
    //             //return label;
    //         }

    //};

    LoadClient = (mustLoadOther: boolean = false) => {
        this.myLoader.open();
        this.ClientsServiceService.GetClient(this.CurrentItem)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        // this.selectAtLeastOne = false;
                        this.sourceStaff.localData = data.staff;
                        this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);
                        this.Client = <ClientModel>data.client;
                        if (this.CurrentItem >= 0) {
                            this.EnabledDisableForm();
                            // this.Client = <ClientModel>data.client;
                            // this.dataAdapterProjects = new jqx.dataAdapter(this.sourceProjectsListBox);                                               
                            this.ImagePath = this.imagePipe.transform(this.Client.img, 'clients');
                            this.tabsReference.enable();
                        }
                        else {
                            //this.Client = new ClientModel();
                            this.Client.id = -1;
                            this.ImagePath = this.imagePipe.transform("generic", 'clients');
                            // this.Client.active = true;
                            // this.Client.fullName = "";
                            // this.Client.idfImg = 0;
                            this.Client.img = "";
                            this.Client.state = "A";
                            this.tabsReference.disable();
                        }
                        this.sourceMedication.localData = data.reminders;
                        this.dataAdapterMedication = new jqx.dataAdapter(this.sourceMedication);
                        setTimeout(() => {
                            this.txtBirthDate.setDate(new Date(this.Client.birthDate));
                            if (mustLoadOther) {
                                this.loadClientData();
                            }
                        });
                        this.checkBoxUsePicture.val(false);
                    } else {
                        this.manageError(data);
                    }

                    this.loadedControl[0] = true;
                    this.HiddeLoaderWhenEnd();
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
            this.isEditing = this.CurrentItem >= 0;
            this.canDeleteOrSave = (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("clienteditor"));
            this.hiddenWhenIsNew = !this.isEditing;
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
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
            async: false
        }
    dataAdapterPeriod: any = new jqx.dataAdapter(this.sourcePeriodsListBox);

    PeriodSelectDrowDown = (event: any): void => {
        if (event.args.item.value == this.currentPeriod) {
            return;
        }
        this.currentPeriod = event.args.item.value;
        this.periodClosed = this.sourcePeriodsListBox.localData[event.args.index].state == "CL";
        this.EnabledDisableForm();
        this.loadClientData();
    }

    initWidgets = (tab: any): void => {
        switch (tab) {
            case 0:
                this.dataAdapterDailyLogs = new jqx.dataAdapter(this.sourceDailyLogs);
                if (this.gridDailyLog)
                    this.gridDailyLog.render();
            case 1:
                this.dataAdapterInjuries = new jqx.dataAdapter(this.sourceInjuries);
                if (this.gridIncidets)
                    this.gridIncidets.render();
                break;
            case 4:
                this.dataAdapterMedication = new jqx.dataAdapter(this.sourceMedication);
                if (this.gridMedication)
                    this.gridMedication.render();
                break;
        }
    }

    cosa6: number[] = [1, 2, 3];
    projectClient: any[] = []

    loadClientData = (): void => {
        this.loadedControl[2] = false;
        this.myLoader.open();
        this.ClientsServiceService.GetClientDataByPeriod(this.currentPeriod, this.CurrentItem)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.projectClient = data.projectClient;
                        this.sourceDailyLogs.localData = data.dailyLogsList;
                        this.dataAdapterDailyLogs = new jqx.dataAdapter(this.sourceDailyLogs);
                        this.sourceInjuries.localData = data.injuriesList;
                        this.dataAdapterInjuries = new jqx.dataAdapter(this.sourceInjuries);
                        let j = 0;

                        data.projectClient.forEach(element => {
                            this.sourceStaffListList.push({
                                dataType: "json",
                                dataFields: [
                                    { name: 'id', type: 'number' },
                                    { name: 'projectColor', type: 'string' },
                                    { name: 'projectInfo', type: 'string' },
                                    { name: 'email', type: 'string' },
                                    { name: 'fullName', type: 'string' },
                                    { name: 'idfUser', type: 'number' },
                                    { name: 'positionName', type: 'string' },
                                    { name: 'group', type: 'string' },
                                    { name: 'hours', type: 'number' },
                                    { name: 'idfPosition', type: 'number' },
                                    { name: 'idfStaff', type: 'number' },
                                    { name: 'abm', type: 'string' },
                                    { name: 'img', type: 'string' },
                                ],
                                id: 'id',
                                async: false
                            });

                            setTimeout(() => {
                                this.sourceStaffListList[j].localData = data.staffList.filter(c => c.projectInfo === element.description);


                                this.dataAdapterStaffListBoxList[j] = new jqx.dataAdapter(this.sourceStaffListList[j]);
                                j++;
                            });

                        });
                        setTimeout(() => {
                            this.projectClient.forEach(pc => {
                                if (pc.idSPP) {
                                    this.supervisorcmb.forEach((messageDirective: any) => {
                                        // debe ser 4347

                                        var item = messageDirective.getItemByValue(pc.idSPP);


                                        if (item) {
                                            messageDirective.selectItem(item);
                                        }
                                        //messageDirective.selectIndex(0);
                                        //auxCosa = messageDirective.viewContainerRef.createEmbeddedView(this.msgTempRef);
                                    });
                                }
                            });
                        });
                        //var item = auxCosa.getItemByValue(String(this.projectClient[i].idSPP));



                        //  setTimeout(() => {
                        //      this.supervisorcmb.forEach((messageDirective:any) => 
                        //     {
                        //         messageDirective.selectIndex(0);
                        //         //auxCosa = messageDirective.viewContainerRef.createEmbeddedView(this.msgTempRef);
                        //     });   
                        //  });





                        // this.dataAdapterDailyLogs = new jqx.dataAdapter(this.sourceIncidents);
                        // this.sourcePeriodsListBox.localData = data.periodsList;
                        // this.dataAdapterPeriod = new jqx.dataAdapter(this.sourcePeriodsListBox);
                    }
                    else {
                        this.manageError(data);
                    }

                    this.loadedControl[2] = true;
                    this.HiddeLoaderWhenEnd();
                    //this.myLoader.close();
                },
                error => {
                    //this.myLoader.close();
                    this.manageError(error);
                }
            );
    }

    gridReadyDailyLog = (): void => {
        //this.translate.get('projects_grid_subject').subscribe((res: string) => { this.gridReference.setcolumnproperty("subject","text",res); });                        
        // this.gridReference.localizestrings(this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en : this.jqxHelper.getGridLocation_es);
    }

    gridReadyInjuries = (): void => {
        // this.translate.get('projects_grid_subject').subscribe((res: string) => { this.gridReference.setcolumnproperty("subject","text",res); });                        
        // this.gridReference.localizestrings(this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en : this.jqxHelper.getGridLocation_es);
    }

    clickNewDailyLog(event: any): void {
        // let cr = this.gridIncidets.getrowdata(this.gridIncidets.selectedrowindex());
        // this.router.navigate(['clients/editclient/' + this.CurrentItem + "/editinjury/-1/" + this.currentPeriod]);        
        this.router.navigate(['clients/editclient/' + this.CurrentItem + "/editdailylog/-1/" + this.currentPeriod + ""]);
    }

    reminder: boolean = false;
    medication: string = "";
    time: Date = new Date();
    from: Date = new Date();
    to: Date = new Date();
    idSpp: number = 0;
    idmedication: number = 0;
    isAddingMedication: boolean = false;

    clickMedication(event: any): void {
        this.isAddingMedication = false;
        let cr: any = this.gridMedication.getrowdata(this.gridMedication.selectedrowindex());
        this.idmedication = cr.id;
        this.medication = cr.description;
        this.time = new Date(cr.datetime);
        this.from = new Date(cr.from);
        this.to = new Date(cr.to);
        this.checkBoxReminder.checked(cr.reminder == 1);
        this.txtTime.val(this.time);
        this.txtFrom.val(this.from);
        this.txtTo.val(this.to);

        let itemsStaff = this.editGenStaff.getItems().filter(x => x.value == cr.idfAssignedTo);
        if (itemsStaff != null && cr.id != 0) {
            this.editGenStaff.selectItem(itemsStaff[0]);
        }
        this.medicalWindow.open();
    }

    clickDailyLog(event: any): void {
        let cr = this.gridDailyLog.getrowdata(this.gridDailyLog.selectedrowindex());
        this.router.navigate(['clients/editclient/' + this.CurrentItem + "/editdailylog/" + cr.id + "/" + this.currentPeriod + ""]);
    }

    clickNewInjuries(event: any): void {
        // let cr = this.gridIncidets.getrowdata(this.gridIncidets.selectedrowindex());
        this.router.navigate(['clients/editclient/' + this.CurrentItem + "/editinjury/-1/" + this.currentPeriod]);
    }

    clickInjuries(event: any): void {
        let cr = this.gridIncidets.getrowdata(this.gridIncidets.selectedrowindex());
        this.router.navigate(['clients/editclient/' + this.CurrentItem + "/editinjury/" + cr.id + "/" + this.currentPeriod]);
        // this.router.navigate(['clients/editclient/' + this.CurrentItem + "/editinjury/", cr.id]);        
    }

    cosa = (event: any): void => {
    }

    OnGridGenericEventgridMedication = (event: any): void => {
        setTimeout(() => {
            this.gridMedication.render();
        }, 2000);
    }

    OnGridGenericEventgridDailyLog2 = (event: any): void => {
        setTimeout(() => {
            this.gridDailyLog.render();
        }, 2000);
    }

    OnGridGenericEventgridDailyLog = (event: any): void => {
        setTimeout(() => {
            this.gridDailyLog.render();
        });
    }

    OnGridGenericEventgridIncidents2 = (event: any): void => {
        setTimeout(() => {
            this.gridIncidets.render();
        }, 2000);
    }

    OnGridGenericEventgridIncidents = (event: any): void => {
        setTimeout(() => {
            this.gridIncidets.render();
        });
    }

    //    cellsrenderer: this.timeRendererMedication
    timeRendererMedication: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        if (this.sourceMedication.localData == undefined) {
            return "";
        }
        return "<div style='line-height:70px;'>" + this.dataAdapterMedication.formatDate(new Date(rowdata.datetime), "t") + "</div>";
    }

    FromRendererMedication: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        if (this.sourceMedication.localData == undefined) {
            return "";
        }
        return "<div style='line-height:70px;'>" + this.dataAdapterMedication.formatDate(new Date(rowdata.from), "d") + "</div>";
    }

    ToRendererMedication: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        if (this.sourceMedication.localData == undefined) {
            return "";
        }
        return "<div style='line-height:70px;'>" + this.dataAdapterMedication.formatDate(new Date(rowdata.to), "d") + "</div>";
    }

    editRendererMedication: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var salida = "<div class='tooltip'> " + "<div data-row='" + row + "' class='btneditmedication'></div>" + "<span class='tooltiptext'>Edit</span></div>";
        return salida;
    }

    editRendererDailyLog: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var salida = "<div class='tooltip'> " + "<div data-row='" + row + "' class='btneditdailylog'></div>" + "<span class='tooltiptext'>Edit</span></div>";
        return salida;
    }

    editRendererInjuries: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var salida = "<div class='tooltip'> " + "<div data-row='" + row + "' class='btneditincident'></div>" + "<span class='tooltiptext'>Edit</span></div>";
        return salida;
    }

    descriptionDailyLogRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        if (this.sourceDailyLogs.localData == undefined) {
            return null;
        }
        var divSquareProject = "<span style='border:1px solid white; height:15px; width:15px !important; background-color:" + rowdata.color + ";'>&nbsp;</span>";

        // + "Placement: " + rowdata.description
        var table = '<table border=0 style="margin-top:25px; width:100%; min-width: 120px;"><tr><td style="font-size:12px;">' + divSquareProject + rowdata.projectName + '</td></tr><tr><td style="heigth:10px !important; color:rgb(51, 51, 51); font-size:12px;">' + "" + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + '</td></tr></table>';
        return table;
    }

    descriptionInjuryRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        if (this.sourceInjuries.localData == undefined) {
            return null;
        }
        var divSquareProject = "<span style='border:1px solid white; height:15px; width:15px !important; background-color:" + rowdata.color + ";'>&nbsp;</span>";
        var table = '<table border=0 style="margin-top:15px; width:100%; min-width: 120px;"><tr><td style="font-size:12px;">' + divSquareProject + rowdata.projectName + '</td></tr><tr><td style="heigth:10px !important; color:rgb(51, 51, 51); font-size:12px;">' + "" + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + "Apparent Cause Of Injury: " + rowdata.description + '</td></tr></table>';
        return table;
    }

    IncidentInvolvedRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        // var salida ="<div class='tooltip'> "  + "<div data-row='" + row + "' class='btneditincident'></div>" +  "<span class='tooltiptext'>Edit</span></div>";
        // return salida;
        return rowdata.idfIncident > 0 ? "<div style='line-height:70px;'>Yes</div>" : "";
    }

    descriptionIncidentRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        if (this.sourceInjuries.localData == undefined) {
            return null;
        }

        var divSquareProject = "<span style='border:1px solid white; height:15px; width:15px !important; background-color:" + rowdata.color + ";'>&nbsp;&nbsp;</span>";
        var table = '<table  border=0 style="width:100%; min-width: 120px;"><tr><td style="font-size:12px;">' + divSquareProject + rowdata.projectName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:12px;">' + "&nbsp;" + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + rowdata.description + '</td></tr></table>';
        return table;
    }

    descriptionResponsableMedication: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        if (this.sourceMedication.localData == undefined) {
            return null;
        }

        var divSquareProject = "<span style='border:1px solid white; height:15px; width:15px !important; background-color:" + rowdata.color + ";'>&nbsp;&nbsp;</span>";
        var table = '<table  border=0 style="width:100%; min-width: 120px; margin-top:10px;"><tr><td style="font-size:12px;">' + divSquareProject + rowdata.projectName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:12px;">' + "" + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + rowdata.sppDescription + '</td></tr></table>';
        return table;
    }


    notificationMedication: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        if (this.sourceMedication.localData == undefined) {
            return null;
        }

        // var divSquareProject = "<span style='border:1px solid white; height:15px; width:15px !important; background-color:" + rowdata.color + ";'>&nbsp;&nbsp;</span>";
        // var table = '<table  border=0 style="width:100%; min-width: 120px; margin-top:10px;"><tr><td style="font-size:12px;">' + divSquareProject + rowdata.projectName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:12px;">' + "" + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' +  rowdata.sppDescription + '</td></tr></table>';
        // return table;
        return rowdata.reminder == 1 ? "<div style='line-height:70px;'><i class='ion ion-ios-notifications-outline' style='margin-top:20px !important; font-size:32px; color:var(--first-color);'></i></div>" : "";;
        // return "<i class='ion ion-ios-notifications-outline' style='padding:0px !important; font-size:16px; color:#465790;'></i>";
    }

    columnsMedication: any[] =
        [
            {
                width: '100px',
                text: '',
                datafield: 'Edit',
                height: '70px',
                menu: false,
                columntype: 'none',
                cellsrenderer: this.editRendererMedication,
            },
            {
                //notificationMedication
                text: '',
                height: '70px',
                menu: false,
                datafield: 'Reminder',
                width: '50px',
                cellsrenderer: this.notificationMedication
            }
            ,
            {
                text: 'Medication',
                datafield: 'description',
                width: 'auto',
                height: 'auto'
            },
            {
                text: 'Time',
                height: '70px',
                datafield: 'datetime',
                width: '60px',
                cellsrenderer: this.timeRendererMedication
            },
            {
                text: 'From',
                height: '70px',
                datafield: 'from',
                width: '90px',
                cellsrenderer: this.FromRendererMedication
            },
            {
                text: 'To',
                height: '70px',
                datafield: 'to',
                width: '90px',
                cellsrenderer: this.ToRendererMedication
            },
            {
                text: 'Assigned to / Responsable',
                height: '70px',
                datafield: 'SppDescription',
                width: '350px',
                cellsrenderer: this.descriptionResponsableMedication
            },
            {
                text: 'ABM',
                datafield: 'abm',
                width: '80px',
                height: '70px',
                hidden: true
            }
        ];

    columnsGridDailyLogs: any[] =
        [
            {
                width: '100px',
                text: '',
                datafield: 'Edit',
                height: '70px',
                menu: false,
                columntype: 'none',
                cellsrenderer: this.editRendererDailyLog,
            },
            {
                text: 'Date',
                datafield: 'dateDailyLog',
                width: '200px',
                height: '70px'
            },
            {
                text: 'Description',
                height: '70px',
                datafield: 'description',
                width: 'auto',
                cellsrenderer: this.descriptionDailyLogRenderer
            },
            {
                text: 'ABM',
                datafield: 'abm',
                width: '80px',
                height: '70px',
                hidden: true
            }
        ];

    columnsGridInjuries: any[] =
        [
            {
                width: '100px',
                text: '',
                menu: false,
                datafield: 'Edit',
                height: '70px',
                columntype: 'none',
                cellsrenderer: this.editRendererInjuries,
            },
            {
                text: 'Date',
                datafield: 'dateInjury',
                width: '200px',
                height: '70px'
            },
            {
                text: 'Description',
                height: '70px',
                datafield: 'description',
                width: 'auto',
                cellsrenderer: this.descriptionInjuryRenderer
            },
            {
                text: 'Incident Involved',
                height: '70px',
                datafield: 'idfIncident',
                width: '140px',
                cellsrenderer: this.IncidentInvolvedRenderer
            },
            {
                text: 'ABM',
                datafield: 'abm',
                width: '140px',
                height: '70px',
                hidden: true
            }
        ];

    sourceDailyLogs: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' },
                { name: 'dateDailyLog', type: 'string' },
                { name: 'color', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'abm', type: 'string' }
            ],
            id: 'id',
        }

    dataAdapterDailyLogs: any = new jqx.dataAdapter(this.sourceDailyLogs);
    sourceInjuries: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' },
                { name: 'dateInjury', type: 'string' },
                { name: 'color', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'abm', type: 'string' },
                { name: 'idfIncident', type: 'number' }
            ],
            id: 'id',
        }

    dataAdapterInjuries: any = new jqx.dataAdapter(this.sourceInjuries);
    sourceMedication: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'idfClient', type: 'number' },
                { name: 'idfAssignedTo', type: 'number' },
                { name: 'description', type: 'string' },
                { name: 'datetime', type: 'Date' },
                { name: 'from', type: 'Date' },
                { name: 'to', type: 'Date' },
                { name: 'reminder', type: 'number' },
                { name: 'state', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'color', type: 'string' },
                { name: 'sppDescription', type: 'string' },
                { name: 'abm', type: 'string' }
            ],
            id: 'id',
        }

    dataAdapterMedication: any = new jqx.dataAdapter(this.sourceMedication);
    //dataAdapterStaffList_1:any;
    gridRowselectInjuries = (event: any): void => {
        // let abmValue = "";
        // if(event)
        // {
        //     var args = event.args;   
        //     var rowData = args.row; 
        //     abmValue = rowData.abm;
        // }
        // else{

        //     if(this.gridDailyLog.getselectedrowindex()>-1)
        //     {
        //         let currentRow = this.gridDailyLog.getrowdata(this.gridDailyLog.getselectedrowindex());  
        //         abmValue = currentRow.abm;
        //     }            
        // }

        this.gridIncidets.hidecolumn("Edit");
        this.gridIncidets.showcolumn("Edit");
    };


    gridRowselectDailyLog = (event: any): void => {
        this.gridDailyLog.hidecolumn("Edit");
        this.gridDailyLog.showcolumn("Edit");
    };

    gridRowselectMedication = (event: any): void => {
        this.gridMedication.hidecolumn("Edit");
        this.gridMedication.showcolumn("Edit");
    };

    renderedGridMedication = (): void => {
        function flatten(arr: any[]): any[] {
            if (arr.length) {
                return arr.reduce((flat: any[], toFlatten: any[]): any[] => {
                    return flat.concat(Array.isArray(toFlatten) ? flatten(toFlatten) : toFlatten);
                }, []);
            }
        }

        setTimeout(() => {
            if (document.getElementsByClassName("btneditmedication").length > 0) {
                //let Buttons = jqwidgets.createInstance(".btneditdailylog", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-undo' style='padding:0px !important; font-size:16px; color:white;'></i>" + ''   , template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });
                let Buttons = jqwidgets.createInstance(".btneditmedication", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-more' style='padding:0px !important; font-size:16px; color:var(--thirteenth-color);'></i>" + '', template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });
                let flattenButtons = flatten(Buttons.length ? Buttons : [Buttons]);
                if (flattenButtons) {
                    for (let i = 0; i < flattenButtons.length; i++) {
                        flattenButtons[i].removeEventHandler('click');
                        flattenButtons[i].addEventHandler('click', (event: any): void => {
                            this.clickMedication(event);
                        });
                    }
                }
            }
        });

    };

    renderedGridDailyLog = (): void => {
        // if(!this.readygrid)
        // {       
        //     return;
        // }
        function flatten(arr: any[]): any[] {
            if (arr.length) {
                return arr.reduce((flat: any[], toFlatten: any[]): any[] => {
                    return flat.concat(Array.isArray(toFlatten) ? flatten(toFlatten) : toFlatten);
                }, []);
            }
        }

        setTimeout(() => {
            if (document.getElementsByClassName("btneditdailylog").length > 0) {
                //let Buttons = jqwidgets.createInstance(".btneditdailylog", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-undo' style='padding:0px !important; font-size:16px; color:white;'></i>" + ''   , template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });
                let Buttons = jqwidgets.createInstance(".btneditdailylog", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-more' style='padding:0px !important; font-size:16px; color:var(--thirteenth-color);'></i>" + '', template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });
                let flattenButtons = flatten(Buttons.length ? Buttons : [Buttons]);
                if (flattenButtons) {
                    for (let i = 0; i < flattenButtons.length; i++) {
                        flattenButtons[i].removeEventHandler('click');
                        flattenButtons[i].addEventHandler('click', (event: any): void => {
                            this.clickDailyLog(event);
                        });
                    }
                }
            }
        });
    };

    renderedGridInjuries = (): void => {
        function flatten(arr: any[]): any[] {
            if (arr.length) {
                return arr.reduce((flat: any[], toFlatten: any[]): any[] => {
                    return flat.concat(Array.isArray(toFlatten) ? flatten(toFlatten) : toFlatten);
                }, []);
            }
        }

        setTimeout(() => {
            if (document.getElementsByClassName("btneditincident").length > 0) {
                let Buttons = jqwidgets.createInstance(".btneditincident", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-more' style='padding:0px !important; font-size:16px; color:var(--thirteenth-color);'></i>" + '', template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });
                let flattenButtons = flatten(Buttons.length ? Buttons : [Buttons]);
                if (flattenButtons) {
                    for (let i = 0; i < flattenButtons.length; i++) {
                        flattenButtons[i].removeEventHandler('click');
                        flattenButtons[i].addEventHandler('click', (event: any): void => {
                            this.clickInjuries(event);
                        });
                    }
                }
            }
        });
    };

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
                    }
                    else {
                        this.manageError(data);
                    }

                    this.loadedControl[1] = true;
                    this.HiddeLoaderWhenEnd();
                    //this.myLoader.close();
                },
                error => {
                    //this.myLoader.close();
                    this.manageError(error);
                }
            );
    }


    sourceStaffList: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'fullName', type: 'string' },
                { name: 'positionName', type: 'string' },
                { name: 'email', type: 'string' },
                { name: 'idfUser', type: 'number' },
                { name: 'group', type: 'string' }

            ],
            id: 'id',
        }

    //dataAdapterStaffList: any; // = new jqx.dataAdapter(this.sourceOwnerList);

    dataAdapterStaffList: any;

    EnabledDisableForm = (): void => {
        //let disabled = this.projectClosed || !this.canDeleteOrSaveProject();// || this.periodClosed;
        //this.projectname.setOptions({ disabled: disabled });
        //this.projectdescription.setOptions({ disabled: disabled });
        //this.myDropDown.setOptions({ disabled: disabled });
        //this.dateTimeFrom.setOptions({ disabled: disabled });
        //this.dateTimeTo.setOptions({ disabled: disabled });
        //this.cmbOwners.setOptions({ disabled: disabled });
    }

    clickNewMedical = (event: any) => {
        this.medicalWindow.open();
        this.isAddingMedication = true;
        this.medication = "";
        this.time = new Date();
        this.from = new Date();
        this.to = new Date();
        this.checkBoxReminder.checked(true);
        this.txtTime.val(this.time);
        this.txtFrom.val(this.from);
        this.txtTo.val(this.to);
        this.txtMedication.val(this.medication);
    }
}
