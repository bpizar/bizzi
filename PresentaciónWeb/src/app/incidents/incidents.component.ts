import { Component, ViewChild, AfterViewInit, OnInit, ChangeDetectorRef } from '@angular/core';
//import { Observable } from 'rxjs/Rx';
import { ConstantService } from '../common/services/app.constant.service';
import { IncidentsService } from './incidents.service';
import { SchedulingService } from '../scheduling/scheduling.service';
//import { EditDuplicatesModel } from '../scheduling/scheduling.component.model';
//import { jqxButtonComponent } from '../../../node_modules/jqwidgets-framework/jqwidgets-ts/angular_jqxbuttons';
import { jqxTabsComponent } from 'jqwidgets-ng/jqwidgets/jqxtabs';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
//import { jqxNotificationComponent } from '../../../node_modules/jqwidgets-ng/jqwidgets/jqxnotification';
import { AuthHelper } from '../common/helpers/app.auth.helper';
import { SchedulingGuard } from '../common/services/scheduling.guard.service';
import { jqxGridComponent } from 'jqwidgets-ng/jqwidgets/jqxgrid';
//import { jqxDateTimeInputComponent } from '../../../node_modules/jqwidgets-framework/jqwidgets-ts/angular_jqxdatetimeinput';
//import { jqxMaskedInputComponent } from '../../../node_modules/jqwidgets-framework/jqwidgets-ts/angular_jqxmaskedinput';
import { jqxListBoxComponent } from 'jqwidgets-ng/jqwidgets/jqxlistbox';
import { CommonHelper } from '../common/helpers/app.common.helper';
import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';
import { jqxMaskedInputComponent } from 'jqwidgets-ng/jqwidgets/jqxmaskedinput';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { JqxHelper } from '../common/helpers/app.jqx.helper'
import { Router } from '@angular/router';

@Component({
    selector: 'incidents',
    templateUrl: '../incidents/incidents.component.template.html',
    providers: [IncidentsService, ConstantService, SchedulingService, CommonHelper, AuthHelper, JqxHelper], // SchedulingGuard
    styleUrls: ['../incidents/incidents.component.css'],
})

export class Incidents implements AfterViewInit, OnInit {
    // $: any;

    constructor(private translate: TranslateService,
        private jqxHelper: JqxHelper,
        private schedulingService: SchedulingService,
        private incidentsservice: IncidentsService,
        private constantService: ConstantService,
        private authHelper: AuthHelper,
        private commonHelper: CommonHelper,
        private router: Router,
        //private schedulingGuard: SchedulingGuard,
        //private chRef: ChangeDetectorRef
    ) {
        this.translate.setDefaultLang('en');
    }

    @ViewChild(jqxMaskedInputComponent) jqxMaskedInputComponent: jqxMaskedInputComponent;
    @ViewChild('gridReference') myGrid: jqxGridComponent;
    // @ViewChild('msgNotificationError') msgNotificationError: jqxNotificationComponent;
    // @ViewChild('msgNotificationSuccess') msgNotificationSuccess: jqxNotificationComponent;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('periodsDropDown') periodsDropDown: jqxListBoxComponent;
    @ViewChild('tabsReference') tabsReference: jqxTabsComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('gridReference') gridReference: jqxGridComponent;
    selectHolder: string = "";
    periodClosed: boolean = false;

    private onDrop(args) {
        let [e, el] = args;
    }

    msgError: string = "";
    msgSuccess: string = "";
    currentPeriod: number = -1;

    // cellclass =  (row, columnfield, value):string => {

    //     if(columnfield=="modifiedTrackingFormat")
    //    {
    //         return "cellmodifiedTrackingFormat";
    //    }

    //    return "";
    // }

    ValidateWindow = (): boolean => {
        let result = true;
        if (this.currentPeriod <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "finanzas_select_period");
            result = false;
        }
        return result;
    }

    Save = (event: any) => {
        if (this.ValidateWindow()) {
            let body = {
                //Tracking: this.dataAdapterGridTrackingTimeReview.records
            };
            this.myLoader.open();
            // this.incidentsservice.SaveTimeTrackingReview(body)
            //     .subscribe(
            //     (data:any)=> {
            //         if (data.result) {
            //                 this.glowMessage.ShowGlow("success","glow_success","finanzas_saved_succesfully");
            //                 this.loadTimeTrackingReview();
            //         }               
            //         else {
            //             this.manageError(data);
            //         }
            //         this.myLoader.close();
            //     },
            //     error => {
            //         this.myLoader.close();
            //         this.manageError(error);
            //     });
        }
    }

    sourceIncidents: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' },
                { name: 'dateIncident', type: 'string' },
                { name: 'color', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'abm', type: 'string' }
            ],
            id: 'id',
        }

    dataAdapterIncidents: any = new jqx.dataAdapter(this.sourceIncidents);

    //INIT EVENT
    ngOnInit(): void { }
    // periodsBindingComplete = (event: any) => {
    //     if (this.sourcePeriodsListBox.localData != undefined) {
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


    serverDateTime: Date;

    //AFTER INIT EVENT
    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
            this.translate.get('global_select').subscribe((res: string) => {
                this.selectHolder = res;
            });

            // this.translate.get('finanzas_time_tracking_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(0,res); });    
            // this.translate.get('finanzas_grid_project').subscribe((res: string) => { this.myGrid.setcolumnproperty("projectName","text",res); });                
            // this.translate.get('finanzas_grid_participant').subscribe((res: string) => { this.myGrid.setcolumnproperty("participantFullName","text",res); });                
            // this.translate.get('finanzas_grid_position').subscribe((res: string) => { this.myGrid.setcolumnproperty("positionName","text",res); });                
            // this.translate.get('finanzas_grid_scheduledtime').subscribe((res: string) => { this.myGrid.setcolumnproperty("scheduledTimeFormat","text",res); });                
            // this.translate.get('finanzas_grid_usertrackingtime').subscribe((res: string) => { this.myGrid.setcolumnproperty("userTrackingFormat","text",res); });                
            // this.translate.get('finanzas_grid_modifiedtrackingtime').subscribe((res: string) => { this.myGrid.setcolumnproperty("modifiedTrackingFormat","text",res); });     
            // this.myGrid.localizestrings(this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en : this.jqxHelper.getGridLocation_es);   

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
                        this.myLoader.close();
                    },
                    error => {
                        this.myLoader.close();
                        this.manageError(error);
                    }
                );
        });
    }

    // notAllowcellbeginedit = (row: any, datafield: any, columntype: any, value: any) => {
    //     return false;
    // }

    // timeRenderer = (row: any, column: any, value: any) => {
    //     if (row < 0) {
    //         return "";
    //     }

    //     if (this.sourceGridTrackingTimeReview) {
    //         let utf: string = this.sourceGridTrackingTimeReview.localData[row].userTrackingFormat;
    //         let mtf: string = value;
    //         let style: string = utf != mtf ? "style='color:green;'" : "";
    //         return "<span " + style + ">" + value + "</span>";
    //     }

    //     return value;
    // }

    // aggregatesrenderer = (aggregates: any, column: any, element: any): string => {
    //     let renderstring = '<div class="jqx-widget-content jqx-widget-content-' + "metro" + '" style="float: left; width: 100%; height: 100%; "/>';
    //     return renderstring;
    // }

    // cellsrendererOriginal = (row: any, column: any, value: any, defaultRender: any,colx:any, rowData:any) => {
    //     if (value.toString().indexOf("custom1") >= 0) {
    //         return defaultRender.replace("custom1", "Total");
    //     }
    // };

    // aggregatesrendererFix = (aggregates: any, column: any, element: any): string => {
    //     let renderstring = '<div style="position: relative; margin-top: 4px; margin-right:5px; text-align: right; overflow: hidden;">' + 'Total: ' + ': ' + this.commonHelper.convertMinsToHrsMins(aggregates.custom1) + '</div>';
    //     return renderstring;
    // };      

    // aggregatesrendererFix2 = (aggregates: any, column: any, element: any): string => {
    //     return aggregates.custom1;
    // };   

    // convertSecsToHrsMins = (secs): string => {
    //     var hours = Math.floor(secs / (60 * 60));
    //     var divisor_for_minutes = secs % (60 * 60);
    //     var minutes = Math.floor(divisor_for_minutes / 60);
    //     var divisor_for_seconds = divisor_for_minutes % 60;
    //     var seconds = Math.ceil(divisor_for_seconds);
    //     var h2 = hours < 10 ? '0' + hours : hours;
    //     var m2 = minutes < 10 ? '0' + minutes : minutes;
    //     var s2 = seconds < 10 ? '0' + seconds : seconds;
    //     return h2 + ':' + m2 + ':' + s2;
    // }

    // convertHrsminsToSecs = (hm: string): number => {
    //     var hours = parseInt(hm.substr(1, 2));
    //     var mins = parseInt(hm.substr(4, 2));
    //     var secs = parseInt(hm.substr(7, 2));
    //     return secs + mins * 60 + hours * 3600;
    // }


    // custom1 = (aggregatedValue: number, currentValue: number, column: any, record: any): number => {
    //     let total = parseInt(record['secondsModifiedTracking']);
    //     return aggregatedValue + total;
    // };

    // ParticipantRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata):string => {
    //     //var isdelete = rowdata.abm == "D" ? " text-decoration:line-through !important; color:Red;" : "";
    //     let path = rowdata.img ?  '/media/images/users/'+ rowdata.img + '.png' : ""; 
    //     let img = "<div style=' margin:1px solid blue;   float:left; margin-top:15px;'><img style='border-radius:50%;' height='40' width='40' src='" + path +  "' /></div>";
    //     //let assignedToPosition = rowdata.assignedToPosition ? "<div style='padding-left:1%; " + isdelete + " float:left; width:79%; margin-top:4px; heigth:10px !important; max-heigth:10px !important; overflow: hidden !important; '>" + rowdata.assignedToPosition + "</div>" : "";
    //     let marginTop = "margin-top:30px;";
    //     let assignedTo = "<div style='padding-left:1%; float:left; width:79%; " + marginTop+ "heigth:10px !important; max-heigth:10px !important; overflow: hidden !important;'>" + rowdata.participantFullName + "</div>"       
    //     return "<div style='height:70px;'>" + img  + assignedTo + " </div>"
    // };

    // projectNameRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
    //     var divSquareProject = "<div style='float:left; margin-left:5px; margin-top:5px;  height:60px; width:5px !important; background-color:" + rowdata.projectColor + ";'>&nbsp;&nbsp;</div>&nbsp;";
    //     return divSquareProject + "<div style='margin-top:10px; margin-left:20px !important;'>"  + rowdata.projectName + "</div>";
    // };
    editRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var salida = "<div class='tooltip'> " + "<div data-row='" + row + "' class='btnedit'></div>" + "<span class='tooltiptext'>Edit</span></div>";
        return salida;
    }

    // columns: any[] =
    // [        
    //     {
    //         width: '160',  //120
    //         text: '',
    //         datafield: 'Edit',
    //         height:'70px',
    //         columntype: 'none',
    //         cellsrenderer : this.editRenderer,           
    //     },     
    //     {
    //         //text: 'Project Name',
    //         // <div translate>Title</div>
    //         text: '&nbsp;',
    //         datafield: 'projectName',
    //         width: 'auto',
    //         //cellsrenderer: this.projectNameRenderer
    //     },
    //     {
    //         text: 'Description',
    //         datafield: 'description',
    //         width: 'auto'
    //     },       
    //     {
    //         text: 'Begin',
    //         datafield: 'beginDate',
    //         width: '140px',
    //         //cellsrenderer: this.dateRenderer
    //     },
    //     {
    //         text: 'End',
    //         datafield: 'endDate',
    //         width: '140px',
    //         //cellsrenderer: this.dateRenderer
    //     }
    // ];

    editRendererIncident: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var salida = "<div class='tooltip'> " + "<div data-row='" + row + "' class='btneditincident'></div>" + "<span class='tooltiptext'>Edit</span></div>";
        return salida;
    }

    // descriptionDailyLogRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
    //     if (this.sourceIncidents.localData == undefined) {
    //         return null;
    //     }

    //     // var divSquareProject = ""; "<span style='border:1px solid white; height:15px; width:15px !important; background-color:" + rowdata.color + ";'>&nbsp;&nbsp;</span>";
    //     // var table = '<table  border=1 style="width:100%; min-width: 120px;"><tr><td style="font-size:12px;">' + divSquareProject + rowdata.projectName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:12px;">' + "&nbsp;" + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' +  rowdata.description + '</td></tr></table>';

    //     var table =  rowdata.description ;

    //     return table;
    // }  

    columnsGridIncidents: any[] =
        [
            {
                width: '90px',
                text: '',
                datafield: 'Edit',
                height: '70px',
                columntype: 'none',
                cellsrenderer: this.editRendererIncident,
            },
            {
                text: 'Date',
                datafield: 'dateIncident',
                width: '140px',
                height: '70px'
            },
            {
                text: 'Description',
                height: '70px',
                datafield: 'description',
                width: 'auto',
                //cellsrenderer: this.descriptionDailyLogRenderer
            },
            {
                text: 'ABM',
                datafield: 'abm',
                width: '80px',
                height: '70px',
                hidden: true
            }
        ];

    loadIncidentList = () => {
        //this.myLoader.open();
        this.incidentsservice.GetIncidentsListByPeriod(this.currentPeriod)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        // this.sourceGridTrackingTimeReview .localData = data.tracking;                   
                        this.sourceIncidents.localData = data.incidents;
                        this.dataAdapterIncidents = new jqx.dataAdapter(this.sourceIncidents);
                        // this.dataAdapterGridTrackingTimeReview = new jqx.dataAdapter(this.sourceGridTrackingTimeReview);
                    }
                    else {
                        this.manageError(data);
                    }
                    this.myLoader.close();
                },
                error => {
                    this.myLoader.close();
                    this.manageError(error);
                }
            );
    }

    canAddNew = (): boolean => {
        return this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("xxx");
    }

    // canDeleteOrSave = (): boolean => {
    //     let disabled = this.periodClosed;
    //     return (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor")) && !disabled;
    // }

    gridRowselect = (event: any): void => {
        let abmValue = "";
        if (event) {
            var args = event.args;
            var rowData = args.row;
            abmValue = rowData.abm;
        }
        else {
            if (this.gridReference.getselectedrowindex() > -1) {
                let currentRow = this.gridReference.getrowdata(this.gridReference.getselectedrowindex());
                abmValue = currentRow.abm;
            }
        }
        this.gridReference.hidecolumn("Edit");
        this.gridReference.showcolumn("Edit");
    };

    PeriodSelectDropDown = (event: any): void => {
        this.currentPeriod = event.args.item.value;

        if (this.currentPeriod > 0) {
            this.periodClosed = this.sourcePeriodsListBox.localData[event.args.index].state == "CL";
            this.loadIncidentList();
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
                { name: 'close', type: 'boolean' },
                { name: 'state', type: 'string' },
            ],
            id: 'idfStaffProjectPosition',
            async: true
        }

    // dataAdapterPeriod: any = new this.$.jqx.dataAdapter(this.sourcePeriodsListBox);
    dataAdapterPeriod: any = new jqx.dataAdapter(this.sourcePeriodsListBox);

    //SCHEDULER'S READY EVENT. 
    ready = (): void => { }

    //CLOSE APPOINTMENT WINDOW
    //closeWindow(event: any): void {}

    //closeWindowEditAppointment(event: any): void {}

    //cancelWindowPeriod(event: any): void {}

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    OnGridGenericEvent2 = (event: any): void => {
        setTimeout(() => {
            this.myGrid.render();
        }, 2000);
    }

    OnGridGenericEvent = (event: any): void => {
        setTimeout(() => {
            this.myGrid.render();
        });
    }

    cellClick(event: any): void {
        // var row = event.args.row.bounddata;
        // this.CurrentRow = row.id;

        // if (event.args.datafield === 'Edit') {
        //     this.router.navigate(['projects/editproject', this.CurrentRow]);
        // }
    }

    createNew(event: any): void {
        this.router.navigate(['incidents/editincident/', "-1", this.currentPeriod]);
    }

    editClick(event: any): void {
        let cr = this.myGrid.getrowdata(this.myGrid.selectedrowindex());
        this.router.navigate(['incidents/editincident/', cr.id, this.currentPeriod]);
    }

    renderedGrid = (): void => {
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
            if (document.getElementsByClassName("btneditincident").length > 0) {
                let Buttons = jqwidgets.createInstance(".btneditincident", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-more' style='padding:0px !important; font-size:16px; color:var(--thirteenth-color);'></i>" + '', template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });
                let flattenButtons = flatten(Buttons.length ? Buttons : [Buttons]);
                if (flattenButtons) {
                    for (let i = 0; i < flattenButtons.length; i++) {
                        flattenButtons[i].removeEventHandler('click');
                        flattenButtons[i].addEventHandler('click', (event: any): void => {
                            this.editClick(event);
                        });
                    }
                }
            }
        });
    };
}