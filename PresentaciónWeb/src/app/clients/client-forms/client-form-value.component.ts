import { Component, OnInit, ViewChild } from '@angular/core';
import { ClientsService } from '../clients.service';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthHelper } from 'src/app/common/helpers/app.auth.helper';
import { ClientForm, ClientFormValue, ClientFormFieldValue } from './client-forms.component.model';
import { FormGroup, FormControl } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { jqxInputComponent } from 'jqwidgets-ng/jqwidgets/jqxinput';
import { jqxButtonComponent } from "jqwidgets-ng/jqwidgets/jqxbuttons";
import { jqxCheckBoxComponent } from 'jqwidgets-ng/jqwidgets/jqxcheckbox';
import { jqxDateTimeInputComponent } from "jqwidgets-ng/jqwidgets/jqxdatetimeinput";
import { FormField } from 'src/app/clients/client-forms/client-forms.component.model';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import * as moment from 'moment';
import { ValidationService } from 'src/app/common/services/validations.service';
import { FileService } from 'src/app/common/services/file.service';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-client-form-value',
  templateUrl: './client-form-value.component.html',
  styleUrls: ['./client-form-value.component.css'],
  providers: [ClientsService, ConstantService, AuthHelper, ValidationService, ClientsService],
})
export class ClientFormValueComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('cmbFormDateTime') cmbFormDateTime: jqxDateTimeInputComponent;
  id: number;
  idfClient: number;
  idfClientForm: number;
  formFields: FormField[];
  clientFormFieldValues: ClientFormFieldValue[];
  clientFormGroup: FormGroup = new FormGroup({});
  formDateTime: string;
  clientName: string;
  clientFormName: string;
  clientInformation: string;
  showInfo: boolean = false;
  fileNameTD: string;

  constructor(
    public fileService: FileService,
    public clientFormService: ClientsService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public validationService: ValidationService,
    public clientsService: ClientsService
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
        this.formDateTime = clientFormValue.formDateTime;
        let fDateTime = moment(new Date(clientFormValue.formDateTime));
        this.cmbFormDateTime.setDate(fDateTime.format('M/D/YYYY'));
        if (this.idfClient && this.idfClientForm) {
          this.loadStructure(this.idfClientForm);
        }
        this.loadWithData(this.id, this.idfClientForm);
        this.myLoader.close();
      },
        (error) => {
          this.myLoader.close();
          this.manageError(error);
        });
    }
    if (this.idfClient && this.idfClientForm) {
      this.loadStructure(this.idfClientForm);
    }
  }

  loadStructure(idClientForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.clientFormService.GetAllFormFieldsByClientForm(idClientForm),
      this.clientsService.GetClient(this.idfClient),
      this.clientFormService.GetClientFormForEditById(this.idfClientForm)
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
        this.clientFormGroup = new FormGroup(group);
      } else {
        this.manageError(data0);
      }
      if (data1.result) {
        this.clientName = data1.client.fullName;
      } else {
        this.manageError(data1);
      }
      if (data2.result) {
        this.clientFormName = data2.clientForm.name;
        this.clientInformation = data2.clientForm.information;
        this.fileNameTD = data2.clientForm.templateFile;
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

  loadWithData(idClientFormValue: number, idClientForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.clientFormService.GetAllFormFieldsByClientForm(idClientForm),
      this.clientFormService.GetAllFormFieldValuesByClientFormValue(idClientFormValue)
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
        this.clientFormGroup = new FormGroup(group);
      } else {
        this.manageError(data0);
      }
      // this.myLoader.close();
      if (data1.result) {
        this.clientFormFieldValues = data1.clientFormFieldValues;
        let values = {};
        this.clientFormFieldValues.forEach((element: ClientFormFieldValue) => {
          let formfield = this.formFields.find(x => x.id = element.idfFormField)
          if (formfield) {
            if (element.value == 'true')
              formfield.isChecked = true;
            if (element.value == 'false')
              formfield.isChecked = false;
            values[element.idfFormField] = element.value;
          }
        });
        this.clientFormGroup.patchValue(
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
    this.router.navigate(['/clients/editclient/', this.idfClient]);
  }
  save(f) {
    this.formDateTime = this.cmbFormDateTime.val();
    let clientFormValue = new ClientFormValue();
    if (this.id > 0)
      clientFormValue.id = this.id;
    else
      clientFormValue.id = -1;
    clientFormValue.idfClient = this.idfClient;
    clientFormValue.idfClientForm = this.idfClientForm;
    clientFormValue.formDateTime = DateTime.fromJSDate(new Date(this.formDateTime)).toISO();
    let clientFormFieldValues: ClientFormFieldValue[] = [];
    let i = 0;
    this.validationService.arrayErrors = [];
    Object.keys(this.clientFormGroup.controls).forEach(key => {
      let clientFormFieldValue = new ClientFormFieldValue;
      clientFormFieldValue.idfFormField = +key;
      clientFormFieldValue.value = this.clientFormGroup.get(key).value;
      clientFormFieldValues.push(clientFormFieldValue);
      this.validationService.setFormField(this.formFields[i], clientFormFieldValue.value);
      i++;
    });
    if (this.validationService.arrayErrors.length == 0) {
      let body = {
        ClientFormValue: clientFormValue,
        ClientFormFieldValues: clientFormFieldValues
      }
      this.clientFormService.SaveClientFormValue(body).subscribe(
        result => {
          this.glowMessage.ShowGlow("success", "glow_success", "glow_client_saved_successfully");
          this.router.navigate(['/clients/editclient/', this.idfClient]);
        }
      );
    }
    else {
      this.glowMessage.ShowGlow("error", "Data Error", this.validationService.getBuildedMessage());
    }
  }

  onRadioChange(event, id) {
    this.clientFormGroup.controls[id].setValue(event.args.checked);
  }

  downloadTemplateFile() {
    this.fileService.GetClientFormTemplate(this.fileNameTD)
      .subscribe((data: Blob) => {
        const url = window.URL.createObjectURL(data);
        const anchor = document.createElement('a');
        anchor.download = `TemplateFile_${this.clientFormName}_${this.idfClientForm}.${this.fileNameTD.split(".")[1]}`
        anchor.href = url;
        anchor.click();
      },
        error => {
          this.manageError("error");
        }
      );
  }
}
