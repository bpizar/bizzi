import { Directive, Component, ViewChild, AfterViewInit, OnInit, OnDestroy, Injectable, EventEmitter, Output, Input, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConstantService } from '../../common/services/app.constant.service';
import { EventEmiterTaskModel } from './edittask_.component.model';
import { Router } from '@angular/router';
import { jqxWindowComponent } from 'jqwidgets-ng/jqwidgets/jqxwindow';
import { jqxNumberInputComponent } from 'jqwidgets-ng/jqwidgets/jqxnumberinput';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqwidgets/jqxdropdownlist';
import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqwidgets/jqxdatetimeinput';
import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { TaskModel } from '../editproject/editproject_.component.model';
import { jqxNotificationComponent } from 'jqwidgets-ng/jqwidgets/jqxnotification';
import { positionsModel } from '../../staff/staff.component.model';
import { settingReminderTimeModel } from '../editproject/editproject_.component.model';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
import { jqxEditorComponent } from 'jqwidgets-ng/jqwidgets/jqxeditor';
import { jqxTabsComponent } from 'jqwidgets-ng/jqwidgets/jqxtabs';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';

@Component({
    selector: 'edittask',
    templateUrl: '../../projects/edittask/edittask_.component.template.html',
    providers: [ConstantService],
    styleUrls: ['./edittask_.component.css'],
})

@Injectable()
export class EditTask implements OnInit, OnDestroy, AfterViewInit {

    @Input('dataAdapterStaffListBox') dataAdapterStaffListBox: any;
    @Input('dataAdapterStatusListBox') dataAdapterStatusListBox: any;
    @Input('sourceStaff') sourceStaff: any;
    @Input('sourceStatus') sourceStatus: any;
    @Input('glowmessage') glowMessage: GlowMessages;
    @Output() onAddTask = new EventEmitter<EventEmiterTaskModel>();
    @ViewChild('jqxWindowComponent') private windowx: jqxWindowComponent;
    // @ViewChild('msgNotificationError') msgNotificationError: jqxNotificationComponent;
    @ViewChild('editGenStaff') editGenStaff: jqxDropDownListComponent;
    @ViewChild('editGenTaskName') editGenTaskName: jqxInputComponent;
    @ViewChild('editGenTaskDescription') editGenTaskDescription: jqxInputComponent;
    @ViewChild('estimatedHours') estimatedHours: jqxNumberInputComponent;
    @ViewChild('positionsDropDown') positionsDropDown: jqxDropDownListComponent;
    @ViewChild('statusDropDown') statusDropDown: jqxDropDownListComponent;
    @ViewChild('editGenDeadline') editGenDeadline: jqxDateTimeInputComponent;
    // @ViewChild('tabsReference3') tabsReference: jqxTabsComponent;
    imagePipe = new ImagePipe();
    dataAdapterClientsListBoxAux: any;
    //tools: string = 'bold italic underline | format font size | color background | left center right | outdent indent | ul ol | image | link | user';  
    //editorNotes: any;  
    PlaceHolderLookingFor: string = "...";
    PlaceHolderParticipant: string;
    PlaceHolderPosition: string;
    PlaceHolderStatus: string;
    PlaceHolderName: string;
    PlaceHolderDescription: string;

    getWidth(): any {
        if (document.body.offsetWidth < 640) {
            return '90%';
        }

        return 640;
    }

    createCommand = (name: string): any => {
        switch (name) {
            case 'user':
                return {
                    type: 'list',
                    tooltip: 'Insert Date/Time',
                    init: (widget: any): void => {
                        widget.jqxDropDownList({ filterable: "true", searchMode: "'containsignorecase'", placeHolder: 'Insert Client', width: 170, source: this.dataAdapterClientsListBoxAux, valueMember: "id", displayMember: "fullName", autoDropDownHeight: true });
                    },
                    refresh: (widget: any, style: any): void => {
                        widget.jqxDropDownList('clearSelection');
                    },
                    action: (widget: any, editor: any): any => {
                        let widgetValue = widget.val();
                        return { command: 'inserthtml', value: '<span style="color: var(--twelfth-color);">@' + this.dataAdapterClientsListBoxAux.originaldata.find(x => x.id == widgetValue).fullName + '</span><span>&nbsp;</span>' };
                        // return { command: 'inserthtml', value: '<span style="color: var(--twelfth-color);">@' + "--" + '</span><span>&nbsp;</span>' };                        
                    }
                }
        }
    };

    hiddenNotes: boolean = true;
    settingReminderTime: settingReminderTimeModel[] = [];
    public isEditing: boolean;
    public currentId: number;
    public currentIdParent: number;
    public notes: string;

    constructor(private translate: TranslateService,
        private constantService: ConstantService,
        private route: ActivatedRoute,
        private router: Router,
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
    type: string = "";

    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    ngAfterViewInit(): void {
        //esta bien
        // this.translate.get('global_lookingfor').subscribe((res: string) => { 
        //     this.PlaceHolderLookingFor = res;
        // });    

        // this.translate.get('projects_select_participant').subscribe((res: string) => { 
        //     this.PlaceHolderParticipant = res;
        //  });    

        //  this.translate.get('projects_select_position').subscribe((res: string) => { 
        //     this.PlaceHolderPosition = res;
        //  });   




        //  this.translate.get('projects_select_status').subscribe((res: string) => { 
        //     this.PlaceHolderStatus = res;
        //  });   

        //  this.translate.get('projects_type_name').subscribe((res: string) => { 
        //     this.PlaceHolderName = res;
        //  });   

        //  this.translate.get('projects_type_description').subscribe((res: string) => { 
        //     this.PlaceHolderDescription = res;
        //  });   
        // esta bien




        // setTimeout(() => {
        //     //this.editGenFrom.val(null);
        //     //this.tabsReference.select(1);
        //     this.tabsReference.selectedItem(0);
        // });
    };

    // myTabsOnSelected(event: any): void {
    //    // this.displayEvent(event);
    //    if(event.args.item==1)
    //    {

    //         //this.initEditor();


    //        // setTimeout(() => {
    //             if(this.editorNotes)
    //             {
    //                 this.editorNotes.val(this.notes);
    //             }   
    //         //});




    //        //this.initEditor();
    //    }
    // };




    // initEditor = () => {

    // if(!this.editorNotes)
    // {

    //     this.editorNotes =  jqwidgets.createInstance('#jqxEditor', 'jqxEditor', {
    //         width: this.getWidth(),
    //         tools:this.tools, 
    //         createCommand: this.createCommand,
    //          height:"'95%'",
    //     });

    //     // this.editorNotes.command.
    //     this.editorNotes.val(this.notes);
    // }

    // }; 

    // initWidgetsx = (tab) => {
    //     switch (tab) {
    //         case 0:            
    //             break;
    //         case 1:
    //             this.initEditor();
    //             break;
    //     }
    // };

    setLanguageChanges = (): void => {
        this.translate.get('global_lookingfor').subscribe((res: string) => {
            this.PlaceHolderLookingFor = res;
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


    show(positions: positionsModel[], type: string, settingReminderTime: settingReminderTimeModel[], canDeleteOrSaveProject: boolean): void {
        this.canDeleteOrSaveProject = canDeleteOrSaveProject;
        this.setLanguageChanges();
        this.clearWindow();
        this.type = type;
        this.currentId = -10;
        this.currentIdParent = 0;
        this.sourcePositions.localData = positions;
        this.dataAdapterPositions = new jqx.dataAdapter(this.sourcePositions);
        this.settingReminderTime = settingReminderTime;
        this.translate.get('projects_requirements').subscribe((resReq: string) => {
            this.translate.get('projects_goals').subscribe((resGoal: string) => {
                this.translate.get('projects_reminders').subscribe((resReminder: string) => {
                    this.windowx.open();
                    let title = type == "RQ" ? resReq : type == "GO" ? resGoal : resReminder;
                    this.translate.get('global_create').subscribe((res: string) => {
                        this.windowx.setTitle(res + " " + title);
                    });
                });
            });
        });
    }

    changeCheckTime = (event: any, item: number): void => { }

    canDeleteOrSaveProject: boolean;

    showEdit(data: TaskModel, positions: positionsModel[], type: string, settingReminderTime: settingReminderTimeModel[], dataAdapterClientsListBoxIn: any, canDeleteOrSaveProject: boolean): void {
        this.canDeleteOrSaveProject = canDeleteOrSaveProject;
        this.setLanguageChanges();
        this.dataAdapterClientsListBoxAux = dataAdapterClientsListBoxIn;
        this.clearWindow();
        this.type = type;
        this.settingReminderTime = settingReminderTime;
        let title = type == "RQ" ? "Requirement" : type == "GO" ? "Goal" : "Reminder";
        this.windowx.setTitle("Edit " + title);
        this.sourcePositions.localData = positions;
        this.dataAdapterPositions = new jqx.dataAdapter(this.sourcePositions);
        this.currentId = data.id;
        this.currentIdParent = data.idfTaskParent;

        setTimeout(x => {
            if (data.idfAssignableRol) {
                let sel = this.positionsDropDown.getItemByValue(data.idfAssignableRol.toString());
                if (sel) {
                    this.positionsDropDown.selectItem(sel);
                }
            }
        });

        setTimeout(x => {
            if (data.idfAssignedTo > 0) {
                let itemsStaff = this.editGenStaff.getItems().filter(x => x.value == data.idfAssignedTo);
                if (itemsStaff != null && data.id != 0) {
                    this.editGenStaff.selectItem(itemsStaff[0])
                }
            }
            else {
                this.editGenStaff.clearSelection();
            }
        });

        if (data.idfStatus > 0) {
            let itemsStatus = this.statusDropDown.getItems().filter(x => x.value == data.idfStatus);
            if (itemsStatus != null && data.id != 0) {
                this.statusDropDown.selectItem(itemsStatus[0])
            }
        }

        if (data.id != 0) {
            this.editGenTaskName.val(data.subject);
            this.estimatedHours.val(data.hours / 3600);
            this.editGenTaskDescription.val(data.description);
        }

        if (data.deadline == null && data.id != 0) {


            this.editGenDeadline.val(null);
        }
        else {
            if (!data.deadline)
            //if(data.deadline == null && data.id!=0)
            {
                this.editGenDeadline.clearString();
            }
            else {
                this.editGenDeadline.setDate(new Date(data.deadline));
            }
        }

        if (type == 'RE') {

        }

        this.notes = data.notes;
        this.windowx.open();
    }

    deleteFrom = (event: any) => {
    }

    deleteDeadLine = (event: any) => {
        this.editGenDeadline.val(null);
    }

    deletePosition = (event: any) => {
        this.positionsDropDown.clearSelection();
    }

    deleteTo = (event: any) => {
    }

    deleteStaff = (event: any) => {
        this.editGenStaff.clearSelection();
    }

    RemoveClick = (event: any): void => {
        this.hiddenNotes = false;
        let emiter = new EventEmiterTaskModel();
        emiter.Id = this.currentId;
        emiter.Abm = "D";
        this.onAddTask.emit(emiter);
        this.windowx.close();
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
        //this.positionsDropDown.clearSelection();
    }

    onPositionsDropDownChange = (event: any) => {
        //this.editGenStaff.clearSelection();
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

        if (taskName.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_type_the_name");
            result = false;
        }

        if (statusDropDown <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_valid_status");
            result = false;
        }

        if (editGenTaskDescription.trim().length <= 0) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_type_the_description");
            result = false;
        }

        switch (this.type) {
            case "RQ":
                // TODO:
                break;
            case "GO":
                // TODO:
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

    OkClick = (event: any): void => {
        this.translate.get('projects_position_window').subscribe((positionText: string) => {
            if (this.validateTask()) {
                let emiter = new EventEmiterTaskModel();
                emiter.Id = this.currentId;
                emiter.IdfTaskParent = this.currentIdParent;
                emiter.TaskName = this.editGenTaskName.val();
                emiter.Hours = this.estimatedHours.val() * 3600;
                emiter.Deadline = this.editGenDeadline.val();
                emiter.type = this.type;
                emiter.Description = this.editGenTaskDescription.val();
                let selPos = this.positionsDropDown.getSelectedItem();

                if (selPos) {
                    emiter.IdfAssignableRol = selPos.value;
                    let idFindx = this.positionsDropDown.getSelectedItem().value;
                    let posSelected = this.sourcePositions.localData.filter(x => x.id == idFindx)[0];
                    emiter.UserFullName = emiter.IdfAssignableRol && emiter.IdfAssignableRol > 0 ? positionText + " : " + posSelected.name : "";
                    emiter.Img = "genericEmploye";
                }

                let selstatus = this.statusDropDown.getSelectedItem();
                if (selstatus) {
                    emiter.IdfStatus = selstatus.value;
                    emiter.Status = selstatus.label;
                }

                if (this.type == 'RE') {
                    emiter.settingReminderTime = this.settingReminderTime;
                }

                emiter.Abm = this.isEditing ? "U" : "I";
                emiter.IdfStaff = -10;

                if (this.editGenStaff.selectedIndex() != -1) {
                    emiter.IdfStaff = this.editGenStaff.val();
                    let idFind = this.editGenStaff.getSelectedItem().value;
                    let staffSelected = this.sourceStaff.localData.filter(x => x.id == idFind)[0];
                    emiter.IdUser = staffSelected.idfUser;
                    emiter.UserFullName = staffSelected.fullName;
                    emiter.AssignedToPosition = staffSelected.positionName;
                    emiter.Img = staffSelected.img;
                }

                this.onAddTask.emit(emiter);
                this.windowx.close();
            }
        });
    }

    CancelClick = (event: any): void => {
        this.windowx.close();
    };

    ngOnInit(): void { }

    ngOnDestroy() { }
}