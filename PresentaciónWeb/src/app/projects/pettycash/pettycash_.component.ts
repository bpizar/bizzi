import { Directive, Component, ViewChild, AfterViewInit, OnInit, OnDestroy, Injectable, EventEmitter, Output, Input, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConstantService } from '../../common/services/app.constant.service';
import { PettyCashModel } from './pettycash_.component.model';
import { Router } from '@angular/router';
import { jqxWindowComponent } from 'jqwidgets-ng/jqwidgets/jqxwindow';
import { jqxNumberInputComponent } from 'jqwidgets-ng/jqwidgets/jqxnumberinput';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqwidgets/jqxdropdownlist';
import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqwidgets/jqxdatetimeinput';
import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
//import { jqxNotificationComponent } from 'jqwidgets-ng/jqwidgets/jqxnotification';
import { ProjectsService } from '../projects.service';
import { AuthHelper } from '../../common/helpers/app.auth.helper';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { JqxHelper } from '../../common/helpers/app.jqx.helper'

@Component({
    selector: 'pettycash',
    templateUrl: '../../projects/pettycash/pettycash_.component.template.html',
    providers: [ConstantService, ProjectsService],
    styleUrls: ['../../projects/pettycash/pettycash_.component.css'],
})

@Injectable()
export class PettyCash implements OnInit, OnDestroy, AfterViewInit {

    currencySymbol: string = "#";
    @ViewChild(jqxWindowComponent) private windowx: jqxWindowComponent;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    //@ViewChild('msgNotificationError') msgNotificationError: jqxNotificationComponent;
    @ViewChild('txtAmount') txtAmount: jqxNumberInputComponent;
    @ViewChild('txtDescription') txtDescription: jqxInputComponent;
    @ViewChild('txtDate') txtDate: jqxDateTimeInputComponent;
    @Input('glowmessage') glowMessage: GlowMessages;
    @ViewChild('cmbCategories') cmbCategories: jqxDropDownListComponent;
    msgError: string = "";
    msgSuccess: string = "";
    PettyCash: PettyCashModel[] = [];
    public isEditing: boolean;
    currentPeriod: number = 0;

    constructor(private translate: TranslateService,
        private constantService: ConstantService,
        private projectsService: ProjectsService,
        private route: ActivatedRoute,
        private router: Router,
        private authHelper: AuthHelper,
        private jqxHelper: JqxHelper,
        private chRef: ChangeDetectorRef) { }

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.currencySymbol = this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en.currencysymbol : this.jqxHelper.getGridLocation_es.currencysymbol;
        });
    };

    idProject: number = -1;
    pettyCashId: number = -1;

    clearHeaderRow = (): void => {
        this.txtDescription.val("");
        this.txtAmount.val("0.00");
    }

    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    addPettyCashClick = (event: any): void => {
        if (this.validatePettyCash()) {
            let newPettyCash = new PettyCashModel();
            newPettyCash.id = this.pettyCashId--;
            newPettyCash.amount = Number(this.txtAmount.val());
            newPettyCash.description = this.txtDescription.val();
            newPettyCash.date = this.txtDate.getDate();
            newPettyCash.idfProject = this.idProject;
            newPettyCash.abm = "I";
            newPettyCash.category = this.cmbCategories.getSelectedItem().label;
            newPettyCash.idfCategories = this.cmbCategories.val();
            this.PettyCash.push(newPettyCash);
            this.clearHeaderRow();
            this.OkClick(null);
        }
    };

    SumPettyCash = (): number => {
        let items = this.PettyCash.filter(c => c.abm != "D");
        let sum: number = 0;
        items.forEach(c => sum += c.amount);
        //return  this.dataAdapterCategoriesListBox. ? this.dataAdapterCategoriesListBox.formatNumber(sum, 'c2', this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en() :  this.jqxHelper.getGridLocation_es()) :  sum.toPre; // , this.jqxHelper.getTreeGridLocalization()
        //return Number(sum.toFixed(2));
        return sum;
    }




    getFormatDate = (dinput: any): any => {
        let d = new Date(dinput);
        let days: number = Number(d.getDate());
        return d.getFullYear() + "/" + Number(Number(d.getMonth()) + 1) + "/" + days;
    }

    removePettyCashClick = (p: PettyCashModel): void => {
        this.PettyCash.filter(c => c.id == p.id)[0].abm = "D";
        setTimeout(() => {
            this.OkClick(null);
        });
    };

    sourceCategories: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'description', type: 'string' },
                { name: 'state', type: 'string' },
                //{ name: 'abm', type: 'string' },
                //{ name: 'idfClient', type: 'number' },
            ],
            id: 'id',
            async: false
        }

    dataAdapterCategoriesListBox: any; // = new jqx.dataAdapter(this.sourceCategories);

    showEdit(idProject: number, idPeriod: number): void {
        this.idProject = idProject;
        this.currentPeriod = idPeriod;
        //let title = "Petty Cash"; 
        //this.windowx.setTitle(title);
        this.myLoader.open();
        this.projectsService.GetPettyCash(this.idProject, this.currentPeriod, -1)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.PettyCash = <PettyCashModel[]>data.pettyCash;
                        this.sourceCategories.localdata = data.pettyCashCategories;
                        this.dataAdapterCategoriesListBox = new jqx.dataAdapter(this.sourceCategories);
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

        this.translate.get('global_pettycash').subscribe((res: string) => {
            //this.PlaceHolderLookingFor = res;
            this.windowx.setTitle(res);
            this.windowx.open();
        });
        //this.windowx.open();
    }

    validatePettyCash = (): boolean => {
        let result = true;
        let amount: number = Number(this.txtAmount.val());
        let description: string = this.txtDescription.val();
        let date: Date = this.txtDate.getDate();

        if (this.cmbCategories.selectedIndex() < 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_petty_cash_select_category");
            result = false;
        }

        if (amount <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_petty_cash_type_amount");
            result = false;
        }

        if (description.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_type_the_description");
            result = false;
        }

        if (date == null) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_valid_date");
            result = false;
        }

        return result;
    }

    OkClick = (event: any): void => {
        this.myLoader.open();

        let body = {
            PettyCash: this.PettyCash.filter(x => x.abm != ''),
            idperiod: this.currentPeriod
        }


        this.projectsService.SavePettyCash(body)
            .subscribe(
                (data: any) => {
                    this.myLoader.close();
                    if (data.result) {
                        this.showEdit(this.idProject, this.currentPeriod);
                        this.glowMessage.ShowGlow("success", "glow_success", "glow_pettycash_saved_successfully");
                        //this.windowx.close();
                    }
                    else {
                        this.manageError(data);
                    }
                },
                error => {
                    this.manageError(error);
                });
    }

    CancelClick = (event: any): void => {
        this.windowx.close();
    };

    ngOnInit(): void { }

    ngOnDestroy() { }
}
