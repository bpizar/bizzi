import { Component, ViewChild, AfterViewInit, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqxdropdownlist';
import { jqxComboBoxComponent } from 'jqwidgets-ng/jqxcombobox';
import { jqxGridComponent } from 'jqwidgets-ng/jqxgrid';
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { jqxButtonComponent } from 'jqwidgets-ng/jqxbuttons';
import { jqxInputComponent } from 'jqwidgets-ng/jqxinput';
import { jqxListBoxComponent } from 'jqwidgets-ng/jqxlistbox';
import { jqxTabsComponent } from 'jqwidgets-ng/jqxtabs';
import { ConstantService } from '../../../common/services/app.constant.service';
import { UploadService } from '../../../common/services/app.upload.service';
import { ClientsService } from '../../clients.service';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqxdatetimeinput';
import { AuthHelper } from '../../../common/helpers/app.auth.helper';
import { GlowMessages } from '../../../common/components/glowmessages/glowmessages.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { jqxCheckBoxComponent } from "jqwidgets-ng/jqxcheckbox";
import { HttpUrlEncodingCodec } from '@angular/common/http';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { SignaturePad } from 'angular2-signaturepad/signature-pad';
import { SaveActionDisplay } from '../../../common/components/saveActionDisplay/saveactiondisplay.component';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';
import * as moment from 'moment';
import * as momenttimezone from 'moment-timezone'

@Component({
    selector: 'editinjury',
    templateUrl: '../../../clients/editclient/editinjury/editinjury.component.template.html',
    providers: [ConstantService, UploadService, AuthHelper, ClientsService],    // SchedulingService, ProjectsService,ClientsService, 
    styleUrls: ['../../../clients/editclient/editinjury/editinjury.component.css'],
})

export class EditInjury implements OnInit, OnDestroy, AfterViewInit {

    @ViewChild('SignaturePad') signaturePad: SignaturePad;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('cmbDegreeOfInjury') cmbDegreeOfInjury: jqxListBoxComponent;
    @ViewChild('cmbSupervisor') cmbSupervisor: jqxComboBoxComponent;
    @ViewChild('listBox_g4') listBox_g4: jqxListBoxComponent;
    @ViewChild('listBox_g5') listBox_g5: jqxListBoxComponent;
    @ViewChild('listBox_g10') listBox_g10: jqxListBoxComponent;
    @ViewChild('cmbProjects') cmbProjects: jqxDropDownListComponent;
    @ViewChild(SaveActionDisplay) saveActionDisplay: SaveActionDisplay;
    @ViewChild('dtTimeOfInjury') dtTimeOfInjury: jqxDateTimeInputComponent;
    sourceProjects: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' },
                { name: 'color', type: 'string' }
            ],
            id: 'id',
            async: false
        }

    dataAdapterProjects: any;
    bodyPoints: any[][];
    loadedControl: boolean[] = [false, false, false]; // 0  client   1  period  2 clientdata  
    ImagePath: String = "";
    imgpathbody: string = "assets/humanOk.jpg";
    h_injuries: any = [];
    catalog: any[];
    clientName: string = "";
    //dateTimeOfInjury:Date;
    dateReportedSupervisor: Date;
    st13_u: string = ""; // other g4
    st14_u: string = ""; // other g5
    st15_u: string = ""; // other g10
    st16_u: string = ""; // apparent cause of injury
    st17_u: string = ""; // supervisor comments
    st18_u: string = ""; // Physician comments
    st19_u: string = ""; // treatment prescribed
    st20_u: string = ""; // Name of witnesses
    CurrentItem: number = -1;
    CurrentClientId: number = -1;
    CurrentPeriodId: number = -1;
    signaturePadOptions: Object = {
        'minWidth': 1,
        'maxWidth': 2,
        'canvasWidth': 618,
        'canvasHeight': 515,
        'penColor': 'red',
        'dotSize': 0.5
    };
    canDeleteOrSave: boolean;
    imagePipe = new ImagePipe();
    isPrinting: boolean = false;
    sourceListBox_g4: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g4: any = new jqx.dataAdapter(this.sourceListBox_g4);

    sourceListBox_g5: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g5: any = new jqx.dataAdapter(this.sourceListBox_g5);

    sourceListBox_g10: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g10: any = new jqx.dataAdapter(this.sourceListBox_g10);

    sourceDegreeIncident: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' }
            ],
            id: 'id',
            async: false
        }

    dataAdapterDegreeIncident: any;

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

    dataAdapterStaffList: any;
    private sub: any;
    calling: boolean = false;

    constructor(private translate: TranslateService,
        private ClientsServiceService: ClientsService,
        private constantService: ConstantService,
        private authHelper: AuthHelper,
        private route: ActivatedRoute,
        private router: Router,
        private uploadService: UploadService,
        private chRef: ChangeDetectorRef) {
        this.translate.setDefaultLang('en');
    }

    back = (event: any) => {
        this.router.navigate(['clients/editclient', this.CurrentClientId]);
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

    onMustSave = (event: any): void => {
        this.Save(null);
    }

    drawComplete() {
        this.bodyPoints = this.signaturePad.toData();
    }

    drawStart() {
        // will be notified of szimek/signature_pad's onBegin event
    }

    cmbTypeOfInjuryBindingComplete = (event: any) => {
        if (this.sourceListBox_g4.localdata != undefined) {
            this.listBox_g4.beginUpdate();
            if (this.sourceListBox_g4.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g4.localdata //.filter(c => c.identifierGroup=="g1" && c.value==="true")
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g4.checkIndex(index);
                        }
                    });
            }
            this.listBox_g4.endUpdate();
        }
    }

    cmbBodyPartInjuryBindingComplete = (event: any) => {
        if (this.sourceListBox_g5.localdata != undefined) {
            this.listBox_g5.beginUpdate();
            if (this.sourceListBox_g5.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g5.localdata //.filter(c => c.identifierGroup=="g1" && c.value==="true")
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g5.checkIndex(index);
                        }
                    });
            }
            this.listBox_g5.endUpdate();
        }
    }

    cmbPlaceOfOccurrenceBindingComplete = (event: any) => {
        if (this.sourceListBox_g10.localdata != undefined) {
            this.listBox_g10.beginUpdate();
            if (this.sourceListBox_g10.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g10.localdata //.filter(c => c.identifierGroup=="g1" && c.value==="true")
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g10.checkIndex(index);
                        }
                    });
            }
            this.listBox_g10.endUpdate();
        }
    }

    cmbDegreeOfInjuryBindingComplete = (event: any) => {
        setTimeout(() => {
            if (this.h_injuries) {
                this.cmbDegreeOfInjury.selectItem(this.cmbDegreeOfInjury.getItemByValue(String(this.h_injuries.idfDegreeOfInjury)));
            }
        });
    }

    BindingCompleteCmbSupervisor = (event: any) => {
        setTimeout(() => {
            if (this.h_injuries && this.h_injuries.idfSupervisor) {
                this.cmbSupervisor.selectItem(this.cmbSupervisor.getItemByValue(this.h_injuries.idfSupervisor));
            }
        });
    }

    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    removeLastPoint = (): void => {
        if (this.bodyPoints.length > 0 && this.canDeleteOrSave) {
            this.bodyPoints.pop();
            this.signaturePad.clear();
            this.signaturePad.fromData(this.bodyPoints);

            this.onAnyChange(null, 'other');
        }
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
            this.ImagePath = this.imagePipe.transform('generic', 'clients');
            this.LoadInjury();
            if (!this.canDeleteOrSave) {
                this.signaturePad.off();
            }
        });
    }

    isEditing = (): boolean => {
        return false;
    }

    validate = (): boolean => {
        let result = true;
        if (this.cmbDegreeOfInjury.getSelectedIndex() < 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_injury_degree");
            result = false;
        }
        if (this.cmbSupervisor.getSelectedIndex() < 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_injury_supervisor");
            result = false;
        }
        // st16_u
        if (this.st16_u.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_injury_apparentcauseofinjury");
            result = false;
        }
        if (this.cmbProjects.getSelectedIndex() < 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_injury_project");
            result = false;
        }
        return result;
    }

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    Save(event: any): void {
        if (this.validate()) {
            this.h_injuries.projectId = this.cmbProjects.getSelectedItem().value;
            //this.myLoader.open();
            //this.h_injuries.dateOfInjury = this.dateTimeOfInjury;
            this.h_injuries.dateOfInjury = this.dtTimeOfInjury.getDate();
            this.h_injuries.idfDegreeOfInjury = this.cmbDegreeOfInjury.getSelectedItem().value;
            this.h_injuries.idfSupervisor = this.cmbSupervisor.getSelectedItem().value;
            this.h_injuries.dateReportedSupervisor = new Date(this.dateReportedSupervisor);
            // 
            this.catalog.forEach(element => {
                element.value = "false";
            });
            var checked_sourceListBox_g4 = this.listBox_g4.getCheckedItems();
            checked_sourceListBox_g4.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });
            // this.catalog.filt
            this.catalog.filter(c => c.id == "st13_u")[0].value = this.st13_u;
            // Body part injured
            var checked_sourceListBox_g5 = this.listBox_g5.getCheckedItems();
            checked_sourceListBox_g5.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });
            this.catalog.filter(c => c.id == "st14_u")[0].value = this.st14_u;
            // place of ocurrence
            var checked_sourceListBox_g10 = this.listBox_g10.getCheckedItems();
            checked_sourceListBox_g10.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });

            this.catalog.filter(c => c.id == "st15_u")[0].value = this.st15_u;
            this.catalog.filter(c => c.id == "st16_u")[0].value = this.st16_u;
            this.catalog.filter(c => c.id == "st17_u")[0].value = this.st17_u;
            this.catalog.filter(c => c.id == "st18_u")[0].value = this.st18_u;
            this.catalog.filter(c => c.id == "st19_u")[0].value = this.st19_u;
            this.catalog.filter(c => c.id == "st20_u")[0].value = this.st20_u;

            this.h_injuries.idfPeriod = this.CurrentPeriodId;
            this.h_injuries.idfClient = this.CurrentClientId;

            if (this.CurrentItem > -1) {
                this.h_injuries.id = this.CurrentItem;
            }

            let saveInjuryRequest = {
                Injury: this.h_injuries,
                Catalog: this.catalog,
                Points: this.bodyPoints,
                TimeDifference: this.getTimeZone()
            };


            if (this.calling && this.h_injuries.id <= 0) {
                return;
            }

            this.calling = true;

            this.ClientsServiceService.SaveInjury(saveInjuryRequest)
                .subscribe(
                    (data: any) => {
                        //this.myLoader.close();
                        if (data.result) {
                            if (this.CurrentItem == -1) {
                                this.glowMessage.ShowGlow("success", "glow_success", "glow_injury_saved_successfully");
                                //this.CurrentItem = data.tagInfo.split('-')[0];
                                this.CurrentItem = data.tagInfo;
                                this.h_injuries.state = "C";
                            }
                            // this.CurrentItem =!isEditing ? data.tagInfo.split('-')[0] : this.CurrentItem;
                            this.saveActionDisplay.saved(true);
                        }
                        else {
                            this.manageError(data);
                            this.saveActionDisplay.saved(false);
                        }
                    },
                    error => {
                        this.myLoader.close();
                        this.manageError(error);
                        this.saveActionDisplay.saved(false);
                    });
        }
        else {
            this.saveActionDisplay.saved(false);
        }
    }


    cmbProjectsBindingComplete = (event: any) => {
        setTimeout(() => {
            if (this.h_injuries) {
                this.cmbProjects.selectItem(this.cmbProjects.getItemByValue(String(this.h_injuries.projectId)));
            }

            setTimeout(() => {
                this.loadedControl[1] = true;
                this.HiddeLoaderWhenEnd();
            });

        });
    }

    getTimeZone = (): number => {
        let aux: any = momenttimezone.tz(Intl.DateTimeFormat().resolvedOptions().timeZone);
        return Number(aux._offset / 60);
    }

    LoadInjury = () => {
        this.myLoader.open();
        this.ClientsServiceService.GetInjuryById(this.CurrentItem, this.CurrentPeriodId, this.CurrentClientId, this.getTimeZone())
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.sourceProjects.localdata = data.projects;
                        this.dataAdapterProjects = new jqx.dataAdapter(this.sourceProjects);
                        if (this.signaturePad)
                            this.signaturePad.clear();
                        this.bodyPoints = data.points;
                        this.ImagePath = this.imagePipe.transform(data.clientImg, 'clients');
                        this.clientName = data.clientName;
                        // catalog main
                        this.catalog = data.catalog;
                        // Injury
                        this.h_injuries = data.injury;
                        // Degree
                        this.sourceDegreeIncident.localdata = data.degreeOfInjuryList;
                        this.dataAdapterDegreeIncident = new jqx.dataAdapter(this.sourceDegreeIncident);
                        // this.dateTimeOfInjury = new Date(data.injury.dateOfInjury);
                        this.dtTimeOfInjury.setDate(this.CurrentItem < 0 ? new Date(data.currentDateTime) : new Date(data.injury.dateOfInjury));
                        this.dateReportedSupervisor = new Date(data.injury.dateReportedSupervisor);
                        this.sourceStaffList.localdata = data.staffs;
                        this.dataAdapterStaffList = new jqx.dataAdapter(this.sourceStaffList);
                        this.sourceListBox_g4.localdata = data.catalog.filter(c => c.identifierGroup == "g4").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });
                        this.sourceListBox_g5.localdata = data.catalog.filter(c => c.identifierGroup == "g5").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });
                        this.sourceListBox_g10.localdata = data.catalog.filter(c => c.identifierGroup == "g10").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });

                        setTimeout(() => {
                            this.dataAdapterListBox_g4 = new jqx.dataAdapter(this.sourceListBox_g4);
                            this.dataAdapterListBox_g5 = new jqx.dataAdapter(this.sourceListBox_g5);
                            this.dataAdapterListBox_g10 = new jqx.dataAdapter(this.sourceListBox_g10);
                            if (this.signaturePad)
                                this.signaturePad.fromData(this.bodyPoints);
                            var groupTextAreas = data.catalog.filter(c => c.identifierGroup == "st").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });
                            groupTextAreas.forEach(element => {
                                this.st13_u = element.id === "st13_u" ? element.value : this.st13_u;
                                this.st14_u = element.id === "st14_u" ? element.value : this.st14_u;
                                this.st15_u = element.id === "st15_u" ? element.value : this.st15_u;
                                this.st16_u = element.id === "st16_u" ? element.value : this.st16_u;
                                this.st17_u = element.id === "st17_u" ? element.value : this.st17_u;
                                this.st18_u = element.id === "st18_u" ? element.value : this.st18_u;
                                this.st19_u = element.id === "st19_u" ? element.value : this.st19_u;
                                this.st20_u = element.id === "st20_u" ? element.value : this.st20_u;
                            });

                            setTimeout(() => {
                                this.loadedControl[0] = true;
                                this.loadedControl[1] = true;
                                this.loadedControl[2] = true;
                                this.HiddeLoaderWhenEnd();
                            });

                        });
                    } else {
                        this.manageError(data);
                    }
                    // this.loadedControl[0] = true;
                    // this.HiddeLoaderWhenEnd();
                },
                error => {
                    this.manageError(error);
                });
    }

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(params => {
            this.CurrentItem = +params['id'];
            this.CurrentClientId = +params['idclient'];
            this.CurrentPeriodId = +params['idperiod'];
            this.canDeleteOrSave = (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("clienteditor"));
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
}