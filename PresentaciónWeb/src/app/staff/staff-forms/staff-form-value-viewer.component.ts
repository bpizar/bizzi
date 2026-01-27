import { Component, OnInit, Input, Output, ViewChild, ChangeDetectorRef, ViewEncapsulation } from '@angular/core';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { EventEmitter } from 'events';
import { jqxWindowComponent } from 'jqwidgets-ng/jqwidgets/jqxwindow';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqwidgets/jqxdropdownlist';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import { jqxButtonComponent } from 'jqwidgets-ng/jqwidgets/jqxbuttons';
import { TranslateService } from '@ngx-translate/core';
import { StaffFormsService } from './staff-forms.service';
import { AuthHelper } from 'src/app/common/helpers/app.auth.helper';
import { Router, ActivatedRoute } from '@angular/router';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { StaffFormByStaff, StaffFormFieldValue, FormField, StaffFormValue, StaffForm } from './staff-forms.component.model';
import { StaffService } from '../staff.service';
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
import { FormGroup, FormControl } from '@angular/forms';
import { BooleanFormFlags } from 'src/app/constants/boolean-form-flags_constants';

@Component({
  selector: 'app-staff-form-value-viewer',
  templateUrl: './staff-form-value-viewer.component.html',
  styleUrls: ['./staff-form-value-viewer.component.css'],
  providers: [StaffService, ConstantService, AuthHelper],
  encapsulation: ViewEncapsulation.None,
})
export class StaffFormValueViewerComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  id: number;
  idfStaff: number;
  idfStaffForm: number;
  formFields: FormField[];
  staffFormFieldValues: StaffFormFieldValue[];
  staffFormGroup: FormGroup;
  report: string;
  constructor(
    public staffFormService: StaffService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
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
        this.load(this.id, this.idfStaffForm);
        this.myLoader.close();
      },
        (error) => {
          this.myLoader.close();
          this.manageError(error);
        });
    }
  }
  load(idStaffFormValue: number, idStaffForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.staffFormService.GetAllFormFieldsByStaffForm(idStaffForm),
      this.staffFormService.GetAllFormFieldValuesByStaffFormValue(idStaffFormValue),
      this.staffFormService.GetStaffFormForEditById(idStaffForm),
    );
    forkJoin(tasks).subscribe(datas => {
      let data0 = datas[0] as any;
      let data1 = datas[1] as any;
      let data2 = datas[2] as any;
      this.report = (data2.staffForm as StaffForm).template;
      if (data0.result) {
        this.formFields = data0.formFields.filter(p => p.isEnabled);
        this.formFields.forEach(formfield => {
          if (formfield.datatype == 'check/no check')
            this.report = this.report.replace(new RegExp("\\$" + formfield.name + "\\$", 'g'), "$" + formfield.id + '~check' + "$");
          else
            if (formfield.datatype == 'yes/no')
              this.report = this.report.replace(new RegExp("\\$" + formfield.name + "\\$", 'g'), "$" + formfield.id + '~yesNo' + "$");
            else
              this.report = this.report.replace(new RegExp("\\$" + formfield.name + "\\$", 'g'), "$" + formfield.id + "$");
        });
      } else {
        this.manageError(data0);
      }
      if (data1.result) {
        this.staffFormFieldValues = data1.staffFormFieldValues;
        this.staffFormFieldValues.forEach((staffFormFieldValue: StaffFormFieldValue) => {
          this.report = this.report.replace(new RegExp("\\$" + staffFormFieldValue.idfFormField + '~check' + "\\$", 'g'), staffFormFieldValue.value == 'true' ? BooleanFormFlags.check : BooleanFormFlags.noCheck);
          this.report = this.report.replace(new RegExp("\\$" + staffFormFieldValue.idfFormField + '~yesNo' + "\\$", 'g'), staffFormFieldValue.value == 'true' ? BooleanFormFlags.yes : BooleanFormFlags.no);
          this.report = this.report.replace(new RegExp("\\$" + staffFormFieldValue.idfFormField + "\\$", 'g'), staffFormFieldValue.value)
        });
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

  print() {
    let printContents, popupWin;
    printContents = document.getElementById('print-section').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title>Print tab</title>
          <style>
          //........Customized style.......
          </style>
        </head>
      <body onload="window.print();">${this.report}</body>
      </html>`
    );
    popupWin.document.close();
  }
}

