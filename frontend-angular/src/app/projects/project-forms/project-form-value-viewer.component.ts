import { Component, OnInit, Input, Output, ViewChild, ChangeDetectorRef, ViewEncapsulation } from '@angular/core';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { EventEmitter } from 'events';
import { jqxWindowComponent } from 'jqwidgets-ng/jqwidgets/jqxwindow';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqwidgets/jqxdropdownlist';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import { jqxButtonComponent } from 'jqwidgets-ng/jqwidgets/jqxbuttons';
import { TranslateService } from '@ngx-translate/core';
import { AuthHelper } from 'src/app/common/helpers/app.auth.helper';
import { Router, ActivatedRoute } from '@angular/router';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { ProjectFormByProject, ProjectFormFieldValue, FormField, ProjectFormValue, ProjectForm } from './project-forms.component.model';
import { ProjectsService } from '../projects.service';
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
import { FormGroup, FormControl } from '@angular/forms';
import { BooleanFormFlags } from 'src/app/constants/boolean-form-flags_constants';

@Component({
  selector: 'app-project-form-value-viewer',
  templateUrl: './project-form-value-viewer.component.html',
  styleUrls: ['./project-form-value-viewer.component.css'],
  providers: [ProjectsService, ConstantService, AuthHelper],
  encapsulation: ViewEncapsulation.None,
})
export class ProjectFormValueViewerComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  id: number;
  idfProject: number;
  idfProjectForm: number;
  formFields: FormField[];
  projectFormFieldValues: ProjectFormFieldValue[];
  projectFormGroup: FormGroup;
  report: string;
  constructor(
    public projectFormService: ProjectsService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
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
        this.load(this.id, this.idfProjectForm);
        this.myLoader.close();
      },
        (error) => {
          this.myLoader.close();
          this.manageError(error);
        });
    }
  }
  load(idProjectFormValue: number, idProjectForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.projectFormService.GetAllFormFieldsByProjectForm(idProjectForm),
      this.projectFormService.GetAllFormFieldValuesByProjectFormValue(idProjectFormValue),
      this.projectFormService.GetProjectFormForEditById(idProjectForm),
    );
    forkJoin(tasks).subscribe(datas => {
      let data0 = datas[0] as any;
      let data1 = datas[1] as any;
      let data2 = datas[2] as any;
      this.report = (data2.projectForm as ProjectForm).template;
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
        this.projectFormFieldValues = data1.projectFormFieldValues;
        this.projectFormFieldValues.forEach((projectFormFieldValue: ProjectFormFieldValue) => {
          this.report = this.report.replace(new RegExp("\\$" + projectFormFieldValue.idfFormField + '~check' + "\\$", 'g'), projectFormFieldValue.value == 'true' ? BooleanFormFlags.check : BooleanFormFlags.noCheck);
          this.report = this.report.replace(new RegExp("\\$" + projectFormFieldValue.idfFormField + '~yesNo' + "\\$", 'g'), projectFormFieldValue.value == 'true' ? BooleanFormFlags.yes : BooleanFormFlags.no);
          this.report = this.report.replace(new RegExp("\\$" + projectFormFieldValue.idfFormField + "\\$", 'g'), projectFormFieldValue.value)
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

