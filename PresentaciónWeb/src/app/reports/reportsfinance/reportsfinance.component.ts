import { Component, ViewChild, OnInit, Input, ChangeDetectorRef, AfterViewInit, Injectable } from '@angular/core';
import { ConstantService } from '../../common/services/app.constant.service';
import { ProjectsService } from '../../projects/projects.service';
import { FinanceService } from '../../finance/finance.service';
import { CellModel } from '../toolbarEmitter.model';
import { RowModel } from '../toolbarEmitter.model';
import { AuthHelper } from '../../common/helpers/app.auth.helper';
import { PagesModel } from '../toolbarEmitter.model';
import { ReportPageModel } from '../toolbarEmitter.model';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { jqxListBoxComponent } from 'jqwidgets-ng/jqxlistbox';
import { jqxNumberInputComponent } from 'jqwidgets-ng/jqxnumberinput';
import { Router } from '@angular/router';
import { CommonHelper } from '../../common/helpers/app.common.helper';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
import { SchedulingService } from '../../scheduling/scheduling.service';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

@Component({
    selector: 'reportfinance',
    templateUrl: 'reportfinance.component.template.html',
    providers: [ConstantService, ProjectsService, FinanceService, AuthHelper, CommonHelper, SchedulingService],
    styleUrls: ['../styleReport.component.css'],
})

@Injectable()
export class ReportFinance implements OnInit, AfterViewInit {
    constructor(public constantService: ConstantService,
        private chRef: ChangeDetectorRef,
        private authHelper: AuthHelper,
        private translate: TranslateService,
        private commonHelper: CommonHelper,
        private schedulingService: SchedulingService,
        public projectService: ProjectsService,
        public financeService: FinanceService,
        public router: Router) {
        this.translate.setDefaultLang('en');
    }

    @Input('titleReport') titleReport: string = "Time Tracking";
    @Input('layout') layout: string = "";
    columns: string[] = [];// ["Position", "Participant", "Scheduled Time","User Tracked","Modified Track.."]; //"Assigned To", 

    @Input('groupsColumns') groupsColumns: string[] = ["idfPeriod"];
    groupsLabels: string[] = [""];
    @Input('columnsWidth') columnsWidth: string[] = ["192", "192", "192", "192", "192"];

    @ViewChild('projectsDropDown') projectsDropDown: jqxListBoxComponent;
    @ViewChild('periodsDropDown') periodsDropDown: jqxListBoxComponent;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('txtNumericPage') txtNumericPage: jqxNumberInputComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;

    subtitleReport: string = "";
    subtitleReport2: string = "";


    subtitleReportAux: string = "";
    subtitleReport2Aux: string = "";

    selectHolder: string;
    loadedControl: boolean[] = [false, false];

    currentProject: number = 0;
    currentPeriod: number = 0;

    maxRowsFirsPage: number = 32;
    maxRowsPerPage: number = 41;

    msgError: string = "";

    report: ReportPageModel = new ReportPageModel();
    currentPage: number = 1;
    zoomNumericValue = 4; //1 2 3 4(100%) 5 6

    ngOnInit() {
    }

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

    periodsBindingComplete = (event: any) => {
        if (this.sourcePeriodsListBox.localData != undefined) {
            if (this.sourcePeriodsListBox.localData.length > 0) {
                this.periodsDropDown.selectedIndex(0);
            }
        }
    }

    PeriodsSelectDropDown = (event: any): void => {
        this.currentPeriod = event.args.item.value;
        if (this.currentPeriod > 0) {
            this.LoadReport();
        }
    }

    sourceProjectsListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'projectName', type: 'string' },
                { name: 'description', type: 'string' },
                { state: 'state', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterProjects: any = new jqx.dataAdapter(this.sourceProjectsListBox);

    projectsBindingComplete = (event: any) => {
        if (this.sourceProjectsListBox.localData != undefined) {
            if (this.sourceProjectsListBox.localData.length > 0) {
                this.projectsDropDown.selectedIndex(0);
            }
        }
    }

    ProjectSelectDropDown = (event: any): void => {
        this.currentProject = event.args.item.value;

        if (this.currentProject > 0) {
            this.LoadReport();
        }
    }

    LoadReport = (): void => {
        //this.groupsColumns = ['projectName'];
        //this.columnsWidth = ['300', '200', '200'];

        if (this.currentPeriod != 0 && this.currentProject != 0) {

            this.myLoader.open();

            this.financeService.GetTimeTrackingReviewByProjectAndPeriod(this.currentPeriod, this.currentProject)
                .subscribe(
                    (rdata: any) => {


                        if (rdata.result) {
                            let data: RowModel[] = [];

                            if (rdata.tracking.length > 0) {
                                this.subtitleReport = this.subtitleReportAux + " : " + rdata.tracking[0].projectName;
                                this.subtitleReport2 = this.subtitleReport2Aux + " : " + this.periodsDropDown.getSelectedItem().label;
                            }

                            for (var i = 0; i < rdata.tracking.length; i++) {
                                let newrow = new RowModel();

                                let newcell654 = new CellModel("idfPeriod", 40);
                                newcell654.value = rdata.tracking[i].idfPeriod;
                                newrow.cells.push(newcell654);

                                let newcell65 = new CellModel("positionName", 40);
                                newcell65.value = rdata.tracking[i].positionName;
                                newrow.cells.push(newcell65);



                                let newcell4 = new CellModel("participantFullName", 40);
                                newcell4.value = rdata.tracking[i].participantFullName;
                                newrow.cells.push(newcell4);

                                let newcell5 = new CellModel("scheduledTimeFormat", 10);
                                //newcell5.value = rdata.tracking[i].from.replace("T"," ");
                                newcell5.value = rdata.tracking[i].scheduledTimeFormat;
                                newrow.cells.push(newcell5);

                                let newcell3 = new CellModel("secondsUserTracking", 40);
                                //newcell3.value = rdata.tracking[i].to.replace("T"," ");
                                newcell3.value = this.commonHelper.convertMinsToHrsMins(rdata.tracking[i].secondsUserTracking);
                                newrow.cells.push(newcell3);

                                let newcell334 = new CellModel("secondsModifiedTracking", 40);
                                //newcell334.value = this.commonHelper.convertMinsToHrsMins(rdata.tracking[i].hours);
                                newcell334.value = this.commonHelper.convertMinsToHrsMins(rdata.tracking[i].secondsModifiedTracking);
                                newrow.cells.push(newcell334);

                                data.push(newrow)
                            }
                            this.ProcessReport(data);
                            this.myLoader.close();
                        }
                        //   else {
                        //       alert("Error");
                        //   }
                    },
                    error => {
                        this.myLoader.close();
                        alert("Error");
                    });

            setTimeout(() => {

            });
        }
    }

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
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

    ngAfterViewInit(): void {
        setTimeout(() => {

            this.translate.use('en');

            this.translate.get('global_select').subscribe((res: string) => {
                this.selectHolder = res;
            });

            this.translate.get('report_name_5_period').subscribe((resPeriod: string) => {
                this.translate.get('report_name_5_project').subscribe((resProjecto: string) => {
                    this.translate.get('report_name_5').subscribe((res: string) => {
                        this.titleReport = res;
                        this.subtitleReportAux = resProjecto;
                        this.subtitleReport2Aux = resPeriod;

                        this.translate.get('report_name_5_col_0').subscribe((resCol0: string) => {
                            this.translate.get('report_name_5_col_1').subscribe((resCol1: string) => {
                                this.translate.get('report_name_5_col_2').subscribe((resCol2: string) => {
                                    this.translate.get('report_name_5_col_3').subscribe((resCol3: string) => {
                                        this.translate.get('report_name_5_col_4').subscribe((resCol4: string) => {

                                            this.columns = [resCol0, resCol1, resCol2, resCol3, resCol4];

                                            this.myLoader.open();
                                            this.projectService.GetProjects()
                                                .subscribe(
                                                    (data: any) => {
                                                        if (data.result) {
                                                            this.sourceProjectsListBox.localData = data.projects;
                                                            this.dataAdapterProjects = new jqx.dataAdapter(this.sourceProjectsListBox);
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

                                            this.schedulingService.GetPeriods()
                                                .subscribe(
                                                    (data: any) => {
                                                        if (data.result) {
                                                            this.sourcePeriodsListBox.localData = data.periodsList;
                                                            this.dataAdapterPeriod = new jqx.dataAdapter(this.sourcePeriodsListBox);
                                                        }
                                                        else {
                                                            this.manageError(data);
                                                        }

                                                        this.loadedControl[1] = true;
                                                        this.HiddeLoaderWhenEnd();
                                                    },
                                                    error => {
                                                        this.manageError(error);
                                                    }
                                                );
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });
    }

    onlyUnique = (value, index, self): any => {
        return self.indexOf(value) === index;
    }

    existInArray = (array: string[], val: string): boolean => {
        if (array) {
            for (var i = 0; i < array.length; i++) {
                if (array[i] == val) {
                    return true;
                }
            }
        }
        return false;
    }

    groupsValuesProductoria: string[][] = [];
    pages: any[] = [];
    groupsValues: string[][] = [];

    ProcessReport = (data: RowModel[]): void => {
        this.groupsValuesProductoria = [];
        this.pages = [];
        this.groupsValues = [];
        this.report = new ReportPageModel();

        for (var i = 0; i < this.groupsColumns.length; i++) {
            this.groupsValues[i] = [];
        }
        if (this.groupsColumns.length > 0) {
            for (var i = 0; i < data.length; i++) {
                for (var c = 0; c < data[i].cells.length; c++) {
                    for (var gc = 0; gc < this.groupsColumns.length; gc++) {
                        if (data[i].cells[c].name.toLowerCase() === this.groupsColumns[gc].toLowerCase()) {
                            if (!this.existInArray(this.groupsValues[gc], data[i].cells[c].value)) {
                                this.groupsValues[gc].push(data[i].cells[c].value);
                            }
                        }
                    }
                }
            }
            switch (this.groupsValues.length) {
                case 1:
                    for (var i = 0; i < this.groupsValues[0].length; i++) {
                        if (!this.groupsValuesProductoria[i]) {
                            this.groupsValuesProductoria[i] = [];
                        }
                        this.groupsValuesProductoria[0].push(this.groupsValues[0][i]);
                    }

                    break;
                case 2:

                    let fila: number = 0;
                    for (var i = 0; i < this.groupsValues[0].length; i++) {
                        for (var j = 0; j < this.groupsValues[1].length; j++) {

                            if (!this.groupsValuesProductoria[fila]) {
                                this.groupsValuesProductoria[fila] = [];
                            }

                            this.groupsValuesProductoria[fila].push(this.groupsValues[0][i]);
                            this.groupsValuesProductoria[fila].push(this.groupsValues[1][j]);

                            fila++;
                        }
                    }

                    break;
            }

            let globalPagination: number = 0;

            for (var g = 0; g < this.groupsValuesProductoria.length; g++) {
                let dataPage: RowModel[] = [];
                globalPagination++;
                switch (this.groupsValues.length) {
                    case 1:
                        dataPage = data.filter(x => x.cells[0].value == this.groupsValuesProductoria[g][0]);
                        break;
                    case 2:
                        dataPage = data.filter(x => x.cells[0].value == this.groupsValuesProductoria[g][0] && x.cells[1].value == this.groupsValuesProductoria[g][1]);
                        break;
                }

                let pagination: number = 1;
                let page: PagesModel = new PagesModel();
                let countrowperpage: number = 0;

                for (var rr = 0; rr < dataPage.length; rr++) {
                    page.groupLabels = pagination == 1 ? this.groupsLabels : [];
                    page.groupsValues = pagination == 1 ? this.groupsValuesProductoria[g] : [];
                    page.title = globalPagination == 1 ? this.titleReport : "";
                    /*check is cell group*/
                    let newrow: RowModel = new RowModel();
                    newrow.cells = [];

                    for (var cg = 0; cg < dataPage[rr].cells.length; cg++) {
                        let newcell: CellModel;
                        let isGroup: boolean = false;

                        for (var gcol = 0; gcol < this.groupsColumns.length; gcol++) {
                            if (dataPage[rr].cells[cg].name.toLowerCase() === this.groupsColumns[gcol].toLowerCase()) {
                                isGroup = true;
                            }
                        }

                        if (!isGroup) {
                            newcell = new CellModel(dataPage[rr].cells[cg].name, dataPage[rr].cells[cg].width);
                            newcell.value = dataPage[rr].cells[cg].value;
                            newrow.cells.push(newcell);
                        }
                    }

                    countrowperpage++;
                    page.rows.push(newrow);

                    if (pagination == 1 && countrowperpage > this.maxRowsFirsPage) {
                        if (page.rows.length > 0) {

                            this.report.pages.push(page);
                        }
                        pagination++;
                        page = new PagesModel();
                        countrowperpage = 0;
                        globalPagination++;
                    }
                    else {
                        if (countrowperpage > this.maxRowsPerPage && pagination > 1) {
                            if (page.rows.length > 0) {

                                this.report.pages.push(page);
                            }
                            pagination++;
                            page = new PagesModel();
                            countrowperpage = 0;
                            globalPagination++;
                        }
                        //maxRowsPerPage
                    }
                }

                if (page.rows.length > 0) {
                    this.report.pages.push(page);
                }
            }
        }
    }

    zoom = (): string => {
        return "zoom" + this.zoomNumericValue;
    };

    numPages = (): number => {
        return document.getElementsByClassName("page").length;
    };

    MoreZoom = (event: any): void => {
        this.zoomNumericValue = this.zoomNumericValue < 6 ? this.zoomNumericValue + 1 : this.zoomNumericValue;
    };

    LessZoom = (event: any): void => {
        this.zoomNumericValue = this.zoomNumericValue > 2 ? this.zoomNumericValue - 1 : this.zoomNumericValue;
    };

    getZommText = (): string => {
        return Number(this.zoomNumericValue / 0.04) + "%";
    }

    MorePage = (event: any): void => {
        this.zoomNumericValue = 4;
        this.currentPage = this.currentPage + 1 > this.numPages() ? this.currentPage : this.currentPage + 1;
        this.txtNumericPage.val(this.currentPage);
        this.scrollTo('#page' + this.currentPage, '#vscrollable', null);
    };

    LessPage = (event: any): void => {
        this.zoomNumericValue = 4;
        this.currentPage = this.currentPage - 1 >= 1 ? this.currentPage - 1 : this.currentPage;
        this.txtNumericPage.val(this.currentPage);
        this.scrollTo('#page' + this.currentPage, '#vscrollable', null);
    };

    keyDownNumber = (event: any) => {
        if (event.keyCode == 13) {
            if (this.report.pages.length > 0) {
                var value = this.txtNumericPage.val();
                this.zoomNumericValue = 4;
                this.currentPage = value > this.numPages() || value < 1 ? this.currentPage : value;
                this.txtNumericPage.val(this.currentPage);
                this.scrollTo('#page' + this.currentPage, '#vscrollable', null);
            }
        }
    }

    Print = (event: any): void => {
        this.printDiv();
    }

    printDiv() {
        let printContents = "";

        for (var i = 0; i < document.getElementsByClassName("page").length; i++) {
            //printContents += "<div class='page pageA4'>" + document.getElementsByClassName("page")[i].innerHTML + "</div>";
            printContents += "<div class='reportMainRight'><div class='page pageA4'>" + document.getElementsByClassName("page")[i].innerHTML + "</div></div>";
        }

        if (window) {
            if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {

                var popup = window.open('', '_blank',
                    'width=600,height=600,scrollbars=no,menubar=no,toolbar=no,'
                    + 'location=no,status=no,titlebar=no');

                popup.window.focus();
                popup.document.write('<!DOCTYPE html><html><head>  '
                    + '<link rel="stylesheet" href="' + this.constantService.REPORT_FILE + '" '
                    + 'media="screen,print">'
                    + '</head><body onload="window.print()"><div class="reward-body">'
                    + printContents + '</div></html>');
                popup.onbeforeunload = function (event) {
                    popup.close();
                    return '.\n';
                };
                popup.onabort = function (event) {
                    popup.document.close();
                    popup.close();
                };
            } else {
                var popup = window.open('', '_blank', 'width=800,height=600');
                popup.document.open();
                popup.document.write('<!DOCTYPE html><html><head>  '
                    + '<link rel="stylesheet" href="' + this.constantService.REPORT_FILE + '" '
                    + 'media="screen,print">'
                    + '</head><body onload="window.print()"><div class="reward-body">'
                    + printContents + '</div></html>');
                popup.document.close();
            }
            popup.document.close();
        }
    }

    clickOnPage = (pageNumber: number): void => {
        this.currentPage = pageNumber;
        this.txtNumericPage.val(this.currentPage);
    }

    scrollTo(selector, parentSelector, horizontal) {
        //scrollTo(
        //    selector,       // scroll to this
        //    parentSelector, // scroll within (null if window scrolling)
        //    horizontal,     // is it horizontal scrolling
        //    0               // distance from top or left
        //);
    }

    scrollEvent(e) {
    }
}