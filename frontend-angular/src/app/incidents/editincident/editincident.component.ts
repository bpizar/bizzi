import { Component, ViewChild, AfterViewInit, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
//import { ClientModel } from './editclient.component.model';
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
import { IncidentsService } from '../incidents.service';

import { SelectClient } from '../../clients/selectclients/selectclient.component';

// import { jqxColorPickerComponent } from 'jqwidgets-ng/jqwidgets/jqxcolorpicker';

import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';

// import { jqxNot//ificationComponent } from '../../../../node_modules/jqwidgets-ng/jqwidgets/jqxnotification';

import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqwidgets/jqxdatetimeinput';

// import { SchedulingService } from '../../scheduling/scheduling.service'; 

import { AuthHelper } from '../../common/helpers/app.auth.helper';

// import { ProjectsService } from '../../projects/projects.service'; 

import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';

import { ImageCropperModule } from 'ngx-image-cropper';
import { jqxCheckBoxComponent } from "jqwidgets-ng/jqwidgets/jqxcheckbox";
import { HttpUrlEncodingCodec } from '@angular/common/http';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { group } from '@angular/animations';
import { isBoolean } from 'util';
//import { SchedulingService } from '../scheduling/scheduling.service';
import { SaveActionDisplay } from '../../common/components/saveActionDisplay/saveactiondisplay.component';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';

@Component({
    selector: 'editincident',
    templateUrl: '../../incidents/editincident/editincident.component.template.html',
    providers: [ConstantService, UploadService, AuthHelper, IncidentsService],    // SchedulingService, ProjectsService,ClientsService, 
    styleUrls: ['../../incidents/editincident/editincident.component.css'],
})

export class EditIncident implements OnInit, OnDestroy, AfterViewInit {
    loadedControl: boolean[] = [false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false];

    // 0 = incident 
    // 1 BindingCompleteCmbSafetyRiskTo  
    // 2 UmabInterventionBindingComplete
    // 3 listBoxBindingCompleteG1
    // 4 listBoxBindingCompleteG2
    // 5 listBoxBindingCompleteG3
    // 6 BindingCompleteCmbStaffInvolved
    // 7 BindingCompleteCmbStaffResponsible
    // 8 MinistryBindingComplete
    // 9 RegionBindingComplete
    // 10 TypeOfSeriousOccurrenceBindingComplete
    // 11 BindingCompleteCmbByWhom
    // 12 listBoxBindingCompleteG6 
    // 13 listBoxBindingCompleteG7
    // 14 listBoxBindingCompleteG8
    // 15 listBoxBindingCompleteG9



    //@ViewChild('checkBoxUsePicture') checkBoxUsePicture: jqxCheckBoxComponent;  
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    //@ViewChild('txtFirstName') txtFirstName: jqxInputComponent;
    //@ViewChild('txtLastName') txtLastName: jqxInputComponent;
    @ViewChild('listBox_g1') listBox_g1: jqxListBoxComponent;
    @ViewChild('listBox_g2') listBox_g2: jqxListBoxComponent;
    @ViewChild('listBox_g3') listBox_g3: jqxListBoxComponent;
    @ViewChild('listBox_g6') listBox_g6: jqxListBoxComponent;
    @ViewChild('listBox_g7') listBox_g7: jqxListBoxComponent;
    @ViewChild('listBox_g8') listBox_g8: jqxListBoxComponent;
    @ViewChild('listBox_g9') listBox_g9: jqxListBoxComponent;


    @ViewChild('cmbSafetyRiskTo') cmbSafetyRiskTo: jqxListBoxComponent;
    @ViewChild('cmbStaffInvolved') cmbStaffInvolved: jqxListBoxComponent;
    @ViewChild('cmbStaffResponsible') cmbStaffResponsible: jqxListBoxComponent;



    @ViewChild('cmbByWhom_t1') cmbByWhom_t1: jqxListBoxComponent;

    //@ViewChild('txtemail') txtemail: jqxInputComponent;
    //    <jqxDateTimeInput #txtBirthDate
    //@ViewChild('txtBirthDate') txtBirthDate: jqxDateTimeInputComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('typeDropDown') typeDropDown: jqxListBoxComponent;
    //@ViewChild('gridDailyLog') gridDailyLog: jqxGridComponent;
    @ViewChild(SelectClient) selectClient: SelectClient;

    @ViewChild('listBoxClients') public listBoxClients: jqxListBoxComponent;



    @ViewChild('checkIfSeriousOcurrence') public checkIfSeriousOcurrence: jqxCheckBoxComponent;

    @ViewChild('cmbTypeOfSeriousOccurrence') public cmbTypeOfSeriousOccurrence: jqxDropDownListComponent;

    @ViewChild('cmbMinistry') public cmbMinistry: jqxDropDownListComponent;

    //
    @ViewChild('cmbRegion') public cmbRegion: jqxDropDownListComponent;


    @ViewChild('cmbUmabIntervention') public cmbUmabIntervention: jqxDropDownListComponent;


    @ViewChild('listBoxInjuries') public listBoxInjuries: jqxListBoxComponent;
    @ViewChild(SaveActionDisplay) saveActionDisplay: SaveActionDisplay;

    imagePipe = new ImagePipe();
    incident: any = [];
    catalog: any[];


    //@ViewChild('cmbTypeOfSeriousOccurrence') public cmbTypeOfSeriousOccurrence: jqxDropDownListComponent;

    // 

    // 
    //@ViewChild('cmbSafetyRiskTo') public cmbSafetyRiskTo: jqxComboBoxComponent;


    dateTimeWhenSeriousOccurrence: Date;

    typeOfSeriousOccurrence: number = 0;


    sc1_u: boolean = false; // physical required
    sc2_u: boolean = false; // coroner notified
    sc2_u_aux: boolean = false; // coroner notified
    sc3_u: boolean = false; // coroner notified
    sc4_u: boolean = false; // CAS
    sc5_u: boolean = false; // Other , who has been notifies


    st1_u: string = ""; // detailed description
    st2_u: string = ""; // describe injury 
    st3_u: string = "";
    st4_u: string = "";
    st5_u: string = "";
    st6_u: string = "";
    st7_u: string = "";
    st8_u: string = "";
    st9_u: string = "";
    st10_u: string = "";
    st11_u: string = "";
    st12_u: string = "";
    st21_u: string = "";
    st22_u: string = "";

    involvedPeople: any[] = null;
    isSeriousOcurrence: boolean = false;
    idfMinistry: number = -1;
    idfRegion: number = -1;
    idfTypeOfSeriousOccurrence: number = -1;
    idfUmabIntervention: number = -1;

    hiddenDeleteClientButton: boolean = true;
    hiddenUndoClientButton: boolean = true;

    hiddenDeleteInjuriesButton: boolean = true;
    hiddenUndoInjuriesButton: boolean = true;



    dateTimeIncident: Date;

    //currentPeriod: number = 0;
    ///periodClosed: boolean = false;
    // @ViewChild('txtBirthDate') txtBirthDate: jqxti; 
    // msgError: string = "";
    //msgSuccess: string = "";    
    ImagePath: String = "";

    // placeHolderEmail:string="";
    // placeHolderFirstName:string="";
    // placeHolderLastName:string="";
    // placeHolderBirthDate:string="";
    // placeHolderPhoneNumber:string="";
    // placeHolderNotes:string="";

    idIncident: number = -1;
    idPeriod: number = -1;
    // idClient:number = -1;

    private sub: any;

    constructor(private translate: TranslateService,
        private incidentsService: IncidentsService,
        private constantService: ConstantService,
        // private projectsService: ProjectsService,
        // private schedulingService: SchedulingService,
        private authHelper: AuthHelper,
        private route: ActivatedRoute,
        private router: Router,
        private uploadService: UploadService,
        private chRef: ChangeDetectorRef) {
        this.translate.setDefaultLang('en');
    }

    // Crop image       
    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    initWidgets = (tab: any): void => {
        switch (tab) {
            case 0:
            // this.dataAdapterDailyLogs = new jqx.dataAdapter(this.sourceDailyLogs);
            // this.gridDailyLog.render();
            case 1:
                this.dataAdapterMinistryList = new jqx.dataAdapter(this.sourceMinistryList);
                this.dataAdapterRegionList = new jqx.dataAdapter(this.sourceRegionList);
                this.dataAdapterTypeSeriousOccurrenceList = new jqx.dataAdapter(this.sourceTypeSeriousOccurrenceList);

                // this.checkIfSeriousOcurrence.check
                // this.dataAdapterIncidents = new jqx.dataAdapter(this.sourceIncidents);
                // this.listBoxClients.render();
                // this.gridIncidets.render();
                // this.dataAdapterTypeIncident = new jqx.dataAdapter(this.sourceTypeIncident);
                // this.dataAdapterDegreeIncident = new jqx.dataAdapter(this.sourceDegreeIncident);
                // this.dataAdapterListBox_g4 = new jqx.dataAdapter(this.sourceListBox_g4);
                // this.dataAdapterListBox_g5 = new jqx.dataAdapter(this.sourceListBox_g5);
                // this.typeDropDown.render();
                break;
        }
    }

    ngAfterViewInit(): void {

        setTimeout(() => {
            this.translate.use('en');
            // this.loadPeriods();
            // this.LoadClient();
            this.ImagePath = this.imagePipe.transform('generic', 'clients'); '/media///generic.png';
            // this.translate.get('clients_edit_email_placeholder').subscribe((res: string) => { 
            //     this.placeHolderEmail = res;
            //  });              
        });

        //if(this.idIncident>=0)
        //{
        this.LoadIncident();
        // }
        // else
        // {
        //  this.loadedControl[0] = true;
        //}


    }

    isEditing = (): boolean => {    //return false;
        return this.idIncident >= 0;
    }

    validate = (): boolean => {
        let result = true;


        if (this.cmbUmabIntervention.getSelectedIndex() < 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_incident_select_umab");
            result = false;
        }

        if (this.st1_u.length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_incident_type_desc");
            result = false;
        }

        return result;
    }




    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    onMustSave = (event: any): void => {
        this.Save(null);
    }

    //loaded:boolean=false;

    onAnyChange = (event: any, type: string = "-"): void => {
        var args = event ? event.args : null;
        if (args || type == "check" || type == "other") {
            let control = true;

            //let i=0;


            this.loadedControl.forEach(element => {
                control = control && element;
                //  i++;
            });

            if (control && this.saveActionDisplay && this.canDeleteOrSave) {
                //  if(!this.loaded)
                //  {
                //      this.loaded = true;
                //      return;    
                //  }


                this.saveActionDisplay.setDirty();
            }
        }
        // else
        // {
        // }
    }


    calling: boolean = false;

    Save(event: any): void {
        if (this.validate()) {

            // Is serious occurrence
            this.incident.isSeriousOcurrence = this.isSeriousOcurrence ? 1 : 0;

            // date time incident
            this.incident.dateIncident = this.dateTimeIncident;

            // date time when serious occurrence
            this.incident.dateTimeWhenSeriousOccurrence = this.dateTimeWhenSeriousOccurrence;

            this.catalog.forEach(element => {
                element.value = "false";
            });

            this.catalog.filter(c => c.id == "sc1_u")[0].value = this.sc1_u;
            this.catalog.filter(c => c.id == "st21_u")[0].value = this.st21_u;
            this.catalog.filter(c => c.id == "st22_u")[0].value = this.st22_u;
            this.catalog.filter(c => c.id == "st6_u")[0].value = this.st6_u;


            this.catalog.filter(c => c.id == "st1_u")[0].value = this.st1_u;
            this.catalog.filter(c => c.id == "st2_u")[0].value = this.st2_u;
            this.catalog.filter(c => c.id == "st3_u")[0].value = this.st3_u;
            this.catalog.filter(c => c.id == "st4_u")[0].value = this.st4_u;
            this.catalog.filter(c => c.id == "st5_u")[0].value = this.st5_u;

            this.catalog.filter(c => c.id == "st8_u")[0].value = this.st8_u;

            this.incident.idfMinistry = this.cmbMinistry.getSelectedItem() != null ? this.cmbMinistry.getSelectedItem().value : this.incident.idfMinistry;
            this.incident.idfRegion = this.cmbRegion.getSelectedItem() != null ? this.cmbRegion.getSelectedItem().value : this.incident.idfMinistry;
            this.incident.idfTypeOfSeriousOccurrence = this.cmbTypeOfSeriousOccurrence.getSelectedItem() != null ? this.cmbTypeOfSeriousOccurrence.getSelectedItem().value : this.incident.idfMinistry;

            // coroner
            this.catalog.filter(c => c.id == "sc2_u")[0].value = this.sc2_u;

            this.catalog.filter(c => c.id == "sc2_u_aux")[0].value = this.sc2_u_aux;

            // please specify
            this.catalog.filter(c => c.id == "st9_u")[0].value = this.st9_u;

            // tick if other...
            this.catalog.filter(c => c.id == "sc3_u")[0].value = this.sc3_u;

            // text details of so.
            this.catalog.filter(c => c.id == "st10_u")[0].value = this.st10_u;

            // cas 
            this.catalog.filter(c => c.id == "sc4_u")[0].value = this.sc4_u;
            // please especify
            this.catalog.filter(c => c.id == "st11_u")[0].value = this.st11_u;

            // Other
            this.catalog.filter(c => c.id == "sc5_u")[0].value = this.sc5_u;
            // please especify
            this.catalog.filter(c => c.id == "st12_u")[0].value = this.st12_u;

            this.incident.idfUmabIntervention = this.cmbUmabIntervention.getSelectedItem().value;

            // umab intervention continnum
            var checked_sourceListBox_g1 = this.listBox_g1.getCheckedItems();

            checked_sourceListBox_g1.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });

            // combo 6
            var checked_sourceListBox_g6 = this.listBox_g6.getCheckedItems();

            checked_sourceListBox_g6.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });

            // combo 7
            var checked_sourceListBox_g7 = this.listBox_g7.getCheckedItems();

            checked_sourceListBox_g7.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });

            // combo 8
            var checked_sourceListBox_g8 = this.listBox_g8.getCheckedItems();

            checked_sourceListBox_g8.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });

            // combo 9
            var checked_sourceListBox_g9 = this.listBox_g9.getCheckedItems();

            checked_sourceListBox_g9.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });

            // check bellow
            var checked_sourceListBox_g2 = this.listBox_g2.getCheckedItems();

            checked_sourceListBox_g2.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });

            // Physical Injuries
            var checked_sourceListBox_g3 = this.listBox_g3.getCheckedItems();

            checked_sourceListBox_g3.forEach(element => {
                this.catalog.filter(c => c.id == element.originalItem.id)[0].value = "true";
            });

            //this.myLoader.open();



            if (this.idIncident > -1) {
                this.incident.id = this.idIncident;
            }

            let saveIncidentRequest = {
                Incident: this.incident,
                Catalog: this.catalog,
                //Clients : [],
                Clients: this.sourceClients.localData == undefined ? null : this.sourceClients.localData.filter((x: any) => x.abm != ''),
                Injuries: this.sourceInjuries.localData.filter((c: any) => c.idfIncident == null),
                InvolvedPeople: this.involvedPeople
            };

            // let saveIncidentRequest = {
            //     Incident : this.incident,                
            //     Catalog : [],
            //     Clients : [],
            //     Injuries : [],
            //     InvolvedPeople : []
            // };





            if (this.calling && this.incident.id <= 0) {
                return;
            }

            this.calling = true;


            this.incidentsService.SaveIncident(saveIncidentRequest)
                .subscribe(
                    (data: any) => {

                        this.calling = false;


                        if (data.result) {

                            // Reset
                            this.sourceClients.localData.filter(x => x.abm != '' && x.abm != 'D')
                                .forEach(element => {
                                    element.abm = "";
                                });

                            // update injuries
                            this.sourceInjuries.localData = data.injuries;
                            this.dataAdapterInjuriesListBox = new jqx.dataAdapter(this.sourceInjuries);


                            if (this.idIncident == -1) {
                                this.glowMessage.ShowGlow("success", "glow_success", "glow_clients_saved_successfully")

                                this.incident.state = "C";
                            }

                            var idAux = !this.isEditing() ? data.tagInfo.split('-')[0] : this.idIncident;

                            // this.myLoader.close();

                            //if (this.idIncident == -1) {
                            // this.user.email = "";
                            // this.txtFirstName.val("");
                            // this.txtLastName.val("");
                            //}

                            this.idIncident = idAux;
                            this.saveActionDisplay.saved(true);
                            // this.LoadIncident();

                            setTimeout(() => {
                                this.listBoxInjuries.render();
                            });
                        }
                        else {
                            this.manageError(data);
                            this.saveActionDisplay.saved(false);
                        }
                    },
                    error => {
                        ///this.myLoader.close();
                        this.manageError(error);
                        this.saveActionDisplay.saved(false);
                    });
        }
        else {
            this.saveActionDisplay.saved(false);
        }
    }

    rendererListBoxClients = (index, label, value): string => {
        if (this.sourceClients.localData == undefined) {
            return null;
        }
        var datarecord = this.sourceClients.localData[index];
        if (datarecord != undefined) {
            var imgurl = this.imagePipe.transform(datarecord.img, 'clients');
            var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';
            var isdelete = datarecord.abm == "D" ? " text-decoration:line-through; color:Red;" : "";
            var table = '<table style="min-width: 120px;"><tr><td style="width: 30px;" rowspan="2">' + img + '</td><td style="color:rgb(51, 51, 51); font-size:12px;' + isdelete + '">' + datarecord.fullName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + '</td></tr></table>';
            return table;
        }
        else {
            return label;
        }
    };

    rendererListBoxInjuries = (index, label, value): string => {
        // dataAdapterInjuriesListBox
        if (this.sourceInjuries.localData == undefined) {
            return null;
        }
        var datarecord = this.sourceInjuries.localData[index];

        if (datarecord != undefined) {
            //var imgurl = '/media/images/clients/' + datarecord.img + '.png';
            //var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';
            var isdelete = datarecord.idfIncident == null ? " text-decoration:line-through; color:Red;" : "";
            //var table = '<table style="min-width: 120px;"><tr><td style="width: 30px;" rowspan="2">' + img + '</td><td style="color:rgb(51, 51, 51); font-size:12px;' + isdelete + '">' + datarecord.fullName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + '</td></tr></table>';
            //return table;



            return "<span style='" + isdelete + "'>" + datarecord.descName + "  </span>";


        }
        // else {
        //     return label;
        // }
    };

    // dataAdapterClients: any = new jqx.dataAdapter(this.sourceClients);
    addClients = (Event: any): void => {
        this.selectClient.show(this.sourceClients.localData);
    };

    removeInjury = (event: any) => {
        var item = this.listBoxInjuries.getSelectedItem();

        if (item) {
            this.sourceInjuries.localData.filter(c => c.id == item.value)[0].idfIncident = null;
        }

        this.listBoxInjuries.clearSelection();
        this.hiddenDeleteInjuriesButton = true;
        this.hiddenUndoInjuriesButton = true;
        this.listBoxInjuries.render();


        this.onAnyChange(null, 'other');
    };


    onAddClient = (event: any): void => {

        event.ClientList.forEach(el => {

            this.sourceClients.localData.push({ id: el.id, fullName: el.fullName, img: el.img, abm: el.abm, idfClient: el.idfClient });
        });

        this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);


        this.onAnyChange(null, 'other');
    };

    sourceClients: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'fullName', type: 'string' },
                { name: 'img', type: 'string' },
                { name: 'abm', type: 'string' },
                { name: 'idfClient', type: 'number' },

            ],
            id: 'id',
            async: false
        }

    dataAdapterClientsListBox: any = new jqx.dataAdapter(this.sourceClients);

    sourceInjuries: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'descName', type: 'string' },
                { name: 'abm', type: 'string' },
                { name: 'idfIncident', type: 'string' }
            ],
            id: 'id',
            async: false
        }

    dataAdapterInjuriesListBox: any = new jqx.dataAdapter(this.sourceInjuries);

    deleteClients = (event: any): void => {
        var selected = this.listBoxClients.getSelectedItems();

        if (selected) {
            this.sourceClients.localData.filter(c => c.id == selected[0].value)[0].abm = "D";
        }

        this.hiddenDeleteClientButton = true;
        this.hiddenUndoClientButton = true;
        this.listBoxClients.clearSelection();
        this.listBoxClients.render();

        this.onAnyChange(null, "other");
    };

    UndoDeleteClients = (event: any): void => {

        var selected = this.listBoxClients.getSelectedItems();

        if (selected) {
            this.sourceClients.localData.filter(c => c.id == selected[0].value)[0].abm = "I";
        }

        this.hiddenDeleteClientButton = true;
        this.hiddenUndoClientButton = true;
        this.listBoxClients.clearSelection();
        this.listBoxClients.render();

        this.onAnyChange(null, "other");
    };

    UndoDeleteInjuries = (event: any): void => {

        var selected = this.listBoxInjuries.getSelectedItems();

        if (selected) {
            this.sourceInjuries.localData.filter(c => c.id == selected[0].value)[0].idfIncident = this.idIncident;
        }

        this.hiddenDeleteInjuriesButton = true;
        this.hiddenUndoInjuriesButton = true;
        this.listBoxInjuries.clearSelection();
        this.listBoxInjuries.render();
    };

    onSelectClient = (event: any): void => {
        let selected = this.listBoxClients.getSelectedItems();
        let isDeleted = false;

        if (selected) {
            isDeleted = this.sourceClients.localData.filter(c => c.id == selected[0].value)[0].abm == "D";
        }

        this.hiddenDeleteClientButton = isDeleted;
        this.hiddenUndoClientButton = !this.hiddenDeleteClientButton;
    };


    onSelectInjuries = (event: any): void => {

        let selected = this.listBoxInjuries.getSelectedItem();
        let isDeleted = false;

        if (selected) {
            isDeleted = this.sourceInjuries.localData.filter(c => c.id == selected.value)[0].idfIncident == null;
        }


        this.hiddenDeleteInjuriesButton = isDeleted;
        this.hiddenUndoInjuriesButton = !this.hiddenDeleteInjuriesButton;
    };

    listBoxBindingCompleteG1 = (event: any) => {

        if (this.sourceListBox_g1.localdata != undefined) {
            this.listBox_g1.beginUpdate();
            if (this.sourceListBox_g1.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g1.localdata //.filter(c => c.identifierGroup=="g1" && c.value==="true")
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g1.checkIndex(index);
                        }
                    });
            }

            this.listBox_g1.endUpdate();

            setTimeout(() => {
                this.loadedControl[3] = true;
                this.HiddeLoaderWhenEnd();
            });

        }

        //this.loadedControl[3] = true;
        // setTimeout(() => {                           
        //     this.loadedControl[3] = true;                         
        //     this.HiddeLoaderWhenEnd();      
        // });
    };

    listBoxBindingCompleteG2 = (event: any) => {

        if (this.sourceListBox_g2.localdata != undefined) {
            this.listBox_g2.beginUpdate();
            if (this.sourceListBox_g2.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g2.localdata
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g2.checkIndex(index);
                        }
                    });
            }

            this.listBox_g2.endUpdate();

            setTimeout(() => {
                this.loadedControl[4] = true;
                this.HiddeLoaderWhenEnd();
            });
        }

        //this.loadedControl[4] = true;
        // setTimeout(() => {                           
        //     this.loadedControl[4] = true;                         
        //     this.HiddeLoaderWhenEnd();      
        // });
    }

    listBoxBindingCompleteG3 = (event: any) => {

        if (this.sourceListBox_g3.localdata != undefined) {
            this.listBox_g3.beginUpdate();
            if (this.sourceListBox_g3.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g3.localdata
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g3.checkIndex(index);
                        }
                    });
            }

            this.listBox_g3.endUpdate();


            setTimeout(() => {
                this.loadedControl[5] = true;
                this.HiddeLoaderWhenEnd();
            });

        }


    }


    listBoxBindingCompleteG6 = (event: any) => {

        if (this.sourceListBox_g6.localdata != undefined) {
            this.listBox_g6.beginUpdate();
            if (this.sourceListBox_g6.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g6.localdata //.filter(c => c.identifierGroup=="g1" && c.value==="true")
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g6.checkIndex(index);
                        }
                    });
            }

            this.listBox_g6.endUpdate();

            setTimeout(() => {
                this.loadedControl[12] = true;
                this.HiddeLoaderWhenEnd();
            });

        }

        //this.loadedControl[12] = true;

        // setTimeout(() => {                           
        //     this.loadedControl[12] = true;                         
        //     this.HiddeLoaderWhenEnd();      
        //  });
    }

    listBoxBindingCompleteG7 = (event: any) => {

        if (this.sourceListBox_g7.localdata != undefined) {
            this.listBox_g7.beginUpdate();
            if (this.sourceListBox_g7.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g7.localdata //.filter(c => c.identifierGroup=="g1" && c.value==="true")
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g7.checkIndex(index);
                        }
                    });
            }

            this.listBox_g7.endUpdate();

            setTimeout(() => {
                this.loadedControl[13] = true;
                this.HiddeLoaderWhenEnd();
            });
        }


    }

    listBoxBindingCompleteG8 = (event: any) => {

        if (this.sourceListBox_g8.localdata != undefined) {
            this.listBox_g8.beginUpdate();
            if (this.sourceListBox_g8.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g8.localdata //.filter(c => c.identifierGroup=="g1" && c.value==="true")
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g8.checkIndex(index);
                        }
                    });
            }

            this.listBox_g8.endUpdate();


            setTimeout(() => {
                this.loadedControl[14] = true;
                this.HiddeLoaderWhenEnd();
            });

        }

    }

    listBoxBindingCompleteG9 = (event: any) => {

        if (this.sourceListBox_g9.localdata != undefined) {
            this.listBox_g9.beginUpdate();
            if (this.sourceListBox_g9.localdata.length > 0) {
                let index = -1;
                this.sourceListBox_g9.localdata //.filter(c => c.identifierGroup=="g1" && c.value==="true")
                    .forEach(element => {
                        index++;
                        if (element.value === "true") {
                            this.listBox_g9.checkIndex(index);
                        }
                    });
            }

            this.listBox_g9.endUpdate();



            setTimeout(() => {
                this.loadedControl[15] = true;
                this.HiddeLoaderWhenEnd();
            });

        }


    }

    BindingCompleteCmbSafetyRiskTo = (event: any) => {
        if (this.involvedPeople) {
            this.involvedPeople.filter(c => c.identifierGroup == "s1")
                .forEach(element => {
                    setTimeout(() => {
                        this.cmbSafetyRiskTo.selectItem(this.cmbSafetyRiskTo.getItemByValue(element.idfSPP));
                    });
                });

            setTimeout(() => {
                this.loadedControl[1] = true;
                this.HiddeLoaderWhenEnd();
            });
        }
    }

    BindingCompleteCmbStaffInvolved = (event: any) => {
        // if (this.involvedPeople && this.involvedPeople.length>0)
        if (this.involvedPeople) {
            this.involvedPeople.filter(c => c.identifierGroup == "g1")
                .forEach(element => {
                    setTimeout(() => {
                        this.cmbStaffInvolved.selectItem(this.cmbStaffInvolved.getItemByValue("" + element.idfSPP + ""));
                    });
                });

            setTimeout(() => {
                this.loadedControl[6] = true;
                this.HiddeLoaderWhenEnd();
            });
        }
        // else{
        //     this.loadedControl[6] = true;                         
        //     this.HiddeLoaderWhenEnd();   
        // }

        //this.loadedControl[6] = true;
    }

    BindingCompleteCmbStaffResponsible = (event: any) => {


        //  if (this.involvedPeople && this.involvedPeople.length>0)
        if (this.involvedPeople) {
            this.involvedPeople.filter(c => c.identifierGroup == "g2")
                .forEach(element => {
                    setTimeout(() => {
                        this.cmbStaffResponsible.selectItem(this.cmbStaffResponsible.getItemByValue(element.idfSPP));
                    });
                });

            setTimeout(() => {
                this.loadedControl[7] = true;
                this.HiddeLoaderWhenEnd();
            });
        }
        // else{

        //      if(!this.involvedPeople)
        //      {
        //         setTimeout(() => {
        //             this.loadedControl[7] = true;                         
        //             this.HiddeLoaderWhenEnd();     
        //         });
        //     }              
        // }      

        // this.loadedControl[7] = true;
    }


    BindingCompleteCmbByWhom = (event: any) => {
        //if (this.involvedPeople && this.involvedPeople.length>0)
        if (this.involvedPeople) {
            this.involvedPeople.filter(c => c.identifierGroup == "s2")
                .forEach(element => {
                    setTimeout(() => {
                        this.cmbByWhom_t1.selectItem(this.cmbByWhom_t1.getItemByValue(element.idfSPP));
                    });
                });

            setTimeout(() => {
                this.loadedControl[11] = true;
                this.HiddeLoaderWhenEnd();
            });

        }
        // else {
        //     // setTimeout(() => {
        //     //     this.loadedControl[11] = true;                         
        //     //     this.HiddeLoaderWhenEnd();     
        //     // });            
        // }

        // this.loadedControl[11] = true;
    }


    MinistryBindingComplete = (event: any) => {
        setTimeout(() => {
            this.cmbMinistry.selectItem(this.cmbMinistry.getItemByValue(String(this.idfMinistry)));

            setTimeout(() => {
                this.loadedControl[8] = true;
                this.HiddeLoaderWhenEnd();
            });

        });

        //this.loadedControl[8]=true;
    }

    RegionBindingComplete = (event: any) => {


        setTimeout(() => {
            this.cmbRegion.selectItem(this.cmbRegion.getItemByValue(String(this.idfRegion)));

            setTimeout(() => {
                this.loadedControl[9] = true;
                this.HiddeLoaderWhenEnd();
            });
        });

        //this.loadedControl[9]=true;
    }

    //
    TypeOfSeriousOccurrenceBindingComplete = (event: any) => {
        setTimeout(() => {
            this.cmbTypeOfSeriousOccurrence.selectItem(this.cmbTypeOfSeriousOccurrence.getItemByValue(String(this.idfTypeOfSeriousOccurrence)));

            setTimeout(() => {
                this.loadedControl[10] = true;
                this.HiddeLoaderWhenEnd();
            });

        });

        //this.loadedControl[10] = true;
    }


    UmabInterventionBindingComplete = (event: any) => {
        setTimeout(() => {
            this.cmbUmabIntervention.selectItem(this.cmbUmabIntervention.getItemByValue(String(this.idfUmabIntervention)));

            setTimeout(() => {
                this.loadedControl[2] = true;
                this.HiddeLoaderWhenEnd();

                //this.onAnyChange(null,'other') 

            });
        });

        //this.loadedControl[2] = true;

    }

    onSelectChangeCmb = (event: any, group: string) => {
        var args = event.args;
        if (args) {
            var item = args.item;
            let search = this.involvedPeople.filter(c => c.idfSPP == item.originalItem.id && c.identifierGroup == group);

            if (search && search.length > 0) {
                search[0].state = event.type == "select" ? "C" : "D";
            }
            else {
                this.involvedPeople.push({ identifierGroup: group, IdfIncident: this.idIncident, idfSPP: item.originalItem.id, state: 'C' });
            }

            this.onAnyChange(null, 'other');
        }
    }


    LoadIncident = () => {
        this.myLoader.open();

        this.incidentsService.GetIncidentById(this.idIncident, this.idPeriod)
            .subscribe(
                (data: any) => {
                    if (data.result) {


                        // Incident
                        this.incident = data.incident;

                        // catalog
                        this.catalog = data.catalog;



                        this.sourceUmabInterventionList.localdata = data.umabIntervention;
                        this.dataAdapterUmabInterventionList = new jqx.dataAdapter(this.sourceUmabInterventionList);

                        this.dateTimeIncident = new Date(data.incident.dateIncident);

                        this.dateTimeWhenSeriousOccurrence = new Date(data.incident.dateTimeWhenSeriousOccurrence);
                        this.idfMinistry = data.incident.idfMinistry;
                        this.idfRegion = data.incident.idfRegion;

                        this.idfUmabIntervention = data.incident.idfUmabIntervention;

                        this.idfTypeOfSeriousOccurrence = data.incident.idfTypeOfSeriousOccurrence;


                        this.sourceTypeSeriousOccurrenceList.localdata = data.typeSeriousOccurrence;


                        this.isSeriousOcurrence = data.incident.isSeriousOcurrence == 1;

                        // load ministeries
                        this.sourceMinistryList.localdata = data.ministeries;

                        // load regions
                        this.sourceRegionList.localdata = data.regionList;

                        // load umab


                        // staff (involved people) list
                        this.involvedPeople = data.involvedPeople;

                        // this.sourceStaffList_1.localdata = data.staffs;
                        // this.sourceStaffList_2.localdata = data.staffs;

                        this.sourceStaffList.localdata = data.staffs;

                        // injuries list    
                        this.sourceInjuries.localData = data.injuries;
                        this.dataAdapterInjuriesListBox = new jqx.dataAdapter(this.sourceInjuries);

                        // client list
                        this.sourceClients.localData = data.clients;
                        this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);

                        // checkbox groups
                        this.sourceListBox_g1.localdata = data.catalog.filter(c => c.identifierGroup == "g1").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });
                        this.sourceListBox_g2.localdata = data.catalog.filter(c => c.identifierGroup == "g2").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });
                        this.sourceListBox_g3.localdata = data.catalog.filter(c => c.identifierGroup == "g3").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });

                        this.sourceListBox_g6.localdata = data.catalog.filter(c => c.identifierGroup == "g6").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });

                        this.sourceListBox_g7.localdata = data.catalog.filter(c => c.identifierGroup == "g7").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });
                        this.sourceListBox_g8.localdata = data.catalog.filter(c => c.identifierGroup == "g8").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });
                        this.sourceListBox_g8.localdata = data.catalog.filter(c => c.identifierGroup == "g8").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });
                        this.sourceListBox_g9.localdata = data.catalog.filter(c => c.identifierGroup == "g9").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });



                        // simple checkbox
                        var groupCheckBoxes = data.catalog.filter(c => c.identifierGroup == "sc").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });

                        groupCheckBoxes.forEach(element => {
                            this.sc1_u = element.id === "sc1_u" && element.value === "true" ? true : this.sc1_u;
                            this.sc2_u = element.id === "sc2_u" && element.value === "true" ? true : this.sc2_u;
                            //this.sc2_u_aux = element.id === "sc2_u" && element.value === "indeterminate" ? true : false;
                            this.sc2_u_aux = element.id === "sc2_u_aux" && element.value === "true" ? true : this.sc2_u_aux;

                            this.sc3_u = element.id === "sc3_u" && element.value === "true" ? true : this.sc3_u;
                            this.sc4_u = element.id === "sc4_u" && element.value === "true" ? true : this.sc4_u;
                            this.sc5_u = element.id === "sc5_u" && element.value === "true" ? true : this.sc5_u;
                            //this.sc4_u = true;

                        });



                        // simple textarea
                        var groupTextAreas = data.catalog.filter(c => c.identifierGroup == "st").map(function (x) { return { "id": x.id, "display": x.title, "value": x.value, "identifierGroup": x.identifierGroup }; });

                        groupTextAreas.forEach(element => {
                            this.st1_u = element.id === "st1_u" ? element.value : this.st1_u;
                            this.st2_u = element.id === "st2_u" ? element.value : this.st2_u;
                            this.st3_u = element.id === "st3_u" ? element.value : this.st3_u;
                            this.st4_u = element.id === "st4_u" ? element.value : this.st4_u;
                            this.st5_u = element.id === "st5_u" ? element.value : this.st5_u;
                            this.st6_u = element.id === "st6_u" ? element.value : this.st6_u;
                            this.st7_u = element.id === "st7_u" ? element.value : this.st7_u;
                            this.st8_u = element.id === "st8_u" ? element.value : this.st8_u;
                            this.st9_u = element.id === "st9_u" ? element.value : this.st9_u;
                            this.st10_u = element.id === "st10_u" ? element.value : this.st10_u;
                            this.st11_u = element.id === "st11_u" ? element.value : this.st11_u;
                            this.st12_u = element.id === "st12_u" ? element.value : this.st12_u;
                            this.st21_u = element.id === "st21_u" ? element.value : this.st21_u;
                            this.st22_u = element.id === "st22_u" ? element.value : this.st22_u;
                        });

                        // data adapters for checkgroups and other controls
                        //setTimeout(() => {
                        this.dataAdapterListBox_g1 = new jqx.dataAdapter(this.sourceListBox_g1);
                        this.dataAdapterListBox_g2 = new jqx.dataAdapter(this.sourceListBox_g2);
                        this.dataAdapterListBox_g3 = new jqx.dataAdapter(this.sourceListBox_g3);

                        this.dataAdapterListBox_g6 = new jqx.dataAdapter(this.sourceListBox_g6);
                        this.dataAdapterListBox_g7 = new jqx.dataAdapter(this.sourceListBox_g7);
                        this.dataAdapterListBox_g8 = new jqx.dataAdapter(this.sourceListBox_g8);
                        this.dataAdapterListBox_g9 = new jqx.dataAdapter(this.sourceListBox_g9);

                        //this.dataAdapterStaffList = new jqx.dataAdapter(this.sourceStaffList);
                        this.dataAdapterStaffList_1 = new jqx.dataAdapter(this.sourceStaffList);
                        this.dataAdapterStaffList_2 = new jqx.dataAdapter(this.sourceStaffList);
                        this.dataAdapterStaffList_3 = new jqx.dataAdapter(this.sourceStaffList);
                        this.dataAdapterStaffList_4 = new jqx.dataAdapter(this.sourceStaffList);


                        // del otro Tab
                        this.dataAdapterMinistryList = new jqx.dataAdapter(this.sourceMinistryList);
                        this.dataAdapterRegionList = new jqx.dataAdapter(this.sourceRegionList);
                        this.dataAdapterTypeSeriousOccurrenceList = new jqx.dataAdapter(this.sourceTypeSeriousOccurrenceList);


                        this.listBoxClients.render();

                        // setTimeout(() => {
                        //     this.loadedControl[0] = true;
                        //     this.HiddeLoaderWhenEnd();        
                        // },2000);

                        setTimeout(() => {
                            this.loadedControl[0] = true;
                            this.HiddeLoaderWhenEnd();
                        });



                        //});
                    } else {
                        this.manageError(data);
                    }

                    //setTimeout(() => {
                    //this.myLoader.close();

                    //this.loadedControl[0] = true;
                    //this.HiddeLoaderWhenEnd();    
                    //});

                },
                error => {
                    this.manageError(error);
                });
    }

    canDeleteOrSave: boolean;

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(params => {
            // this.idClient = +params['idclient'];
            this.idIncident = +params['idincident'];
            this.idPeriod = +params['idperiod'];

            this.canDeleteOrSave = (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor"));

        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    cmbTypeOfSeriousOccurrenceOnSelect(event: any): void {


        let args = event.args;
        let item = this.cmbTypeOfSeriousOccurrence.getItem(args.index);
        if (item != null) {
            //this.myPanel.prepend('<div style="margin-top: 5px;">Selected: ' + item.label + '</div>');



            this.typeOfSeriousOccurrence = Number(item.value);

            this.onAnyChange(null, 'other');

        }
        // else{
        // }
    };


    HiddeLoaderWhenEnd = (): void => {

        let control = true;

        this.loadedControl.forEach(element => {
            control = control && element;
        });

        if (control) {
            this.myLoader.close();
        }
    }

    sourceListBox_g1: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
                { name: 'identifierGroup', type: 'string' },

            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g1: any = new jqx.dataAdapter(this.sourceListBox_g1);

    sourceListBox_g2: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
                { name: 'identifierGroup', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g2: any = new jqx.dataAdapter(this.sourceListBox_g2);

    sourceListBox_g3: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
                { name: 'identifierGroup', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g3: any = new jqx.dataAdapter(this.sourceListBox_g3);


    sourceListBox_g6: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
                { name: 'identifierGroup', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g6: any = new jqx.dataAdapter(this.sourceListBox_g6);




    sourceListBox_g7: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
                { name: 'identifierGroup', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g7: any = new jqx.dataAdapter(this.sourceListBox_g7);

    sourceListBox_g8: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
                { name: 'identifierGroup', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g8: any = new jqx.dataAdapter(this.sourceListBox_g8);


    sourceListBox_g9: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'display', type: 'string' },
                { name: 'value', type: 'string' },
                { name: 'identifierGroup', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterListBox_g9: any = new jqx.dataAdapter(this.sourceListBox_g9);

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

    // dataAdapterStaffList: any; // = new jqx.dataAdapter(this.sourceOwnerList);

    dataAdapterStaffList_1: any;
    dataAdapterStaffList_2: any;
    dataAdapterStaffList_3: any;
    dataAdapterStaffList_4: any;



    sourceMinistryList: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' },
            ],
            id: 'id',
        }

    dataAdapterMinistryList: any;




    sourceRegionList: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' },
            ],
            id: 'id',
        }

    dataAdapterRegionList: any;


    sourceTypeSeriousOccurrenceList: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' },
            ],
            id: 'id',
        }

    dataAdapterTypeSeriousOccurrenceList: any;


    sourceUmabInterventionList: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' },
            ],
            id: 'id',
        }

    dataAdapterUmabInterventionList: any;

}







    // sourceClients: any =
    // {
    //     dataType: "json",
    //     dataFields: [
    //         { name: 'id', type: 'number' },
    //         { name: 'idfTaskParent', type: 'string' },
    //         { name: 'subject', type: 'string' },           
    //         { name: 'abm', type: 'string' },            
    //     ],
    //     id: 'id',
    // }

    // sourceListBox_g4: any =
    // {
    //     dataType: "json",
    //     dataFields: [
    //         { name: 'id', type: 'string' },
    //         { name: 'display', type: 'string' },
    //         { name: 'value', type: 'string' },         
    //     ],
    //     id: 'id',
    //     async: true
    // }

    // dataAdapterListBox_g4: any = new jqx.dataAdapter(this.sourceListBox_g4);



    // sourceListBox_g5: any =
    // {
    //     dataType: "json",
    //     dataFields: [
    //         { name: 'id', type: 'string' },
    //         { name: 'display', type: 'string' },
    //         { name: 'value', type: 'string' },         
    //     ],
    //     id: 'id',
    //     async: true
    // }

    // dataAdapterListBox_g5: any = new jqx.dataAdapter(this.sourceListBox_g5);

    // sourceTypeIncident: any =
    // {
    //     dataType: "json",
    //     dataFields: [
    //         { name: 'id', type: 'number' },
    //         { name: 'description', type: 'string' }
    //     ],
    //     id: 'id',
    //     async: false
    // }

    // dataAdapterTypeIncident: any; // = new jqx.dataAdapter(this.sourceTypeIncident);

    // sourceDegreeIncident: any =
    // {
    //     dataType: "json",
    //     dataFields: [
    //         { name: 'id', type: 'number' },
    //         { name: 'description', type: 'string' }
    //     ],
    //     id: 'id',
    //     async: false
    // }

    // dataAdapterDegreeIncident: any;

    // sourceStaffList_1: any =
    // {
    //     dataType: "json",
    //     dataFields: [
    //         { name: 'id', type: 'string' },
    //         { name: 'fullName', type: 'string' },
    //         { name: 'positionName', type: 'string' },
    //         { name: 'email', type: 'string' },
    //         { name: 'idfUser', type: 'number' },
    //         { name: 'group', type: 'string' }

    //     ],
    //     id: 'id',
    // }
