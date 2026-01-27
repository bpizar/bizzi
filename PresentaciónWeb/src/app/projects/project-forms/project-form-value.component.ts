import { Component, OnInit, ViewChild } from '@angular/core';
import { ProjectsService } from '../projects.service';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthHelper } from 'src/app/common/helpers/app.auth.helper';
import { ProjectForm, ProjectFormValue, ProjectFormFieldValue } from './project-forms.component.model';
import { FormGroup, FormControl } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { jqxInputComponent } from 'jqwidgets-ng/jqxinput';
import { jqxButtonComponent } from "jqwidgets-ng/jqxbuttons";
import { jqxCheckBoxComponent } from 'jqwidgets-ng/jqxcheckbox';
import { jqxDateTimeInputComponent } from "jqwidgets-ng/jqxdatetimeinput";
import { FormField } from 'src/app/clients/client-forms/client-forms.component.model';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import * as moment from 'moment';
import { ValidationService } from 'src/app/common/services/validations.service';
import { FileService } from 'src/app/common/services/file.service';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-project-form-value',
  templateUrl: './project-form-value.component.html',
  styleUrls: ['./project-form-value.component.css'],
  providers: [ProjectsService, ConstantService, AuthHelper, ValidationService],
})
export class ProjectFormValueComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('cmbFormDateTime') cmbFormDateTime: jqxDateTimeInputComponent;
  id: number;
  idfProject: number;
  idfProjectForm: number;
  formFields: FormField[];
  projectFormFieldValues: ProjectFormFieldValue[];
  projectFormGroup: FormGroup = new FormGroup({});;
  formDateTime: string;
  projectName: string;
  projectFormName: string;
  projectInformation: string;
  showInfo: boolean = false;
  fileNameTD: string;

  constructor(
    public fileService: FileService,
    public projectFormService: ProjectsService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public validationService: ValidationService
  ) {
    activatedRoute.params.subscribe(params => {
      this.id = params.id;
      this.idfProject = params.idProject;
      this.idfProjectForm = params.idProjectForm;
    });
  }

  ngAfterViewInit(): void {
    if (this.id) {
      this.myLoader.open();
      this.projectFormService.GetProjectFormValuesForEditById(this.id).subscribe(data => {
        let projectFormValue = data['projectFormValue'];
        this.idfProject = projectFormValue.idfProject;
        this.idfProjectForm = projectFormValue.idfProjectForm;
        this.formDateTime = projectFormValue.formDateTime;
        let fDateTime = moment(new Date(projectFormValue.formDateTime));
        this.cmbFormDateTime.setDate(fDateTime.format('M/D/YYYY'));
        if (this.idfProject && this.idfProjectForm) {
          this.loadStructure(this.idfProjectForm);
        }
        this.loadWithData(this.id, this.idfProjectForm);
        this.myLoader.close();
      },
        (error) => {
          this.myLoader.close();
          this.manageError(error);
        });
    }
    if (this.idfProject && this.idfProjectForm) {
      this.loadStructure(this.idfProjectForm);
    }
  }

  loadStructure(idProjectForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.projectFormService.GetAllFormFieldsByProjectForm(idProjectForm),
      this.projectFormService.GetProject(this.idfProject),
      this.projectFormService.GetProjectFormForEditById(this.idfProjectForm)
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
        this.projectFormGroup = new FormGroup(group);
      } else {
        this.manageError(data0);
      }
      if (data1.result) {
        this.projectName = data1.project.description;
      } else {
        this.manageError(data1);
      }
      if (data2.result) {
        this.projectFormName = data2.projectForm.name;
        this.projectInformation = data2.projectForm.information;
        this.fileNameTD = data2.projectForm.templateFile;
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

  loadWithData(idProjectFormValue: number, idProjectForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.projectFormService.GetAllFormFieldsByProjectForm(idProjectForm),
      this.projectFormService.GetAllFormFieldValuesByProjectFormValue(idProjectFormValue)
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
        this.projectFormGroup = new FormGroup(group);
      } else {
        this.manageError(data0);
      }
      // this.myLoader.close();
      if (data1.result) {
        this.projectFormFieldValues = data1.projectFormFieldValues;
        let values = {};
        this.projectFormFieldValues.forEach((element: ProjectFormFieldValue) => {
          let formfield = this.formFields.find(x => x.id = element.idfFormField)
          if (formfield) {
            if (element.value == 'true')
              formfield.isChecked = true;
            if (element.value == 'false')
              formfield.isChecked = false;
            values[element.idfFormField] = element.value;
          }
        });
        this.projectFormGroup.patchValue(
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
    this.router.navigate(['/projects/editproject/', this.idfProject]);
  }
  save(f) {
    this.formDateTime = this.cmbFormDateTime.val();
    let projectFormValue = new ProjectFormValue();
    if (this.id > 0)
      projectFormValue.id = this.id;
    else
      projectFormValue.id = -1;
    projectFormValue.idfProject = this.idfProject;
    projectFormValue.idfProjectForm = this.idfProjectForm;
    projectFormValue.formDateTime = DateTime.fromJSDate(new Date(this.formDateTime)).toISO();
    let projectFormFieldValues: ProjectFormFieldValue[] = [];
    let i = 0;
    this.validationService.arrayErrors = [];
    Object.keys(this.projectFormGroup.controls).forEach(key => {
      let projectFormFieldValue = new ProjectFormFieldValue;
      projectFormFieldValue.idfFormField = +key;
      projectFormFieldValue.value = this.projectFormGroup.get(key).value;
      projectFormFieldValues.push(projectFormFieldValue);
      this.validationService.setFormField(this.formFields[i], projectFormFieldValue.value);
      i++;
    });
    if (this.validationService.arrayErrors.length == 0) {
      let body = {
        ProjectFormValue: projectFormValue,
        ProjectFormFieldValues: projectFormFieldValues
      }
      this.projectFormService.SaveProjectFormValue(body).subscribe(
        result => {
          this.glowMessage.ShowGlow("success", "glow_success", "glow_project_saved_successfully");
          this.router.navigate(['/projects/editproject/', this.idfProject]);
        }
      );
    }
    else {
      this.glowMessage.ShowGlow("error", "Data Error", this.validationService.getBuildedMessage());
    }
  }

  onRadioChange(event, id) {
    this.projectFormGroup.controls[id].setValue(event.args.checked);
  }

  downloadTemplateFile() {
    this.fileService.GetProjectFormTemplate(this.fileNameTD)
      .subscribe((data: Blob) => {
        const url = window.URL.createObjectURL(data);
        const anchor = document.createElement('a');
        anchor.download = `TemplateFile_${this.projectFormName}_${this.idfProjectForm}.${this.fileNameTD.split(".")[1]}`
        anchor.href = url;
        anchor.click();
      },
        error => {
          this.manageError("error");
        }
      );
  }
}
