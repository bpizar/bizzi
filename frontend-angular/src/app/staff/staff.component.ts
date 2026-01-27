import { Component, ViewChild, AfterViewInit, OnInit, ChangeDetectorRef } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { StaffService } from './staff.service';
import { jqxGridComponent } from 'jqwidgets-ng/jqxgrid';
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { jqxInputComponent } from 'jqwidgets-ng/jqxinput';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { Router } from '@angular/router';
import { AuthHelper } from '../common/helpers/app.auth.helper';
import { positionsModel } from './staff.component.model';
import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { JqxHelper } from '../common/helpers/app.jqx.helper'
import { jqxTooltipComponent } from 'jqwidgets-ng/jqxtooltip';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';

@Component({
    selector: 'project',
    templateUrl: '../staff/staff.component.template.html',
    providers: [StaffService, ConstantService, AuthHelper, JqxHelper],
})

export class Staff implements OnInit, AfterViewInit {

    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('gridReference') myGrid: jqxGridComponent;
    @ViewChild('positionsWindow') positionsWindow: jqxWindowComponent;
    @ViewChild('txtAddPosition') txtAddPosition: jqxInputComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('xtooltip') xtooltip: jqxTooltipComponent;
    @ViewChild('gridReference') gridReference: jqxGridComponent;
    imagePipe = new ImagePipe();
    isInrol: boolean = false;
    ignoreCheckedPositions: boolean = false;

    selectPosition = (event: any) => {
        this.ignoreCheckedPositions = true;
        this.currentPositionSelected = event.id;
        this.ignoreCheckedPositions = false;
    }

    validateAddPosition = (): boolean => {
        let result = true;
        let txtAddPosition: string = this.txtAddPosition.val();
        // let rolesChecked = this.cmbAddRoles.getSelectedItems();

        if (txtAddPosition.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_staff_type_position");
            result = false;
        }
        else {
            let filtered = this.positions.filter(c => c.abm != "D" && c.name == txtAddPosition.trim());
            if (filtered.length > 0) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_staff_exist_position");
                result = false;
            }
        }
        return result;
    }

    idpositionsmanager: number = -1;
    currentPositionSelected: number = -1;

    addPositionClick = (event: any) => {
        if (this.validateAddPosition()) {
            let txtAddPosition: string = this.txtAddPosition.val();
            let newid = this.idpositionsmanager--;
            let newPosition = new positionsModel();
            newPosition.id = newid;
            newPosition.name = txtAddPosition.trim();
            newPosition.abm = "I";
            this.positions.push(newPosition);
            this.idpositionsmanager--;
            this.txtAddPosition.val("");
        }
    };

    savePositions = (event: any) => {
        if (!this.isInrol) {
            this.positionsWindow.close();
            return;
        }

        var request = {
            Positions: this.positions.filter(x => x.abm != "")
        };

        this.staffServiceService.SavePositions(request)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.positionsWindow.close();
                        this.glowMessage.ShowGlow("warn", "glow_success", "glow_staff_position_saved_successfully");
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

    canAddNew = (): boolean => {
        return this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor");
    }

    cancelWindowPositions = (event: any) => {
        this.positionsWindow.close();
    }

    removePositionClick = (event: any) => {
        this.positions.filter(c => c.id == event.id)[0].abm = "D";
        this.currentPositionSelected = -1;
    }

    positions: positionsModel[] = [];

    openWindowManagePositions = (event: any) => {
        this.translate.get('staff_positions_manager').subscribe((res: string) => {
            this.positionsWindow.setTitle(res);
            this.positionsWindow.open();
        });

        this.currentPositionSelected = -1;
        this.staffServiceService.GetPositions()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        setTimeout(() => {
                            this.positions = <positionsModel[]>data.positions;
                        });
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
    };

    constructor(private jqxHelper: JqxHelper,
        private translate: TranslateService,
        private staffServiceService: StaffService,
        private chRef: ChangeDetectorRef,
        private authHelper: AuthHelper,
        private constantService: ConstantService,
        private router: Router) {
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

    //Deprecar ?? Sure?
    ngAfterViewInit(): void {
        setTimeout(() => {
            this.isInrol = this.authHelper.IsInRol("positionsmanager");
            this.translate.use('en');
            this.LoadStaff();
            this.myGrid.localizestrings(this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en : this.jqxHelper.getGridLocation_es);
            this.translate.get('staff_grid_fullname').subscribe((res: string) => { this.myGrid.setcolumnproperty("fullName", "text", res); });
            this.translate.get('global_email').subscribe((res: string) => { this.myGrid.setcolumnproperty("email", "text", res); });
        });
    }

    CurrentRow: number = -1;

    isEditing = (): boolean => {
        return this.CurrentRow >= 0;
    }

    createNew(event: any): void {
        this.CurrentRow = -1;
        this.router.navigate(['staff/editstaff', this.CurrentRow]);
    }

    imagerenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var path = this.imagePipe.transform(rowdata.img, 'users');
        return '<img style="border-radius:50%; margin: 5px;" height="60" width="60" src="' + path + '"/>';
    };

    msgError: string = "";
    msgSuccess: string = "";

    manageError = (data: any): void => {
        // if (data.status != undefined && data.status == 403) {
        //     this.msgError = "Unauthorized";
        // }
        // else {
        //     this.msgError = data.messages != undefined ? data.messages[0].description : this.authHelper.loggedIn() ? "Connection error" : "Your session expired";
        // }

        // this.msgNotificationError.elementRef.nativeElement.childNodes[0].innerText = this.msgError;
        // this.msgNotificationError.open();
        this.glowMessage.ShowGlowByError(data);
    }

    source: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'fullName', type: 'string' },
                { name: 'positionName', type: 'string' },
                { name: 'email', type: 'string' },
                { name: 'idfUser', type: 'number' },
                { name: 'img', type: 'string' },
                { name: 'cellNumber', type: 'string' },
                { name: 'homePhone', type: 'string' },
                { name: 'city', type: 'string' },
                { name: 'projectInfo', type: 'string' }
            ],
            id: 'id',
        }

    dataAdapter: any = new jqx.dataAdapter(this.source);

    rendererPositionColumn = (row: any, column: any, value: any) => { };

    editClick(event: any): void {
        let cr = this.myGrid.getrowdata(this.myGrid.selectedrowindex());
        this.router.navigate(['staff/editstaff', cr.id]);
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

    readygrid: boolean;

    ready = (): void => {
        this.readygrid = true;
    }

    renderedGrid = (): void => {
        if (!this.readygrid) {
            return;
        }
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
                //let flattenButtons = flatten(Buttons);
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
        //return "<i class='ion ion-edit' style='padding:0px !important; font-size:16px; color:white;'></i>";
        // return "<i class='ion ion-ios-more' style='padding:0px !important; font-size:16px; color:white;'></i>";
        // return '<jqxTooltip  margin: 5px;" height="60" width="60"  [position]="\'bottom\'" [content]="\'View Details\'"> <i class="ion ion-ios-more" style="padding:0px !important; font-size:16px; color:white;"></i> </jqxTooltip>';
        var salida = "<div class='tooltip'> " + "<div data-row='" + row + "' class='btnedit'></div>" + "<span class='tooltiptext'>Edit</span></div>";
        return salida;
    };

    columns: any[] =
        [
            {
                width: '90px',
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
                text: '',
                datafield: 'image',
                width: '80px',
                cellsrenderer: this.imagerenderer,
                menu: false,
                sortable: false,
                filterable: false
            },
            {
                text: 'id',
                datafield: 'id',
                width: '300px;',
                hidden: true
            },
            {
                text: 'Fullname',
                datafield: 'fullName',
                width: 'auto'
            },
            // {
            //     text: 'email',
            //     datafield: 'email',
            //     width: '200px'
            // },
            {
                text: 'Current Programs',
                datafield: 'projectInfo',
                width: 'auto'
            },
            {
                text: 'Cell Number',
                datafield: 'cellNumber',
                width: '140px'
            },
            {
                text: 'Home Phone',
                datafield: 'homePhone',
                width: '140px'
            },
            {
                text: 'City',
                datafield: 'city',
                width: '200px'
            }
            //{ name: 'cellNumber', type: 'string' },
            //{ name: 'homePhone', type: 'string' },
            //{ name: 'city', type: 'string' },
        ];

    cellhover = (element: any, pageX: any, pageY: any): void => {
        // update tooltip.
        // $("#jqxgrid").jqxTooltip({ content: element.innerHTML });
        //this.xtooltip.
        // open tooltip.
        // $("#jqxgrid").jqxTooltip('open', pageX + 15, pageY + 15);
    }

    cellClick(event: any): void {
        // var row = event.args.row.bounddata;
        // this.CurrentRow = row.id;
        // if (event.args.datafield === 'Edit') {
        //     this.router.navigate(['staff/editstaff', this.CurrentRow]); 
        // }
    }

    LoadStaff = () => {
        this.myLoader.open();
        this.staffServiceService.GetStaffList()
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

    ngOnInit(): void { }
}