import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ClientForm, FormField, ClientFormReminders } from './client-forms.component.model';
import { Router, ActivatedRoute } from '@angular/router';
import { ClientsService } from '../clients.service';
import { NgForm } from '@angular/forms';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { AuthHelper } from 'src/app/common/helpers/app.auth.helper';
import { forkJoin } from 'rxjs';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { jqxTabsComponent } from "jqwidgets-ng/jqxtabs";
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqxdropdownlist';
import { jqxDateTimeInputComponent } from "jqwidgets-ng/jqxdatetimeinput";
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { jqxInputComponent } from 'jqwidgets-ng/jqxinput';
import { jqxTextAreaComponent } from 'jqwidgets-ng/jqwidgets/jqxtextarea';
import * as moment from 'moment';
import { StaffService } from 'src/app/staff/staff.service';
import { Staff } from 'src/app/staff/staff-forms/staff-forms.component.model';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.css'],
  providers: [ClientsService, ConstantService, AuthHelper, StaffService],
})
export class ClientFormComponent {
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
  @ViewChild('cmbLowReminderClient') cmbLowReminderClient: jqxDropDownListComponent;
  @ViewChild('cmbCriticalReminderClient') cmbCriticalReminderClient: jqxDropDownListComponent;
  @ViewChild('tabs') tabs: jqxTabsComponent;
  @ViewChild('cmbConstraints') cmbConstraints: jqxDropDownListComponent;
  @ViewChild('editWindow') editWindow: jqxWindowComponent;
  @ViewChild('txtName') txtName: jqxInputComponent;
  @ViewChild('txtPlaceholder') txtPlaceholder: jqxInputComponent;
  @ViewChild('txtDescription') txtDescription: jqxTextAreaComponent;
  @ViewChild('cmbDatatype') cmbDatatype: jqxDropDownListComponent;
  clientForm: ClientForm = new ClientForm();
  formFields: FormField[] = [];
  formField: FormField = new FormField();
  clientFormReminders: ClientFormReminders[] = [];
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
  criticalPeriodValue: number = 1;
  dayMonth: number = 1;
  recurrenceDetail: number;
  editorvalue = "";
  editingRow = new FormField();
  editIndex: number = 0;
  fileToUpload: File | null = null;

  constructor(
    public staffService: StaffService,
    public clientFormService: ClientsService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
  ) {
    activatedRoute.params.subscribe(params => {
      this.id = params.id;
      this.clientForm.id = -1;
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

  load(IdClientForm: number) {
    this.myLoader.open();
    if (IdClientForm > 0) {
      const tasks = [];
      tasks.push(
        this.staffService.GetAllStaff(),
        this.clientFormService.GetClientFormForEditById(IdClientForm),
        this.clientFormService.GetAllFormFieldsByClientForm(IdClientForm),
        this.clientFormService.GetClientFormRemindersByClientForm(IdClientForm)
      );
      forkJoin(tasks).subscribe(results => {
        if ((results[0] as any).result) {
          this.loadStaff(results[0]);
        }
        if ((results[1] as any).result) {
          this.clientForm = (results[1] as any).clientForm;
          if (this.clientForm.template === 'ImagePortrait') {
            this.cmbTemplateType.selectIndex(0);
          }
          else if (this.clientForm.template == 'ImageLandscape') {
            this.cmbTemplateType.selectedIndex(1);
          }
          else {
            this.cmbTemplateType.selectedIndex(2);
            this.editorvalue = this.clientForm.template;
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
          this.clientFormReminders = (results[3] as any).clientFormReminders;
          this.loadReminders();
        }
        this.myLoader.close();
      },
        (error) => {
          this.myLoader.close();
          this.manageError(error);
          this.router.navigate(['/client-forms']);
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
          this.clientForm.template = 'ImagePortrait';
          break;
        case 1:
          this.clientForm.template = 'ImageLandscape';
          break;
        case 2:
          this.clientForm.template = this.editorvalue;
      }
      this.clientForm.idfRecurrence = this.recurrence;
      if (this.recurrence == 4)
        this.clientForm.idfRecurrenceDetail = this.dayMonth;
      else
        this.clientForm.idfRecurrenceDetail = this.recurrenceDetail;
      this.prepareReminders();
      this.prepareidUsers();
      let body = {
        clientForm: this.clientForm,
        clientFormReminders: this.clientFormReminders,
        formFields: this.formFields
      }
      this.clientFormService.SaveClientForm(body)
        .subscribe(
          (data: any) => {

            if (data.result) {

              this.back();
            }
            // f.reset(f.value);
            // if (this.id > 0) {
            // Mensajes.Mostrar('Ligero', 'success', 'Unidad Actualizado', clientForm.Nombre)
            // } else {
            // Mensajes.Mostrar('Ligero', 'success', 'Unidad Creado', clientForm.Nombre)
            // };
            // if (!this.modal) {
            // this.clientForm.id = clientForm.id;
            // this.router.navigate(['/unidad', clientForm.id ]);
            // } else {
            //   this.modalService.notificacion.emit(clientForm);
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
    this.router.navigate(['/clients/client-forms']);
    // } else {
    // this.modalService.ocultarModal();
    // }
  }

  removeFormFieldFromClientForm(idClientForm: number, formField: FormField) {
    this.clientFormService.RemoveClientFormField(idClientForm, formField.id)
      .subscribe(() => this.load(idClientForm));
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
    this.cmbRecurrence.selectIndex(this.clientForm.idfRecurrence);
    this.recurrenceItem = this.recurrenceList[this.clientForm.idfRecurrence];
    switch (this.clientForm.idfRecurrence) {
      case 0:
        this.recurrenceDetail = 0;
        break;
      case 1:
        this.dtiHour.setDate(this.militaryToHour(this.clientForm.idfRecurrenceDetail));
        break;
      case 2:
        this.cmbWeekday.selectIndex(this.clientForm.idfRecurrenceDetail);
        break;
      case 3:
        this.cmbTwoTimesWeek.selectIndex(this.clientForm.idfRecurrenceDetail);
        break;
      case 4:
        this.dayMonth = this.clientForm.idfRecurrenceDetail;
        break;
      case 5:
        this.dtiDate.setDate(moment().dayOfYear(this.clientForm.idfRecurrenceDetail).format('M/D/YYYY'));
        break;
      default:
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
    this.clientFormReminders = [];
    let lowClientFormReminder = new ClientFormReminders();
    lowClientFormReminder.idfReminderLevel = 0;
    lowClientFormReminder.idfPeriodType = this.cmbLowPeriodType.getSelectedIndex();
    lowClientFormReminder.idfPeriodValue = Number(this.lowPeriodValue);
    this.clientFormReminders.push(lowClientFormReminder);

    let criticalClientFormReminder = new ClientFormReminders();
    criticalClientFormReminder.idfReminderLevel = 1;
    criticalClientFormReminder.idfPeriodType = this.cmbCriticalPeriodType.getSelectedIndex();
    criticalClientFormReminder.idfPeriodValue = Number(this.criticalPeriodValue);
    this.clientFormReminders.push(criticalClientFormReminder);
    return this.clientFormService.SaveClientFormReminder(this.clientFormReminders);
  }

  prepareidUsers() {
    this.clientFormReminders[0].idfUsers = [];
    let lowFilter = this.cmbLowReminderClient.getItems().filter(f => f.disabled == true);
    lowFilter.forEach(item => this.clientFormReminders[0].idfUsers.push(item.value));

    this.clientFormReminders[1].idfUsers = [];
    let critialFilter = this.cmbCriticalReminderClient.getItems().filter(f => f.disabled == true);
    critialFilter.forEach(item => this.clientFormReminders[1].idfUsers.push(item.value));
  }

  loadReminders() {
    if (this.clientFormReminders[0]) {
      this.cmbLowPeriodType.selectIndex(this.clientFormReminders[0].idfPeriodType);
      this.lowPeriodValue = this.clientFormReminders[0].idfPeriodValue;
      setTimeout(() => {
        this.clientFormReminders[0].idfUsers.forEach((user: number) => {
          let items = this.cmbLowReminderClient.getItems();
          let filter = items.filter(c => c.value == user);
          if (filter != null && filter.length > 0) {
            this.cmbLowReminderClient.selectItem(filter[0]);
          }
        });
      }, 100);
    }
    if (this.clientFormReminders[1]) {
      this.cmbCriticalPeriodType.selectIndex(this.clientFormReminders[1].idfPeriodType);
      this.criticalPeriodValue = this.clientFormReminders[1].idfPeriodValue;
      setTimeout(() => {
        this.clientFormReminders[1].idfUsers.forEach((user: number) => {
          let items = this.cmbCriticalReminderClient.getItems();
          let filter = items.filter(c => c.value == user);
          if (filter != null && filter.length > 0) {
            this.cmbCriticalReminderClient.selectItem(filter[0]);
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
      this.clientForm.templateFile = e.target.result.toString();
    };

    reader.onerror = function (error) {
      console.log('Error: ', error);
    };
  }
}
