import { Component, OnInit, ViewChild } from '@angular/core';
import { StaffService } from '../staff.service';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthHelper } from 'src/app/common/helpers/app.auth.helper';
import { StaffForm, StaffFormValue, StaffFormFieldValue } from './staff-forms.component.model';
import { FormGroup, FormControl } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { jqxInputComponent } from 'jqwidgets-ng/jqxinput';
import { jqxButtonComponent } from "jqwidgets-ng/jqxbuttons";
import { jqxCheckBoxComponent } from 'jqwidgets-ng/jqxcheckbox';
import { jqxRadioButtonComponent } from 'jqwidgets-ng/jqxradiobutton';
import { jqxDateTimeInputComponent } from "jqwidgets-ng/jqxdatetimeinput";
import { jqxTextAreaComponent } from 'jqwidgets-ng/jqxtextarea';
import { FormField } from 'src/app/clients/client-forms/client-forms.component.model';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import * as moment from 'moment';
import { ValidationService } from 'src/app/common/services/validations.service';
import { FileService } from 'src/app/common/services/file.service';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-staff-form-value',
  templateUrl: './staff-form-value.component.html',
  styleUrls: ['./staff-form-value.component.css'],
  providers: [StaffService, ConstantService, AuthHelper, ValidationService],
})
export class StaffFormValueComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('cmbFormDateTime') cmbFormDateTime: jqxDateTimeInputComponent;
  id: number;
  idfStaff: number;
  idfStaffForm: number;
  formFields: FormField[];
  staffFormFieldValues: StaffFormFieldValue[];
  staffFormGroup: FormGroup = new FormGroup({});
  formDateTime: string;
  staffName: string;
  staffFormName: string;
  staffInformation: string;
  showInfo: boolean = false;
  fileNameTD: string;

  constructor(
    public fileService: FileService,
    public staffFormService: StaffService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public validationService: ValidationService
  ) {
    activatedRoute.params.subscribe(params => {
      this.id = params.id;
      this.idfStaff = params.idStaff;
      this.idfStaffForm = params.idStaffForm;
    });

  }

  ngAfterViewInit(): void {
    if (this.id) {
      this.myLoader.open();
      this.staffFormService.GetStaffFormValuesForEditById(this.id).subscribe(data => {
        let staffFormValue = data['staffFormValue'];
        this.idfStaff = staffFormValue.idfStaff;
        this.idfStaffForm = staffFormValue.idfStaffForm;
        this.formDateTime = staffFormValue.formDateTime;
        let fDateTime = moment(new Date(staffFormValue.formDateTime));
        this.cmbFormDateTime.setDate(fDateTime.format('M/D/YYYY'));
        if (this.idfStaff && this.idfStaffForm) {
          this.loadStructure(this.idfStaffForm);
        }
        this.loadWithData(this.id, this.idfStaffForm);
        this.myLoader.close();
      },
        (error) => {
          this.myLoader.close();
          this.manageError(error);
        });
    }
    if (this.idfStaff && this.idfStaffForm) {
      this.loadStructure(this.idfStaffForm);
    }
  }

  loadStructure(idStaffForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.staffFormService.GetAllFormFieldsByStaffForm(idStaffForm),
      this.staffFormService.GetStaffForEditById(this.idfStaff),
      this.staffFormService.GetStaffFormForEditById(this.idfStaffForm)
    );
    forkJoin(tasks).subscribe(datas => {
      let data0 = datas[0] as any;
      let data1 = datas[1] as any;
      let data2 = datas[2] as any;
      if (data0.result) {
        this.formFields = data0.formFields.filter(formfield => formfield.isEnabled);
        let group = {};
        this.formFields.forEach(input_template => {
          group[input_template.id] = new FormControl('');
        })
        this.staffFormGroup = new FormGroup(group);
      } else {
        this.manageError(data0);
      }
      if (data1.result) {
        this.staffName = data1.staff.idfUserNavigation.firstName + ' ' + data1.staff.idfUserNavigation.lastName;
      } else {
        this.manageError(data1);
      }
      if (data2.result) {
        this.staffFormName = data2.staffForm.name;
        this.staffInformation = data2.staffForm.information;
        this.fileNameTD = data2.staffForm.templateFile;
      } else {
        this.manageError(data2);
      }
      this.myLoader.close();
    },
      (error) => {
        this.myLoader.close();
        this.manageError(error);
      });
  }

  loadWithData(idStaffFormValue: number, idStaffForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.staffFormService.GetAllFormFieldsByStaffForm(idStaffForm),
      this.staffFormService.GetAllFormFieldValuesByStaffFormValue(idStaffFormValue)
    );
    forkJoin(tasks).subscribe(datas => {
      let data0 = datas[0] as any;
      let data1 = datas[1] as any;
      if (data0.result) {
        this.formFields = data0.formFields.filter(formfield => formfield.isEnabled);
        let group = {};
        this.formFields.forEach(input_template => {
          group[input_template.id] = new FormControl('');
        })
        this.staffFormGroup = new FormGroup(group);
      } else {
        this.manageError(data0);
      }
      // this.myLoader.close();
      if (data1.result) {
        this.staffFormFieldValues = data1.staffFormFieldValues;
        let values = {};
        this.staffFormFieldValues.forEach((element: StaffFormFieldValue) => {
          let formfield = this.formFields.find(x => x.id = element.idfFormField)
          if (formfield) {
            if (element.value == 'true')
              formfield.isChecked = true;
            if (element.value == 'false')
              formfield.isChecked = false;
            values[element.idfFormField] = element.value;
          }
        });
        this.staffFormGroup.patchValue(
          values
        );
      } else {
        this.manageError(data1);
      }
      this.myLoader.close();
    },
      (error) => {
        this.myLoader.close();
        this.manageError(error);
      });
  }

  manageError = (data: any): void => {
    this.glowMessage.ShowGlowByError(data);
  }

  cancel() {
    this.router.navigate(['/staff/editstaff/', this.idfStaff]);
  }
  save(f) {
    this.formDateTime = this.cmbFormDateTime.val();
    let staffFormValue = new StaffFormValue();
    if (this.id > 0)
      staffFormValue.id = this.id;
    else
      staffFormValue.id = -1;
    staffFormValue.idfStaff = this.idfStaff;
    staffFormValue.idfStaffForm = this.idfStaffForm;
    staffFormValue.formDateTime = DateTime.fromJSDate(new Date(this.formDateTime)).toISO();
    let staffFormFieldValues: StaffFormFieldValue[] = [];
    let i = 0;
    this.validationService.arrayErrors = [];
    Object.keys(this.staffFormGroup.controls).forEach(key => {
      let staffFormFieldValue = new StaffFormFieldValue;
      staffFormFieldValue.idfFormField = +key;
      staffFormFieldValue.value = this.staffFormGroup.get(key).value;
      staffFormFieldValues.push(staffFormFieldValue);
      this.validationService.setFormField(this.formFields[i], staffFormFieldValue.value);
      i++;
    });
    if (this.validationService.arrayErrors.length == 0) {
      let body = {
        StaffFormValue: staffFormValue,
        StaffFormFieldValues: staffFormFieldValues
      }
      this.staffFormService.SaveStaffFormValue(body).subscribe(
        result => {
          this.glowMessage.ShowGlow("success", "glow_success", "glow_staff_saved_successfully");
          this.router.navigate(['/staff/editstaff/', this.idfStaff]);
        }
      );
    }
    else {
      this.glowMessage.ShowGlow("error", "Data Error", this.validationService.getBuildedMessage());
    }
  }

  onRadioChange(event, id) {
    this.staffFormGroup.controls[id].setValue(event.args.checked);
  }

  downloadTemplateFile() {
    this.fileService.GetStaffFormTemplate(this.fileNameTD)
      .subscribe((data: Blob) => {
        const url = window.URL.createObjectURL(data);
        const anchor = document.createElement('a');
        anchor.download = `TemplateFile_${this.staffFormName}_${this.idfStaffForm}.${this.fileNameTD.split(".")[1]}`
        anchor.href = url;
        anchor.click();
      },
        error => {
          this.manageError("error");
        }
      );
  }
}
