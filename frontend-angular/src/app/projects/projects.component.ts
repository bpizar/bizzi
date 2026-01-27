import { Component, ViewChild, AfterViewInit, OnInit, ChangeDetectorRef } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { ProjectsService } from './projects.service';
import { jqxGridComponent } from 'jqwidgets-ng/jqwidgets/jqxgrid';
import { Router } from '@angular/router';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
//import { jqxNotificationComponent } from 'jqwidgets-ng/jqwidgets/jqxnotification';
import { AuthHelper } from '../common/helpers/app.auth.helper';
import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';
import { JwtInterceptor } from '../common/helpers/app.http.interceptor';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { JqxHelper } from '../common/helpers/app.jqx.helper'
import { jqxScrollBarComponent } from 'jqwidgets-ng/jqwidgets/jqxscrollbar';
import { jqxTooltipComponent } from 'jqwidgets-ng/jqwidgets/jqxtooltip';

@Component({
    selector: 'project',
    templateUrl: '../projects/projects.component.template.html',
    providers: [ProjectsService, ConstantService, AuthHelper, JwtInterceptor, JqxHelper] //
})

export class Projects implements OnInit, AfterViewInit {

    @ViewChild('gridReference') myGrid: jqxGridComponent;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    // @ViewChild('msgNotificationError') msgNotificationError: jqxNotificationComponent;
    //@ViewChild('msgNotificationSuccess') msgNotificationSuccess: jqxNotificationComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('gridReference') gridReference: jqxGridComponent;
    msgError: string = "";
    msgSuccess: string = "";

    constructor(private jqxHelper: JqxHelper,
        private translate: TranslateService,
        private projectsService: ProjectsService,
        private authHelper: AuthHelper,
        private chRef: ChangeDetectorRef,
        private router: Router,
    ) {
        this.translate.setDefaultLang('en');
    }

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

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');

            this.LoadProjects();
            this.myGrid.localizestrings(this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en : this.jqxHelper.getGridLocation_es);
        });
    }

    CurrentRow: number = -1;
    ProjectName: string = "";
    Description: string = "";

    isEditing = (): boolean => {
        return this.CurrentRow >= 0;
    }

    createNew(event: any): void {
        this.CurrentRow = -1;
        this.router.navigate(['projects/editproject', this.CurrentRow]);
    }

    ready = (): void => {
        this.translate.get('projects_grid_projects').subscribe((res: string) => { this.myGrid.setcolumnproperty("projectName", "text", res); });
        this.translate.get('projects_grid_description').subscribe((res: string) => { this.myGrid.setcolumnproperty("description", "text", res); });
        this.translate.get('projects_grid_begin').subscribe((res: string) => { this.myGrid.setcolumnproperty("beginDate", "text", res); });
        this.translate.get('projects_grid_end').subscribe((res: string) => { this.myGrid.setcolumnproperty("endDate", "text", res); });
    };

    source: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'description', type: 'string' },
                { name: 'state', type: 'string' },
                { name: 'color', type: 'color' },
                { name: 'beginDate', type: 'date' },
                { name: 'endDate', type: 'date' },
                { name: 'visible', type: 'boolean' },
                { name: 'totalHours', type: 'number' },
            ],
            id: 'id',
        }

    dataAdapter: any = new jqx.dataAdapter(this.source);

    projectNameRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var divSquareProject = "<div style='float:left; margin-left:5px; margin-top:5px;  height:60px; width:5px !important; background-color:#" + rowdata.color + ";'>&nbsp;&nbsp;</div>&nbsp;";
        return divSquareProject + "<div style='margin-top:10px; margin-left:20px !important;'>" + rowdata.projectName + "</div>";
    };

    dateRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        let date = new Date(value);
        return "<div style='margin-top:30px; text-indent:10px;'>" + date.toDateString() + "</div>";
    };


    editClick(event: any): void {
        let cr = this.myGrid.getrowdata(this.myGrid.selectedrowindex());
        this.router.navigate(['projects/editproject', cr.id]);
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
            if (document.getElementsByClassName("btnedit").length > 0) {
                let Buttons = jqwidgets.createInstance(".btnedit", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-more' style='padding:0px !important; font-size:16px; color:var(--thirteenth-color);'></i>" + '', template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });


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

    editRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var salida = "<div class='tooltip'> " + "<div data-row='" + row + "' class='btnedit'></div>" + "<span class='tooltiptext'>Edit</span></div>";
        return salida;
    }

    columns: any[] =
        [
            {
                width: '90',  //120
                text: '',
                datafield: 'Edit',
                height: '70px',
                columntype: 'none',
                cellsrenderer: this.editRenderer,
                menu: false,
                sortable: false,
                filterable: false
            },
            {
                //text: 'Project Name',
                // <div translate>Title</div>
                text: '&nbsp;',
                datafield: 'projectName',
                width: 'auto',
                cellsrenderer: this.projectNameRenderer
            },
            {
                text: 'Description',
                datafield: 'description',
                width: 'auto'
            },
            {
                text: 'Begin',
                datafield: 'beginDate',
                width: '140px',
                cellsrenderer: this.dateRenderer
            },
            {
                text: 'End',
                datafield: 'endDate',
                width: '140px',
                cellsrenderer: this.dateRenderer
            }
        ];

    cellClick(event: any): void {
        // var row = event.args.row.bounddata;
        // this.CurrentRow = row.id;

        // if (event.args.datafield === 'Edit') {
        //     this.router.navigate(['projects/editproject', this.CurrentRow]);
        // }
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

    canAddNew = (): boolean => {
        return this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor");
    }

    manageError = (data: any): void => {
        this.glowMessage.ShowGlowByError(data);
    }

    LoadProjects = () => {
        this.myLoader.open();
        this.projectsService.GetProjectsList()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.source.localData = data;
                        this.dataAdapter = new jqx.dataAdapter(this.source);
                        this.myGrid.updatebounddata();
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

    ngOnInit() {
        // this.translate.onLangChange.subscribe((event: LangChangeEvent) => {    
        //     this.translate.get('Title').subscribe((res: string) => { this.myGrid.setcolumnproperty("projectName","text",res); });
        //     // this.translate.get('dashboard_dash1_description').subscribe((res: string) => { this.descriptionDashboard1 = res;});
        //     // this.translate.get('dashboard_dash2_title').subscribe((res: string) => { this.titleDashboard2.text = res;});
        // });
        // this.translate.onLangChange.subscribe((event: LangChangeEvent) => {  

        // });
    }
}
