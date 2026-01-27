import { Component, ViewChild, AfterViewInit, OnInit, OnDestroy, ChangeDetectorRef, Input } from '@angular/core';
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
import { ReportPageModel, PagesModel } from 'src/app/reports/toolbarEmitter.model';
import { SaveActionDisplay } from '../../../common/components/saveActionDisplay/saveactiondisplay.component';
// import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqxdatetimeinput';
import { jqxCalendarComponent } from 'jqwidgets-ng/jqxcalendar';

import { ImagePipe } from 'src/app/common/pipes/image.pipe';
import * as moment from 'moment';
import * as momenttimezone from 'moment-timezone'

@Component({
    selector: 'editdailylog',
    templateUrl: '../../../clients/editclient/editdailylog/editdailylog.component.template.html',
    providers: [ConstantService, UploadService, AuthHelper, ClientsService],
    styleUrls: ['../../../reports/styleReport.component.css']
})

export class EditDailyLog implements OnInit, OnDestroy, AfterViewInit {
    loadedControl: boolean[] = [false, false, false]; // 0 = incident  1 = program 2 = workers of shift 
    isPrinting: boolean = false;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('cmbWorkersOnShift') cmbWorkersOnShift: jqxComboBoxComponent;
    @Input('layout') layout: string = "";
    @ViewChild('cmbProjects') cmbProjects: jqxDropDownListComponent;
    @ViewChild(SaveActionDisplay) saveActionDisplay: SaveActionDisplay;
    @ViewChild('dateTime1') dateTime1: jqxDateTimeInputComponent;
    @ViewChild('fake') jqxCalendarComponent: jqxCalendarComponent;
    imagePipe = new ImagePipe();
    zoomNumericValue = 4; //1 2 3 4(100%) 5 6
    zoom = (): string => {
        return "zoom" + this.zoomNumericValue;
    };
    clientName: string = "";
    involvedPeople: any[] = [];
    dailyLog: any = {};
    report: ReportPageModel = new ReportPageModel();
    ImagePath: String = "";
    //dateTimeDailyLog:Date;
    CurrentItem: number = -1;
    CurrentClientId: number = -1;
    CurrentPeriodId: number = -1;
    private sub: any;
    canDeleteOrSave: boolean;
    calling: boolean = false;
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

    onMustSave = (event: any): void => {
        this.Save(null);
    }

    cmbProjectsBindingComplete = (event: any) => {
        setTimeout(() => {
            if (this.dailyLog) {
                this.cmbProjects.selectItem(this.cmbProjects.getItemByValue(String(this.dailyLog.projectId)));
            }
            setTimeout(() => {
                this.loadedControl[1] = true;
                this.HiddeLoaderWhenEnd();
            });
        });
    }

    // Crop image       
    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
            this.report = new ReportPageModel();
            this.LoadDailyLog();
            this.ImagePath = this.imagePipe.transform('generic', 'clients');
        });
    }

    isEditing = (): boolean => {
        return this.CurrentItem >= 0;
    }

    validate = (): boolean => {
        let result = true;
        // if(!this.dailyLog.placement || this.dailyLog.placement.trim().length<=0)
        // {            
        //     this.glowMessage.ShowGlow("warn","glow_invalid","glow_dailylog_placement");           
        //     result = false;
        // }

        if (this.cmbProjects.getSelectedIndex() < 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_dailylog_project");
            result = false;
        }
        return result;
    }

    Print = (event: any): void => {
        this.printDiv();
    }

    printDiv() {
        let printContents = "";
        for (var i = 0; i < document.getElementsByClassName("page").length; i++) {
            printContents += "<div class='page pageA4'>" + document.getElementsByClassName("page")[i].innerHTML + "</div>";
        }
        if (window) {
            if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
                var popup = window.open('', '_blank', 'width=600,height=600,scrollbars=no,menubar=no,toolbar=no,'
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

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    getZommText = (): string => {
        return Number(this.zoomNumericValue / 0.04) + "%";
    }

    MoreZoom = (event: any): void => {
        this.zoomNumericValue = this.zoomNumericValue < 6 ? this.zoomNumericValue + 1 : this.zoomNumericValue;
    };

    LessZoom = (event: any): void => {
        this.zoomNumericValue = this.zoomNumericValue > 2 ? this.zoomNumericValue - 1 : this.zoomNumericValue;
    };

    clickPrint(event: any): void {
        // let cr = this.gridDailyLog.getrowdata(this.gridDailyLog.selectedrowindex());
        this.router.navigate(['clients/editclient/' + this.CurrentClientId + "/editdailylog/print/" + this.CurrentItem + "/" + this.CurrentPeriodId + ""]);
    }

    onAnyChange = (event: any, type: string = "-"): void => {
        var args = event ? event.args : null;
        if (args || type == "check" || type == "other") {
            let control = true;
            this.loadedControl.forEach(element => {
                control = control && element;
            });

            if (control && this.saveActionDisplay && this.canDeleteOrSave) {

                this.saveActionDisplay.setDirty();
            }
        }
        // else
        // {
        // }
    }

    Save(event: any): void {
        if (this.validate()) {
            this.dailyLog.projectId = this.cmbProjects.getSelectedItem().value;
            this.dailyLog.date = this.dateTime1.getDate(); // this.dateTimeDailyLog; 
            this.dailyLog.idfPeriod = this.CurrentPeriodId;
            this.dailyLog.clientId = this.CurrentClientId;
            if (this.CurrentItem > -1) {
                this.dailyLog.id = this.CurrentItem;
            }
            let saveDailyLogRequestx = {
                DailyLog: this.dailyLog,
                InvolvedPeople: this.involvedPeople,
                TimeDifference: this.getTimeZone()
            };
            // let saveDailyLogRequestx = {
            //     DailyLog : this.dailyLog,
            //     InvolvedPeople : this.involvedPeople
            // };
            // this.myLoader.open();
            if (this.calling && this.dailyLog.id <= 0) {
                return;
            }
            this.calling = true;
            this.ClientsServiceService.SaveDailyLog(saveDailyLogRequestx)
                .subscribe(
                    (data: any) => {
                        this.calling = false;
                        if (data.result) {
                            var isEditing = this.isEditing();
                            if (this.CurrentItem == -1) {
                                this.glowMessage.ShowGlow("success", "glow_success", "glow_editdailylog_saved_successfully")
                            }
                            this.CurrentItem = !isEditing ? data.tagInfo.split('-')[0] : this.CurrentItem;
                            // this.myLoader.close();
                            //this.glowMessage.ShowGlow("success","glow_success","glow_editdailylog_saved_successfully")
                            if (!isEditing) {
                                // this.LoadDailyLog();
                            }
                            this.saveActionDisplay.saved(true);
                        }
                        else {
                            this.manageError(data);
                            this.saveActionDisplay.saved(false);
                        }
                    },
                    error => {
                        this.calling = false;
                        //this.myLoader.close();
                        this.manageError(error);
                        this.saveActionDisplay.saved(false);
                    });
        }
        else {
            this.saveActionDisplay.saved(false);
        }
    }

    //     onSelectChangeCmbStaffWorkersOnShift= (event: any) => {             
    //         var args = event.args;

    // console.clear();

    //         if (args) {
    //            // var index = args.index;
    //             var item = args.item;


    //             let search = this.involvedPeople.filter(c=>c.idfSPP == item.originalItem.id);

    //             if(search && search.length>0)
    //             {
    //                 search[0].state = event.type == "select" ? "C" : "D";
    //             }
    //             else
    //             {
    //                 this.involvedPeople.push({ identifierGroup:'s1',idfDailyLog: this.CurrentItem , idfSPP: item.originalItem.id,state:'C'}); 
    //             }


    //         } 
    //         else{
    //         }
    //     }

    onSelectChangeCmb = (event: any, group: string) => {
        var args = event.args;
        if (args) {
            var item = args.item;
            let search = this.involvedPeople.filter(c => c.idfSPP == item.originalItem.id && c.identifierGroup == group);
            if (search && search.length > 0) {
                search[0].state = event.type == "select" ? "C" : "D";
            }
            else {
                this.involvedPeople.push({ identifierGroup: group, idfDailyLog: this.CurrentItem, idfSPP: item.originalItem.id, state: 'C' });
                //this.involvedPeople.push({ identifierGroup:group,IdfIncident: this.CurrentItem , idfSPP: item.originalItem.id,state:'C'}); 
            }
            this.onAnyChange(null, 'other');
        }
    }

    BindingCompleteCmbStaffWorkersOnShift = (event: any) => {
        // //if (this.involvedPeople && this.involvedPeople.length>0)
        // if (this.involvedPeople)
        // {        
        //     this.involvedPeople.filter(c => c.identifierGroup=="s1")
        //     .forEach(element => {
        //         setTimeout(() => {
        //              this.cmbWorkersOnShift.selectItem(this.cmbWorkersOnShift.getItemByValue(element.idfSPP) );
        //         });
        //     });

        //     setTimeout(() => {                           
        //         this.loadedControl[2] = true;                         
        //         this.HiddeLoaderWhenEnd();                      
        //      });

        // }        
        if (this.involvedPeople) {
            this.involvedPeople.filter(c => c.identifierGroup == "s1")
                .forEach(element => {
                    setTimeout(() => {
                        this.cmbWorkersOnShift.selectItem(this.cmbWorkersOnShift.getItemByValue(element.idfSPP));
                    });
                });
            setTimeout(() => {
                this.loadedControl[2] = true;
                this.HiddeLoaderWhenEnd();
            });
        }
    }

    getTimeZone = (): number => {
        let aux: any = momenttimezone.tz(Intl.DateTimeFormat().resolvedOptions().timeZone);
        return Number(aux._offset / 60);
    }

    closeWindow = (event: any): void => {
        this.router.navigate(['clients/editclient/' + this.CurrentClientId + "/editdailylog/" + this.CurrentItem + "/" + this.CurrentPeriodId + "/false"]);
    }

    LoadDailyLog = () => {
        this.myLoader.open();
        this.ClientsServiceService.GetDailyLogById(this.CurrentItem, this.CurrentPeriodId, this.CurrentClientId, this.getTimeZone())
            .subscribe(
                (data: any) => {
                    let page: PagesModel = new PagesModel();
                    page.title = "abc";
                    this.report.pages.push(page);
                    if (data.result) {
                        this.sourceProjects.localdata = data.projects;
                        this.dataAdapterProjects = new jqx.dataAdapter(this.sourceProjects);
                        this.ImagePath = this.imagePipe.transform(data.clientImg, 'clients');
                        this.clientName = data.clientName;
                        this.involvedPeople = data.involvedPeople;
                        this.dailyLog = data.dailyLog;
                        // this.dateTimeDailyLog = new Date(data.dailyLog.date);
                        this.sourceStaffList.localdata = data.staffs;
                        setTimeout(() => {
                            this.dataAdapterStaffList_1 = new jqx.dataAdapter(this.sourceStaffList);
                            //this.dateTime1.val(this.dateTimeDailyLog);
                            // this.dateTime1.setDate(new Date(data.dailyLog.date));
                            this.dateTime1.setDate(this.CurrentItem < 0 ? new Date(data.currentDateTime) : new Date(data.dailyLog.date));
                            //this.dateTimeDailyLog = this.CurrentItem < 0 ? new Date(data.currentDateTime) : new Date(data.dailyLog.date);
                            setTimeout(() => {
                                this.loadedControl[0] = true;
                                this.HiddeLoaderWhenEnd();
                            });
                        });
                    } else {
                        this.manageError(data);
                    }
                    // setTimeout(() => {
                    //     this.loadedControl[0] = true;
                    //     this.HiddeLoaderWhenEnd();
                    // });
                },
                error => {
                    this.manageError(error);
                });
    }

    back = (event: any) => {
        this.router.navigate(['clients/editclient', this.CurrentClientId]);
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
    dataAdapterStaffList_1: any;
}