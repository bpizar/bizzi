import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { StaffForm, FormField, StaffFormReminders, Staff } from './staff-forms.component.model';
import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { jqxTextAreaComponent } from 'jqwidgets-ng/jqwidgets/jqxtextarea';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqwidgets/jqxdropdownlist';
import { jqxDateTimeInputComponent } from "jqwidgets-ng/jqwidgets/jqxdatetimeinput";
import { jqxTabsComponent } from "jqwidgets-ng/jqwidgets/jqxtabs";
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import { jqxWindowComponent } from 'jqwidgets-ng/jqwidgets/jqxwindow';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { Router, ActivatedRoute } from '@angular/router';
import { StaffService } from '../staff.service';
import { NgForm } from '@angular/forms';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { AuthHelper } from 'src/app/common/helpers/app.auth.helper';
import { forkJoin } from 'rxjs';
import * as moment from 'moment';

@Component({
  selector: 'app-staff-form',
  templateUrl: './staff-form.component.html',
  styleUrls: ['./staff-form.component.css'],
  providers: [StaffService, ConstantService, AuthHelper],
})
export class StaffFormComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @Input() public modal: boolean;
  @Input() public id: number;
  @ViewChild('cmbTemplateType') cmbTemplateType: jqxDropDownListComponent;
  @ViewChild('cmbRecurrence') cmbRecurrence: jqxDropDownListComponent;
  @ViewChild('cmbWeekday') cmbWeekday: jqxDropDownListComponent;
  @ViewChild('cmbTwoTimesWeek') cmbTwoTimesWeek: jqxDropDownListComponent;
  @ViewChild('dtiHour') dtiHour: jqxDateTimeInputComponent;
  @ViewChild('dtiDate') dtiDate: jqxDateTimeInputComponent;
  @ViewChild('cmbLowPeriodType') cmbLowPeriodType: jqxDropDownListComponent;
  @ViewChild('cmbCriticalPeriodType') cmbCriticalPeriodType: jqxDropDownListComponent;
  @ViewChild('cmbLowReminderStaff') cmbLowReminderStaff: jqxDropDownListComponent;
  @ViewChild('cmbCriticalReminderStaff') cmbCriticalReminderStaff: jqxDropDownListComponent;
  @ViewChild('tabs') tabs: jqxTabsComponent;
  @ViewChild('cmbConstraints') cmbConstraints: jqxDropDownListComponent;
  @ViewChild('editWindow') editWindow: jqxWindowComponent;
  @ViewChild('txtName') txtName: jqxInputComponent;
  @ViewChild('txtPlaceholder') txtPlaceholder: jqxInputComponent;
  @ViewChild('txtDescription') txtDescription: jqxTextAreaComponent;
  @ViewChild('cmbDatatype') cmbDatatype: jqxDropDownListComponent;
  staffForm: StaffForm = new StaffForm();
  formFields: FormField[] = [];
  formField: FormField = new FormField();
  staffFormReminder = new StaffFormReminders();
  staffFormReminders: StaffFormReminders[] = [];
  staffList: Staff[] = [];
  loading = true;
  dataTypeList: string[] = ['text', 'number', 'date', 'time', 'check/no check', 'note', 'yes/no', 'decimal number'];
  constraintsList: string[] = [];
  stringConstraints: string[] = ['None', 'Alphanumeric', 'Alphabetic', 'Email', 'Numbers'];
  integerConstraints: string[] = ['None', 'Number >= 0', 'Number > 0', 'Number < 0'];
  dateConstraints: string[] = ['None', 'From today', 'Until today'];
  timeConstraints: string[] = ['None'];
  booleanConstraints: string[] = ['None'];
  decimalConstraints: string[] = ['None', 'Number >= 0', 'Number > 0', 'Number < 0'];
  templateTypeList: string[] = ['Image Portrait', 'Image Landscape', 'Custom'];
  templateHTMLValue: string = "";
  recurrence: number = 0;
  recurrenceItem: string = 'Once';
  recurrenceList: string[] = ['Once', 'Daily', 'Weekly', 'Biweekly', 'Monthly', 'Annually'];
  weekRecurrenceList: string[] = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
  BiweeklyRecurrenceList: string[] = ['1st Monday', '1st Tuesday', '1st Wednesday', '1st Thursday', '1st Friday', '1st Saturday', '1st Sunday',
    '2nd Monday', '2nd Tuesday', '2nd Wednesday', '2nd Thursday', '2nd Friday', '2nd Saturday', '2nd Sunday'];
  periodTypeList: string[] = ['Days', 'Weeks', 'Months'];
  lowPeriodValue: number = 1;
  dayMonth: number = 1;
  recurrenceDetail: number;
  criticalPeriodValue: number = 1;
  editorvalue = "";
  editingRow = new FormField();
  editIndex: number = 0;
  fileToUpload: File | null = null;

  constructor(
    public staffFormService: StaffService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
  ) {
    activatedRoute.params.subscribe(params => {
      this.id = params.id;
      this.staffForm.id = -1;
    });
  }

  ngAfterViewInit(): void {
    this.load(this.id);
    this.dtiHour.createComponent();
    this.constraintsList = this.stringConstraints;
    this.initFormFields();
  }

  initFormFields() {
    this.formField.id = 0;
    this.formField.name = undefined;
    this.txtName.val('')
    this.formField.placeholder = undefined;
    this.txtPlaceholder.val('')
    this.txtDescription.val('');
    this.formField.datatype = 'text';
    this.cmbDatatype.selectedIndex(0);
    this.formField.constraints = 'None';
    this.cmbConstraints.selectedIndex(0);
  }

  cmbDatatypeChange(event) {
    this.formField.datatype = this.dataTypeList[event.args.index];
    switch (event.args.index) {
      case 1: // number
        this.constraintsList = this.integerConstraints;
        this.cmbConstraints.selectedIndex(0);
        break;
      case 2: // date
        this.constraintsList = this.dateConstraints;
        this.cmbConstraints.selectedIndex(0);
        break;
      case 3: // time
        this.constraintsList = this.timeConstraints;
        this.cmbConstraints.selectedIndex(0);
        break;
      case 4: // check/no check
        this.constraintsList = this.booleanConstraints;
        this.cmbConstraints.selectedIndex(0);
        break;
      case 5: // note
        this.constraintsList = this.stringConstraints;
        this.cmbConstraints.selectedIndex(0);
        break;
      case 6: // yes/no
        this.constraintsList = this.booleanConstraints;
        this.cmbConstraints.selectedIndex(0);
        break;
      case 7: // decimal
        this.constraintsList = this.decimalConstraints;
        this.cmbConstraints.selectedIndex(0);
        break;
      default: // text
        this.constraintsList = this.stringConstraints;
        this.cmbConstraints.selectedIndex(0);
        break;
    }
  }

  cmbConstraintsChange(event) {
    if (event.args)
      this.formField.constraints = this.constraintsList[event.args.index];
    else {
      this.formField.constraints = this.constraintsList[0];
      this.cmbConstraints.selectedIndex(0);
    }
  }

  load(IdStaffForm: number) {
    this.myLoader.open();
    if (IdStaffForm > 0) {
      const tasks = [];
      tasks.push(
        this.staffFormService.GetAllStaff(),
        this.staffFormService.GetStaffFormForEditById(IdStaffForm),
        this.staffFormService.GetAllFormFieldsByStaffForm(IdStaffForm),
        this.staffFormService.GetStaffFormRemindersByStaffForm(IdStaffForm)
      );
      forkJoin(tasks).subscribe(results => {
        if ((results[0] as any).result) {
          this.loadStaff(results[0]);
        }
        if ((results[1] as any).result) {
          this.staffForm = (results[1] as any).staffForm;
          if (this.staffForm.template == 'ImagePortrait') {
            this.cmbTemplateType.selectedIndex(0);
          }
          else if (this.staffForm.template == 'ImageLandscape') {
            this.cmbTemplateType.selectedIndex(1);
          }
          else {
            this.cmbTemplateType.selectedIndex(2);
            this.editorvalue = this.staffForm.template;
          }
          this.loadRecurrence();

        } else {
          this.manageError(results[1]);
        }
        if ((results[2] as any).result) {
          (results[2] as any).formFields.forEach((rowFormFiled: FormField) => {
            let formField = new FormField();
            formField.id = rowFormFiled.id;
            formField.name = rowFormFiled.name;
            formField.placeholder = rowFormFiled.placeholder;
            formField.datatype = rowFormFiled.datatype;
            formField.description = rowFormFiled.description;
            formField.constraints = rowFormFiled.constraints;
            formField.isEnabled = rowFormFiled.isEnabled;
            this.formFields.push(formField);
          });
        } else {
          this.manageError(results[2]);
        }
        if ((results[3] as any).result) {
          this.staffFormReminders = (results[3] as any).staffFormReminders;
          this.loadReminders();
        }
        this.myLoader.close();
      },
        (error) => {
          this.myLoader.close();
          this.manageError(error);
          this.router.navigate(['/staff-forms']);
        }
      );
    } else {
      this.myLoader.close();
    }
  }

  manageError = (data: any): void => {
    this.glowMessage.ShowGlowByError(data);
  }

  cmbTemplateTypeChange() {
    switch (this.cmbTemplateType.getSelectedIndex()) {
      case 0:
        if (this.templateHTMLValue === '') {
          this.templateHTMLValue = this.editorvalue;
        }
        this.editorvalue = 'Image Portrait';
        this.tabs.selectedItem(1);
        break;
      case 1:
        if (this.templateHTMLValue === '') {
          this.templateHTMLValue = this.editorvalue;
        }
        this.editorvalue = 'Image Landscape';
        this.tabs.selectedItem(1);
        break;
      case 2:
        this.editorvalue = this.templateHTMLValue;
        this.templateHTMLValue = '';
        break;
    }
  }

  save(f: NgForm) {
    this.fillOrderNumbers();
    if (this.thereAreRepeatNames()) {
      this.glowMessage.ShowGlow('error', 'Error saving Fields', 'There are repeat Names on your fields, please check only one');
    } else {
      switch (this.cmbTemplateType.getSelectedIndex()) {
        case 0:
          this.staffForm.template = 'ImagePortrait';
          break;
        case 1:
          this.staffForm.template = 'ImageLandscape';
          break;
        case 2:
          this.staffForm.template = this.editorvalue;
      }
      this.staffForm.idfRecurrence = this.recurrence;
      if (this.recurrence == 4)
        this.staffForm.idfRecurrenceDetail = this.dayMonth;
      else
        this.staffForm.idfRecurrenceDetail = this.recurrenceDetail;

      this.prepareReminders();
      this.prepareidUsers();
      let body = {
        staffForm: this.staffForm,
        staffFormReminders: this.staffFormReminders,
        formFields: this.formFields
      }
      this.staffFormService.SaveStaffForm(body)
        .subscribe(
          (data: any) => {
            if (data.result) {
              this.back();
            }


            // f.reset(f.value);
            // if (this.id > 0) {
            // Mensajes.Mostrar('Ligero', 'success', 'Unidad Actualizado', staffForm.Nombre)
            // } else {
            // Mensajes.Mostrar('Ligero', 'success', 'Unidad Creado', staffForm.Nombre)
            // };
            // if (!this.modal) {
            // this.staffForm.id = staffForm.id;
            // this.router.navigate(['/unidad', staffForm.id ]);
            // } else {
            //   this.modalService.notificacion.emit(staffForm);
            //   this.modalService.ocultarModal();
            // }
          });
      // else
      // {
      // Mensajes.Mostrar('Ligero', 'success', 'No se modificaron los datos', 'No hubo cambios')
      // }

      // else
      // {
      // Mensajes.Mostrar('Ligero', 'success', 'Datos no válidos', 'Error en el Formulario')
      // }
    }
  }

  cancel(f: NgForm) {
    if (!f.pristine) {
      // Mensajes.MensajeCondicional('SiNo', 'question', 'Está seguro que desea cancelar?', 'Todos los cambios no guardados se perderán')
      // .then(
      //   result=>{
      //     if (result.value)
      //     {
      this.back();
      //     }
      //   }
      // )
    }
    else {
      this.back();
    }
  }

  back() {
    // if (!this.modal) {
    this.router.navigate(['/staff/staff-forms']);
    // } else {
    // this.modalService.ocultarModal();
    // }
  }

  removeFormFieldFromStaffForm(idStaffForm: number, formField: FormField) {
    this.staffFormService.RemoveStaffFormField(idStaffForm, formField.id)
      .subscribe(() => this.load(idStaffForm));
  }

  addFormField() {
    if ((this.formField.name == undefined) || (this.formField.placeholder == undefined) ||
      (this.formField.datatype == undefined) || (this.formField.constraints == undefined)) {
      this.glowMessage.ShowGlow('error', 'Error Adding Fields', 'All fields are necessary to add data');
    }
    else {
      let formField = new FormField();
      formField.id = this.formField.id;
      formField.name = this.formField.name;
      formField.placeholder = this.formField.placeholder;
      formField.datatype = this.formField.datatype;
      formField.constraints = this.formField.constraints;
      formField.description = this.formField.description;
      formField.isEnabled = true;
      this.formFields.push(formField);
      this.initFormFields();
    }
  }

  loadRecurrence() {
    this.cmbRecurrence.selectIndex(this.staffForm.idfRecurrence);
    this.recurrenceItem = this.recurrenceList[this.staffForm.idfRecurrence];
    switch (this.staffForm.idfRecurrence) {
      case 0:
        this.recurrenceDetail = 0;
        break;
      case 1:
        this.dtiHour.setDate(this.militaryToHour(this.staffForm.idfRecurrenceDetail));
        break;
      case 2:
        this.cmbWeekday.selectIndex(this.staffForm.idfRecurrenceDetail);
        break;
      case 3:
        this.cmbTwoTimesWeek.selectIndex(this.staffForm.idfRecurrenceDetail);
        break;
      case 4:
        this.dayMonth = this.staffForm.idfRecurrenceDetail;
        break;
      default:
        this.dtiDate.setDate(moment().dayOfYear(this.staffForm.idfRecurrenceDetail).format('M/D/YYYY'));
        break;
    }
  }

  onRecurrenceChange() {
    this.initRecurrence();
    this.recurrenceItem = this.recurrenceList[this.cmbRecurrence.getSelectedIndex()];
    switch (this.cmbRecurrence.getSelectedIndex()) {
      case 0:
        this.recurrenceDetail = 0;
        break;
      case 1:
        this.recurrence = 1;
        this.recurrenceDetail = 1200
        break;
      case 2:
        this.recurrence = 2;
        this.recurrenceDetail = 0;
        break;
      case 3:
        this.recurrence = 3;
        this.recurrenceDetail = 0;
        break;
      case 4:
        this.recurrence = 4;
        this.dayMonth = 1;
        this.recurrenceDetail = 1;
        break;
      default:
        this.recurrence = 5;
        this.recurrenceDetail = 1;
        break;
    }
  }

  initRecurrence() {
    this.dtiHour.setDate('12:00');
    this.cmbWeekday.selectIndex(0);
    this.cmbTwoTimesWeek.selectIndex(0);
    this.dayMonth = 1;
    this.dtiDate.setDate('1/1/' + moment().year());
  }

  onDtiHourChange() {
    let hour: string = this.dtiHour.val();
    let hour2 = hour.split(':');
    this.recurrenceDetail = parseInt(hour2[0] + hour2[1]);
  }

  onWeekDayChange() {
    this.recurrenceDetail = this.cmbWeekday.getSelectedIndex();
  }

  biweeklyChange() {
    this.recurrenceDetail = this.cmbTwoTimesWeek.getSelectedIndex();
  }

  onDayMonthChange() {
    this.recurrenceDetail = this.dayMonth
  }

  ondtiDateChange() {
    let date = moment(this.dtiDate.val());
    this.recurrenceDetail = date.dayOfYear();
  }

  militaryToHour(_hours: number): string {
    let hours = _hours.toString();
    if (hours.length == 4) {
      return hours.slice(0, 2) + ':' + hours.slice(2, 4);
    } else {
      return hours.slice(0, 1) + ':' + hours.slice(1, 3);
    }
  }

  cmbLowPeriodTypeChange() { }
  cmbCriticalPeriodTypeChange() { }

  loadStaff(staffList) {
    let list: any[] = [];
    let staffs = staffList.staffs;
    staffs.forEach(item => {
      let staff = new Staff();
      staff.id = item.idfUser;
      staff.name = item.fullName;
      list.push(staff);
    });
    this.staffList = list;
  }

  prepareReminders() {
    this.staffFormReminders = [];
    let lowStaffFormReminder = new StaffFormReminders();
    lowStaffFormReminder.idfReminderLevel = 0;
    lowStaffFormReminder.idfPeriodType = this.cmbLowPeriodType.getSelectedIndex();
    lowStaffFormReminder.idfPeriodValue = Number(this.lowPeriodValue);
    this.staffFormReminders.push(lowStaffFormReminder);

    let criticalStaffFormReminder = new StaffFormReminders();
    criticalStaffFormReminder.idfReminderLevel = 1;
    criticalStaffFormReminder.idfPeriodType = this.cmbCriticalPeriodType.getSelectedIndex();
    criticalStaffFormReminder.idfPeriodValue = Number(this.criticalPeriodValue);
    this.staffFormReminders.push(criticalStaffFormReminder);
    return this.staffFormService.SaveStaffFormReminder(this.staffFormReminders);
  }

  prepareidUsers() {
    this.staffFormReminders[0].idfUsers = [];
    let lowFilter = this.cmbLowReminderStaff.getItems().filter(f => f.disabled == true);
    lowFilter.forEach(item => this.staffFormReminders[0].idfUsers.push(item.value));

    this.staffFormReminders[1].idfUsers = [];
    let critialFilter = this.cmbCriticalReminderStaff.getItems().filter(f => f.disabled == true);
    critialFilter.forEach(item => this.staffFormReminders[1].idfUsers.push(item.value));
  }

  loadReminders() {
    if (this.staffFormReminders[0]) {
      this.cmbLowPeriodType.selectIndex(this.staffFormReminders[0].idfPeriodType);
      this.lowPeriodValue = this.staffFormReminders[0].idfPeriodValue;
      setTimeout(() => {
        this.staffFormReminders[0].idfUsers.forEach((user: number) => {
          let items = this.cmbLowReminderStaff.getItems();
          let filter = items.filter(c => c.value == user);
          if (filter != null && filter.length > 0) {
            this.cmbLowReminderStaff.selectItem(filter[0]);
          }
        });

      }, 100);
    }
    if (this.staffFormReminders[1]) {
      this.cmbCriticalPeriodType.selectIndex(this.staffFormReminders[1].idfPeriodType);
      this.criticalPeriodValue = this.staffFormReminders[1].idfPeriodValue;
      setTimeout(() => {
        this.staffFormReminders[1].idfUsers.forEach((user: number) => {
          let items = this.cmbCriticalReminderStaff.getItems();
          let filter = items.filter(c => c.value == user);
          if (filter != null && filter.length > 0) {
            this.cmbCriticalReminderStaff.selectItem(filter[0]);
          }
        });
      }, 100);
    }
  }

  ckbEnabledChange(event, index) {
    this.formFields[index].isEnabled = event.args.checked;
  }

  thereAreRepeatNames(): boolean {
    let thereAreRepeat = false;
    this.formFields.forEach(f => {
      if (this.formFields.filter(filter => (f.name.trim() == filter.name.trim()) && (filter.isEnabled == true)).length > 1)
        thereAreRepeat = true;
    });
    return thereAreRepeat;
  }

  moveUp(i: number) {
    let j = i - 1;
    this.formFields.splice(j, 0, this.formFields.splice(i, 1)[0]);
  }

  moveDown(i: number) {
    let j = i + 1;
    this.formFields.splice(j, 0, this.formFields.splice(i, 1)[0]);
  }

  fillOrderNumbers() {
    let i: number = 0;
    this.formFields.forEach(formField => {
      formField.position = i;
      i++;
    });
  }

  showEditWindow(index: number) {
    this.editIndex = index;
    this.editingRow.placeholder = this.formFields[index].placeholder;
    this.editingRow.description = this.formFields[index].description;
    this.editWindow.open();
  }

  saveClick() {
    this.formFields[this.editIndex].placeholder = this.editingRow.placeholder;
    this.formFields[this.editIndex].description = this.editingRow.description;
    this.editWindow.close();
  }

  cancelClick() {
    this.editWindow.close();
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    let reader = new FileReader();
    reader.readAsDataURL(this.fileToUpload);
    reader.onload = e => {
      this.staffForm.templateFile = e.target.result.toString();
    };

    reader.onerror = function (error) {
      console.log('Error: ', error);
    };
  }
}
