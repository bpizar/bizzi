import { Directive, Component, ViewChild, AfterViewInit, OnInit, ChangeDetectorRef, ElementRef, Renderer2 } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { SchedulingService } from './scheduling.service';
import { StaffService } from '../staff/staff.service';
import { EditDuplicatesModel } from './scheduling.component.model';
import { jqxListBoxComponent } from 'jqwidgets-ng/jqxlistbox';
import { jqxButtonComponent } from 'jqwidgets-ng/jqxbuttons';
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { jqxTabsComponent } from 'jqwidgets-ng/jqxtabs';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqxdropdownlist';
import { jqxButtonGroupComponent } from 'jqwidgets-ng/jqxbuttongroup';
import { jqxSchedulerComponent } from 'jqwidgets-ng/jqxscheduler';
import { jqxRadioButtonComponent } from 'jqwidgets-ng/jqxradiobutton';
import { jqxCheckBoxComponent } from 'jqwidgets-ng/jqxcheckbox';
import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqxdatetimeinput';
import { jqxNumberInputComponent } from 'jqwidgets-ng/jqxnumberinput';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { AuthHelper } from '../common/helpers/app.auth.helper';
import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';
import { CommonHelper } from '../common/helpers/app.common.helper';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { JqxHelper } from '../common/helpers/app.jqx.helper'
import { jqxComboBoxComponent } from 'jqwidgets-ng/jqxcombobox';
import * as moment from 'moment';
import * as momenttimezone from 'moment-timezone'
import { isFunction } from 'util';
//import { registerLocaleData } from '@angular/common';
//import localeFr from '@angular/common/locales/fr-CA';
import { SidebarModule } from 'primeng/sidebar';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';
import pdfMake from "pdfmake/build/pdfmake";
import pdfFonts from "pdfmake/build/vfs_fonts";
pdfMake.vfs = pdfFonts.pdfMake.vfs;

//registerLocaleData(localeFr, 'fr');
//import {  jqxMenuComponent} from  'jqwidgets-ng/jqxmenu';

@Component({
    selector: 'scheduling',
    templateUrl: '../scheduling/scheduling.component.template.html',
    providers: [SchedulingService, ConstantService, StaffService, AuthHelper, CommonHelper, JqxHelper],
    styleUrls: ['../scheduling/scheduling.component.css'],
})

// @Directive({
//     selector: '[appColor]'
//   })

export class Scheduling implements AfterViewInit, OnInit {

    constructor(private jqxHelper: JqxHelper,
        private translate: TranslateService,
        private schedulingService: SchedulingService,
        private staffService: StaffService,
        private constantService: ConstantService,
        private authHelper: AuthHelper,
        private el: ElementRef,
        private renderer: Renderer2,
        private CommonHelper: CommonHelper,
        private chRef: ChangeDetectorRef) {
        this.translate.setDefaultLang('en');
    }

    // @ViewChild('dummy') dummyMenu: jqxMenuComponent;

    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('buttonWindowDiv') buttonWindowDiv: ElementRef;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('periodsStateDropDown') periodsStateDropDown: jqxListBoxComponent;
    @ViewChild('periodsDropDown') periodsDropDown: jqxListBoxComponent;
    @ViewChild('listBoxStaffFilter') public listBoxStaffFilter: jqxListBoxComponent;
    @ViewChild('listBoxOverTimeFilter') public listBoxOverTimeFilter: jqxListBoxComponent;
    @ViewChild('listBoxOverLapFilter') public listBoxOverLapFilter: jqxListBoxComponent;
    @ViewChild('editAppointmentWindow') editAppointmentWindow: jqxWindowComponent;
    @ViewChild('appointmentWindow') windowAppointment: jqxWindowComponent;
    @ViewChild('periodsWindow') periodsWindow: jqxWindowComponent;
    @ViewChild('buttonTimeSelector') buttonTimeSelector: jqxButtonComponent;
    @ViewChild('schedulerReference') scheduler: jqxSchedulerComponent;
    @ViewChild("schedulerReference") schedulerRef: ElementRef;
    @ViewChild('tabsReference') tabsReference: jqxTabsComponent;
    @ViewChild('tabsEdit') tabsEdit: jqxTabsComponent;
    @ViewChild('calendarModeButtons') calendarModeButtons: jqxButtonGroupComponent;
    //Edit Window General Tab.
    @ViewChild('editGenStaff') editGenStaff: jqxListBoxComponent;
    @ViewChild('editGenDate') editGenDate: jqxDateTimeInputComponent;
    @ViewChild('editGenFrom') editGenFrom: jqxDateTimeInputComponent;
    @ViewChild('editGenTo') editGenTo: jqxDateTimeInputComponent;
    @ViewChild('dateFromPeriod') dateFromPeriod: jqxDateTimeInputComponent;
    @ViewChild('dateToPeriod') dateToPeriod: jqxDateTimeInputComponent;
    @ViewChild('duplicateComboSelection') duplicateComboSelection: jqxDropDownListComponent;
    @ViewChild('duplicateNumberRepeatEvery') duplicateNumberRepeatEvery: jqxNumberInputComponent;
    //duplicate (week)   
    @ViewChild('duplicateCheckDayRepeatOnWeek_Su') duplicateCheckDayRepeatOnWeek_Su: jqxCheckBoxComponent;
    @ViewChild('duplicateCheckDayRepeatOnWeek_Mo') duplicateCheckDayRepeatOnWeek_Mo: jqxCheckBoxComponent;
    @ViewChild('duplicateCheckDayRepeatOnWeek_Tu') duplicateCheckDayRepeatOnWeek_Tu: jqxCheckBoxComponent;
    @ViewChild('duplicateChecDaykRepeatOnWeek_We') duplicateCheckDayRepeatOnWeek_We: jqxCheckBoxComponent;
    @ViewChild('duplicateCheckDayRepeatOnWeek_Th') duplicateCheckDayRepeatOnWeek_Th: jqxCheckBoxComponent;
    @ViewChild('duplicateCheckDayRepeatOnWeek_Fr') duplicateCheckDayRepeatOnWeek_Fr: jqxCheckBoxComponent;
    @ViewChild('duplicateCheckDayRepeatOnWeek_Sa') duplicateCheckDayRepeatOnWeek_Sa: jqxCheckBoxComponent;
    //duplicate(month)
    //@ViewChild('duplicateInputDayRepeatOnMonth') duplicateInputDayRepeatOnMonth: jqxNumberInputComponent;
    //duplicate (End After)    
    @ViewChild('duplicateRadioButtonEndAfter') duplicateRadioButtonEndAfter: jqxRadioButtonComponent;
    @ViewChild('duplicateNumberEndAfter') duplicateNumberEndAfter: jqxNumberInputComponent;
    //dupolicate (End On)
    @ViewChild('duplicateRadioButtonEndOn') duplicateRadioButtonEndOn: jqxRadioButtonComponent;
    @ViewChild('dateTimeEndOn') dateTimeEndOn: jqxDateTimeInputComponent;
    @ViewChild('editAppointmentDate') editAppointmentDate: jqxDateTimeInputComponent;
    @ViewChild('editAppointmentFrom') editAppointmentFrom: jqxDateTimeInputComponent;
    @ViewChild('editAppointmentTo') editAppointmentTo: jqxDateTimeInputComponent;
    @ViewChild('projectsDropDown') projectsDropDown: jqxDropDownListComponent;
    // @ViewChild('projectsDropDown') projectsDropDown: jqxComboBoxComponent;
    @ViewChild('Left') buttonpos: ElementRef;

    imagePipe = new ImagePipe();
    panelRightSelectVisible: boolean = false;
    PlaceHolderLookingFor: string;
    showOverTimeAlert: boolean = false;
    showOverLapAlert: boolean = false;
    loadedControl: boolean[] = [false, true, true];
    serverDateTime: Date;
    timeScaleStart: number = 0.1;
    timeScaleEnd: number = 23.59;
    dayOfWeekStart: number = 0;
    dayOfWeekEnd: number = 6;
    hourWorkStart: number = 0.1;
    hourWorkEnd: number = 23.59;
    isInrolAdmin: boolean = false;
    isInrolSchedulingEditor: boolean = false;
    periodClosed: boolean = false;
    //showingButtonTimeSelector: boolean = false;
    //dateFrom: Date = null;
    //dateTo: Date = null;
    allowListBoxStaffFilter: boolean = true;
    //ISSUE, DONT TAKE EFFECT. (JQWIDGETS AUTOR ERROR)    
    msgError: string = "";
    msgSuccess: string = "";
    endModel: string = 'endAfter';
    schedulerFrom: any;
    schedulerTo: any;
    date: any = new jqx.date('todayDate');
    currentPeriod: number = -1;

    /*EDIT SECTION*/

    windowTitle: string = 'Select edition mode';
    idCurrentAppointment: number = -1;
    editAppointmentWindowImgUrl: string = "";
    editAppointmentWindowFullName: string = "";
    editAppointmentProjectColor: string = "";
    editAppointmentProjectName: string = "";
    typeEdit: string = "";
    editAppointmentSubject: string = "";
    calendarModeSelected: number = 0;
    selectedDuplicateCombo: string = 'N';
    view: string = "weekView";

    views: any =
        [
            { type: "dayView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd }, workTime: { fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart, toHour: this.hourWorkEnd } },
            { type: "weekView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd }, workTime: { fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart, toHour: this.hourWorkEnd } },
            { type: "monthView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd }, workTime: { fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart, toHour: this.hourWorkEnd } },
            // { type: "agendaView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd },workTime:{  fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart,toHour: this.hourWorkEnd } },
            // timelineDayView
            // { type: "timelineDayView", timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd },workTime:{ fromHour: this.hourWorkStart,toHour: this.hourWorkEnd } }
            { type: "timelineDayView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd }, workTime: { fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart, toHour: this.hourWorkEnd } },
        ];

    newScheduleButtonDisabled: boolean = false;

    // SOURCE LISTBOX STAFF

    originalSourceStaffListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'idfUser', type: 'number' },
                { name: 'idfStaff', type: 'number' },
                { name: 'idfProject', type: 'number' },
                { name: 'fullUserName', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'positionName', type: 'string' },
                { name: 'color', type: 'string' },
                { name: 'group', type: 'string' },
                { name: 'idfStaffProjectPosition', type: 'number' },
                { name: 'img', type: 'string' },
                { name: 'hours', type: 'number' },
                { name: 'nav' },
                { name: 'idNavAux', type: 'number' }
            ],
            id: 'idfStaffProjectPosition',
            async: true
        }

    sourceStaffListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'idfUser', type: 'number' },
                { name: 'idfStaff', type: 'number' },
                { name: 'idfProject', type: 'number' },
                { name: 'fullUserName', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'positionName', type: 'string' },
                { name: 'color', type: 'string' },
                { name: 'group', type: 'string' },
                { name: 'idfStaffProjectPosition', type: 'number' },
                { name: 'img', type: 'string' },
                { name: 'hours', type: 'number' },
                { name: 'nav' },
                { name: 'idNavAux', type: 'number' }
            ],
            id: 'idfStaffProjectPosition',
            async: true
        }

    dataAdapterStaffListBox: any = new jqx.dataAdapter(this.sourceStaffListBox);

    selectProjectsDropDown = (event: any): void => {
        // this.sourceStaffListBox.localData = this.originalSourceStaffListBox.localData.filter(c=>c.idfProject == event.args.item.value || event.args.item.value == 0);
        // this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);  
        this.applyStaffFilters(event.args.item.value);
    }

    // Source Overtime

    sourceOverTimeListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'color', type: 'string' },
                { name: 'group', type: 'string' },
                { name: 'hours', type: 'number' },
                { name: 'img', type: 'string' },
                { name: 'positionName', type: 'string' },
                { name: 'projectName', type: 'string' },

                { name: 'nav' },
                { name: 'idNavAux', type: 'number' }
            ],
            id: 'id',
            async: true
        }

    dataAdapterOverTinmeListBox: any;// = new jqx.dataAdapter(this.sourceOverTimeListBox);

    // OVERLAP    

    sourceOverLapListBox: any =
        {
            dataType: "json",
            dataFields: [

                { name: 'color', type: 'string' },
                { name: 'from', type: 'string' },
                { name: 'group', type: 'string' },
                { name: 'id', type: 'number' },
                { name: 'idtask', type: 'number' },
                { name: 'positionName', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'to', type: 'string' },
                { name: 'idNavAux', type: 'number' }
            ],
            id: 'id',
            async: true
        }

    dataAdapterOverLapListBox: any;

    //SOURCE PERIODS
    sourcePeriodsListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'dateJoin', type: 'string' },
                { name: 'from', type: 'date' },
                { name: 'to', type: 'date' },
                { name: 'close', type: 'boolean' },
                { name: 'state', type: 'string' },
            ],
            id: 'idfStaffProjectPosition',
            async: true
        }

    dataAdapterPeriod: any = new jqx.dataAdapter(this.sourcePeriodsListBox);

    //DIRECT SOURCE FOR DUPLICATE COMBO.
    sourceDuplicateCombo: any[] = [{ id: 'N', value: 'None' },
    { id: 'D', value: 'Daily' },
    { id: 'W', value: 'Weekly' },
    { id: 'B', value: 'Biweekly' },
    ];

    // SOURCE FOR TASKS. (Scheduler)

    sourceOriginal: any =
        {
            dataType: "array",
            dataFields: [
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
                { name: 'resizable', type: 'boolean' },
                { name: 'allDay', type: 'boolean' }
            ],
            id: 'id'
        };

    source: any =
        {
            dataType: "array",
            dataFields: [
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
                { name: 'resizable', type: 'boolean' },
                { name: 'allDay', type: 'boolean' },
                { name: 'hours', type: 'number' },
                { name: 'projectColor', type: 'string' },
                { name: 'img', type: 'string' },
            ],
            id: 'id'
        };

    dataAdapter: any;// = new $.jqx.dataAdapter(this.source);

    // APPOINTMENT'S DATAFIELDS
    appointmentDataFields: any =
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
            draggable: "draggable",
            status: "state",
            allDay: "allDay"
            //appointment:"description"
        };

    // RECOURCES FOR SCHEDULER.
    resources: any =
        {
            dataField: "projectName",
            orientation: "vertical",
            source: new jqx.dataAdapter(this.source)
        };

    canAddOrEditSchedule = (): boolean => {
        return (this.isInrolAdmin || this.isInrolSchedulingEditor) && !this.periodClosed;
    }

    canManagePeriods = (): boolean => {
        return this.authHelper.IsInRol("admin"); //&& current period state == active.
    }

    private onDrop(args) {
        let [e, el] = args;
    }

    onAppointmentClick = (event: any): void => {
        // return;

        // var args = event ? event.args : null;
        // if(args && !this.isShowingContextMenu)
        // {


        //     var appointment = args.appointment ;
        //     this.addAppointmentToListSelected(appointment);
        // } 
        // else
        // {
        // }       
    }


    // CLICK ON APPOINTMENT OBJECT
    onAppointmentDoubleClick = (event: any, extra: any = null): void => {
        var args = event ? event.args : null;
        var appointment = args ? args.appointment : extra;
        var isTimeTracking = appointment.status == "tentative";

        this.idCurrentAppointment = appointment.id;
        let record = this.source.localData.filter(x => x.id == appointment.id)[0];

        //this.typeEdit = "C";
        this.typeEdit = record.state == "C" || record.state == "outOfOffice" ? "" : record.state;
        //this.editAppointmentWindowImgUrl = '/media/images/users/' + record.img + '.png';
        //this.editAppointmentWindowFullName = record.assignedToFullName + " - " + record.assignedToPosition;

        //this.editAppointmentWindow.setTitle(isTimeTracking ? "Time Tracking" : "Edit Scheduled");

        this.editAppointmentProjectColor = "#" + record.projectColor;
        this.editAppointmentProjectName = record.projectName;

        if (this.typeEdit == "") {
            this.editAppointmentWindowFullName = record.assignedToFullName + " - " + record.assignedToPosition;
            this.editAppointmentWindowImgUrl = this.imagePipe.transform(record.img, 'users');
            this.editAppointmentWindow.setTitle(isTimeTracking ? "Time Tracking" : "Edit Scheduled");
            // this.editAppointmentWindowImgUrl = '/media/images/users/' + record.img + '.png';
            // this.editAppointmentWindowFullName = record.assignedToFullName + " - " + record.assignedToPosition;

            // this.editAppointmentWindow.setTitle(isTimeTracking ? "Time Tracking" : "Edit Scheduled");

            // this.editAppointmentProjectColor = "#" + record.projectColor;
            // this.editAppointmentProjectName = record.projectName;
            this.editAppointmentWindow.open();
            // this.editAppointmentDate.setDate(new Date(record.from + this.constantService.TIMEZONE));
            // this.editAppointmentFrom.setDate(new Date(record.from + this.constantService.TIMEZONE));
            // this.editAppointmentTo.setDate(new Date(record.to + this.constantService.TIMEZONE));
            this.editAppointmentDate.setDate(new Date(record.from));
            this.editAppointmentFrom.setDate(new Date(record.from));
            this.editAppointmentTo.setDate(new Date(record.to));
            // this.editAppointmentWindow.open();
            let dateAux = this.schedulerFrom.toDate();
            dateAux.setSeconds(0);
            // this.editAppointmentDate.setMinDate(this.schedulerFrom.toDate());
            // sirven o asi eran
            // this.editAppointmentDate.setMinDate(dateAux);
            // this.editAppointmentDate.setMaxDate(this.schedurerTo.toDate()); 

            var recordPeriod = this.sourcePeriodsListBox.localData.find(x => x.id == this.currentPeriod);

            if (recordPeriod) {
                var datef = new Date(recordPeriod.from);
                datef.setSeconds(0);
                // this.editGenDate.setMinDate(datef);        
                // this.editGenDate.setMaxDate(new Date(recordPeriod.to));

                // this.dateTimeEndOn.setMinDate(datef);
                // this.dateTimeEndOn.setMaxDate(new Date(recordPeriod.to));
                // this.editAppointmentDate.setMinDate(datef);
                // this.editAppointmentDate.setMaxDate(new Date(recordPeriod.to));
            }

            let disabledControls: boolean = !this.canAddOrEditSchedule();
            this.editAppointmentDate.disabled(disabledControls || isTimeTracking);
            this.editAppointmentFrom.disabled(disabledControls || isTimeTracking);
            this.editAppointmentTo.disabled(disabledControls || isTimeTracking);
            this.buttonWindowDiv.nativeElement.style.display = disabledControls || isTimeTracking ? 'none' : '';
        }
        else {
            this.editAppointmentWindowFullName = (this.typeEdit == "TT" ? "" : "Client: ") + record.assignedToFullName + " " + (this.typeEdit == "TT" ? " - " + record.assignedToPosition : "");
            this.editAppointmentWindowImgUrl = this.imagePipe.transform(record.img, (this.typeEdit == "TT" ? "users" : "clients"));
            this.editAppointmentWindow.open();
            //this.editAppointmentFrom.disabled(false);
            this.editAppointmentFrom.setDate(new Date(record.from));
            this.editAppointmentWindow.setTitle(this.typeEdit == "TT" ? "Task" : "Medication Schedule");
            this.editAppointmentDate.disabled(true);
            this.editAppointmentFrom.disabled(true);
            this.editAppointmentDate.setDate(new Date(record.from));
            this.editAppointmentSubject = record.subject;
            //this.editAppointmentWindow.open();
        }

        let fromaux = this.schedulerFrom.toDate();
        fromaux.setHours(0, 0, 0, 0);

        this.editAppointmentDate.setMinDate(fromaux);
        this.editAppointmentDate.setMaxDate(this.schedulerTo.toDate());
        // this.typeEdit= record.state== "C" ? "" : record.state   ;
    };

    // CLICK ON NEW APPOINTMENT BUTTON.
    newAppointment = (event: any): void => {
        this.clearEditWindow();
        this.idCurrentAppointment = -1;
        var dateAux = this.schedulerFrom.toDate();
        dateAux.setSeconds(0);
        // this.editGenDate.setMinDate(this.schedulerFrom.toDate());
        var recordPeriod = this.sourcePeriodsListBox.localData.find(x => x.id == this.currentPeriod);

        if (recordPeriod) {
            var datef = new Date(recordPeriod.from);
            datef.setSeconds(0);
            // this.editGenDate.setMinDate(datef);        
            // this.editGenDate.setMaxDate(new Date(recordPeriod.to));

            // this.dateTimeEndOn.setMinDate(datef);
            // this.dateTimeEndOn.setMaxDate(new Date(recordPeriod.to));
        }


        // this.dateTimeEndOn.setMinDate(this.schedulerFrom.toDate());
        // this.dateTimeEndOn.setMaxDate(this.schedurerTo.toDate());

        let selection: any = this.scheduler.getSelection();


        // if (selection != null) {
        //     this.editGenDate.setDate(selection.from.toDate());
        //     this.editGenFrom.setDate(selection.from.toDate());

        //     if (selection.to.toDate().getDate() > selection.from.toDate().getDate()) {
        //         var selAux = selection.from.toDate();
        //         this.editGenTo.setDate(new Date(selAux.getFullYear(), selAux.getMonth() + 1, selAux.getDate(), 23, 59, 0, 0));
        //         //this.msgSuccess = "The selection can not contain more than one date, your date selected was changed";
        //         this.glowMessage.ShowGlow("warn", "glow_info", "glow_scheduling_more_than_one_date");       

        //         this.closeWindow(null);
        //     }
        //     else {

        //         let self1 = selection.from.toDate();
        //         let self2 = selection.to.toDate();

        //         let selPeriod = this.periodsDropDown.getSelectedItem();
        //         let regPeriod = this.sourcePeriodsListBox.localData.filter(x => x.id == selPeriod.value)[0];
        //         let fperiodFrom = new Date(regPeriod.from);
        //         let fperiodTo = new Date(regPeriod.to);

        //         if ((self1 < fperiodFrom && self1 < fperiodTo) || (self1 > fperiodFrom && self1 > fperiodTo) ||
        //             (self2 < fperiodFrom && self2 < fperiodTo) || (self2 > fperiodFrom && self2 > fperiodTo)) {               
        //             this.glowMessage.ShowGlow("warn", "glow_info", "glow_scheduling_date_not_betten_ranges");     
        //             this.editGenTo.setDate(new Date(fperiodFrom.getFullYear(), fperiodFrom.getMonth() + 1, fperiodFrom.getDate(), 0, 0, 1, 0));
        //         }
        //         else {
        //             this.editGenTo.setDate(selection.to.toDate());
        //         }
        //     }
        // }

        // dont allow selection with more than 24 hours
        if (selection != null) {
            let diff = selection.to.toDate().valueOf() - selection.from.toDate().valueOf();
            let selectedHours = diff / (1000 * 3600);
            if (selectedHours >= 24) {
                this.glowMessage.ShowGlow("warn", "glow_info", "glow_scheduling_not_more_than_24");
                return;
            }

            //this.editGenDate.setDate(selection.from.toDate());
            this.editGenFrom.setDate(selection.from.toDate());
            this.editGenTo.setDate(selection.to.toDate());
        }

        this.windowAppointment.open();

        this.translate.get('scheduling_newscheduled').subscribe((res: string) => { this.windowAppointment.setTitle(res); });

        let auxfrom = this.schedulerFrom.toDate();
        auxfrom.setHours(0, 0, 0, 0);

        this.editGenDate.setMinDate(auxfrom);
        this.editGenDate.setMaxDate(this.schedulerTo.toDate());

        //  this.dateTimeEndOn
        this.dateTimeEndOn.setMinDate(auxfrom);
        this.dateTimeEndOn.setMaxDate(this.schedulerTo.toDate());
    };

    getAndValidateDuplicateScheduledValue = (): EditDuplicatesModel => {
        let model = new EditDuplicatesModel();
        model.id = -1;
        model.duplicateValue = this.sourceDuplicateCombo.filter(x => x.value == this.duplicateComboSelection.getSelectedItem().value)[0].id;
        model.repeatEvery = 0;
        model.endAfter = 0;

        if (model.duplicateValue != 'N') {
            switch (model.duplicateValue) {
                case "D":
                    break;
                case "W":
                    model.weekly_Fr = this.duplicateCheckDayRepeatOnWeek_Su.checked() ? 1 : 0;
                    model.weekly_Mo = this.duplicateCheckDayRepeatOnWeek_Mo.checked() ? 1 : 0;
                    model.weekly_Tu = this.duplicateCheckDayRepeatOnWeek_Tu.checked() ? 1 : 0;
                    model.weekly_We = this.duplicateCheckDayRepeatOnWeek_We.checked() ? 1 : 0;
                    model.weekly_Th = this.duplicateCheckDayRepeatOnWeek_Th.checked() ? 1 : 0;
                    model.weekly_Fr = this.duplicateCheckDayRepeatOnWeek_Fr.checked() ? 1 : 0;
                    model.weekly_Sa = this.duplicateCheckDayRepeatOnWeek_Sa.checked() ? 1 : 0;
                    break;
                case "B":
                    model.weekly_Fr = this.duplicateCheckDayRepeatOnWeek_Su.checked() ? 1 : 0;
                    model.weekly_Mo = this.duplicateCheckDayRepeatOnWeek_Mo.checked() ? 1 : 0;
                    model.weekly_Tu = this.duplicateCheckDayRepeatOnWeek_Tu.checked() ? 1 : 0;
                    model.weekly_We = this.duplicateCheckDayRepeatOnWeek_We.checked() ? 1 : 0;
                    model.weekly_Th = this.duplicateCheckDayRepeatOnWeek_Th.checked() ? 1 : 0;
                    model.weekly_Fr = this.duplicateCheckDayRepeatOnWeek_Fr.checked() ? 1 : 0;
                    model.weekly_Sa = this.duplicateCheckDayRepeatOnWeek_Sa.checked() ? 1 : 0;
                    break;
            }

            model.repeatEvery = this.duplicateNumberRepeatEvery.val();
            if (model.duplicateValue == 'B') {
                model.repeatEvery = 2;
                model.duplicateValue = 'W';
            }
            model.endAfter = this.duplicateRadioButtonEndAfter.checked() ? this.duplicateNumberEndAfter.val() : 0;
            if (this.duplicateRadioButtonEndOn.checked()) {
                let dateEndOnAux = new Date(this.dateTimeEndOn.getDate());
                dateEndOnAux.setSeconds(1);
                //model.endOn = new Date(this.dateTimeEndOn.getDate());
                model.endOn = dateEndOnAux;
            }
        }
        else {
            return null;
        }
        return model;
    };

    validateAppointment = (): boolean => {
        let result = true;
        var recordPeriod = this.sourcePeriodsListBox.localData.find(x => x.id == this.currentPeriod);

        if (recordPeriod) {
            var datef = new Date(recordPeriod.from);
            datef.setSeconds(0);
            datef.setMinutes(0);
            datef.setHours(0);

            var datet = new Date(recordPeriod.to);
            datet.setSeconds(59);
            datet.setMinutes(59);
            datet.setHours(23);

            let date1: Date = this.editGenDate.getDate();
            let time1: Date = this.editGenFrom.getDate();
            let time2: Date = this.editGenTo.getDate();
            let time3: Date = this.dateTimeEndOn.getDate();
            let checkedItemsStaff = this.editGenStaff.getCheckedItems();



            if (checkedItemsStaff.length == 0) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_at_least_one_Staff");
                result = false;
            }

            if (time1 == null) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_select_valid_time_from");
                result = false;
            }
            // else {
            //     if (time1.getTime() >= time2.getTime()) {

            //         this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_time_from_must_smaller_than_to");                
            //         result = false;
            //     }
            // }

            if (time2 == null) {

                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_select_valid_time_to");
                result = false;
            }

            if (this.duplicateRadioButtonEndOn.checked()) {
                //time3.setSeconds(0);
                //time3.setMinutes(0);
                //time3.setHours(0);
                if (date1.getTime() >= time3.getTime()) {
                    //if (time1.getTime() >= time3.getTime()) {
                    this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_time_from_must_smaller_than_date");
                    result = false;
                }
            }

            // if (date1.getDate() < datef.getDate() || date1.getDate() > datet.getDate()) {

            if (date1 < datef || date1 > datet) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_date_must_between_range");
                result = false;
            }
        }
        else {
            result = false;
        }
        return result;
    }

    getTimeZone = (): number => {
        let aux: any = momenttimezone.tz(Intl.DateTimeFormat().resolvedOptions().timeZone);
        return Number(aux._offset / 60);
    }

    // CLICK ON SAVE APPOINTMENT BUTTON.
    SaveAppointment(event: any): void {
        if (this.validateAppointment()) {
            this.myLoader.open();

            //this.loadedControl=[true,false,false];
            this.loadedControl = [true, true, false];

            let duplicates = this.getAndValidateDuplicateScheduledValue();
            let checkedItemsStaff = this.editGenStaff.getCheckedItems();
            let chedkedItemsSaffs: number[] = [];
            for (var i = 0; i < checkedItemsStaff.length; i++) {
                chedkedItemsSaffs.push(checkedItemsStaff[i].value);
            }

            let date1: Date = this.editGenDate.getDate();
            let time1: Date = this.editGenFrom.getDate();
            let time2: Date = this.editGenTo.getDate();

            //var offset =  Intl.DateTimeFormat(); // new Date().getTimezoneOffset();
            var offset = this.getTimeZone();
            //alert(offset);

            //return;

            let Request = {
                Period: this.currentPeriod,
                StaffsIds: chedkedItemsSaffs,
                //Date: new Date(date1.getFullYear(),date1.getMonth(),date1.getDate(),0,0,0),
                Date: date1,
                Time1: time1,
                Time2: time2,
                DuplicateScheduling: duplicates,
                TimeDifference: offset
            };
            //return ;
            this.windowAppointment.close();

            this.schedulingService.SaveScheduling(Request)
                .subscribe(
                    (data: any) => {
                        if (data.result) {
                            //this.windowAppointment.close();
                            //this.myLoader.close();
                            this.glowMessage.ShowGlow("success", "glow_success", "glow_scheduling_saved_succesfully");
                            //this.getStaff();
                            this.getSchedules(this.currentPeriod);
                        }
                        else {
                            //this.myLoader.close();
                            this.loadedControl = [true, true, true];
                            this.HiddeLoaderWhenEnd();
                            this.manageError(data);
                            this.windowAppointment.open();
                        }


                        //this.loadedControl[2] = true;
                        //this.HiddeLoaderWhenEnd();
                    },
                    error => {
                        //this.myLoader.close();
                        this.loadedControl = [true, true, true];
                        this.HiddeLoaderWhenEnd();
                        this.manageError(error);
                        this.windowAppointment.open();
                    });
        }
    }

    clearEditWindow = (): void => {
        //General Settings
        this.editGenStaff.clearSelection();
        this.duplicateNumberEndAfter.val("1");
        this.duplicateNumberRepeatEvery.val("1");
        this.duplicateComboSelection.selectedIndex(0);
        //this.duplicateInputDayRepeatOnMonth.val('1');
        this.duplicateCheckDayRepeatOnWeek_Su.uncheck();
        this.duplicateCheckDayRepeatOnWeek_Mo.uncheck();
        this.duplicateCheckDayRepeatOnWeek_Tu.uncheck();
        this.duplicateCheckDayRepeatOnWeek_We.uncheck();
        this.duplicateCheckDayRepeatOnWeek_Th.uncheck();
        this.duplicateCheckDayRepeatOnWeek_Fr.uncheck();
        this.duplicateCheckDayRepeatOnWeek_Sa.uncheck();
        this.duplicateRadioButtonEndAfter.uncheck();
        this.duplicateRadioButtonEndOn.uncheck();
    };

    filter1 = (): void => {
        this.sourceStaffListBox.localData = this.originalSourceStaffListBox.localData.filter(c => c.idfProject == 13);
        this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);
        // setTimeout(() => {
        //     this.listBoxStaffFilter.render();   
        // });
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

    clearFilter = (): void => {
        this.sourceStaffListBox.localData = this.originalSourceStaffListBox.localData; //  data.staffs;
        this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);

        setTimeout(() => {
            //this.listBoxStaffFilter.render();   
        });
        // this.sourceStaffListBox.localData = this.staffs;
        // this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);
    }

    // projectsChange = (event: any) => {


    // };


    // projectsBindingComplete = (event: any) => {
    //     // if (this.sourceProjectsListBox.localData != undefined) {
    //     //     if (this.sourceProjectsListBox.localData.length > 0) {
    //     //         this.projectsDropDown.selectedIndex(0);
    //     //     }
    //     // }
    //     // setTimeout(() => {
    //     //     this.projectsDropDown.renderer()   
    //     // });
    // }


    getSchedules = (period: number): void => {
        // this.myLoader.open();

        this.schedulingService.GetScheduling(period)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        if (this.countTimesLoaded < 0) {
                            this.sourceProjectsListBox.localData = data.projects;
                            this.dataAdapterProjects = new jqx.dataAdapter(this.sourceProjectsListBox);
                        }
                        this.sourceOriginal.localData = data.scheduling;
                        // this.source.localData = data.scheduling;
                        // this.dataAdapter = new jqx.dataAdapter(this.source);
                        this.originalSourceStaffListBox.localData = data.staffs;
                        // this.sourceStaffListBox.localData = data.staffs;
                        // this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);

                        setTimeout(() => {
                            //this.applyStaffFilters(this.projectsDropDown.getSelectedItem().value);    
                            if (this.countTimesLoaded < 0) {
                                this.projectsDropDown.selectedIndex(0);
                            }
                            else {
                                this.applyStaffFilters(this.projectsDropDown.getSelectedItem().value);
                                this.loadedControl = [true, true, true];
                                this.HiddeLoaderWhenEnd();
                            }

                            if (!this.date) {
                                this.date = new jqx.date(data.currentDateTime);
                            }
                        });

                        //     setTimeout(() => {
                        //         // this.sourceStaffListBox.localData = this.originalSourceStaffListBox.localData.filter(c=>c.idfProject == 13) ; //  data.staffs;
                        //         this.sourceStaffListBox.localData = this.originalSourceStaffListBox.localData; //  data.staffs;
                        //         this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);   

                        //         setTimeout(() => {
                        //               this.projectsDropDown.selectedIndex(0);   
                        //         });
                        //   });

                        this.sourceOverTimeListBox.localData = data.overTime;
                        this.dataAdapterOverTinmeListBox = new jqx.dataAdapter(this.sourceOverTimeListBox);

                        this.sourceOverLapListBox.localData = data.overLap;
                        this.dataAdapterOverLapListBox = new jqx.dataAdapter(this.sourceOverLapListBox);

                        this.showOverTimeAlert = data.overTime.length > 0;
                        this.showOverLapAlert = data.overLap.length > 0;

                        if (this.showOverTimeAlert) {
                            this.tabsReference.enableAt(1);
                        }
                        else {
                            this.tabsReference.disableAt(1);
                        }

                        if (this.showOverLapAlert) {
                            this.tabsReference.enableAt(2);
                        }

                        else {
                            this.tabsReference.disableAt(2);
                        }

                        //if(this.showOverTimeAlert)

                        this.tabsReference.select(0);

                        // setTimeout(() => {
                        //     if (this.source.localData != null) {
                        //         if (this.source.localData.length > 0) {                                                              
                        //             this.scheduler.ensureAppointmentVisible(this.source.localData[0].id.toString());

                        //         }
                        //     }

                        //     this.loadedControl[2] = true;
                        //     this.HiddeLoaderWhenEnd();
                        // });
                    }
                    else {
                        this.manageError(data);
                    }

                    //this.myLoader.close();
                },
                error => {
                    this.myLoader.close();
                    this.manageError(error);
                });
    }

    countTimesLoaded: number = -1;

    applyStaffFilters = (idProject: number): void => {

        this.countTimesLoaded++;

        //  originalSourceStaffListBox
        setTimeout(() => {
            // this.sourceStaffListBox.localData = this.originalSourceStaffListBox.localData.filter(c=>c.idfProject == 13) ; //  data.staffs;
            //   this.sourceStaffListBox.localData = this.originalSourceStaffListBox.localData; //  data.staffs;
            //   this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);   

            //if(this.countTimesLoaded>0)
            //{
            this.sourceStaffListBox.localData = this.originalSourceStaffListBox.localData.filter(c => c.idfProject == idProject || idProject == 0);
            //this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);   
            //}
            //else
            // {
            //  this.sourceStaffListBox.localData = this.originalSourceStaffListBox.localData;                  
            // }
            this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);

            this.source.localData = this.sourceOriginal.localData.filter(c => c.idfProject == idProject || idProject == 0);
            this.dataAdapter = new jqx.dataAdapter(this.source);

            setTimeout(() => {
                // if (this.source.localData != null) {
                //     if (this.source.localData.length > 0) {                                                              
                //         this.scheduler.ensureAppointmentVisible(this.source.localData[0].id.toString());

                //     }
                // }

                this.loadedControl[2] = true;
                this.HiddeLoaderWhenEnd();
            });

        });

    }

    // INIT EVENT
    ngOnInit(): void {
        // let elements = this.el.nativeElement.querySelectorAll('jqx-cell');

        //this.renderer.setStyle(this.schedulerRef., 'color', 'green');
    }

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


    HiddeLoaderWhenEnd = (): void => {
        let control = true;
        this.loadedControl.forEach(element => {
            control = control && element;
        });
        if (control) {
            this.myLoader.close();
        }
    }

    setLanguageEspecialControls = (): void => {
        this.translate.get('scheduling_schedule_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(0, res); });
        this.translate.get('scheduling_overtime_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(1, res); });
        this.translate.get('scheduling_overlap_tab').subscribe((res: string) => { this.tabsReference.setTitleAt(2, res); });

        this.translate.get('global_lookingfor').subscribe((res: string) => {
            this.PlaceHolderLookingFor = res;
        });

        this.translate.get('global_positions').subscribe((res: string) => { let myContainer = document.getElementById('Left') as HTMLInputElement; myContainer.append(res); });
        this.translate.get('menu_projects').subscribe((res: string) => { let myContainer = document.getElementById('Center') as HTMLInputElement; myContainer.append(res); });
        this.translate.get('global_users').subscribe((res: string) => { let myContainer = document.getElementById('Right') as HTMLInputElement; myContainer.append(res); });
    }

    // AFTER INIT EVENT
    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');

            this.setLanguageEspecialControls();
            this.myLoader.open();

            this.isInrolAdmin = this.authHelper.IsInRol("admin");
            this.isInrolSchedulingEditor = this.authHelper.IsInRol("schedulingeditor");
            this.calendarModeButtons.setSelection(1);
            this.newScheduleButtonDisabled = true;

            this.schedulingService.GetPeriods()
                .subscribe(
                    (data: any) => {
                        if (data.result) {
                            this.serverDateTime = new Date(data.currentDateTime);
                            this.sourcePeriodsListBox.localData = data.periodsList;
                            this.dataAdapterPeriod = new jqx.dataAdapter(this.sourcePeriodsListBox);
                            this.loadedControl[0] = true;
                            this.HiddeLoaderWhenEnd();
                        }
                        else {
                            this.manageError(data);
                        }
                    },
                    error => {
                        this.myLoader.close();
                        this.manageError(error);
                    }
                );
            //this.panelRightSelectVisible =true;
        });
    }

    PeriodSelectDrowDown = (event: any): void => {
        if (this.currentPeriod == event.args.item.value) {
            return;
        }
        this.loadedControl = [true, true, false];
        this.listBoxStaffFilter.clearSelection();
        this.myLoader.open();
        this.newScheduleButtonDisabled = false;
        this.currentPeriod = event.args.item.value;
        this.schedulerFrom = new jqx.date(new Date(this.sourcePeriodsListBox.localData[event.args.index].from));
        this.schedulerTo = new jqx.date(new Date(this.sourcePeriodsListBox.localData[event.args.index].to));
        // no borrar esta linea.
        //this.date = new jqx.date(this.schedulerFrom.toDate()); //new $.jqx.date(2017, 1, 25);       
        this.periodClosed = this.sourcePeriodsListBox.localData[event.args.index].state == "CL";
        this.chRef.detectChanges();
        this.getSchedules(event.args.item.value);
        //  let found = false;
        //        if (this.sourcePeriodsListBox.localData != undefined) {
        //          if (this.sourcePeriodsListBox.localData.length > 0) {
        //let i = 0;
        // this.sourcePeriodsListBox.localData.forEach(element => {
        //     let dtf = new Date(element.from);
        //     let dtt = new Date(element.to);

        //     if(this.serverDateTime > dtf && this.serverDateTime < dtt)
        //     {                       
        //         //this.periodsDropDown.selectedIndex(i);                        
        //         this.date = this.serverDateTime;
        //         found= true;
        //     } 
        //     i++;
        // });
        if (this.serverDateTime > this.schedulerFrom.toDate() && this.serverDateTime < this.schedulerTo.toDate()) {
            this.date = this.serverDateTime
            //            found=true;
        }
        else {
            this.date = this.schedulerFrom.toDate();
        }
        //        }
        //    }
    }

    //ON SCHEDULER VIEWCHANGE
    onViewChange = (event: any): void => {
        this.view = event.args.newViewType;
        this.renderSchedulerByDates();
    };
    // renderingScheduler=():void=>
    // {
    // }
    showOutOfRange: boolean = false;

    renderSchedulerByDates = (): void => {
        this.showOutOfRange = false;
        if (!this.schedulerFrom || !this.schedulerTo) {
            return;
        }
        let elements = this.el.nativeElement.querySelectorAll('.jqx-cell');
        let t_df = this.schedulerFrom.toDate();
        t_df.setHours(0, 0, 0, 0);
        let t_dt = this.schedulerTo.toDate();
        t_dt.setHours(23, 59, 59, 59); // 23:59:59 ??
        [].forEach.call(elements, (div: any) => {
            var attributeDate = div.getAttribute('data-date');
            if (attributeDate) {
                let dc = new Date(attributeDate);
                if (dc < t_df || dc > t_dt) {
                    this.showOutOfRange = true;
                    div.style.background = "#e5e5e5";
                }
            }
        });
    }

    // CHANGE DATE SCHEDULER EVENT
    onDateChange = (event: any): void => {
        //lert("onDateChange");
        this.renderSchedulerByDates();
    };

    rendererListBoxStaffWindow = (index, label, value): string => {
        if (this.sourceStaffListBox.localData == undefined) {
            return null;
        }
        var datarecord = this.sourceStaffListBox.localData[index];
        if (datarecord != undefined) {
            var imgurl = this.imagePipe.transform(datarecord.img, 'images');
            var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';
            //var table = '<table style="width: 400px  !important;"><tr><td style="background-color:red; width:40px !important; width-max:40px !important;" rowspan= "2">' + img + '</td><td style="background-color:blue; width:270px !important; color:rgb(51, 51, 51); font-size:12px;"> ' + datarecord.fullUserName + '</td><td style="background-color:pink; width:70px; color:black; font-size:12px;">' + datarecord.positionName + '</td></tr></table>';
            var table = '<ul class="ultable"><li style="width:40px;">' + img + '</li><li style="width:270px;">' + datarecord.fullUserName + '</li><li style="width:70px;">' + datarecord.positionName + '</li></ul>';
            return table;
        }
        else {
            if (label != undefined) {
                var values = label.split("|");
                var divSquareProject = "<span style=' border:1px solid var(--thirteenth-color); height:15px; width:15px !important; background-color:" + values[0] + ";'>&nbsp;&nbsp;</span>";
                return divSquareProject + values[1];
            }
            else {
                return label;
            }
        }
    };

    //RENDER LISTBOX STAFF
    rendererListBoxStaff = (index, label, value, source): string => {
        if (source.localData == undefined) {
            return null;
        }
        var datarecord = source.localData[index];

        if (datarecord != undefined) {
            var imgurl = this.imagePipe.transform(datarecord.img, 'users');
            var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';
            var table = '<table style="min-width: 120px;"><tr><td style="width: 30px;" rowspan="2">' + img + '</td><td style="color:rgb(51, 51, 51); font-size:12px;">' + datarecord.fullUserName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + this.CommonHelper.convertMinsToHrsMins(datarecord.hours) + " " + datarecord.positionName + '</td></tr></table>';
            return table;
        }
        else {
            if (label != undefined) {
                var values = label.split("|");
                var divSquareProject = "<span style=' border:1px solid var(--thirteenth-color); height:15px; width:15px !important; background-color:" + values[0] + ";'>&nbsp;&nbsp;</span>";

                return divSquareProject + values[1];
            }
            else {
                return label;
            }
        }
    };

    //RENDER LISTBOX STAFF WITHOUT FILTER
    rendererListBoxAll = (index, label, value): string => {
        return this.rendererListBoxStaff(index, label, value, this.sourceStaffListBox);
    };


    hideMessageFilter = (): boolean => {
        if (this.projectsDropDown != undefined) {
            // this.hideMessageFilter = this.projectsDropDown.getSelectedIndex() > 0;
            return !(this.projectsDropDown.getSelectedIndex() > 0 && this.tabsReference.selectedItem() > 0);
        }
        return true;
    }

    initTabSchedule = (tab: any): void => {
        switch (tab) {
            case 0:
                this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);
                break;
            case 1:
                this.dataAdapterOverTinmeListBox = new jqx.dataAdapter(this.sourceOverTimeListBox);
                this.listBoxOverTimeFilter.render();

                break;
            case 2:
                this.dataAdapterOverLapListBox = new jqx.dataAdapter(this.sourceOverLapListBox);
                this.listBoxOverLapFilter.render();
                break;
        }
    }

    clickSheduleListBox = ($event: any): void => {
        let item = this.listBoxStaffFilter.getSelectedItem();
        if (item) {
            let oitem = item.originalItem;

            if (oitem.nav && oitem.nav.length > 0) {
                oitem.idNavAux = (oitem.idNavAux + 1) >= oitem.nav.length ? 0 : oitem.idNavAux + 1;
                this.scheduler.ensureAppointmentVisible(oitem.nav[oitem.idNavAux]);
                this.scheduler.selectAppointment(oitem.nav[oitem.idNavAux]);
            }
        }
    }

    clickOverTimeListBox = ($event: any): void => {
        let item = this.listBoxOverTimeFilter.getSelectedItem();
        if (item) {
            let oitem = item.originalItem;
            if (oitem.nav && oitem.nav.length > 0) {
                oitem.idNavAux = (oitem.idNavAux + 1) >= oitem.nav.length ? 0 : oitem.idNavAux + 1;
                this.scheduler.ensureAppointmentVisible(oitem.nav[oitem.idNavAux]);
                this.scheduler.selectAppointment(oitem.nav[oitem.idNavAux]);
            }
        }
    }

    clickOverLapListBox = ($event: any): void => {
        let item = this.listBoxOverLapFilter.getSelectedItem();
        if (item) {
            let oitem = item.originalItem;
            this.scheduler.ensureAppointmentVisible(oitem.idNavAux);
            this.scheduler.selectAppointment(oitem.idNavAux);
        }
    }

    rendererScheduleListBox = (index, label, value): string => {
        if (this.sourceStaffListBox.localData == undefined) {
            return null;
        }

        var datarecord = this.sourceStaffListBox.localData[index];
        //var redColor = "#ff9999";
        var redColor = "#f5877f";
        var greenColor = "#99cc99";
        // var grayColor0 ="#f2f2f2";
        // var grayColor ="#d9d9d9";
        var grayColor0 = "var(--first-color)";
        var grayColor = "color:rgb(51, 51, 51);";

        if (datarecord != undefined) {
            let iconWarning = '<i class="icon ion-ios-warning" style="font-size:18px; color:' + grayColor0 + ';"></i>';
            let icon1 = datarecord.hours == 0 ? iconWarning : '<i class="icon ion-ios-time" style="color:' + grayColor0 + '; font-size:16px; width:20px; float:left;"></i>';
            // not erase
            //let icon2 = datarecord.hours == 0 ? "" : '<i class="icon ion-ios-time" style="color:' +  (datarecord.hoursFree > 0 ? greenColor : redColor) + '; font-size:16px; width:20px; float:left;"></i>';
            let icon2 = "";
            var imgurl = this.imagePipe.transform(datarecord.img, 'users');
            var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';
            // not erase
            //var table = '<table border=0  min-width: 120px; width:325px;">  <td style="border-right:4px solid #' + datarecord.color + '; width: 30px;" rowspan="2" >  </i>  </td>    <td style="width: 30px;" rowspan="2">' + img + '</td>      <td style="color:rgb(51, 51, 51); font-size:12px;  width:180px; " class="dots">' + datarecord.fullUserName + '</td>           <td rowspan="2">     <table  border=0 style="width:80px; font-size:12px; color:rgb(51, 51, 51);"> <tr> <td style="width:20px;">' + icon1 + '</td> <td style="width:80px; color:'+ grayColor+  ';">' + (datarecord.hours == 0  ? "" : this.CommonHelper.convertMinsToHrsMins(datarecord.hours)) + '</td>  <tr> <td>' + icon2 +'</td> <td style="font-size:10px; color:' + grayColor + '">' + (datarecord.hours == 0  ? "" : this.CommonHelper.convertMinsToHrsMins(datarecord.hoursFree))  +  '</td></tr> </tr>    </table>             </td>            </tr>        <tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + " " + datarecord.positionName + '</td>       </tr>     </table>';
            var table = '<table border=0  min-width: 120px; width:200px;">  <td style="border-right:4px solid #' + datarecord.color + '; width: 30px;" rowspan="2" >  </i>  </td>    <td style="width: 30px;" rowspan="2">' + img + '</td>      <td style="color:rgb(51, 51, 51); font-size:12px;  width:140px; max-width:140px;" class="dots">' + datarecord.fullUserName + '</td>           <td rowspan="2">     <table  border=0 style="width:80px; font-size:12px; color:rgb(51, 51, 51);"> <tr> <td style="width:20px;">' + icon1 + '</td> <td style="width:80px; color:' + grayColor + ';">' + (datarecord.hours == 0 ? "" : this.CommonHelper.convertMinsToHrsMins(datarecord.hours)) + '</td>  <tr> <td>' + icon2 + '</td> <td style="font-size:10px; color:' + grayColor + '">' + "" + '</td></tr> </tr>    </table>             </td>            </tr>        <tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + " " + datarecord.positionName + '</td>       </tr>     </table>';
            return table;
        }
        else {
            if (label != undefined) {
                var values = label.split("|");
                return values[1];
            }
            else {
                return label;
            }
        }
    }

    onSelectedScheduledTab = (event: any): void => {
        let selectedTab = event.args.item;
        switch (selectedTab) {
            case 0:
                //this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);   
                this.listBoxStaffFilter.render();
                break;
            case 1:
                //this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);
                this.listBoxOverTimeFilter.render();
                break;
            case 1:
                //this.dataAdapterClientsListBox = new jqx.dataAdapter(this.sourceClients);
                this.listBoxOverLapFilter.render();
                break;
        }
    }

    rendererOverTimeListBox = (index, label, value): string => {
        if (this.sourceOverTimeListBox.localData == undefined) {
            return null;
        }

        var datarecord = this.sourceOverTimeListBox.localData[index];
        // var redColor = "#ff9999";
        var redColor = "#ff3232";
        var greenColor = "#99cc99";
        var grayColor0 = "var(--second-color)";
        var grayColor = "#d9d9d9";
        var blackColor = "black";

        if (datarecord != undefined) {
            let iconWarning = '<i class="icon ion-ios-warning" style="font-size:18px; color:' + grayColor0 + ';"></i>';
            let icon1 = datarecord.hours == 0 ? iconWarning : '<i class="icon ion-ios-time" style="color:' + grayColor + '; font-size:16px; width:20px; float:left;"></i>';
            let icon2 = datarecord.hours == 0 ? "" : '<i class="icon ion-ios-time" style="color:' + (datarecord.hoursFree == 0 ? grayColor : datarecord.hoursFree < 0 ? redColor : greenColor) + '; font-size:16px; width:20px; float:left;"></i>';

            var imgurl = this.imagePipe.transform(datarecord.img, 'users');
            var img = "";
            var table = '<table border=0 style="min-width: 120px; width:325px;">     <td style="width: 30px;  border-right:4px solid #' + datarecord.color + '; "  rowspan="2">' + img + '</td>      <td style="color:rgb(51, 51, 51); font-size:12px;  width:240px; ">' + datarecord.projectName + '</td>           <td rowspan="2">     <table  border=0 style="width:70px; font-size:12px; color:rgb(51, 51, 51);"> <tr> <td style="width:20px;">' + icon1 + '</td> <td style="width:70px; color:' + grayColor + ';">' + (datarecord.hours == 0 ? "" : this.CommonHelper.convertMinsToHrsMins(datarecord.hours)) + '</td>  <tr> <td>' + icon2 + '</td> <td style="font-size:12px; color:' + grayColor + '">' + (datarecord.hours == 0 ? "" : this.CommonHelper.convertMinsToHrsMins(datarecord.hoursFree)) + '</td></tr> </tr>    </table>             </td>            </tr>        <tr><td style="color:rgb(51, 51, 51); font-size:12px;">' + " " + datarecord.positionName + '</td>       </tr>     </table>';
            return table;
        }
        else {
            if (label != undefined) {
                var values = label.split("|");
                var imgX = '<img style="border-radius:50%;" height="30" width="30" src="' + this.imagePipe.transform(values[1], 'users') + '"/>';

                var fullNameX = values[0];
                var maxHours: number = Number(values[2]);
                var currrentHours: number = Number(values[3]);
                let icon1 = '<i class="icon ion-ios-time" style="color:' + grayColor + '; font-size:16px; width:15px; float:left;"></i>';
                let icon2 = '<i class="icon ion-ios-time" style="color:' + (currrentHours < maxHours ? grayColor : redColor) + '; font-size:16px; width:20px; float:left;"></i>';
                var table = '<table border=0  style=" min-width: 120px; width:100%;">  <td style="width: 30px;" rowspan="2">' + imgX + '</td>   <td style="font-size:12px; font-weight:bold;  width:200px; ">' + fullNameX + '</td>           <td rowspan="2">     <table  border=0 style="width:80px; font-size:12px; color:rgb(51, 51, 51);"> <tr> <td style="width:20px;">' + icon1 + '</td> <td style="width:80px; font-weight:bold;">' + (this.CommonHelper.convertMinsToHrsMins(maxHours)) + '</td>  <tr> <td>' + icon2 + '</td> <td style="font-size:12px; font-weight:bold;">' + (this.CommonHelper.convertMinsToHrsMins(currrentHours)) + '</td></tr> </tr>    </table>             </td>            </tr>        <tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + " " + '</td>       </tr>     </table>';
                return table;
            }
            else {
                return label;
            }
        }
    }

    rendererOverLapListBox = (index, label, value): string => {
        if (this.sourceOverLapListBox.localData == undefined) {
            return null;
        }

        var datarecord = this.sourceOverLapListBox.localData[index];
        // var redColor = "#ff9999";
        var redColor = "#f5877f";
        //var greenColor = "#99cc99";              
        //var grayColor0 ="#f2f2f2";
        var grayColor = "#d9d9d9";
        //var blackColor="black";
        if (datarecord != undefined) {
            // let iconWarning = '<i class="icon ion-ios-warning" style="font-size:18px; color:' + grayColor0 +  ';"></i>';
            let icon1 = '<i class="icon ion-ios-time" style="color:' + redColor + '; font-size:16px; width:20px; float:left;"></i>';
            //let icon2 = datarecord.hours == 0 ? "" : '<i class="icon ion-ios-time" style="color:' +  (datarecord.hoursFree == 0 ? grayColor : datarecord.hoursFree < 0 ? redColor : greenColor )  + '; font-size:16px; width:20px; float:left;"></i>';
            //let icon2="";
            var imgurl = this.imagePipe.transform(datarecord.img, 'users');
            var img = "";
            var table = '<table border=0  style="min-width: 120px; width:325px;">     <td style="width: 30px;  border-right:4px solid #' + datarecord.color + '; "  rowspan="2">' + img + '</td>      <td style="color:rgb(51, 51, 51); font-size:12px;  width:200px; ">' + datarecord.projectName + '</td>           <td rowspan="2">     <table  border=0 style="width:80px; font-size:12px; color:rgb(51, 51, 51);"> <tr> <td style="width:20px;">' + icon1 + '</td> <td style="width:80px; color:' + grayColor + '">' + datarecord.from + '</td>  <tr> <td>' + icon1 + '</td> <td style=" color:' + grayColor + '">' + datarecord.to + '</td></tr> </tr>    </table>             </td>            </tr>        <tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + " " + datarecord.positionName + '</td>       </tr>     </table>';
            return table;
        }
        else {
            if (label != undefined) {
                var values = label.split("|");
                var imgX = '<img style="border-radius:50%;" height="30" width="30" src="' + this.imagePipe.transform(values[1], 'users') + '"/>';
                var fullNameX = values[0];
                //var maxHours:number = Number(values[2]);
                //var currrentHours:number = Number(values[3]);
                //let icon1 = '<i class="icon ion-ios-time" style="color:' +  grayColor + '; font-size:16px; width:15px; float:left;"></i>';
                //let icon2 = '<i class="icon ion-ios-time" style="color:' + (currrentHours < maxHours ? grayColor : redColor) + '; font-size:16px; width:20px; float:left;"></i>';
                var table = '<table border=0 style=" min-width: 120px; width:100%;">  <td style="width: 30px;">' + imgX + '</td>   <td style="font-size:12px; font-weight:bold;  width:200px; ">' + fullNameX + '</td>   </table>';
                return table;
            }
            else {
                return label;
            }
        }
    }

    getFormatDate = (dinput: any, isBeginDate: boolean): any => {
        let d = new Date(dinput);
        //let days: number = isBeginDate ? Number(Number(d.getDate()) + 1) : Number(d.getDate());
        let days: number = isBeginDate ? Number(Number(d.getDate()) + 0) : Number(d.getDate());
        let month: number = Number(Number(d.getMonth()) + 1);
        //return d.getFullYear() + "/" + (month <= 9 ? "0" + month : month) + "/" +  (days <= 9 ? "0" + days : days);
        return (month <= 9 ? "0" + month : month) + "/" + (days <= 9 ? "0" + days : days) + "/" + d.getFullYear();
    }

    //CLICK EVENT ON CALENDAR MODE (Projects, Positions, Users)
    calendarModeOnClick = (event: any): void => {
        this.scheduler.beginAppointmentsUpdate();
        this.calendarModeSelected = event.args.index;
        switch (event.args.index) {
            case 0:
                this.appointmentDataFields.resourceId = "assignedToPosition";
                this.resources.dataField = "assignedToPosition";
                break;
            case 1:
                this.appointmentDataFields.resourceId = "projectName";
                this.resources.dataField = "projectName";
                break;
            case 2:
                this.appointmentDataFields.resourceId = "assignedToFullName";
                this.resources.dataField = "assignedToFullName";
                break;
        }

        this.dataAdapter = new jqx.dataAdapter(this.source);
        this.scheduler.endAppointmentsUpdate();
    };

    //CLICK ON (Calendarview, TimelineView) BUTTONS.
    // changeTimeLineView = (event: any) => {

    //     let aux:any = this.showingButtonTimeSelector ?
    //     [
    //         { type: "dayView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd },workTime:{  fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart,toHour: this.hourWorkEnd} },
    //         { type: "weekView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd },workTime:{  fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart,toHour: this.hourWorkEnd} },
    //         { type: "monthView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd } ,workTime:{  fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart,toHour: this.hourWorkEnd}},
    //         //{ type: "agendaView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd },workTime:{  fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart,toHour: this.hourWorkEnd } },
    //         // timelineDayView            
    //     ] :
    //     [
    //         //{ type: "timelineDayView", timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd },workTime:{ fromHour: this.hourWorkStart,toHour: this.hourWorkEnd } }
    //         { type: "timelineDayView", showWeekends: true, timeRuler: { scaleStartHour: this.timeScaleStart, scaleEndHour: this.timeScaleEnd },workTime:{  fromDayOfWeek: this.dayOfWeekStart, toDayOfWeek: this.dayOfWeekEnd, fromHour: this.hourWorkStart,toHour: this.hourWorkEnd } },
    //     ];

    //     this.views = aux;
    //     this.scheduler.view(!this.showingButtonTimeSelector ? 'timelineDayView' : 'dayView');
    //     this.scheduler.views(this.views);
    //     this.showingButtonTimeSelector = !this.showingButtonTimeSelector;
    // };

    //EVENT CHANGE SELECTION ON DUPLICATECOMBO (Daily, Weekly,Monthly)
    selectDuplicateCombo = (event: any): void => {
        var args = event.args;
        if (args) {
            this.selectedDuplicateCombo = args.item.originalItem.id;
            this.duplicateRadioButtonEndAfter.check();
            let heightWindow = args.index == 0 ? "520" : args.index == 1 ? "630" : "660";
            this.windowAppointment.height(heightWindow);
        }
    };

    getScheduleLocation = (): any => {
        //return this.translate.currentLang == "en" ? this.jqxHelper.getScheduleLocation_en : this.jqxHelper.getScheduleLocation_es;
        return this.jqxHelper.getScheduleLocation_en;
    }

    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    //SCHEDULER BINDING COMPLETE EVENT
    schedulerBindingComplete = (event: any): void => {
        setTimeout(() => {
            // alert("ready");
            this.renderSchedulerByDates();
        });
    };

    //ISSUE EVENT FROM AUTOR COMPONENT (NEVER RAISE)
    scheduleRenderer = (): void => { };

    //SCHEDULER'S READY EVENT. 
    ready = (): void => {
        //alert("ready");

        // setTimeout(() => {
        //     alert("ready");
        //     this.renderSchedulerByDates();   
        // });
        // let elements = this.el.nativeElement.querySelectorAll('.jqx-cell');

        // // elements.array.forEach(element => {
        // // });
        // [].forEach.call(elements, function(div) {
        //     // do whatever
        //     div.style.background = "red";
        //   });
    }

    statusesScheduled: any = {
        free: "green",
        tentative: "tentative",
        busy: "transparent",
        doNotDisturb: "var(--twelfth-color)",
        outOfOffice: "#ff3232"
    }

    //RENDER APPOINTMENT
    renderAppointment = (data: any): any => {
        if (data.appointment != undefined) {
            let appointmentWidth = data.width;
            let appointmentHeight = data.height;
            let record = this.source.localData.filter(x => x.id == data.appointment.id)[0];
            if (record) {
                let colorBackTask = "#ffb6c1";
                let colorBackMedical = "#7fffd4";
                let assignedToFullName = record.assignedToFullName;
                let assignedToPosition = record.assignedToPosition;
                let projectColor = record.projectColor;
                // let subject = record.subject;
                let hours = this.CommonHelper.convertMinsToHrsMins(record.hours);
                // data.background = isTracking ? '#ffdb99' : '#e5f2e5';          
                data.background = record.state == "TT" ? colorBackTask : record.state == "MM" ? colorBackMedical : 'white';
                // data.textColor = '#808080';
                //PARECE IMPORTANTE
                //data.textColor = record.isDirty ? "#ff3232" : '#808080';
                data.textColor = record.state == "TT" ? "#909599" : '#808080';
                //data.textColor = "red";
                // http://192.168.0.2:38007/media/images/clients/1a96ec51-481a-4714-aec3-04d183a1b01e.png
                var imgurl = this.imagePipe.transform(record.img, record.state == "MM" ? "clients" : "users");
                let smallFactor = 50;
                let smallSpace = appointmentHeight <= smallFactor || appointmentWidth <= smallFactor;
                let imgSize = smallSpace ? 20 : 30;
                let img = '<img style="border-radius:50%; margin-right:4px; margin-left:4px; float:left;" height="' + imgSize + '" width="' + imgSize + '" src="' + imgurl + '"/>';
                let duplicateIcon = record.idDuplicate > 0 ? '<i class="ion-arrow-return-left" style="color:black; font-size:16px; width:20px; float:left;"></i>' : '';
                data.cssClass = "appointmentCommonClass";
                // data.borderColor = record.isDirty ? "#ff3232" : "#" +projectColor;
                data.borderColor = record.state == "TT" ? colorBackTask : record.state == "MM" ? colorBackMedical : "#" + projectColor;
                // data.borderColor = record.state == "TT" ? colorBackTask :    "#" + projectColor;
                //data.style ="font-size:10px;";
                //data.status = "tentative";
                //data.textColor = record.isDirty ? "red" : '#808080';
                //this.scheduler.rendered()

                switch (data.view) {
                    case 'weekView':
                    case 'dayView':
                    case 'monthView':
                    case 'timelineDayView':
                        // case 'timelineWeekView':
                        // case 'timelineMonthView':                
                        let squarewith = smallSpace ? '4px !important' : '100%';
                        let squareHeight = smallSpace ? imgSize + 'px' : '4px';
                        var divSquareProject = (record.state == "MM" ? "<i  class='icon ion-ios-thermometer iconmenu' style='color:var(--thirteenth-color); float:left; margin-right:4px; font-size:18px;'></i>" : record.state == "TT" ? "<i  class='icon ion-ios-settings iconmenu' style='color:var(--thirteenth-color); float:left; margin-right:4px; font-size:18px;'></i>" : "") + "<div style='float:left; height:" + squareHeight + "; width:" + squarewith + "; background-color:" + projectColor + ";'>" + duplicateIcon + "</div>";

                        if (!smallSpace) {
                            data.html = divSquareProject + img;
                            data.html = data.html + assignedToFullName;
                            data.html = data.html + " " + assignedToPosition + " ";
                            data.html = record.state == "T" ? data.subject : data.html + "<br/>" + hours + "";
                            // data.borderColor = isTracking ? "var(--twelfth-color)" : "#" +projectColor;
                        }
                        else {
                            data.html = appointmentHeight > smallFactor * 2 ? "<br/>" + divSquareProject + duplicateIcon + img : divSquareProject + duplicateIcon + img;
                            //data.borderColor = "#" +projectColor;
                            let hoursAux = "" + hours + "";
                            data.html = appointmentHeight > smallFactor * 2 ?
                                data.html + "<br/><br/><div style='float:left;'>" + (record.state == "TT" || record.state == "MM" ? "" : hoursAux) + " " + assignedToFullName + (record.state == "MM" || record.state == "TT" ? " " + record.subject : "") + "</div>" :
                                data.html + "<div>" + (record.state == "TT" || record.state == "MM" ? "" : hoursAux) + " " + assignedToFullName + (record.state == "MM" || record.state == "TT" ? " " + record.subject : "") + "</div>";
                        }

                        break;
                }
            }
        } // end record
        return data;
    }

    //CLOSE APPOINTMENT WINDOW
    closeWindow(event: any): void {
        this.windowAppointment.close();
    }

    closeWindowEditAppointment(event: any): void {
        this.editAppointmentWindow.close();
    }

    cancelWindowPeriod(event: any): void {
        this.periodsWindow.close();
    }

    savePeriods(event: any): void {
        let periodschanged = this.sourcePeriodsListBox.localData.filter(c => c.abm != '' && !(c.id < 0 && c.abm == "D"));
        if (periodschanged != undefined && periodschanged.length > 0) {
            let Request = {
                Periods: periodschanged
            };
            this.myLoader.open();
            this.schedulingService.SavePeriods(Request)
                .subscribe(
                    (data: any) => {
                        if (data.result) {
                            this.periodsWindow.close();
                            this.glowMessage.ShowGlow("success", "glow_success", "glow_scheduling_periods_saved_successfully");
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

                                        this.myLoader.close();
                                    },
                                    error => {
                                        this.myLoader.close();
                                        this.manageError(error);
                                    }
                                );
                        }
                        else {
                            this.myLoader.close();
                            this.manageError(data);
                        }
                    },
                    error => {
                        this.myLoader.close();
                        this.manageError(error);
                    });
        }
        else {
            this.glowMessage.ShowGlow("warn", "glow_info", "glow_no_changes_to_save");
        }
        this.periodsWindow.close();
    }

    validateNewPeriod = (): boolean => {
        let result = true;
        let time1: Date = this.dateFromPeriod.getDate();
        let time2: Date = this.dateToPeriod.getDate();

        if (time1 == null) {
            //this.glowMessage.ShowGlow("error", "glow_globalconexionerror", "glow_gloablcantconnecttoserver");
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_select_valid_time_from");
            result = false;
        }
        else {
            if (time1.getTime() > time2.getTime()) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_time_from_must_smaller_than_to");
                result = false;
            }
        }

        if (time2 == null) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_select_valid_time_to");
            result = false;
        }
        return result;
    }

    removePeriodClick = (event: any): void => {
        event.abm = "D";
    }

    changeCloseTextBoxPosition = (event: any): void => {
        event.abm = "E";
        event.close = true;
        event.state = "CL";
    }

    addPeriodClick = (event: any): void => {
        if (this.validateNewPeriod()) {
            let time1: Date = this.dateFromPeriod.getDate();
            let time2: Date = this.dateToPeriod.getDate();

            var newPeriod = {
                id: -1,
                state: "",
                description: "",
                abm: 'I',
                from: time1,
                to: time2
            };

            this.sourcePeriodsListBox.localData.push(newPeriod);
            this.dateFromPeriod.setDate(null);
            this.dateToPeriod.setDate(null);
        }
    }

    removeWindowEditAppointment(event: any): void {
        let Request = {
            Id: Number(this.idCurrentAppointment)
        };
        this.myLoader.open();
        this.schedulingService.DeleteScheduling(Request)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.windowAppointment.close();
                        this.myLoader.close();
                        this.glowMessage.ShowGlow("success", "glow_success", "glow_scheduling_deleted_succesfully");
                        //this.getStaff();
                        this.getSchedules(this.currentPeriod);
                        this.editAppointmentWindow.close();
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

    validateEditWindowAppointment = (): boolean => {
        let result = true;
        var recordPeriod = this.sourcePeriodsListBox.localData.find(x => x.id == this.currentPeriod);

        if (recordPeriod) {
            var datef = new Date(recordPeriod.from);
            datef.setSeconds(1);
            datef.setMinutes(0);
            datef.setHours(0);

            var datet = new Date(recordPeriod.to);
            datet.setSeconds(59);
            datet.setMinutes(59);
            datet.setHours(23);

            // this.editGenDate.setMinDate(datef);        
            // this.editGenDate.setMaxDate(new Date(recordPeriod.to));

            // this.dateTimeEndOn.setMinDate(datef);
            // this.dateTimeEndOn.setMaxDate(new Date(recordPeriod.to));
            // this.editAppointmentDate.setMinDate(datef);
            // this.editAppointmentDate.setMaxDate(new Date(recordPeriod.to));

            let date1: Date = this.editAppointmentDate.getDate();
            let time1: Date = this.editAppointmentFrom.getDate();
            let time2: Date = this.editAppointmentTo.getDate();

            if (time1 == null) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_select_valid_time_from");
                result = false;
            }
            //else {
            // if (time1.getTime() > time2.getTime()) {
            //     this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_scheduling_time_from_must_smaller_than_to");
            //     result = false;
            // }
            //}

            if (date1 < datef || date1 > datet) {
                this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_date_must_between_range");
                result = false;
            }
        }
        else {
            result = false;
        }
        return result;
    }

    SaveWindowEditAppointment(event: any): void {
        if (this.typeEdit != "") {
            this.editAppointmentWindow.close();
            return;
        }

        if (this.validateEditWindowAppointment()) {

            let date1: Date = this.editAppointmentDate.val();
            let time1: Date = this.editAppointmentFrom.val();
            let time2: Date = this.editAppointmentTo.val();

            //var offset = new Date().getTimezoneOffset();

            let Request = {
                Id: this.idCurrentAppointment,
                Date: date1,
                Time1: time1,
                Time2: time2,
                FixTimeZone: false,
                TimeDifference: 0 // this.getTimeZone()
            };

            this.myLoader.open();

            this.schedulingService.UpdateScheduling(Request)
                .subscribe(
                    (data: any) => {
                        if (data.result) {
                            this.windowAppointment.close();
                            this.myLoader.close();
                            this.glowMessage.ShowGlow("success", "glow_success", "glow_scheduling_saved_succesfully");
                            //this.getStaff();
                            this.getSchedules(this.currentPeriod);
                            this.editAppointmentWindow.close();
                        }
                        else {
                            this.myLoader.close();
                            this.manageError(data);
                        }
                    },
                    error => {
                        this.myLoader.close();
                        this.manageError(error);
                    });
        }
    }

    openPeriodsManager = (event: any): void => {
        for (var i = 0; i < this.sourcePeriodsListBox.localData.length; i++) {
            if (this.sourcePeriodsListBox.localData[i].id > 0) {
                this.sourcePeriodsListBox.localData[i].abm = "";
            }
        }

        this.dateFromPeriod.setDate(null);
        this.dateToPeriod.setDate(null);
        this.periodsWindow.open();
    }

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    onAppointmentChange = (event: any): void => {
        var args = event.args;
        var appointment = args.appointment;
        let date1: Date = appointment.from.toDate();//this.editAppointmentDate.val();
        let time1: Date = appointment.from.toDate();//this.editAppointmentFrom.val();
        let time2: Date = appointment.to.toDate();//this.editAppointmentTo.val();

        let Request = {
            Id: appointment.id,
            Date: date1,
            Time1: time1,
            Time2: time2,
            TimeDifference: this.getTimeZone()
        };

        this.myLoader.open();

        this.schedulingService.UpdateScheduling(Request)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.myLoader.close();
                        this.glowMessage.ShowGlow("success", "glow_success", "glow_scheduling_saved_succesfully");
                        //this.getStaff();
                        this.getSchedules(this.currentPeriod);
                    }
                    else {
                        this.myLoader.close();
                        this.manageError(data);
                    }
                },
                error => {
                    this.myLoader.close();
                    this.manageError(error);
                });
    }


    //isShowingContextMenu:boolean=false;
    contextMenuOpen = (menu, appointment, event) => {
        //this.isShowingContextMenu =true;
        if (menu && event) {
            event.stopPropagation();
            //this.isShowingContextMenu =true; 
            menu.jqxMenu('hideItem', 'editAppointment');
            menu.jqxMenu('hideItem', 'createAppointment');

            menu.jqxMenu(appointment && appointment.status != "TT" && appointment.status != "MM" && this.canAddOrEditSchedule() ? 'showItem' : 'hideItem', 'edit');
            menu.jqxMenu(appointment && appointment.status != "TT" && appointment.status != "MM" && this.canAddOrEditSchedule() ? 'showItem' : 'hideItem', 'selectOne');
            //menu.jqxMenu(appointment && this.canAddOrEditSchedule() ? 'hideItem' : 'showItem', 'selectAll');

            menu.jqxMenu(appointment && appointment.status == "TT" ? 'showItem' : 'hideItem', 'editTask');
            menu.jqxMenu(appointment && appointment.status == "MM" ? 'showItem' : 'hideItem', 'editMedical');
        }
    };

    appointmentsSelected: any[] = [];

    closePanelRight = (): void => {
        // TODO : mmmmmmm
        this.appointmentsSelected = [];
        this.panelRightSelectVisible = false;
        // setTimeout(() => {
        //     this.scheduler.beginAppointmentsUpdate();
        //     this.dataAdapter = new jqx.dataAdapter(this.source);
        //     this.scheduler.endAppointmentsUpdate();        
        // });
    }

    removeSelected = (event: any): void => {
        if (this.appointmentsSelected.length == 0) {
            this.glowMessage.ShowGlow("success", "glow_success", "glow_scheduling_saved_succesfully");
            return;
        }

        this.loadedControl = [true, true, false];
        let ids: number[] = [];
        this.appointmentsSelected.forEach(element => {
            ids.push(element.originalData.id);
        });

        let Request: any = {
            listSchedules: ids
        }

        this.schedulingService.DeleteSelectedSchedules(Request)
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.glowMessage.ShowGlow("success", "glow_success", "glow_scheduling_saved_succesfully");
                        this.getSchedules(this.currentPeriod);
                        this.appointmentsSelected = [];
                        this.closePanelRight();
                    }
                    else {
                        this.loadedControl = [true, true, true];
                        this.HiddeLoaderWhenEnd();
                        this.manageError(data);
                        this.windowAppointment.open();
                    }
                },
                error => {
                    // this.myLoader.close();
                    this.loadedControl = [true, true, true];
                    this.HiddeLoaderWhenEnd();
                    this.manageError(error);
                    this.windowAppointment.open();
                });
    }

    clearAllSelected = (event: any): void => {
        this.appointmentsSelected = [];
        // alert("clear all");
    }

    unselectRemoveSchedule = (event: any, index: number): void => {
        this.appointmentsSelected.splice(index, 1);
        // alert("unselect " + index);
    }

    addAppointmentToListSelected = (appointment: any): void => {
        var find = this.appointmentsSelected.filter(c => c.originalData.id == appointment.originalData.id).length;

        if (find == 0) {
            this.appointmentsSelected.push(appointment);
            var aux = this.panelRightSelectVisible;
            this.panelRightSelectVisible = true;

            // setTimeout(() => {
            //     if(!aux)
            //     {
            //         // fixed this, becasuse jqxscheduler dont has render method
            //         this.scheduler.beginAppointmentsUpdate();
            //         this.dataAdapter = new jqx.dataAdapter(this.source);
            //         this.scheduler.endAppointmentsUpdate();
            //     }    
            // });

            // this.scheduler.rendered();
        }
    }


    contextMenuClose = (event: any) => {
        //this.isShowingContextMenu = false;
    }

    contextMenuItemClick = (menu, appointment, event) => {
        if (event && event.args) {
            let args = event.args;
            switch (args.id) {
                case 'edit':
                    this.onAppointmentDoubleClick(null, appointment);
                    return true;
                case 'selectOne':
                    this.addAppointmentToListSelected(appointment);
                    return true;
                // case 'selectAll':
                //     this.panelRightSelectVisible = true;
                //     return true;
                case 'editTask':
                    this.onAppointmentDoubleClick(null, appointment);
                    return true;
                case 'editMedical':
                    this.onAppointmentDoubleClick(null, appointment);
                    return true;
            }
        }
    };

    contextMenuCreate = (menu: any, settings: any): void => {
        if (settings && settings.source) {
            //var source = settings.source;
            //source.pop
            //settings.source.pop();
            //settings.source.pop();
            var source = settings.source;

            source.push({ id: "edit", label: "Edit" });
            source.push({ id: "selectOne", label: "Select Schedule" });
            //source.push({ id: "selectAll", label: "Select Schedules" });

            source.push({ id: "editTask", label: "View Task" });
            source.push({ id: "editMedical", label: "View Medical Reminder" });

            // source.push({
            //     id: "status", label: "Set Status", items:
            //         [
            //             { label: "Free", id: "free" },
            //             { label: "Out of Office", id: "outOfOffice" },
            //             { label: "Tentative", id: "tentative" },
            //             { label: "Busy", id: "busy" }
            //         ]
            // });
        }
    };



    dragEndProperty = (dragItem: any, dropItem: any): void => {
        if (dropItem && this.canAddOrEditSchedule()) {
            var selectedItem = this.listBoxStaffFilter.getSelectedItem();
            if (selectedItem != null) {
                this.clearEditWindow();
                // this.editGenDate.setMinDate(this.schedulerFrom.toDate());
                // this.editGenDate.setMaxDate(this.schedurerTo.toDate());
                this.editGenDate.setDate(new Date());
                let selection: any = this.scheduler.getSelection();

                if (selection != null) {
                    // this.editGenDate.setDate(selection.from.toDate());
                    this.editGenDate.setDate(selection.from.toDate());
                    this.editGenFrom.setDate(selection.from.toDate());
                    if (selection.to.toDate().getDate() > selection.from.toDate().getDate()) {
                        var selAux = selection.from.toDate();
                        this.editGenTo.setDate(new Date(selAux.getFullYear(), selAux.getMonth() + 1, selAux.getDate(), 23, 59, 0, 0));
                    }
                    else {
                        this.editGenTo.setDate(selection.to.toDate());
                    }
                }

                let items = this.editGenStaff.getItems().filter(x => x.value == selectedItem.value);
                if (items != null) {
                    this.editGenStaff.checkItem(items[0]);
                }
                this.windowAppointment.open();
                this.windowAppointment.setTitle("New Scheduled");
            }
        }
    }

    print = (event: any): void => {
        console.log(this.sourceOriginal.localData);
        console.log(this.currentPeriod);
        console.log(this.schedulerFrom.toDate());
        console.log(this.schedulerTo.toDate());
        let body = [
            [
                { text: 'Staff', style: 'tableHeader' },
                { text: 'Date', style: 'tableHeader' },
                { text: 'From', style: 'tableHeader' },
                { text: 'To', style: 'tableHeader' },
            ],
        ];
        this.sourceOriginal.localData.forEach(item => {
            body.push(
                [
                    { text: item.assignedToFullName, style: '' },
                    { text: moment(new Date(item.from)).format('MMMM Do, YYYY'), style: '' },
                    { text: moment(new Date(item.from)).format('HH:mm'), style: '' },
                    { text: moment(new Date(item.to)).format('HH:mm'), style: '' },
                ]);
        });
        var docDefinition = {
            content: [
                { text: 'Schedule Report', style: 'header' },
                { text: 'From: ' + moment(this.schedulerFrom.toDate()).format('MMMM Do, YYYY') + ' To: ' + moment(this.schedulerTo.toDate()).format('MMMM Do, YYYY'), style: 'subheader' },
                {
                    table: {
                        headerRows: 1,
                        widths: ['*', '*', 70, 70],
                        body: body,
                    },
                },
            ],
            styles: {
                header: {
                    fontSize: 18,
                    bold: true,
                    margin: [0, 0, 0, 10]
                },
                subheader: {
                    fontSize: 16,
                    bold: true,
                    margin: [0, 10, 0, 5]
                },
                tableExample: {
                    margin: [0, 5, 0, 15]
                },
                tableHeader: {
                    bold: true,
                    fontSize: 13,
                    color: 'black'
                }
            },
        }
        pdfMake.createPdf(docDefinition).print();
    }
}