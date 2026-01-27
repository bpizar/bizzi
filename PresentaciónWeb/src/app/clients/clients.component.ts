import { Component, ViewChild, AfterViewInit, OnInit, ChangeDetectorRef } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { ClientsService } from './clients.service';
import { jqxGridComponent } from 'jqwidgets-ng/jqwidgets/jqxgrid';
import { jqxWindowComponent } from 'jqwidgets-ng/jqwidgets/jqxwindow';
import { jqxButtonComponent } from 'jqwidgets-ng/jqwidgets/jqxbuttons';
import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import { Router } from '@angular/router';
//import { jqxNotificationComponent } from '../../../node_modules/jqwidgets-ng/jqwidgets/jqxnotification';
import { AuthHelper } from '../common/helpers/app.auth.helper';
import { jqxListBoxComponent } from 'jqwidgets-ng/jqwidgets/jqxlistbox';
import { jqxComboBoxComponent } from 'jqwidgets-ng/jqwidgets/jqxcombobox';
import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { JqxHelper } from '../common/helpers/app.jqx.helper'
import { ImagePipe } from '../common/pipes/image.pipe';

@Component({
    selector: 'clients',
    templateUrl: '../clients/clients.component.template.html',
    providers: [ClientsService, ConstantService, AuthHelper, JqxHelper],
})

export class Clients implements OnInit, AfterViewInit {
    //loadedControl:boolean[]=[false,false]; // 0  client   1  period
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('gridReference') myGrid: jqxGridComponent;
    // @ViewChild('msgNotificationError') msgNotificationError: jqxNotificationComponent;
    // @ViewChild('msgNotificationSuccess') msgNotificationSuccess: jqxNotificationComponent;
    //@ViewChild('positionsWindow') positionsWindow: jqxWindowComponent;
    @ViewChild('listBoxPositionsRoles') listBoxPositionsRoles: jqxListBoxComponent;
    @ViewChild('txtAddPosition') txtAddPosition: jqxInputComponent;
    @ViewChild('cmbAddRoles') cmbAddRoles: jqxComboBoxComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('gridReference') gridReference: jqxGridComponent;
    imagePipe = new ImagePipe();
    selectedPositionTitle: string = "";
    ignoreCheckedPositions: boolean = false;

    constructor(private jqxHelper: JqxHelper,
        private translate: TranslateService,
        private clientsServiceService: ClientsService,
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

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
            this.LoadClients();
            this.myGrid.localizestrings(this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en : this.jqxHelper.getGridLocation_es);
            this.translate.get('clients_grid_fullname').subscribe((res: string) => { this.myGrid.setcolumnproperty("fullName", "text", res); });
            this.translate.get('clients_grid_email').subscribe((res: string) => { this.myGrid.setcolumnproperty("email", "text", res); });
            this.translate.get('clients_grid_phonenumber').subscribe((res: string) => { this.myGrid.setcolumnproperty("phoneNumber", "text", res); });
            this.translate.get('clients_grid_birthdate').subscribe((res: string) => { this.myGrid.setcolumnproperty("birthDate", "text", res); });
        });
    }

    CurrentRow: number = -1;
    isEditing = (): boolean => {
        return this.CurrentRow >= 0;
    }

    createNew(event: any): void {
        this.CurrentRow = -1;
        this.router.navigate(['clients/editclient', this.CurrentRow]);
    }

    msgError: string = "";
    msgSuccess: string = "";
    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    source: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                //{ name: 'firstName', type: 'string' },
                //{ name: 'lastName', type: 'string' },
                { name: 'fullName', type: 'string' },
                { name: 'birthDate', type: 'datetime' },
                { name: 'phoneNumber', type: 'number' },
                //{ name: 'projectName', type: 'string' },
                { name: 'email', type: 'string' },
                { name: 'notes', type: 'string' },
                { name: 'img', type: 'string' },
                { name: 'programInfo', type: 'string' },
                //{ name: 'projectId', type: 'number' }
            ],
            id: 'id',
        };
    dataAdapter: any = new jqx.dataAdapter(this.source);

    rendererPositionColumn = (row: any, column: any, value: any) => { };
    imagerenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        return `<img style="border-radius:50%; margin: 5px;" height="60" width="60" src=" ${this.imagePipe.transform(rowdata.img, 'clients')}"/>`;
    };

    editClick(event: any): void {
        let cr = this.myGrid.getrowdata(this.myGrid.selectedrowindex());
        this.router.navigate(['clients/editclient', cr.id]);
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
                //let flattenButtons = flatten(Buttonss);
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

    dateRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        if (Date.parse(value)) {
            let date = new Date(value);
            return "<div style='margin-top:30px; text-indent:10px;'>" + date.toDateString() + "</div>";
        }
        else {
            return "";
        }
    };

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

    hiddenWhenIsNew = (): boolean => {
        return !this.isEditing();
    }

    // canDeleteOrSave = (): boolean => {
    //     return (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("clienteditor"));
    // }

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
                width: '70px',
                cellsrenderer: this.imagerenderer,
                menu: false,
                sortable: false,
                filterable: false
            },
            {
                text: 'id',
                datafield: 'id',
                width: 'auto;',
                hidden: true
            },
            // {
            //     text: 'First Name',
            //     datafield: 'firstName',
            //     width: 'auto'
            // },
            // {
            //     text: 'Last Name',
            //     datafield: 'lastName',
            //     width: '200px'
            // },
            {
                text: 'Fullname',
                datafield: 'fullName',
                width: '200px'
            },
            // {
            //     text: 'email',
            //     datafield: 'email',
            //     width: '200px'
            // },

            {
                text: 'Programs',
                datafield: 'programInfo',
                width: 'auto'
            },

            // {
            //     text: 'Project Name',
            //     datafield: 'projectName',
            //     width: '200px'
            // },
            {
                text: 'Phone Number',
                datafield: 'phoneNumber',
                width: '150px'
            },
            {
                text: 'Birth Date',
                datafield: 'birthDate',
                width: '150px',
                cellsrenderer: this.dateRenderer
            },
            // {
            //     text: 'Notes',
            //     datafield: 'notes',
            //     width: '200px'
            // },
        ];
    readygrid: boolean;

    ready = (): void => {
        this.readygrid = true;
    }

    cellClick(event: any): void {
        // var row = event.args.row.bounddata;
        // this.CurrentRow = row.id;
        // if (event.args.datafield === 'Edit') {
        //     this.router.navigate(['clients/editclient', this.CurrentRow]); 
        // }
    }

    canAddNew = (): boolean => {
        return this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("clienteditor");
    }

    LoadClients = () => {
        this.myLoader.open();
        this.clientsServiceService.GetAllClientsList()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.source.localData = data.clients;
                        this.dataAdapter = new jqx.dataAdapter(this.source);
                        this.myGrid.updatebounddata();
                        // setTimeout(() => {
                        //     this.myGrid.render();
                        // });
                    }
                    else {
                        this.manageError(data);
                    }

                    this.myLoader.close();
                    //this.loadedControl[0] = true;
                    //this.HiddeLoaderWhenEnd();
                },
                error => {
                    this.myLoader.close();
                    this.manageError(error);
                });
    }

    ngOnInit(): void { }
}
