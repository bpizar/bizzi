import { Component, ViewChild, AfterViewInit, OnInit, ChangeDetectorRef } from '@angular/core';
//import { Observable } from 'rxjs/Rx';
import { ConstantService } from '../common/services/app.constant.service';
import { FinanceService } from './finance.service';
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
import { SaveActionDisplay } from '../common/components/saveActionDisplay/saveactiondisplay.component';
import { ImagePipe } from '../common/pipes/image.pipe';

@Component({
    selector: 'scheduling',
    templateUrl: '../finance/finance.component.template.html',
    providers: [FinanceService, ConstantService, SchedulingService, CommonHelper, AuthHelper, JqxHelper], // SchedulingGuard
    styleUrls: ['../finance/finance.component.css'],
})

export class Finance implements AfterViewInit, OnInit {
    // $: any;

    constructor(private translate: TranslateService,
        private jqxHelper: JqxHelper,
        private schedulingService: SchedulingService,
        private financeService: FinanceService,
        private constantService: ConstantService,
        private authHelper: AuthHelper,
        private commonHelper: CommonHelper,
        //private schedulingGuard: SchedulingGuard,
        //private chRef: ChangeDetectorRef
    ) {
        this.translate.setDefaultLang('en');
    }

    @ViewChild(jqxMaskedInputComponent) jqxMaskedInputComponent: jqxMaskedInputComponent;
    @ViewChild('gridTrackingTimeReview') myGrid: jqxGridComponent;
    // @ViewChild('msgNotificationError') msgNotificationError: jqxNotificationComponent;
    // @ViewChild('msgNotificationSuccess') msgNotificationSuccess: jqxNotificationComponent;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('periodsDropDown') periodsDropDown: jqxListBoxComponent;
    @ViewChild('tabsReference') tabsReference: jqxTabsComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild(SaveActionDisplay) saveActionDisplay: SaveActionDisplay;
    imagePipe = new ImagePipe();
    selectHolder: string = "";
    periodClosed: boolean = false;

    private onDrop(args) {
        let [e, el] = args;
    }

    msgError: string = "";
    msgSuccess: string = "";
    currentPeriod: number = -1;

    onMustSave = (event: any): void => {
        this.Save(null);
    }

    onAnyChange = (event: any, type: string = "-"): void => {
        var args = event ? event.args : null;
        if (args || type == "check" || type == "other") {
            // let control = true;
            // this.loadedControl.forEach(element => {
            //     control = control && element;
            // });
            // if(control && this.saveActionDisplay)
            // {
            this.saveActionDisplay.setDirty();
            //}
        }
        else {
        }
    }

    cellclass = (row, columnfield, value): string => {
        if (columnfield == "modifiedTrackingFormat") {
            return "cellmodifiedTrackingFormat";
        }
        return "";
    }

    ValidateWindow = (): boolean => {
        let result = true;
        // if (this.currentPeriod <= 0) {
        //      this.glowMessage.ShowGlow("warn","glow_invalid","finanzas_select_period");
        //result = false;
        //}
        return result;
    }

    Save = (event: any) => {
        if (this.ValidateWindow()) {
            let body = {
                Tracking: this.dataAdapterGridTrackingTimeReview.records
            };
            //this.myLoader.open();
            this.financeService.SaveTimeTrackingReview(body)
                .subscribe(
                    (data: any) => {
                        if (data.result) {
                            //this.glowMessage.ShowGlow("success","glow_success","finanzas_saved_succesfully");
                            //this.loadTimeTrackingReview();
                            this.saveActionDisplay.saved(true);
                        }
                        else {
                            this.saveActionDisplay.saved(false);
                            this.manageError(data);
                        }
                        this.myLoader.close();
                    },
                    error => {
                        this.myLoader.close();
                        this.manageError(error);
                    });
        }
    }




    /*EDIT SECTION-------------------------------------------------------------------------*/
    sourceGridTrackingTimeReview: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'participantFullName', type: 'string' },
                { name: 'positionName', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'scheduledTimeFormat', type: 'string' },
                { name: 'userTrackingFormat', type: 'string' },
                { name: 'modifiedTrackingFormat', type: 'string' },
                { name: 'secondsModifiedTracking', type: 'numeric' },
                { name: 'projectColor', type: 'string' },
                { name: 'img', type: 'string' },
            ],
            id: 'id',
        }

    //dataAdapterGridTrackingTimeReview: any = new this.$.jqx.dataAdapter(this.sourceGridTrackingTimeReview);
    dataAdapterGridTrackingTimeReview: any = new jqx.dataAdapter(this.sourceGridTrackingTimeReview);

    // INIT EVENT
    ngOnInit(): void {
        let disabled = this.periodClosed;
        this.canDeleteOrSave = (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor")) && !disabled;
    }

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
            this.translate.get('finanzas_time_tracking_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(0, res); });
            this.translate.get('finanzas_grid_project').subscribe((res: string) => { this.myGrid.setcolumnproperty("projectName", "text", res); });
            this.translate.get('finanzas_grid_participant').subscribe((res: string) => { this.myGrid.setcolumnproperty("participantFullName", "text", res); });
            this.translate.get('finanzas_grid_position').subscribe((res: string) => { this.myGrid.setcolumnproperty("positionName", "text", res); });
            this.translate.get('finanzas_grid_scheduledtime').subscribe((res: string) => { this.myGrid.setcolumnproperty("scheduledTimeFormat", "text", res); });
            this.translate.get('finanzas_grid_usertrackingtime').subscribe((res: string) => { this.myGrid.setcolumnproperty("userTrackingFormat", "text", res); });
            this.translate.get('finanzas_grid_modifiedtrackingtime').subscribe((res: string) => { this.myGrid.setcolumnproperty("modifiedTrackingFormat", "text", res); });
            this.myGrid.localizestrings(this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en : this.jqxHelper.getGridLocation_es);
            this.myLoader.open();
            this.schedulingService.GetPeriods()
                .subscribe(
                    (data: any) => {
                        if (data.result) {
                            this.serverDateTime = new Date(data.currentDateTime);
                            this.sourcePeriodsListBox.localData = data.periodsList;
                            //this.dataAdapterPeriod = new this.$.jqx.dataAdapter(this.sourcePeriodsListBox);
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

    notAllowcellbeginedit = (row: any, datafield: any, columntype: any, value: any) => {
        return false;
    }

    timeRenderer = (row: any, column: any, value: any) => {
        if (row < 0) {
            return "";
        }

        if (this.sourceGridTrackingTimeReview) {
            let utf: string = this.sourceGridTrackingTimeReview.localData[row].userTrackingFormat;
            let mtf: string = value;
            let style: string = utf != mtf ? "style='color:green;'" : "";
            return "<span " + style + ">" + value + "</span>";
        }
        return value;
    }

    aggregatesrenderer = (aggregates: any, column: any, element: any): string => {
        let renderstring = '<div class="jqx-widget-content jqx-widget-content-' + "metro" + '" style="float: left; width: 100%; height: 100%; "/>';
        return renderstring;
    }

    cellsrendererOriginal = (row: any, column: any, value: any, defaultRender: any, colx: any, rowData: any) => {
        if (value.toString().indexOf("custom1") >= 0) {
            return defaultRender.replace("custom1", "Total");
        }
    };

    aggregatesrendererFix = (aggregates: any, column: any, element: any): string => {
        let renderstring = '<div style="position: relative; margin-top: 4px; margin-right:5px; text-align: right; overflow: hidden;">' + 'Total: ' + ': ' + this.commonHelper.convertMinsToHrsMins(aggregates.custom1) + '</div>';
        return renderstring;
    };

    aggregatesrendererFix2 = (aggregates: any, column: any, element: any): string => {
        return aggregates.custom1;
    };

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


    custom1 = (aggregatedValue: number, currentValue: number, column: any, record: any): number => {
        let total = parseInt(record['secondsModifiedTracking']);
        return aggregatedValue + total;
    };

    ParticipantRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata): string => {
        //var isdelete = rowdata.abm == "D" ? " text-decoration:line-through !important; color:Red;" : "";
        let path = rowdata.img ? this.imagePipe.transform(rowdata.img, 'users') : "";
        let img = "<div style=' margin:1px solid blue;   float:left; margin-top:15px;'><img style='border-radius:50%;' height='40' width='40' src='" + path + "' /></div>";
        //let assignedToPosition = rowdata.assignedToPosition ? "<div style='padding-left:1%; " + isdelete + " float:left; width:79%; margin-top:4px; heigth:10px !important; max-heigth:10px !important; overflow: hidden !important; '>" + rowdata.assignedToPosition + "</div>" : "";
        let marginTop = "margin-top:30px;";
        let assignedTo = "<div style='padding-left:1%; float:left; width:79%; " + marginTop + "heigth:10px !important; max-heigth:10px !important; overflow: hidden !important;'>" + rowdata.participantFullName + "</div>"
        return "<div style='height:70px;'>" + img + assignedTo + " </div>"
    };

    projectNameRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var divSquareProject = "<div style='float:left; margin-left:5px; margin-top:5px;  height:60px; width:5px !important; background-color:" + rowdata.projectColor + ";'>&nbsp;&nbsp;</div>&nbsp;";
        return divSquareProject + "<div style='margin-top:10px; margin-left:20px !important;'>" + rowdata.projectName + "</div>";
    };

    columns: any[] =
        [
            {
                text: 'Project',
                datafield: 'projectName',
                width: 'auto',
                cellbeginedit: this.notAllowcellbeginedit,
                aggregatesrenderer: this.aggregatesrenderer,
                cellsrenderer: this.projectNameRenderer
                //groupschanged: this.groupschanged
            },
            {
                text: 'Participant',
                datafield: 'participantFullName',
                width: '240px',
                cellbeginedit: this.notAllowcellbeginedit,
                aggregatesrenderer: this.aggregatesrenderer,
                cellsrenderer: this.ParticipantRenderer
            },
            {
                text: 'Position',
                datafield: 'positionName',
                width: '200px',
                cellbeginedit: this.notAllowcellbeginedit,
                aggregatesrenderer: this.aggregatesrenderer
            },
            {
                text: 'Scheduled Time',
                datafield: 'scheduledTimeFormat',
                width: '125px',
                cellbeginedit: this.notAllowcellbeginedit,
                aggregatesrenderer: this.aggregatesrenderer
            },
            {
                width: '140px',
                text: 'User Tracking Time',
                datafield: 'userTrackingFormat',
                cellbeginedit: this.notAllowcellbeginedit,
                aggregatesrenderer: this.aggregatesrenderer
            },
            {
                width: '160px',
                text: 'Modified Tracking Time',
                datafield: 'modifiedTrackingFormat',
                columntype: 'template',
                cellclassname: this.cellclass,
                createeditor: function (row, cellvalue, editor, cellText, width, height) {
                    editor.jqxMaskedInput({ theme: "metro", width: width, promptChar: '', mask: '[0-9][0-9]:[0-5][0-9]:[0-5][0-9]', height: height });
                },
                initeditor: function (row, cellvalue, editor, celltext, pressedkey) {
                    if (cellvalue != null) {
                        editor.val(cellvalue);
                    }
                    else {
                        editor.val('');
                    }
                },
                geteditorvalue: function (row, cellvalue, editor) {
                    return editor.val();
                },
                aggregatesrenderer: this.aggregatesrendererFix,
                aggregates: [{
                    "custom1": this.custom1
                }],
            }
        ];

    loadTimeTrackingReview = () => {
        this.myLoader.open();
        this.financeService.GetTimeTrackingReview(this.currentPeriod)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.sourceGridTrackingTimeReview.localData = data.tracking;
                        // this.dataAdapterGridTrackingTimeReview = new this.$.jqx.dataAdapter(this.sourceGridTrackingTimeReview);
                        this.dataAdapterGridTrackingTimeReview = new jqx.dataAdapter(this.sourceGridTrackingTimeReview);
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

    canDeleteOrSave: boolean;// = (): boolean => {
    // let disabled = this.periodClosed;
    // return (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor")) && !disabled;
    // }

    PeriodSelectDropDown = (event: any): void => {
        this.currentPeriod = event.args.item.value;
        if (this.currentPeriod > 0) {
            this.periodClosed = this.sourcePeriodsListBox.localData[event.args.index].state == "CL";
            this.loadTimeTrackingReview();
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

    //dataAdapterPeriod: any = new this.$.jqx.dataAdapter(this.sourcePeriodsListBox);
    dataAdapterPeriod: any = new jqx.dataAdapter(this.sourcePeriodsListBox);

    //SCHEDULER'S READY EVENT. 
    ready = (): void => { }

    //CLOSE APPOINTMENT WINDOW
    closeWindow(event: any): void { }

    closeWindowEditAppointment(event: any): void { }

    cancelWindowPeriod(event: any): void { }

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }
}