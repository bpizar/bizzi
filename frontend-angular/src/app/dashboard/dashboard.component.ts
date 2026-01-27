import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { DashBoardService } from './dashboard.service';
import { Observable } from 'rxjs';
// import { jqxChartComponent } from 'jqwidgets-ng/jqwidgets/jqxChart';
import { jqxBarGaugeComponent } from 'jqwidgets-ng/jqxbargauge';
import { jqxGaugeComponent } from 'jqwidgets-ng/jqxgauge';
// import {  jqxDrawComponent} from  'jqwidgets-ng/jqxdraw';
// '../../modules/bargauge.module';
import { ConstantService } from '../common/services/app.constant.service';
import { AuthHelper } from '../common/helpers/app.auth.helper';
import { jqxGridComponent } from 'jqwidgets-ng/jqxgrid';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { jqxNotificationComponent } from 'jqwidgets-ng/jqxnotification';
//import { DashBoardService } from './dashboard.service';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

@Component({
    selector: 'dashboard',
    templateUrl: '../dashboard/dashboard.component.template.html',
    providers: [DashBoardService, ConstantService, AuthHelper, DashBoardService] // 
})

export class Dashboard implements OnInit, AfterViewInit {
    titleDashboard1: string;
    descriptionDashboard1: string;
    titleDashboard3: string;
    descriptionDashboard3: string;
    // @ViewChild('dashboard1') dashboard1: jqxChartComponent;
    @ViewChild('dashboard2') dashboard2: jqxBarGaugeComponent;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;

    constructor(private dashboarsService: DashBoardService,
        private constantService: ConstantService,
        private translate: TranslateService) {
        this.translate.setDefaultLang('en');
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
            this.myLoader.close();
            this.translate.get('dashboard_dash1_title').subscribe((res: string) => { this.titleDashboard1 = res; });
            this.translate.get('dashboard_dash1_description').subscribe((res: string) => { this.descriptionDashboard1 = res; });
            this.translate.get('dashboard_dash2_title').subscribe((res: string) => { this.titleDashboard2.text = res; });
            this.translate.get('dashboard_dash1_title').subscribe((res: string) => { this.titleDashboard3 = res; });
            // this.translate.get('dashboard_dash1_description').subscribe((res: string) => { this.descriptionDashboard3 = res;});
            this.descriptionDashboard3 = "";
            this.translate.get('dashboard_dash3_xaxistitle').subscribe((res: string) => { this.valueAxis3.title.text = res; });
            this.translate.get('dashboard_dash3_title').subscribe((res: string) => { this.titleDashboard3 = res; });
        });
    }

    ngOnInit() {
        this.LoadDashboar1();
        this.LoadDashboar2();
        this.LoadDashboar3();
        //this.translate.onLangChange.subscribe((event: LangChangeEvent) => {

        // this.translate.get('dashboard_dash1_title').subscribe((res: string) => { this.titleDashboard1 = res;});
        // this.translate.get('dashboard_dash1_description').subscribe((res: string) => { this.descriptionDashboard1 = res;});
        // this.translate.get('dashboard_dash2_title').subscribe((res: string) => { this.titleDashboard2.text = res;});
        //});
    }

    // DASHBOARD 1 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    sourceDashboard1: any =
        {
            datatype: 'json',
            datafields: [
                { name: 'id' },
                { name: 'description' }
            ],
        };

    dataAdapterDashboard1: any;
    paddingDashboard1: any = { left: 5, top: 5, right: 5, bottom: 5 };
    titlePaddingDashboard1: any = { left: 0, top: 0, right: 0, bottom: 0 };
    legendLayout1: any = { left: 20, top: 40, width: 150, height: 180, flow: 'vertical' };

    sourceDashboard3: any =
        {
            datatype: 'json',
            datafields: [
                { name: 'id' },
                { name: 'description1' },
                { name: 'description2' }
            ],
        };

    dataAdapterDashboard3: any;
    // formatFunction2=(value:any):number =>
    // {
    //     var realVal = parseInt(value);
    //     //return ('Year: 2016<br/>Price Index:' + realVal);
    //     return 34;
    // }

    seriesGroupsDashboard1: any[] =
        [
            {
                type: 'donut',
                showLabels: true,
                series:
                    [
                        {
                            dataField: 'description',
                            displayText: 'id',
                            labelRadius: 140,
                            initialAngle: 15,
                            radius: 100,
                            innerRadius: 20,
                            centerOffset: 0,
                            formatSettings: { sufix: '', decimalPlaces: 0 }
                        }
                    ]
            }
        ];

    LoadDashboar1 = (): void => {
        this.dashboarsService.GetTasksForDashboard1()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.descriptionDashboard1 = data.tagInfo;
                        // this.dashboard1.setOptions({ description: "Period: " + data.tagInfo });
                        this.sourceDashboard1.localData = data.messages;
                        this.dataAdapterDashboard1 = new jqx.dataAdapter(this.sourceDashboard1,
                            {
                                async: true,
                                autoBind: true,
                                // loadError: (xhr: any, status: any, error: any) => { alert('Error loading : ' + error); }
                            });
                    }
                    else {
                    }
                },
                error => {
                });
    }

    // DASHBOARD 2 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    maxDashboard2: number = 10;
    valuesDashboard2: number[] = [];
    projectsDashboard2: string[] = [];

    labels2: any =
        {
            formatFunction: (value: string, index: number): any => {
                return parseInt(value).toString();
            }
        }

    tooltipDashboard2: any =
        {
            visible: true,
            formatFunction: (value: string, index: number) => {
                let project = this.projectsDashboard2[index];
                return "Project : " + project;
            }
        };

    customColorSchemeDashboard2: any = {
        name: 'rgb',
        colors: []
    }

    titleDashboard2: any = {
        text: 'Not done tasks by Projects',
        font: {
            size: 14,
            weight: 'bold'
        },
        verticalAlignment: 'top',
        margin: 0,
        subtitle: {
            text: '',
            font: {
                size: 14,
                weight: 200
            }
        },
    };

    LoadDashboar2 = (): void => {
        this.dashboarsService.GetDashboard2()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.titleDashboard2.subtitle.text = data.tagInfo;
                        this.customColorSchemeDashboard2.colors = data.colors;
                        this.maxDashboard2 = data.maxValue;
                        this.valuesDashboard2 = data.values;
                        this.projectsDashboard2 = data.projectNames;
                        this.dashboard2.setOptions({ title: this.titleDashboard2 });
                    }
                    else {
                    }
                },
                error => {
                });
    }

    valueDashboard3: number = 0;

    xAxis3: any =
        {
            dataField: 'id',
            unitInterval: 1,
            axisSize: 'auto',
            tickMarks: {
                visible: true,
                interval: 1,
                color: '#BCBCBC'
            },
            gridLines: {
                visible: true,
                interval: 1,
                color: '#BCBCBC'
            }
        };

    valueAxis3: any =
        {
            unitInterval: 1,
            minValue: 0,
            maxValue: 5,
            title: { text: '' },
            labels: { horizontalAlignment: 'right' },
            tickMarks: { color: '#BCBCBC' },
            // gridLines: { 
            //     visible: false,
            //     step: 10,
            //     color: '#888888'
            // }
        };

    seriesGroups3: any[] =
        [
            {
                type: 'stackedcolumn',
                columnsGapPercent: 20,
                seriesGapPercent: 20,
                series: [
                    //{ dataField: 'id', displayText: 'Period' },
                    { dataField: 'description1', displayText: 'Not Assigned' },
                    { dataField: 'description2', displayText: 'Assigned' }
                ]
            }
        ];

    LoadDashboar3 = (): void => {
        this.dashboarsService.GetDashboard3()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        // this.valueAxis3.maxValue =  Number(data.values[0].description1) +  Number(data.values[0].description2);
                        ////this.translate.get('dashboard_dash3_xaxistitle').subscribe((res: string) => {});
                        this.valueAxis3.maxValue = (Number(data.values[0].description1) + Number(data.values[0].description2));
                        //this.valueAxis3.
                        this.valueAxis3.unitInterval = this.valueAxis3.maxValue < 10 ? this.valueAxis3.unitInterval = 1 : this.valueAxis3.maxValue < 100 ? 10 : this.valueAxis3.maxValue < 1000 ? 100 : 1000;
                        //this.valueAxis3.gridLines.step=5;
                        //this.valueDashboard3 = data.values[0] ? data.values[0].description : "";
                        this.sourceDashboard3.localData = data.values;
                        this.dataAdapterDashboard3 = new jqx.dataAdapter(this.sourceDashboard3,
                            {
                                async: true,
                                autoBind: true,
                                // loadError: (xhr: any, status: any, error: any) => { alert('Error loading : ' + error); }
                            });
                    }
                    else {
                    }
                },
                error => {
                });
    }
}