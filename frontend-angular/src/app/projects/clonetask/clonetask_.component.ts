import { Component, ViewChild, AfterViewInit, OnInit, OnDestroy, Injectable, EventEmitter, Output, Input, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConstantService } from '../../common/services/app.constant.service';
import { EventEmiterTaskModel } from './clonetask_.component.model';
import { Router } from '@angular/router';
import { jqxWindowComponent } from 'jqwidgets-ng/jqwidgets/jqxwindow';
import { jqxNumberInputComponent } from 'jqwidgets-ng/jqwidgets/jqxnumberinput';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqwidgets/jqxdropdownlist';
import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqwidgets/jqxdatetimeinput';
import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { TaskModel, settingReminderTimeModel } from '../editproject/editproject_.component.model';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
//import { settingReminderTimeModel } from '../editproject/editproject.component.model';
import { jqxTabsComponent } from 'jqwidgets-ng/jqwidgets/jqxtabs';
import { ProjectsService } from '../projects.service';
import { AuthHelper } from '../../common/helpers/app.auth.helper';
import { jqxRadioButtonComponent } from 'jqwidgets-ng/jqwidgets/jqxradiobutton';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { jqxScrollBarComponent } from 'jqwidgets-ng/jqwidgets/jqxscrollbar';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';

@Component({
    selector: 'clonetask',
    templateUrl: '../../projects/clonetask/clonetask_.component.template.html',
    providers: [ConstantService, ProjectsService],
    styleUrls: ['../../projects/clonetask/clonetask_.component.css'],
})

@Injectable()
export class CloneTask implements OnInit, OnDestroy, AfterViewInit {

    @Input('dataAdapterStatusListBox') dataAdapterStatusListBox: any;
    @Input('dataAdapterPeriodListBox') dataAdapterPeriodListBox: any;
    @Input('dataAdapterProjectListBox') dataAdapterProjectListBox: any;
    @Input('sourceStatus') sourceStatus: any;
    @Input('sourcePeriod') sourcePeriod: any;
    @Input('sourceProject') sourceProject: any;
    @Input('glowmessage') glowMessage: GlowMessages;
    @Output() onCloneTask = new EventEmitter<EventEmiterTaskModel>();
    @ViewChild(jqxWindowComponent) private windowx: jqxWindowComponent;
    //@ViewChild('msgNotificationError') msgNotificationError: jqxNotificationComponent;
    @ViewChild('editGenStaff') editGenStaff: jqxDropDownListComponent;
    @ViewChild('editGenTaskName') editGenTaskName: jqxInputComponent;
    @ViewChild('editGenTaskDescription') editGenTaskDescription: jqxInputComponent;
    @ViewChild('estimatedHours') estimatedHours: jqxNumberInputComponent;
    @ViewChild('positionsDropDown') positionsDropDown: jqxDropDownListComponent;
    @ViewChild('statusDropDown') statusDropDown: jqxDropDownListComponent;
    @ViewChild('periodDropDown') periodDropDown: jqxDropDownListComponent;
    @ViewChild('projectDropDown') projectDropDown: jqxDropDownListComponent;
    @ViewChild('editGenDeadline') editGenDeadline: jqxDateTimeInputComponent;
    @ViewChild('tabsReference') tabsReference: jqxTabsComponent;
    @ViewChild('radioButtonCopy') radioButtonCopy: jqxRadioButtonComponent;
    @ViewChild('radioButtonMove') radioButtonMove: jqxRadioButtonComponent;

    imagePipe = new ImagePipe();
    PlaceHolderLookingFor: string;
    PlaceHolderSelectProject: string;
    PlaceHolderSelectPeriod: string;
    PlaceHolderParticipant: string;
    PlaceHolderPosition: string;
    PlaceHolderStatus: string;
    PlaceHolderName: string;
    PlaceHolderDescription: string;
    msgError: string = "";
    settingReminderTime: settingReminderTimeModel[] = [];
    public isEditing: boolean;
    public currentId: number;

    constructor(
        private translate: TranslateService,
        private constantService: ConstantService,
        private route: ActivatedRoute,
        private router: Router,
        private authHelper: AuthHelper,
        private projectsService: ProjectsService,
        private chRef: ChangeDetectorRef) { }

    rendererListBoxStaff = (index, label, value): string => {
        if (this.sourceStaff.localData == undefined) {
            return null;
        }

        var datarecord = this.sourceStaff.localData[index];
        if (datarecord != undefined) {
            var imgurl = this.imagePipe.transform(datarecord.img, 'users');
            var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';
            var table = '<table style="min-width: 120px;"><tr><td style="width: 30px;" rowspan="2">' + img + '</td><td style="color:rgb(51, 51, 51); font-size:12px;">' + datarecord.fullName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + datarecord.positionName + '</td></tr></table>';
            return table;
        }
        else {
            return label;
        }
    };

    rendererListBoxStatus = (index, label, value): string => {
        if (this.sourceStatus.localData == undefined) {
            return null;
        }
        var datarecord = this.sourceStatus.localData[index];
        if (datarecord != undefined) {
            var table = '<table style="min-width: 120px;"><tr><td style="color:rgb(51, 51, 51); font-size:12px;">' + datarecord.status + '</td></tr>';
            return table;
        }
        else {
            return label;
        }
    };

    sourcePositions: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'name', type: 'string' },
            ],
            id: 'id',
            async: false
        }

    dataAdapterPositions: any = new jqx.dataAdapter(this.sourcePositions);

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
    dataAdapterStaffListBox_: any = new jqx.dataAdapter(this.sourceStaff);
    type: string = "";

    initWidgets = (tab: any): void => {
        switch (tab) {
            case 0:
                // this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);
                this.translate.get('global_copy').subscribe((res: string) => {
                    let myContainer = document.getElementById('x_copy') as HTMLInputElement;
                    setTimeout(x => {
                        myContainer.innerHTML = res;
                    });
                });

                this.translate.get('global_move').subscribe((res: string) => {
                    let myContainer = document.getElementById('x_move') as HTMLInputElement;
                    myContainer.innerHTML = res;
                });
                break;
            case 1:
                // this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);
                // this.listBoxClients.render();
                break;
        }
    }

    setLanguageEspecialControls = (): void => {
        this.translate.get('projects_copy_move_action_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(0, res); });
        this.translate.get('projects_copy_move_preview_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(1, res); });
        // this.translate.get('global_copy').subscribe((res: string) => {  
        //     let myContainer = document.getElementById('x_copy') as HTMLInputElement;
        //      myContainer.innerHTML = res; 
        // });

        // this.translate.get('global_move').subscribe((res: string) => {  
        //     let myContainer = document.getElementById('x_move') as HTMLInputElement;
        //      myContainer.innerHTML = res; 
        // });

        this.translate.get('global_lookingfor').subscribe((res: string) => {
            this.PlaceHolderLookingFor = res;
        });

        this.translate.get('projects_select_project').subscribe((res: string) => {
            this.PlaceHolderSelectProject = res;
        });

        this.translate.get('projects_select_period').subscribe((res: string) => {
            this.PlaceHolderSelectPeriod = res;
        });

        this.translate.get('projects_select_participant').subscribe((res: string) => {
            this.PlaceHolderParticipant = res;
        });

        this.translate.get('projects_select_position').subscribe((res: string) => {
            this.PlaceHolderPosition = res;
        });

        this.translate.get('projects_select_status').subscribe((res: string) => {
            this.PlaceHolderStatus = res;
        });

        this.translate.get('projects_type_name').subscribe((res: string) => {
            this.PlaceHolderName = res;
        });

        this.translate.get('projects_type_description').subscribe((res: string) => {
            this.PlaceHolderDescription = res;
        });
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
        });
    };

    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    showClone(data: TaskModel, type: string, settingReminderTime: settingReminderTimeModel[]): void {
        this.tabsReference.select(0);
        this.radioButtonCopy.uncheck();
        this.radioButtonMove.uncheck();
        this.clearWindow();
        this.type = type;
        this.settingReminderTime = settingReminderTime;
        //let title = type == "RQ" ? " Requirement" : type == "GO" ? " Goal" : " Reminder";
        //this.windowx.setTitle(" " + title);
        this.dataAdapterPositions = new jqx.dataAdapter(this.sourcePositions);
        this.currentId = data.id;
        setTimeout(x => {
            if (data.idfAssignableRol) {
                let sel = this.positionsDropDown.getItemByValue(data.idfAssignableRol.toString());
                if (sel) {
                    this.positionsDropDown.selectItem(sel);
                } else {

                }
            }
        });
        //editGenDeadline
        if (data.idfAssignedTo > 0) {
            let itemsStaff = this.editGenStaff.getItems().filter(x => x.value == data.idfAssignedTo);
            if (itemsStaff != null) {
                this.editGenStaff.selectItem(itemsStaff[0])
            }
        }
        else {
            this.editGenStaff.clearSelection();
        }

        if (data.idfStatus > 0) {
            let itemsStatus = this.statusDropDown.getItems().filter(x => x.value == data.idfStatus);
            if (itemsStatus != null) {
                this.statusDropDown.selectItem(itemsStatus[0])
            }
        }

        this.periodDropDown.clearSelection()
        this.projectDropDown.clearSelection();
        this.editGenTaskName.val(data.subject);
        this.estimatedHours.val(data.hours / 3600);
        this.editGenTaskDescription.val(data.description);
        // if (data.deadline == null) {
        //     this.editGenDeadline.val(null);
        // }
        // else {
        //     this.editGenDeadline.setDate(new Date(data.deadline));
        // }

        if (data.deadline == null && data.id != 0) {
            this.editGenDeadline.val(null);
        }
        else {
            if (!data.deadline) {
                this.editGenDeadline.clearString();
            }
            else {
                this.editGenDeadline.setDate(new Date(data.deadline));
            }
        }

        if (type == 'RE') {

        }

        //this.windowx.open();
        //
        this.translate.get('projects_requirements').subscribe((resReq: string) => {
            //     //this.myGrid.setcolumnproperty("projectName","text",res); 
            this.translate.get('projects_goals').subscribe((resGoal: string) => {
                //         //this.myGrid.setcolumnproperty("projectName","text",res); 
                this.translate.get('projects_reminders').subscribe((resReminder: string) => {
                    //             //this.myGrid.setcolumnproperty("projectName","text",res); 
                    this.windowx.open();
                    let title = type == "RQ" ? resReq : type == "GO" ? resGoal : resReminder;
                    //this.translate.get('global_create').subscribe((res: string) => { 
                    this.windowx.setTitle(title);
                    //});
                });
            });
        });

        setTimeout(() => {
            this.setLanguageEspecialControls();
        });
    }

    deleteDeadLine = (event: any) => {
        this.editGenDeadline.val(null);
    }

    deletePosition = (event: any) => {
        this.positionsDropDown.clearSelection();
    }

    deleteStaff = (event: any) => {
        this.editGenStaff.clearSelection();
    }

    clearWindow = (): void => {
        this.editGenStaff.clearSelection();
        this.positionsDropDown.clearSelection();
        this.statusDropDown.clearSelection();
        this.editGenTaskName.val(null);
        this.editGenTaskDescription.val(null);
        this.estimatedHours.val(0);
        this.editGenDeadline.val(null);
    }

    onEditGenStaffChange = (event: any) => {
        this.positionsDropDown.clearSelection();
    }

    onPositionsDropDownChange = (event: any) => {
        this.editGenStaff.clearSelection();
    }

    onProject_or_PeriodDropDownChange = (event: any) => {
        let period = this.periodDropDown.val();
        let project = this.projectDropDown.val();
        if (period == "" || project == "") {
            return;
        }
        this.projectsService.GetStaffAndPositionsForCopyWindow(project, period)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.sourceStaff.localData = data.staffs;
                        this.dataAdapterStaffListBox_ = new jqx.dataAdapter(this.sourceStaff);
                        this.sourcePositions = data.positions;
                        this.dataAdapterPositions = new jqx.dataAdapter(this.sourcePositions);
                    }
                    else {
                        this.manageError(data);
                    }
                },
                error => {
                    this.manageError(error);
                });
    }

    manageError = (data: any): void => {

        this.glowMessage.ShowGlowByError(data);
    }

    validateTask = (): boolean => {
        let result = true;
        let statusDropDown: number = this.statusDropDown.val();
        let editGenStaff: number = this.editGenStaff.val();
        let positionsDropDown: number = this.positionsDropDown.val();
        let taskName: string = this.editGenTaskName.val();
        let editGenTaskDescription: string = this.editGenTaskDescription.val();
        let hours: number = this.estimatedHours.val();
        let editGenDeadline: string = this.editGenDeadline.val();
        let isStaffSelected = editGenStaff.toString().length > 0 || positionsDropDown.toString().length > 0;
        let project = this.projectDropDown.val();
        let period = this.periodDropDown.val();

        if (!(this.radioButtonCopy.val() || this.radioButtonMove.val())) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_copy_move");
            result = false;
            this.tabsReference.select(0);
        }

        if (period == "" || project == "") {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_project_period");
            this.tabsReference.select(0);
            return false;
        }

        if (taskName.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_type_the_name");
            result = false;
        }

        switch (this.type) {
            case "RQ":
                //TODO:
                break;
            case "GO":
                //TODO:
                break;
            case "RE":
                if (!isStaffSelected) {
                    this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_participant_or_position");
                    result = false;
                }
                if (editGenDeadline.length <= 0) {
                    this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_dealline");
                    result = false;
                }
                break;
        }

        return result;
    }

    OkNext = (event: any): void => {
        this.tabsReference.select(1);
    }

    OkClick = (event: any): void => {
        if (this.validateTask()) {
            let emiter = new EventEmiterTaskModel();
            emiter.Abm = this.radioButtonCopy.val() == true ? "C" : 'M'; // era M
            emiter.Id = this.currentId;
            emiter.TaskName = this.editGenTaskName.val();
            emiter.Hours = this.estimatedHours.val() * 3600;
            emiter.Deadline = this.editGenDeadline.val();
            emiter.type = this.type;
            emiter.IdfPeriod = this.periodDropDown.val();
            emiter.IdfProject = this.projectDropDown.val();
            emiter.Description = this.editGenTaskDescription.val();
            let selPos = this.positionsDropDown.getSelectedItem();

            if (selPos) {
                emiter.IdfAssignableRol = selPos.value;
            }

            let selstatus = this.statusDropDown.getSelectedItem();
            if (selstatus) {
                emiter.IdfStatus = selstatus.value;
                emiter.Status = selstatus.label;
            }

            if (this.type == 'RE') {
                emiter.settingReminderTime = this.settingReminderTime;
            }

            // emiter.IdfStaff = -10;
            emiter.IdfStaff = 0;
            emiter.UserFullName = "Unassigned";

            if (this.editGenStaff.selectedIndex() != -1) {
                emiter.IdfStaff = this.editGenStaff.val();
                let idFind = this.editGenStaff.getSelectedItem().value;
                let staffSelected = this.sourceStaff.localData.filter(x => x.id == idFind)[0];
                emiter.IdUser = staffSelected.idfUser;
                emiter.UserFullName = staffSelected.fullName
            }

            this.onCloneTask.emit(emiter);
            this.windowx.close();
        }
    }

    CancelClick = (event: any): void => {
        this.windowx.close();
    };

    ngOnInit(): void { }

    ngOnDestroy() { }
}