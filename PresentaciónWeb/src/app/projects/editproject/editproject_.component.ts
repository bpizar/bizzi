import { Component, ViewChild, AfterViewInit, OnInit, OnDestroy, Inject, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { settingReminderTimeModel, SubtaskModel } from './editproject_.component.model';
import { tasksRemindersModel, EventEmiterTaskModel } from '../edittask/edittask_.component.model';
import { EventEmiterNoteModel } from '../notes/notes.component.model';
import { EventEmiterParticipantModel } from '../../staff/selectstaff/selectstaff.component.model';
import { ProjectsService } from '../projects.service';
import { ConstantService } from '../../common/services/app.constant.service';
import { Router } from '@angular/router';
import { jqxSchedulerComponent } from 'jqwidgets-ng/jqxscheduler';
import { TaskModel, ProjectModel, OwnerModel } from './editproject_.component.model';
import { jqxListBoxComponent } from 'jqwidgets-ng/jqxlistbox';
import { jqxInputComponent } from 'jqwidgets-ng/jqxinput';
import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqxdatetimeinput';
import { jqxDropDownButtonComponent } from 'jqwidgets-ng/jqxdropdownbutton';
import { SelectStaff } from '../../staff/selectstaff/selectstaff.component';
import { SelectClient } from '../../clients/selectclients/selectclient.component';
import { CloneTask } from '../clonetask/clonetask_.component';
import { EditTask } from '../edittask/edittask_.component';
import { PettyCash } from '../pettycash/pettycash_.component';
import { NotesTask } from '../notes/notes.component';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { SchedulingService } from '../../scheduling/scheduling.service';
import { jqxComboBoxComponent } from 'jqwidgets-ng/jqxcombobox';
import { StaffService } from '../../staff/staff.service';
import { jqxButtonComponent } from 'jqwidgets-ng/jqxbuttons';
import { AuthHelper } from '../../common/helpers/app.auth.helper';
import { jqxButtonGroupComponent } from 'jqwidgets-ng/jqxbuttongroup';
import { CommonHelper } from '../../common/helpers/app.common.helper';
import { positionsModel } from '../../staff/staff.component.model';
import { jqxGridComponent } from 'jqwidgets-ng/jqxgrid';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
import { jqxEditorComponent } from 'jqwidgets-ng/jqxeditor';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { jqxTabsComponent } from 'jqwidgets-ng/jqxtabs';
import { JqxHelper } from '../../common/helpers/app.jqx.helper'
// import {  jqxCalendarComponent} from  'jqwidgets-ng/jqxcalendar';
// import {  jqxDateTimeInputComponent } from  'jqwidgets-ng/jqxdatetimeinput';
import { SaveActionDisplay } from '../../common/components/saveActionDisplay/saveactiondisplay.component';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';

@Component({
    selector: 'editproject',
    templateUrl: './editproject_.component.template.html',
    providers: [StaffService, SchedulingService, ProjectsService, ConstantService, AuthHelper, CommonHelper, JqxHelper],
    styleUrls: ['./editproject_.component.css'],
})

export class EditProject implements OnInit, OnDestroy, AfterViewInit {
    // @ViewChild('xxx') dateTimeTo: jqxCalendarComponent;
    @ViewChild('ModeTaskButtons') modeTaskButtons: jqxButtonGroupComponent;
    @ViewChild('gridReference') gridReference: jqxGridComponent;
    @ViewChild('windowDeleteConfirmation') windowDeleteConfirmation: jqxWindowComponent;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('listBoxParticipants') public listBoxParticipants: jqxListBoxComponent;
    @ViewChild('projectname') projectname: jqxInputComponent;
    @ViewChild('projectaddreess') projectaddreess: jqxInputComponent;
    @ViewChild('projectcity') projectcity: jqxInputComponent;
    @ViewChild('projectPhone1') projectPhone1: jqxInputComponent;
    @ViewChild('projectPhone2') projectPhone2: jqxInputComponent;
    @ViewChild('projectdescription') projectdescription: jqxInputComponent;
    @ViewChild('dropDownColor') myDropDown: jqxDropDownButtonComponent;
    @ViewChild('dateTimeFrom') dateTimeFrom: jqxDateTimeInputComponent;
    @ViewChild('dateTimeTo') dateTimeTo: jqxDateTimeInputComponent;
    @ViewChild(SelectStaff) selectStaff: SelectStaff;
    @ViewChild(SelectClient) selectClient: SelectClient;
    @ViewChild(EditTask) editTask: EditTask;
    @ViewChild(CloneTask) cloneTask: CloneTask;
    @ViewChild(NotesTask) notesTask: NotesTask;
    @ViewChild(PettyCash) pettyCash: PettyCash;
    @ViewChild('periodsDropDown') periodsDropDown: jqxListBoxComponent;
    @ViewChild('deleteParticipantButton') deleteParticipantButton: jqxButtonComponent;
    @ViewChild('cmbOwners') cmbOwners: jqxComboBoxComponent;
    @ViewChild('schedulerReference') scheduler: jqxSchedulerComponent;
    @ViewChild('listBoxClients') public listBoxClients: jqxListBoxComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('tabsReference') tabsReference: jqxTabsComponent;
    @ViewChild('tabsReference2') tabsReference2: jqxTabsComponent;
    @ViewChild('fake') fakeEditorComponent: jqxEditorComponent;
    @ViewChild(SaveActionDisplay) saveActionDisplay: SaveActionDisplay;
    imagePipe = new ImagePipe();
    isInrolAdmin: boolean = false;

    hiddenWhenIsNew = (): boolean => {
        return !this.isEditing();
    }

    serverDateTime: Date;
    PlaceHolderLookingFor: string = "";
    PlaceHolderSelectPeriod: string = "";
    PlaceHolderProjectName: string = "";
    PlaceHolderDescription: string = "";
    loadedControl: boolean[] = [false, false, false, false, false];
    msgError: string = "";
    msgSuccess: string = "";
    negativeSequence: number = -10;
    positions: positionsModel[] = [];
    settingReminderTime: settingReminderTimeModel[] = [];

    //SOURCE FOR TASKS. (Scheduler)
    sourceScheduler: any =
        {
            datatype: "array",
            datafields: [
                { name: 'id', type: 'string' },
                { name: 'description', type: 'string' },
                { name: 'location', type: 'string' },
                { name: 'subject', type: 'string' },
                { name: 'idfAssignedTo', type: 'string' },
                { name: 'from', type: 'date' },
                { name: 'to', type: 'date' },
                { name: 'state', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'assignedToPosition', type: 'string' },
                { name: 'assignedToFullName', type: 'string' },
                { name: 'idUser', type: 'number' },
                { name: 'idfStaff', type: 'number' },
                { name: 'draggable', type: 'boolean' },
                { name: 'resizable', type: 'boolean' }
            ],
            id: 'id'
        };

    dataAdapterScheduler: any;
    currentModeTask: string = 'RQ';

    ModeTaskButtonsClick = (event: any): void => {
        this.currentModeTask = event.args.index == 0 ? "RQ" : event.args.index == 1 ? 'GO' : "RE";
        this.applyGidFilter();
    };

    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    applyGidFilter = (fn: any = null): void => {
        // this.gridReference.refreshfilterrow();
        this.gridReference.clearfilters();

        setTimeout(() => {
            let filtergroup = new jqx.filter();
            let filter_or_operator = 1;
            let filtervalue = this.currentModeTask;
            let filtercondition = 'equal';
            let filter1 = filtergroup.createfilter('stringfilter', filtervalue, filtercondition);

            setTimeout(() => {
                filtergroup.addfilter(filter_or_operator, filter1);
                this.gridReference.addfilter('type', filtergroup);

                setTimeout(() => {
                    this.gridReference.applyfilters();

                    setTimeout(() => {
                        if (typeof fn === "function") {
                            fn();
                        }
                    });
                });
            });
        });
    }

    onSelectedTab = (event: any): void => {
        let selectedTab = event.args.item;
        switch (selectedTab) {
            case 0:
                //this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);
                this.listBoxParticipants.render();
                break;
            case 1:
                //this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);

                this.listBoxClients.render();
                break;
        }
    }

    initWidgets = (tab: any): void => {
        switch (tab) {
            case 0:
                this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);
                break;
            case 1:
                this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);
                this.listBoxClients.render();
                break;
        }
    }

    gridReady = (): void => {
        this.modeTaskButtons.setSelection(0);
        this.translate.get('projects_grid_subject').subscribe((res: string) => { this.gridReference.setcolumnproperty("subject", "text", res); });
        this.translate.get('projects_grid_status').subscribe((res: string) => { this.gridReference.setcolumnproperty("status", "text", res); });
        this.translate.get('projects_grid_assigned_to').subscribe((res: string) => { this.gridReference.setcolumnproperty("assignedToFullName", "text", res); });
        this.translate.get('projects_grid_deadline').subscribe((res: string) => { this.gridReference.setcolumnproperty("deadLine", "text", res); });
        this.translate.get('projects_grid_time').subscribe((res: string) => { this.gridReference.setcolumnproperty("hours", "text", res); });
        this.gridReference.localizestrings(this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en : this.jqxHelper.getGridLocation_es);
    }

    sourceTasks: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'idfTaskParent', type: 'string' },
                { name: 'subject', type: 'string' },
                { name: 'idfStatus', type: 'number' },
                { name: 'status', type: 'string' },
                { name: 'description', type: 'string' },
                { name: 'deadline', type: 'date' },
                { name: 'idfAssignedTo', type: 'number' },
                { name: 'RecurrencePattern', type: 'string' },
                { name: 'RecurrenceException', type: 'string' },
                { name: 'AllDay', type: 'boolean' },
                { name: 'IdfCreatedBy', type: 'number' },
                { name: 'CreationDate', type: 'date' },
                { name: 'Lat', type: 'string' },
                { name: 'Lon', type: 'string' },
                { name: 'Address', type: 'string' },
                { name: 'IdDuplicate', type: 'number' },
                { name: 'idUser', type: 'number' },
                { name: 'hours', type: 'number' },
                { name: 'assignedToFullName', type: 'string' },
                { name: 'assignedToPosition', type: 'string' },
                { name: 'abm', type: 'string' },
                { name: 'idfPeriod', type: 'number' },
                { name: 'idfProject', type: 'number' },
                { name: 'idfAssignableRol', type: 'number' },
                { name: 'type', type: 'string' },
                { name: 'img', type: 'string' }
            ],
            id: 'id',
        }

    dataAdapterTasks: any = new jqx.dataAdapter(this.sourceTasks);

    ShouldHide = (): boolean => {
        return this.periodClosed || this.projectClosed || this.currentPeriod <= 0;
    }

    editClick(event: any): void {
        let cr = this.gridReference.getrowdata(this.gridReference.selectedrowindex());
        let item = this.tasks.filter(x => x.id == cr.id)[0];
        this.editTaskClick(item, this.currentModeTask);
        // this.router.navigate(['clients/editclient', cr.id]);
    }

    cloneClick(event: any): void {
        let cr = this.gridReference.getrowdata(this.gridReference.selectedrowindex());
        // let item = this.tasks.filter(x => x.id == cr.id)[0];
        // this.editTaskClick(item, this.currentModeTask);
        this.cloneTask.isEditing = true;
        for (var a = 0; a < this.settingReminderTime.length; a++) {
            let item = this.tasksReminders.filter(x => x.idfSettingReminderTime == this.settingReminderTime[a].id && x.idfTask == event.id)[0];
            this.settingReminderTime[a].state = item ? "true" : "false";
        }
        this.cloneTask.showClone(cr, this.currentModeTask, this.settingReminderTime);
        // this.router.navigate(['clients/editclient', cr.id]);
    }

    notesClick(event: any): void {
        let cr = this.gridReference.getrowdata(this.gridReference.selectedrowindex());
        let item = this.tasks.filter(x => x.id == cr.id)[0];
        this.notesTask.showEdit(item, this.dataAdapterClientsListBox, item.notes, this.canDeleteOrSaveProject());
    }

    undoClick(event: any): void {
        //let cr = this.gridReference.getrowdata(this.gridReference.selectedrowindex());
        //let item = this.tasks.filter(x => x.id == cr.id)[0];
        //this.notesTask.showEdit(item, this.dataAdapterClientsListBox, item.notes);

        let idx = this.gridReference.getselectedrowindex();
        if (idx > -1) {
            let cr = this.gridReference.getrowdata(this.gridReference.selectedrowindex());
            let item = this.tasks.filter(x => x.id == cr.id)[0];
            item.abm = "";
            this.UpdateCurrentRow("");
            if (this.canDeleteOrSaveProject()) {
                this.saveActionDisplay.setDirty();
            }

            setTimeout(() => {
                // this.gridReference.clearselection();
            });
        }
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
            if (document.getElementsByClassName("btnundo").length > 0) {
                let Buttons = jqwidgets.createInstance(".btnundo", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-undo' style='padding:0px !important; font-size:16px; color:var(--thirteenth-color);'></i>" + '', template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });
                let flattenButtons = flatten(Buttons.length ? Buttons : [Buttons]);
                if (flattenButtons) {
                    for (let i = 0; i < flattenButtons.length; i++) {
                        flattenButtons[i].removeEventHandler('click');
                        flattenButtons[i].addEventHandler('click', (event: any): void => {
                            this.undoClick(event);
                        });
                    }
                }
            }

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

            if (document.getElementsByClassName("btnclone").length > 0) {
                let Buttons = jqwidgets.createInstance(".btnclone", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-shuffle' style='padding:0px !important; font-size:16px; color:var(--thirteenth-color);'></i>" + '', template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });
                let flattenButtons = flatten(Buttons.length ? Buttons : [Buttons]);
                if (flattenButtons) {
                    for (let i = 0; i < flattenButtons.length; i++) {
                        flattenButtons[i].removeEventHandler('click');
                        flattenButtons[i].addEventHandler('click', (event: any): void => {
                            this.cloneClick(event);
                        });
                    }
                }
            }

            if (document.getElementsByClassName("btnnotes").length > 0) {
                let Buttons = jqwidgets.createInstance(".btnnotes", 'jqxButton', { width: 90, height: 24, value: "<i class='ion ion-ios-list' style='padding:0px !important; font-size:16px; color:var(--thirteenth-color);'></i>" + '', template: 'link', imgPosition: "left", textPosition: "left", textImageRelation: "imageBeforeText" });
                let flattenButtons = flatten(Buttons.length ? Buttons : [Buttons]);
                if (flattenButtons) {
                    for (let i = 0; i < flattenButtons.length; i++) {
                        flattenButtons[i].removeEventHandler('click');
                        flattenButtons[i].addEventHandler('click', (event: any): void => {
                            this.notesClick(event);
                        });
                    }
                }
            }
        });

    };




    editRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        // var salida =           "<div class='tooltip'> "  + "<div data-row='" + row + "' class='btnedit'></div>" +  "<span class='tooltiptext'>Edit</span></div>";
        // var salida = salida +  "<div class='tooltip'> "  + "<div data-row='" + row + "' class='btnclone'></div>" +  "<span class='tooltiptext' style='font-size:8px;'>Copy Move</span></div>";
        // var salida = salida +  "<div class='tooltip'> "  + "<div data-row='" + row + "' class='btnnotes'></div>" +  "<span class='tooltiptext'>Notes</span></div>";
        var salida = rowdata.abm == "D" ? "<div class='tooltip'> " + "<div data-row='" + row + "' class='btnundo'></div>" + "<span class='tooltiptext'>Undo</span></div>" :
            "<div class='tooltip'> " + "<div data-row='" + row + "' class='btnedit'></div>" + "<span class='tooltiptext'>Edit</span></div>";
        salida = rowdata.abm != "D" && this.canDeleteOrSaveProject() ? salida + "<div class='tooltip'> " + "<div data-row='" + row + "' class='btnclone'></div>" + "<span class='tooltiptext' style='font-size:8px;'>Copy Move</span></div>" : salida + "";
        salida = rowdata.abm != "D" ? salida + "<div class='tooltip'> " + "<div data-row='" + row + "' class='btnnotes'></div>" + "<span class='tooltiptext'>Notes</span></div>" : salida + "";
        return salida;
    }

    // undoRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
    //     return "<div style='height:70px;'><i class='ion ion-ios-undo' style='padding:0px !important; font-size:16px; color:white;'></i></div>";
    // };

    // cloneRenderer = (row:any, columnfield:any, value:any, defaulthtml:any, columnproperties:any, rowdata:any):string => {
    //     return "<div style='height:70px;'><i class='ion ion-ios-shuffle' style='padding:0px !important; font-size:18px; color:white;'></i></div>";
    // };

    // noteRenderer = (row:any, columnfield:any, value:any, defaulthtml:any, columnproperties:any, rowdata:any):string => {
    //     return "<div style='height:70px;'><i class='ion ion-ios-list' style='padding:0px !important; font-size:18px; color:white;'></i></div>";
    // };

    deadLineRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        //let valueAux = "<div>" + (value && Date.parse(value) ? value.toLocaleDateString(): "") + "</div>"
        //return rowdata.deadline;
        let valueAux = "<div>" + (rowdata.deadline && Date.parse(rowdata.deadline) ? rowdata.deadline.toLocaleDateString() : "") + "</div>"
        var isdelete = rowdata.abm == "D" ? " text-decoration:line-through !important; color:red;" : "";
        return "<div style='height:70px;" + isdelete + " text-indent:15px;  line-height:70px; '  >" + valueAux + " </div>"
    };

    CommonColumnRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var isdelete = rowdata.abm == "D" ? " text-decoration:line-through !important; color:red;" : "";
        return "<div style='height:70px; " + isdelete + " text-indent:15px;  line-height:70px; '  >" + value + " </div>"
    };

    timeRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        var isdelete = rowdata.abm == "D" ? " text-decoration:line-through !important; color:red;" : "";
        let valueAux = "<div>" + (value && value != 0 ? this.commonHelper.convertMinsToHrsMins(value) : "") + "</div>"
        return "<div style='height:70px; " + isdelete + " text-indent:15px;  line-height:70px; '  >" + valueAux + " </div>"
    };

    assignedToRenderer: any = (row, columnfield, value, defaulthtml, columnproperties, rowdata) => {
        // var isdelete = rowdata.abm == "D" ? " text-decoration:line-through !important; color:Red;" : "";
        // let path = rowdata.img ?  '/media/images/users/'+ rowdata.img + '.png' : "";
        // let img = path!="" ? !rowdata.isUser || rowdata.isUser == -1 ? "<div style=' margin:1px solid blue;   float:left; margin-top:15px;'><img height='40' width='40' src='" + path +  "' /></div>" : "" : "<div style='float:left; margin-top:15px; width:40px;height:40px; min-width:40px; min-height:40px;'></div>";
        // let assignedToPosition = rowdata.assignedToPosition ? "<div style='padding-left:1%; " + isdelete + " float:left; width:79%; margin-top:4px; heigth:10px !important; max-heigth:10px !important; overflow: hidden !important; '>" + rowdata.assignedToPosition + "</div>" : "";
        // let marginTopAssignedTo = rowdata.idfAssignedTo && assignedToPosition.length > 0  ? "margin-top:15px;" :"margin-top:30px;";
        // let assignedTo = "<div style='padding-left:1%; " + isdelete + " float:left; width:79%; " + marginTopAssignedTo + "heigth:10px !important; max-heigth:10px !important; overflow: hidden !important;'>" + rowdata.assignedToFullName + "</div>"
        // return "<div style='height:70px;'>" + img  + assignedTo + assignedToPosition + " </div>"
        var isToUser = rowdata.idfAssignedTo && rowdata.idfAssignedTo > 0;
        var isToRol = rowdata.idfAssignableRol && rowdata.idfAssignableRol > 0;
        var isdelete = rowdata.abm == "D" ? " text-decoration:line-through !important; color:Red;" : "";
        let path = rowdata.img ? this.imagePipe.transform(rowdata.img, 'users') : "";
        let img = path != "" ? !rowdata.isUser || rowdata.isUser == -1 ? "<div style=' margin:1px solid blue;   float:left; margin-top:15px;'><img height='40' width='40' style='border-radius:50%; ' src='" + path + "' /></div>" : "" : "<div style='float:left; margin-top:15px; width:40px;height:40px; min-width:40px; min-height:40px;'></div>";
        //let marginTopAssignedTo = rowdata.idfAssignedTo && assignedToPosition.length > 0  ? "margin-top:15px;" :"margin-top:30px;";
        let assignedTo: string = isToUser ? "<div style='padding-left:1%; " + isdelete + " margin-top:15px !important; float:left; width:79%; margin-top:4px; heigth:10px !important; max-heigth:10px !important; overflow: hidden !important; '>" + rowdata.assignedToFullName + "<br/>" + rowdata.assignedToPosition + "</div>" : "";
        let assignedToRol: string = isToRol ? "<div style='padding-left:1%; " + isdelete + " margin-top:20px !important; float:left; width:79%; margin-top:4px; heigth:10px !important; max-heigth:10px !important; overflow: hidden !important; '>" + rowdata.assignedToFullName + "</div>" : "";

        //  if(!(isToUser || isToRol))
        //  {
        //     assignedToRol = rowdata.assignedToFullName;
        //  }

        return "<div style='height:70px;'>" + img + assignedTo + assignedToRol + " </div>";
    };

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

        // if(this.ShouldHide())
        // {
        //     //this.gridReference.hidecolumn("Edit");
        //     //this.gridReference.hidecolumn("Undo");
        //     return;
        // }

        // //this.gridReference.hidecolumn(abmValue=="D" ? "Edit": "Undo");
        // //this.gridReference.showcolumn(abmValue=="D" ? "Undo" : "Edit");

        //this.gridReference.hidecolumn(abmValue=="D" ? "Edit": "Undo");
        //this.gridReference.showcolumn(abmValue=="D" ? "Undo" : "Edit");
        this.gridReference.hidecolumn("Edit");
        this.gridReference.showcolumn("Edit");
    };

    gridCellClick = (event: any): void => {
        // var args = event.args;
        // var rowBoundIndex = args.rowindex;

        // // fix selection problem
        // if(rowBoundIndex!=this.gridReference.getselectedrowindex())
        // {
        //     return;
        // }

        // var row = event.args.row.bounddata;

        // if (event.args.datafield === 'Edit') {
        //     let item = this.tasks.filter(x => x.id == row.id)[0];
        //     this.editTaskClick(item, this.currentModeTask);
        // }

        // if (event.args.datafield === 'Clone') {


        //     this.cloneTask.isEditing = true;
        //     for (var a = 0; a < this.settingReminderTime.length; a++) {
        //         let item = this.tasksReminders.filter(x => x.idfSettingReminderTime == this.settingReminderTime[a].id && x.idfTask == event.id)[0];
        //         this.settingReminderTime[a].state = item ? "true" : "false";
        //     }

        //     this.cloneTask.showClone(row, this.currentModeTask, this.settingReminderTime);
        // }

        // if(event.args.datafield==="Undo")
        // {
        //     let idx = this.gridReference.getselectedrowindex();
        //     if(idx>-1)
        //     {
        //         let currentRow = this.gridReference.getrowdata(this.gridReference.getselectedrowindex());
        //         var t = this.tasks.filter(x => x.id == currentRow.id)[0];
        //         t.abm = "";
        //         this.UpdateCurrentRow();

        //         setTimeout(() => {
        //             this.gridReference.clearselection();
        //         });
        //     }
        // }


        // if (event.args.datafield === 'Note') {
        //     let item = this.tasks.filter(x => x.id == row.id)[0];
        //     //this.editTaskClick(item, this.currentModeTask);
        //     this.notesTask.showEdit(item, this.dataAdapterClientsListBox, item.notes);
        // }
    }

    columnsGridTasks: any[] =
        [
            {
                width: '200px',
                text: '',
                datafield: 'Edit',
                height: '70px',
                columntype: 'none',
                cellsrenderer: this.editRenderer,
                menu: false,
                sortable: false,
                filterable: false
            },

            // {
            //     text: '',
            //     datafield: 'Undo',
            //     width: '50px',
            //     height:'70px',
            //     columntype: 'button',
            //     hidden:true,
            //     cellsrenderer: this.undoRenderer
            // },
            // {
            //     text: '',
            //     datafield: 'Edit',
            //     width: '45px',
            //     height:'70px',
            //     columntype: 'button',
            //     cellsrenderer: this.editRenderer
            // },
            // {
            //     text: '',
            //     datafield: 'Clone',
            //     width: '50px',
            //     height:'70px',
            //     columntype: 'button',
            //     cellsrenderer: this.cloneRenderer
            // },


            // {
            //     text: '',
            //     datafield: 'Note',
            //     width: '50px',
            //     height:'70px',
            //     columntype: 'button',
            //     cellsrenderer: this.noteRenderer
            // },

            {
                text: 'Subject',
                height: '70px',
                datafield: 'subject',
                width: 'auto',
                cellsrenderer: this.CommonColumnRenderer
            },
            {
                text: 'Status',
                datafield: 'status',
                width: '95px',
                height: '70px',
                cellsrenderer: this.CommonColumnRenderer
            },
            {
                text: 'Assigned To',
                datafield: 'assignedToFullName',
                width: '200px',
                height: '70px',
                cellsrenderer: this.assignedToRenderer
            },
            {
                text: 'Deadline',
                datafield: 'deadLine',
                width: '100px',
                height: '70px',
                cellsrenderer: this.deadLineRenderer
            },
            {
                text: 'Time',
                datafield: 'hours',
                width: '80px',
                height: '70px',
                cellsrenderer: this.timeRenderer
            },
            {
                text: 'TYPE',
                datafield: 'type',
                width: '80px',
                height: '70px',
                hidden: true
            },
            {
                text: 'ABM',
                datafield: 'abm',
                width: '80px',
                height: '70px',
                hidden: true
            },
        ];

    views: string[] =
        [
            'agendaView'
        ];

    tasksDataFields: any =
        {
            from: "from",
            to: "to",
            id: "id",
            description: "description",
            location: "location",
            subject: "subject",
            idUser: "idUser",
            resourceId: "projectName",
            resizable: "resizable",
            draggable: "draggable"
        };

    canDeleteOrSaveProject = (): boolean => {
        let disabled = this.projectClosed;
        return (this.authHelper.IsInRol("admin") || this.authHelper.IsInRol("projecteditor")) && !disabled;
    }

    periodClosed: boolean = false;
    projectClosed: boolean = false;

    changeCloseProject = (event: any): void => {
        this.project.abm = "U";
        this.project.state = event.target.checked ? "CL" : "C";

    }

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    tasksHours: number = 0;
    Owners: OwnerModel[] = [];
    sourceOwnerList: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'string' },
                { name: 'fullName', type: 'string' },
                { name: 'positionName', type: 'string' },
                { name: 'email', type: 'string' },
                { name: 'idfUser', type: 'number' }
            ],
            id: 'id',
        }

    dataAdapterOwnerList: any = new jqx.dataAdapter(this.sourceOwnerList);

    cmbOwnersChange = (event: any): void => { }

    //SOURCE PERIODS
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

    canDeleteProject = (): boolean => {
        return (this.isInrolAdmin); // && !this.periodClosed;
    }

    OkWindowDeleteConfirmation = (event: any): void => {
        let body = {
            Project: this.project,
            IdPeriod: this.currentPeriod
        }
        body.Project.abm = "D";

        this.projectsService.DeleteProject(body)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.saveActionDisplay.saved(true);
                        this.router.navigate(['projects']);
                    }
                    else {
                        this.saveActionDisplay.saved(false);
                        this.manageError(data);
                    }
                },
                error => {
                    this.saveActionDisplay.saved(false);
                    this.manageError(error);
                });
        this.windowDeleteConfirmation.close();
    }

    cancelWindowDeleteConfirmation = (event: any): void => {
        this.windowDeleteConfirmation.close();
    }

    openWindowDeleteConfirmation = (event: any): void => {
        this.translate.get('projects_delete_window_title').subscribe((res: string) => {
            this.windowDeleteConfirmation.setTitle(res);
        });
        this.windowDeleteConfirmation.open();
    }

    constructor(private jqxHelper: JqxHelper,
        private translate: TranslateService,
        private constantService: ConstantService,
        private staffServiceService: StaffService,
        private schedulingService: SchedulingService,
        private projectsService: ProjectsService,
        private route: ActivatedRoute,
        private authHelper: AuthHelper,
        public commonHelper: CommonHelper,
        private router: Router,
        private chRef: ChangeDetectorRef) {
        this.translate.setDefaultLang('en');
    }

    addParticipants = (event: any): void => {
        this.selectStaff.show();
    };

    addClients = (Event: any): void => {
        this.selectClient.show(this.sourceClients.localData);
    }

    deleteParticipants = (event: any): void => {
        var selected = this.listBoxParticipants.getSelectedItems();

        if (selected) {
            this.sourceStaff.localData.filter(c => c.id == selected[0].value)[0].abm = "D";
        }
        else {
        }
        this.hiddenDeleteParticipantButton = true;
        this.hiddenUndoParticipantButton = true;
        this.listBoxParticipants.clearSelection();
        this.listBoxParticipants.render();
        this.onAnyChange(null, "other");
    };

    UndoDeleteParticipants = (event: any): void => {
        var selected = this.listBoxParticipants.getSelectedItems();
        if (selected) {
            this.sourceStaff.localData.filter(c => c.id == selected[0].value)[0].abm = "I";
        }
        this.hiddenDeleteParticipantButton = true;
        this.hiddenUndoParticipantButton = true;
        this.listBoxParticipants.clearSelection();
        this.listBoxParticipants.render();
        this.onAnyChange(event, "other");
    };



    deleteClients = (event: any): void => {
        var selected = this.listBoxClients.getSelectedItems();
        if (selected) {
            this.sourceClients.localData.filter(c => c.id == selected[0].value)[0].abm = "D";
        }
        this.hiddenDeleteClientButton = true;
        this.hiddenUndoClientButton = true;
        this.listBoxClients.clearSelection();
        this.listBoxClients.render();
        this.onAnyChange(event, "other");
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
        this.onAnyChange(event, "other");
    };

    addTask = (event: any): void => {
        this.editTask.isEditing = false;
        event.idfTaskParent = 0;
        this.editTask.show(this.positions, this.currentModeTask, this.settingReminderTime, this.canDeleteOrSaveProject());
    };

    rendererListBoxStaff = (index, label, value): string => {
        if (this.sourceStaff.localData == undefined) {
            return null;
        }

        var datarecord = this.sourceStaff.localData[index];
        if (datarecord != undefined) {
            var imgurl = this.imagePipe.transform(datarecord.img, 'users');
            var img = imgurl.length > 0 ? '<img  style="border-radius:50%; " height="30" width="30" src="' + imgurl + '"/>' : "";
            var isdelete = datarecord.abm == "D" ? " text-decoration:line-through; color:Red;" : "";
            var table = '<table style="min-width: 120px;"><tr><td style="width: 30px;" rowspan="2">' + img + '</td><td style="color:rgb(51, 51, 51); font-size:12px;' + isdelete + '">' + datarecord.fullName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + this.commonHelper.convertMinsToHrsMins(datarecord.hours) + ' Hours</td></tr></table>';
            return table;
        }
        else {
            return label;
        }
    };

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

    //SOURCE LISTBOX Projects FILTERED
    sourceProject: any =
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

    // SOURCE LISTBOX STAFF FILTERED
    sourceStaff: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'color', type: 'string' },
                { name: 'email', type: 'string' },
                { name: 'fullName', type: 'string' },
                { name: 'idfUser', type: 'number' },
                { name: 'positionName', type: 'string' },
                { name: 'group', type: 'string' },
                { name: 'hours', type: 'number' },
                { name: 'idfPosition', type: 'number' },
                { name: 'idfStaff', type: 'number' },
                { name: 'abm', type: 'string' },
                { name: 'img', type: 'string' },
            ],
            id: 'id',
            async: false
        }

    //SOURCE LISTBOX STAFF FILTERED
    sourceStatus: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'status', type: 'string' }
            ],
            id: 'id',
            async: false
        }

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

    sourceClientsAllPeriods: any =
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

    dataAdapterStaffListBox: any = new jqx.dataAdapter(this.sourceStaff);
    dataAdapterStatusListBox: any = new jqx.dataAdapter(this.sourceStatus);
    dataAdapterProjectListBox: any = new jqx.dataAdapter(this.sourceProject);
    dataAdapterClientsListBox: any = new jqx.dataAdapter(this.sourceClients);
    dataAdapterClientsAllPeriods: any = new jqx.dataAdapter(this.sourceClientsAllPeriods);
    currentPeriod: number = 0;

    loadTasks = (): void => {
        if (this.currentPeriod == undefined) {
            return;
        }

        this.hiddenDeleteClientButton = true;
        this.hiddenDeleteParticipantButton = true;

        this.myLoader.open();

        this.projectsService.GetProject2(this.CurrentItem, this.currentPeriod)
            .subscribe(
                (data: any) => {
                    // this.setLanguageEspecialControls();
                    if (data.result) {
                        setTimeout(() => {
                            this.tasksHours = data.tasksHours;
                            this.tasks = <TaskModel[]>data.tasks;
                            this.tasksReminders = <tasksRemindersModel[]>data.tasksReminders;
                            this.dataAdapterScheduler = new jqx.dataAdapter(this.sourceScheduler);
                            this.sourceTasks.localData = this.tasks;
                            this.dataAdapterTasks = new jqx.dataAdapter(this.sourceTasks);
                        });

                        setTimeout(() => {
                            this.applyGidFilter();
                            setTimeout(() => {
                                if (data.tasks.length > 0) {
                                    this.gridReference.clearselection();
                                }
                            });

                        });

                        this.sourceStaff.localData = data.staffs;
                        this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);

                        this.sourceClients.localData = data.clients;
                        this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);

                        setTimeout(() => {
                            //this.applyGidFilter();
                            this.listBoxParticipants.render();
                            this.listBoxClients.render();
                        });

                        this.Owners = <OwnerModel[]>data.owners;
                        this.sourceOwnerList.localData = data.staffsForOwners;
                        this.dataAdapterOwnerList = new jqx.dataAdapter(this.sourceOwnerList);

                        setTimeout(() => {
                            this.showOwners();

                            // last line added. maybe solve the random bug // tasks hidden.
                            this.gridReference.render();
                        });
                    }
                    else {
                        this.manageError(data);
                    }

                    this.loadedControl[4] = true;
                    this.HiddeLoaderWhenEnd();
                    // this.myLoader.close();
                },
                error => {
                    //this.myLoader.close();
                    this.manageError(error);
                });
    }

    //NG DISABLED ISSUE IN JQWIDGETS
    EnabledDisableForm = (): void => {
        let disabled = this.projectClosed || !this.canDeleteOrSaveProject();
        this.projectname.setOptions({ disabled: disabled });
        this.projectdescription.setOptions({ disabled: disabled });
        this.myDropDown.setOptions({ disabled: disabled });
        this.dateTimeFrom.setOptions({ disabled: disabled });
        this.dateTimeTo.setOptions({ disabled: disabled });
        this.cmbOwners.setOptions({ disabled: disabled });
        this.projectaddreess.setOptions({ disabled: disabled });
        this.projectcity.setOptions({ disabled: disabled });
        this.projectPhone1.setOptions({ disabled: disabled });
        this.projectPhone2.setOptions({ disabled: disabled });
        this.projectdescription
    }

    PeriodOpen = (event: any): void => {
        // if(event.args.item.value == this.currentPeriod)
        // {
        //     return;
        // }

        // this.currentPeriod = event.args.item.value;
        // this.periodClosed = this.sourcePeriodsListBox.localData[event.args.index].state == "CL";
        // this.EnabledDisableForm();
        // this.loadTasks();


        if (!this.saveActionDisplay.isSaved()) {
            setTimeout(() => {
                this.glowMessage.ShowGlow("warn", "global_warning", "glow_project_warning1");
            });
        }
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

    PeriodSelectDrowDown = (event: any): void => {
        if (event.args.item.value == this.currentPeriod) {
            return;
        }

        this.currentPeriod = event.args.item.value;
        this.periodClosed = this.sourcePeriodsListBox.localData[event.args.index].state == "CL";
        this.EnabledDisableForm();
        this.loadTasks();
    }

    // periodsBindingComplete = (event: any) => {
    //     if (this.sourcePeriodsListBox.localData != undefined)
    //     {
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

    loadPeriods = (): void => {
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
                    this.loadedControl[0] = true;
                    this.HiddeLoaderWhenEnd();
                    //this.myLoader.close();
                },
                error => {
                    //this.myLoader.close();
                    this.manageError(error);
                }
            );
    }

    loadStatus = (): void => {
        this.myLoader.open();
        this.projectsService.GetStatuses()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.sourceStatus.localData = data.statusesList;
                        this.dataAdapterStatusListBox = new jqx.dataAdapter(this.sourceStatus);
                    }
                    else {
                        this.manageError(data);
                    }
                    //this.myLoader.close();
                    this.loadedControl[3] = true;
                    this.HiddeLoaderWhenEnd();
                },
                error => {
                    //this.myLoader.close();
                    this.manageError(error);
                }
            );
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            //this.commonHelper.playAudio();
            this.isInrolAdmin = this.authHelper.IsInRol("admin");
            this.translate.use('en');
            this.myDropDown.setContent(this.getTextElementByColor({ hex: "FFFFFF" }));
            // put it in one single call.
            this.loadPeriods();
            this.LoadCurrentProject();
            this.LoadAllProjects();
            this.loadStatus();
            this.setLanguageEspecialControls();
        });
    }

    setLanguageEspecialControls = (): void => {
        this.translate.get('projects_participants_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(0, res); });
        this.translate.get('projects_clients_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(1, res); });
        this.translate.get('projects_forms_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(2, res); });
        this.translate.get('projects_others_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(3, res); });
        this.translate.get('projects_tasks_tab').subscribe((res: string) => { this.tabsReference2.setTitleAt(0, res); });
        this.translate.get('global_lookingfor').subscribe((res: string) => {
            this.PlaceHolderLookingFor = res;
        });

        this.translate.get('projects_requirements').subscribe((res: string) => { let myContainer = document.getElementById('Left') as HTMLInputElement; myContainer.append(res); });
        this.translate.get('projects_goals').subscribe((res: string) => { let myContainer = document.getElementById('Center') as HTMLInputElement; myContainer.append(res); });
        this.translate.get('projects_reminders').subscribe((res: string) => { let myContainer = document.getElementById('Right') as HTMLInputElement; myContainer.append(res); });
        this.translate.get('projects_select_period').subscribe((res: string) => {
            this.PlaceHolderSelectPeriod = res;
        });
        //

        this.translate.get('projects_edit_name').subscribe((res: string) => {
            this.PlaceHolderProjectName = res;
        });
        this.translate.get('projects_edit_description').subscribe((res: string) => {
            this.PlaceHolderDescription = res;
        });

        // this.gridTrackingTimeReview

        // if(this.gridReference)
        // {
        //     // this.translate.get('projects_grid_subject').subscribe((res: string) => { this.gridReference.setcolumnproperty("subject","text",res); });
        //     // this.translate.get('projects_grid_status').subscribe((res: string) => { this.gridReference.setcolumnproperty("status","text",res); });
        //     // this.translate.get('projects_grid_assigned_to').subscribe((res: string) => { this.gridReference.setcolumnproperty("assignedToFullName","text",res); });
        //     // this.translate.get('projects_grid_deadline').subscribe((res: string) => { this.gridReference.setcolumnproperty("deadLine","text",res); });
        //     // this.translate.get('projects_grid_time').subscribe((res: string) => { this.gridReference.setcolumnproperty("hours","text",res); });

        //     // this.gridReference.localizestrings(this.translate.currentLang == "en" ? this.jqxHelper.getGridLocation_en : this.jqxHelper.getGridLocation_es);
        // }
    }

    tasks: TaskModel[] = [];
    project: ProjectModel = new ProjectModel();

    getTextElementByColor(color: any): any {
        if (color == 'transparent' || color.hex == "") {
            return '<div style="text-shadow: none; position: relative; padding-bottom: 2px; margin-top: 2px;">transparent</div>';
        }
        let nThreshold = 105;
        let bgDelta = (color.r * 0.299) + (color.g * 0.587) + (color.b * 0.114);
        let foreColor = (255 - bgDelta < nThreshold) ? 'Black' : 'White';
        this.project.color = color.hex;
        let element = '<div style="text-shadow: none; position: relative; padding-bottom: 2px; margin-top: 2px;color:' + foreColor + '; background: #' + color.hex + '">&nbsp;</div>';
        return element;
    }

    colorPickerEvent(event: any): void {
        this.myDropDown.setContent(this.getTextElementByColor(event.args.color));
    }

    onAddParticipant = (event: EventEmiterParticipantModel): void => {
        let search = this.sourceStaff.localData.filter(c => c.idfPosition == event.IdfPosition && c.idfStaff == event.IdfStaff) // selected[0].value)[0].abm == "D";
        if (search.length <= 0) {
            this.negativeSequence--;
            this.sourceStaff.localData.push({
                color: null,
                email: "",
                fullName: event.UserName,
                group: event.PositionName,
                id: this.negativeSequence,
                idfUser: event.IdUser,
                positionName: event.PositionName,
                staffprojectposition: null,
                state: null,
                user: null,
                hours: 0,// event.Hours,
                idfPosition: Number(event.IdfPosition),
                idfStaff: Number(event.IdfStaff),
                abm: event.abm,
                img: event.img
            });

            this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);
            this.onAnyChange(event, "other");
        }
    }

    onAddClient = (event: any): void => {
        event.ClientList.forEach(el => {

            this.sourceClients.localData.push({ id: el.id, fullName: el.fullName, img: el.img, abm: el.abm, idfClient: el.idfClient });
        });

        this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);
        this.onAnyChange(event, "other");
    }

    hiddenDeleteParticipantButton: boolean = true;
    hiddenUndoParticipantButton: boolean = true;

    hiddenDeleteClientButton: boolean = true;
    hiddenUndoClientButton: boolean = true;

    onSelectParticipant = (event: any): void => {
        let selected = this.listBoxParticipants.getSelectedItems();
        let isDeleted = false;

        if (selected) {
            isDeleted = this.sourceStaff.localData.filter(c => c.id == selected[0].value)[0].abm == "D";
        }

        this.hiddenDeleteParticipantButton = isDeleted || this.periodClosed || this.currentPeriod <= 0 || !this.canDeleteOrSaveProject();
        this.hiddenUndoParticipantButton = !this.hiddenDeleteParticipantButton || !this.canDeleteOrSaveProject();
    }

    OnGridGenericEvent2 = (event: any): void => {
        setTimeout(() => {
            this.gridReference.render();
        }, 2000);
    }

    OnGridGenericEvent = (event: any): void => {
        setTimeout(() => {
            this.gridReference.render();
        });
    }

    onSelectClient = (event: any): void => {
        //var args = event.args;
        let selected = this.listBoxClients.getSelectedItems();
        let isDeleted = false;

        if (selected) {
            isDeleted = this.sourceClients.localData.filter(c => c.id == selected[0].value)[0].abm == "D";
        }

        this.hiddenDeleteClientButton = isDeleted || this.periodClosed || this.currentPeriod <= 0 || !this.canDeleteOrSaveProject();
        this.hiddenUndoClientButton = !this.hiddenDeleteClientButton || !this.canDeleteOrSaveProject();
    }

    editPettyCashclick = (event: any): void => {
        this.pettyCash.showEdit(this.project.id, this.currentPeriod);
    }

    editTaskClick = (event: any, type: string): void => {
        this.editTask.isEditing = true;
        for (var a = 0; a < this.settingReminderTime.length; a++) {
            let item = this.tasksReminders.filter(x => x.idfSettingReminderTime == this.settingReminderTime[a].id && x.idfTask == event.id)[0];
            this.settingReminderTime[a].state = item ? "true" : "false";
        }
        // this.dataAdapterClientsAllPeriods
        //this.editTask.showEdit(event, this.positions, type, this.settingReminderTime,this.dataAdapterClientsListBox);
        this.editTask.showEdit(event, this.positions, type, this.settingReminderTime, this.dataAdapterClientsAllPeriods, this.canDeleteOrSaveProject());
    };

    onCloneTask = (event: EventEmiterTaskModel): void => {
        this.myLoader.open();

        // TODO: allways is null or undefined....
        if (event.settingReminderTime) {
            for (var e = 0; e < event.settingReminderTime.length; e++) {
                let item = this.tasksReminders.filter(c => c.idfTask == event.Id && c.idfSettingReminderTime == event.settingReminderTime[e].id)[0];
                if (item) {
                    item.state = event.settingReminderTime[e].state;
                    item.idfTask = event.Id;
                }
                else {
                    let xxx = new tasksRemindersModel();
                    xxx.idfPeriod = this.currentPeriod;
                    xxx.idfSettingReminderTime = event.settingReminderTime[e].id;
                    xxx.idfTask = event.Id;
                    xxx.state = event.settingReminderTime[e].state;
                    this.tasksReminders.push(xxx);
                }
            }
        }

        //var t = this.tasks.filter(x => x.id == event.Id)[0];
        let t = new TaskModel();
        t.id = event.Id;
        t.idfStatus = event.IdfStatus;
        t.status = event.Status;
        t.deadline = new Date(event.Deadline);
        t.subject = event.TaskName;
        t.hours = event.Hours;
        t.idfAssignedTo = event.IdfStaff;
        t.assignedToFullName = event.UserFullName;
        t.idfAssignableRol = event.IdfAssignableRol;
        t.abm = event.Abm;
        t.idfPeriod = event.IdfPeriod;
        t.idfProject = event.IdfProject;
        t.description = event.Description;

        let request: any = {
            Task: t,
            Move: event.Abm == "M",
            IdfProject: event.IdfProject,
            IdfPeriod: event.IdfPeriod
        };

        this.projectsService.CloneTask(request)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.glowMessage.ShowGlow("success", "global_success", "global_operation_saved_successfully");
                        this.loadTasks();
                    }
                    else {
                        this.manageError(data);
                    }

                    this.myLoader.close();
                },
                error => {
                    this.myLoader.close();
                    this.manageError(error)
                });
    }


    onAddNote = (event: EventEmiterNoteModel): void => {
        // hacemos algo
        var t = this.tasks.filter(x => x.id == event.IdTask)[0];
        t.abm = t.abm != "I" ? "U" : t.abm;
        t.notes = event.Value;

        if (this.canDeleteOrSaveProject()) {
            this.saveActionDisplay.setDirty();
        }
        // t.status = event.Status;
    }

    onAddTask = (event: EventEmiterTaskModel): void => {
        switch (event.Abm) {
            case "I":
                this.negativeSequence--;
                let newTask = new TaskModel();
                newTask.id = this.negativeSequence;
                newTask.idfTaskParent = event.IdfTaskParent;//this.currentIdParent;
                newTask.deadline = event.Deadline;
                newTask.idfStatus = event.IdfStatus;
                newTask.status = event.Status;
                newTask.idUser = event.IdUser;
                newTask.subject = event.TaskName;
                //newTask.assignedToFullName = event.UserFullName;
                newTask.hours = event.Hours;
                newTask.abm = event.Abm;
                //newTask.idfAssignedTo = event.IdfStaff;
                newTask.abm = event.Abm;
                newTask.idfPeriod = this.currentPeriod;
                //newTask.idfAssignableRol = event.IdfAssignableRol;
                newTask.type = event.type;
                newTask.description = event.Description;
                newTask.assignedToFullName = event.UserFullName;
                newTask.idfAssignedTo = event.IdfStaff;
                newTask.idfAssignableRol = event.IdfAssignableRol;
                newTask.assignedToPosition = event.AssignedToPosition;
                newTask.img = event.Img;
                //newTask.notes = event.Notes;
                this.tasks.push(newTask);
                if (event.settingReminderTime) {
                    for (var e = 0; e < event.settingReminderTime.length; e++) {
                        let item = this.tasksReminders.filter(c => c.idfTask == event.Id && c.idfSettingReminderTime == event.settingReminderTime[e].id)[0];
                        if (item) {
                            item.state = event.settingReminderTime[e].state;
                            item.idfTask = newTask.id;
                        }
                        else {
                            let xxx = new tasksRemindersModel();
                            xxx.idfPeriod = this.currentPeriod;
                            xxx.idfSettingReminderTime = event.settingReminderTime[e].id;
                            xxx.idfTask = newTask.id;// event.Id;
                            xxx.state = event.settingReminderTime[e].state;
                            this.tasksReminders.push(xxx);
                        }
                    }
                }
                break;
            case "U":
                // TODO: allways is null or undefined....
                if (event.settingReminderTime) {
                    for (var e = 0; e < event.settingReminderTime.length; e++) {
                        let item = this.tasksReminders.filter(c => c.idfTask == event.Id && c.idfSettingReminderTime == event.settingReminderTime[e].id)[0];
                        if (item) {
                            item.state = event.settingReminderTime[e].state;
                            item.idfTask = event.Id;
                        }
                        else {
                            let xxx = new tasksRemindersModel();
                            xxx.idfPeriod = this.currentPeriod;
                            xxx.idfSettingReminderTime = event.settingReminderTime[e].id;
                            xxx.idfTask = event.Id;
                            xxx.state = event.settingReminderTime[e].state;
                            this.tasksReminders.push(xxx);
                        }
                    }
                }

                var t = this.tasks.filter(x => x.id == event.Id)[0];
                t.idfStatus = event.IdfStatus;
                t.status = event.Status;
                //t.deadline = new Date(event.Deadline);
                t.deadline = event.Deadline;
                t.subject = event.TaskName;
                t.hours = event.Hours;
                t.abm = t.id > 0 ? event.Abm : "I";
                t.idfPeriod = this.currentPeriod;
                t.description = event.Description;
                t.assignedToFullName = event.UserFullName;
                t.idfAssignedTo = event.IdfStaff;
                t.idfAssignableRol = event.IdfAssignableRol;
                t.assignedToPosition = event.AssignedToPosition;
                t.img = event.Img;
                //t.notes =  event.Notes;
                break;
            case "D":
                var t = this.tasks.filter(x => x.id == event.Id)[0];
                t.abm = event.Abm;
                break;
        }

        // Esto provoca el redraw y desorden en filas de tasks
        this.UpdateCurrentRow(event.Abm);

        if (this.canDeleteOrSaveProject()) {
            this.saveActionDisplay.setDirty();
        }

        // setTimeout(() => {
        //     //this.gridReference.clearselection();
        //  });
    };

    UpdateCurrentRow = (abm: string): void => {
        var indexRow = this.gridReference.getselectedrowindex();
        this.gridReference.rendergridrows(this.gridReference.selectedrowindexes() as any);
        //this.currentModeTask = event.args.index == 0 ? "RQ" : event.args.index == 1 ?  'GO' : "RE";
        if (this.currentModeTask == "RQ") {
            setTimeout(() => {
                this.gridReference.ensurerowvisible(indexRow)
            });
        }
        else {
            setTimeout(() => {
                this.applyGidFilter(() => {
                    setTimeout(() => {
                        this.gridReference.ensurerowvisible(indexRow)
                    });
                });
            });
        }
    };

    isValidDate(value) {
        var dateWrapper = new Date(value);
        return !isNaN(dateWrapper.getDate());
    }

    toLocaleDateString(val: any) {
        return this.isValidDate(val) ? new Date(val).toLocaleDateString() : "-";
    }

    showOwners = (): void => {
        for (var i = 0; i < this.Owners.length; i++) {
            let items = this.cmbOwners.getItems();
            let filter = items.filter(c => c.value == this.Owners[i].idfStaff);
            if (filter != null && filter.length > 0) {
                this.cmbOwners.selectItem(filter[0]);
            }
        }
    }

    tasksReminders: tasksRemindersModel[] = [];

    LoadAllProjects = () => {
        this.myLoader.open();
        this.projectsService.GetProjects()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.sourceProject.localData = data.projects;
                        this.dataAdapterProjectListBox = new jqx.dataAdapter(this.sourceProject);
                    }
                    else {
                        this.manageError(data);
                    }

                    this.loadedControl[2] = true;
                    this.HiddeLoaderWhenEnd();
                    //this.myLoader.close();
                },
                error => {
                    //this.myLoader.close();
                    this.manageError(error);
                });
    }

    LoadCurrentProject = () => {
        this.myLoader.open();
        this.projectsService.GetProject(this.CurrentItem)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        if (this.CurrentItem >= 0) {
                            this.projectClosed = data.project.state == "CL";
                            this.EnabledDisableForm();
                            this.positions = <positionsModel[]>data.positions;
                            this.tasks = <TaskModel[]>data.project.tasks;
                            this.project = <ProjectModel>data.project;
                            this.myDropDown.setContent(this.getTextElementByColor({ hex: data.project.color.replace('#', '') }));
                            this.dateTimeFrom.setDate(new Date(data.project.beginDate));
                            this.dateTimeTo.setDate(new Date(data.project.endDate));
                            // this.Owners = <OwnerModel[]>data.owners;
                            this.sourceClientsAllPeriods.localdata = data.clientsAllPeriods;
                            this.dataAdapterClientsAllPeriods = new jqx.dataAdapter(this.sourceClientsAllPeriods);
                        }
                        else {
                            this.project.id = -1;
                            this.project.projectName = '';
                            this.project.description = '';
                            this.project.totalHours = 0;
                            this.project.beginDate = new Date();
                            this.project.endDate = new Date();
                            this.myDropDown.setContent(this.getTextElementByColor({ hex: "FFFFFF" }));
                            this.sourceStaff.localData = data.staffs;
                            this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);
                        }

                        setTimeout(() => {
                            //this.loadedControl[1] = true;
                            //this.HiddeLoaderWhenEnd();
                            setTimeout(() => {
                                this.loadedControl[1] = true;
                                this.HiddeLoaderWhenEnd();
                            }, 2000);
                        });
                        // this.loadedControl[1] = true;
                        // this.HiddeLoaderWhenEnd();

                        this.settingReminderTime = data.settingsReminderTime;

                        // this.sourceOwnerList.localData = data.staffsForOwners;
                        // this.dataAdapterOwnerList = new jqx.dataAdapter(this.sourceOwnerList);

                        // setTimeout(() => {
                        //     this.showOwners();
                        // });
                    }
                    else {
                        this.manageError(data);
                    }

                    //this.myLoader.close();
                },
                error => {
                    //this.myLoader.close();
                    this.manageError(error);
                });
    }

    //VARIABLES
    CurrentItem: number = -1;
    private sub: any;

    isEditing = (): boolean => {
        return this.CurrentItem >= 0;
    }

    validateProject = (): boolean => {
        let result = true;
        let projectName: string = this.projectname.val();
        let projectDescription: string = this.projectdescription.val();
        let date1: Date = this.dateTimeFrom.getDate();
        let date2: Date = this.dateTimeTo.getDate();

        if (projectName.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_type_project_name");
            result = false;
        }
        // if (projectDescription.trim().length <= 0) {
        //     //alert("sdf")
        //     this.glowMessage.ShowGlow("warn","glow_invalid","glow_project_type_project_description");
        //     result = false;
        // }
        if (date1 == null) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_valid_date");
            result = false;
        }
        else {
            if (date1.getTime() > date2.getTime()) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_date_from_smaller_than_to");
                result = false;
            }
        }
        if (date2 == null) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_valid_date");
            result = false;
        }

        return result;
    }

    Save(body: any): void {
        if (this.validateProject()) {
            // this.myLoader.open();
            // this.glowMessage.msgs = [];
            this.projectsService.SaveProject(body)
                .subscribe(
                    (data: any) => {
                        if (data.result) {
                            // reset data.

                            // this.sourceStaff.localData.filter(x => x.abm == 'U')
                            // .forEach(element => {
                            //     element.abm = "";
                            // });

                            this.sourceStaff.localData.filter(x => x.abm != '' && x.abm != 'D')
                                .forEach(element => {
                                    element.abm = "";
                                });

                            this.sourceClients.localData.filter(x => x.abm != '' && x.abm != 'D')
                                .forEach(element => {
                                    element.abm = "";
                                });

                            this.sourceTasks.localData.filter(x => x.abm != '' && x.abm != 'D')
                                .forEach(element => {
                                    element.abm = "";
                                });

                            // end reset data.

                            this.saveActionDisplay.saved(true);

                            if (body.Project.abm == "D") {
                                this.router.navigate(['projects']);
                            }
                            else {
                                // this.glowMessage.msgs = [];
                                // this.glowMessage.ShowGlow("success","glow_success","glow_project_saved_successfully");

                                setTimeout(() => {
                                    this.CurrentItem = data.tagInfo;
                                    this.project.id = Number(this.CurrentItem);
                                    this.project.state = "C";
                                    // this.loadedControl[1] = false;
                                    // this.loadedControl[4] = false;
                                    // this.LoadCurrentProject();
                                    // this.loadTasks();
                                    // this.currentModeTask = 'RQ';
                                    // setTimeout(() => {
                                    //      this.applyGidFilter();
                                    // });
                                }, 3);
                            }

                            if (data.forceLoad) {
                                setTimeout(() => {
                                    this.tasksHours = data.tasksHours;
                                    this.tasks = <TaskModel[]>data.tasks;
                                    this.tasksReminders = <tasksRemindersModel[]>data.tasksReminders;
                                    // this.dataAdapterScheduler = new jqx.dataAdapter(this.sourceScheduler);
                                    this.sourceTasks.localData = this.tasks;
                                    this.dataAdapterTasks = new jqx.dataAdapter(this.sourceTasks);
                                    this.sourceStaff.localData = data.staffs;
                                    this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);
                                    this.sourceClients.localData = data.clients;
                                    this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);
                                    //this.tasksReminders = <tasksRemindersModel[]>data.tasksReminders;

                                    setTimeout(() => {
                                        this.listBoxParticipants.render();
                                        this.listBoxClients.render();
                                        this.gridReference.render();
                                    });
                                });
                            }
                        }
                        else {
                            this.saveActionDisplay.saved(false);
                            this.manageError(data);
                        }
                        //this.myLoader.close();
                    },
                    error => {
                        this.saveActionDisplay.saved(false);
                        // this.myLoader.close();
                        this.manageError(error);
                    });
        }
        else {
            this.saveActionDisplay.saved(false);
        }
    }

    getCheckedOwners = (): OwnerModel[] => {
        let result: OwnerModel[] = [];
        let items = this.cmbOwners.getSelectedItems();
        if (items != null) {
            for (var i = 0; i < items.length; i++) {
                let toAdd = new OwnerModel();
                toAdd.idfProject = this.project.id;
                toAdd.idfOwner = items[i].originalItem.id;
                toAdd.state = "C";
                result.push(toAdd);
            }
        }
        return result;
    }

    onAnyChange = (event: any, type: string = "-"): void => {
        var args = event ? event.args : null;

        if (args || type == "check" || type == "other") {
            //var item = args.item;
            let control = true;
            this.loadedControl.forEach(element => {
                control = control && element;
            });

            if (control) {
                if (this.canDeleteOrSaveProject()) {
                    this.saveActionDisplay.setDirty();
                }
            }
        }
        else {
        }
    }

    onMustSave = (event: any): void => {
        this.SaveProject(null);
        // setTimeout(() => {
        //     this.saveActionDisplay.saved(false);
        // }, 2000);
    }

    SaveProject(event: any): void {
        console.log(this.sourceClients.localData.filter(x => x.abm != ''));
        let body = {
            Project: this.project,
            Tasks: this.tasks.filter(x => x.abm != '' && !(x.id <= 0 && x.abm == "D")),
            Staffs: this.sourceStaff.localData == undefined ? null : this.sourceStaff.localData.filter(x => x.abm != ''),
            Owners: this.getCheckedOwners(),
            IdPeriod: this.currentPeriod,
            tasksReminders: this.tasksReminders.filter(c => c.state == "true" || c.state == "false"),
            clients: this.sourceClients.localData == undefined ? null : this.sourceClients.localData.filter(x => x.abm != '')
        }

        body.Project.abm = this.isEditing() ? "U" : "I";
        body.Project.beginDate = this.dateTimeFrom.getDate();
        body.Project.endDate = this.dateTimeTo.getDate();
        console.log(body);
        this.Save(body);
    };

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(params => {
            this.CurrentItem = +params['id'];
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
