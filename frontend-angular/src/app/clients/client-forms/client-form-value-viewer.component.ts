import { Component, OnInit, Input, Output, ViewChild, ChangeDetectorRef, ViewEncapsulation } from '@angular/core';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { EventEmitter } from 'events';
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqxdropdownlist';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { jqxButtonComponent } from 'jqwidgets-ng/jqxbuttons';
import { TranslateService } from '@ngx-translate/core';
import { AuthHelper } from 'src/app/common/helpers/app.auth.helper';
import { Router, ActivatedRoute } from '@angular/router';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { ClientFormByClient, ClientFormFieldValue, FormField, ClientFormValue, ClientForm } from './client-forms.component.model';
import { ClientsService } from '../clients.service';
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
import { FormGroup, FormControl } from '@angular/forms';
import { BooleanFormFlags } from 'src/app/constants/boolean-form-flags_constants';

@Component({
  selector: 'app-client-form-value-viewer',
  templateUrl: './client-form-value-viewer.component.html',
  styleUrls: ['./client-form-value-viewer.component.css'],
  providers: [ClientsService, ConstantService, AuthHelper],
  encapsulation: ViewEncapsulation.None,
})
export class ClientFormValueViewerComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  id: number;
  idfClient: number;
  idfClientForm: number;
  formFields: FormField[];
  clientFormFieldValues: ClientFormFieldValue[];
  clientFormGroup: FormGroup;
  report: string;
  constructor(
    public clientFormService: ClientsService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
  ) {
    activatedRoute.params.subscribe(params => {
      this.id = params.id;
      this.idfClient = params.idClient;
      this.idfClientForm = params.idClientForm;
    });
  }

  ngAfterViewInit(): void {
    if (this.id) {
      this.myLoader.open();
      this.clientFormService.GetClientFormValuesForEditById(this.id).subscribe(data => {
        let clientFormValue = data['clientFormValue'];
        this.idfClient = clientFormValue.idfClient;
        this.idfClientForm = clientFormValue.idfClientForm;
        this.load(this.id, this.idfClientForm);
        this.myLoader.close();
      },
        (error) => {
          this.myLoader.close();
          this.manageError(error);
        });
    }
  }
  load(idClientFormValue: number, idClientForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.clientFormService.GetAllFormFieldsByClientForm(idClientForm),
      this.clientFormService.GetAllFormFieldValuesByClientFormValue(idClientFormValue),
      this.clientFormService.GetClientFormForEditById(idClientForm),
    );
    forkJoin(tasks).subscribe(datas => {
      let data0 = datas[0] as any;
      let data1 = datas[1] as any;
      let data2 = datas[2] as any;
      this.report = (data2.clientForm as ClientForm).template;
      if (data0.result) {
        this.formFields = data0.formFields.filter(formfield => formfield.isEnabled);
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
        this.clientFormFieldValues = data1.clientFormFieldValues;
        this.clientFormFieldValues.forEach((clientFormFieldValue: ClientFormFieldValue) => {
          this.report = this.report.replace(new RegExp("\\$" + clientFormFieldValue.idfFormField + '~check' + "\\$", 'g'), clientFormFieldValue.value == 'true' ? BooleanFormFlags.check : BooleanFormFlags.noCheck);
          this.report = this.report.replace(new RegExp("\\$" + clientFormFieldValue.idfFormField + '~yesNo' + "\\$", 'g'), clientFormFieldValue.value == 'true' ? BooleanFormFlags.yes : BooleanFormFlags.no);
          this.report = this.report.replace(new RegExp("\\$" + clientFormFieldValue.idfFormField + "\\$", 'g'), clientFormFieldValue.value)
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

