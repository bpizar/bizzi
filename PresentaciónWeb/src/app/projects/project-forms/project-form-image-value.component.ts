import { Component, OnInit, ViewChild } from '@angular/core';
import { UploadService } from 'src/app/common/services/app.upload.service';
import { jqxCheckBoxComponent } from 'jqwidgets-ng/jqxcheckbox';
import { jqxDateTimeInputComponent } from "jqwidgets-ng/jqxdatetimeinput";
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectsService } from '../projects.service';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { forkJoin } from 'rxjs';
import { ProjectFormImageValue } from './project-forms.component.model';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';
import * as moment from 'moment';
import { FileService } from 'src/app/common/services/file.service';

@Component({
  selector: 'app-project-form-image-value',
  templateUrl: './project-form-image-value.component.html',
  styleUrls: ['./project-form-image-value.component.css'],
  providers: [ProjectsService, ConstantService],
})
export class ProjectFormImageValueComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('cmbFormDateTime') cmbFormDateTime: jqxDateTimeInputComponent;
  imagePipe = new ImagePipe();
  id: number;
  idfProject: number;
  idfProjectForm: number;
  ImagePath: string = '';
  projectFormImageValue: ProjectFormImageValue = new ProjectFormImageValue()
  // Crop image
  imgSet: boolean = false;
  imageChangedEvent: any = '';
  croppedImage: any = '';
  cropperReady = false;
  selectAtLeastOne: boolean = false;
  template: string = 'ImagePortrait';
  projectName: string;
  projectFormName: string;
  projectInformation: string;
  showInfo: boolean = false;
  fileNameTD: string;

  @ViewChild('checkBoxUsePicture') checkBoxUsePicture: jqxCheckBoxComponent;
  constructor(
    public fileService: FileService,
    public projectFormService: ProjectsService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public http: HttpClient,
  ) {
    activatedRoute.params.subscribe(params => {
      this.id = params.id;
      this.idfProject = params.idProject;
      this.idfProjectForm = params.idProjectForm;
    });
  }

  ngAfterViewInit(): void {
    if (this.idfProjectForm) {
      this.loadData(this.idfProjectForm);
    }
    if (this.id) {
      this.myLoader.open();
      this.projectFormService.GetProjectFormImagenValuesForEditById(this.id).subscribe((data: any) => {
        if (data.result) {
          this.idfProject = data.projectFormImageValue.idfProject;
          this.idfProjectForm = data.projectFormImageValue.idfProjectForm;
          this.loadData(this.idfProjectForm);
          let fDateTime = moment(new Date(data.projectFormImageValue.formDateTime));
          this.cmbFormDateTime.setDate(fDateTime.format('M/D/YYYY'));
          this.ImagePath = this.imagePipe.transform(data.projectFormImageValue.image, 'projectFormValues');
        } else {
          this.manageError(data);
        }
        this.myLoader.close();
      },
        (error) => {
          this.myLoader.close();
          this.manageError(error);
        });
    }
  }

  loadData(idProjectForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.projectFormService.GetProjectFormForEditById(idProjectForm),
      this.projectFormService.GetProject(this.idfProject),
      this.projectFormService.GetProjectFormForEditById(this.idfProjectForm)
    );
    forkJoin(tasks).subscribe(datas => {
      let data0 = datas[0] as any;
      let data1 = datas[1] as any;
      let data2 = datas[2] as any;
      if (data0.result) {
        this.template = data0.projectForm.template;
      } else {
        this.manageError(data0);
      }
      if (data1.result) {
        this.projectName = data1.project.projectName;
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

  load(idProject: number, idProjectForm: number) {
    const tasks = [];
    tasks.push(
      // this.projectFormService.GetAllFormFieldsByProjectForm(idProjectForm),
      this.projectFormService.GetProjectFormImageValueByProjectFormAndProject(idProjectForm, idProject)
    );
    forkJoin(tasks).subscribe(datas => {
      let data0 = datas[0] as any;
      let data1 = datas[1] as any;
      if (data0.result) {
        this.projectFormImageValue = data0.projectFormImageValue;
        this.ImagePath = this.imagePipe.transform(this.projectFormImageValue.image, 'projectFormValues');
      } else {
        // this.manageError(data);
      }
    },
      (error) => {
        // this.myLoader.close();
        // this.manageError(error);
      });

  }
  cancel() {
    this.router.navigate(['/projects/editproject/', this.idfProject]);

  }
  imageCroppedBase64(image: string) {
    this.croppedImage = image;
  }

  imageLoaded() {
    this.cropperReady = true;
  }

  imageLoadFailed() {
    // this.glowMessage.msgs=[];
    // this.glowMessage.ShowGlow("error","glow_error","glow_project_load_image_failed");
  }
  fileChangeEvent(event: any): void {
    // if(this.canDeleteOrSave)
    // {      
    this.imgSet = event.srcElement.files.length > 0;
    this.selectAtLeastOne = this.selectAtLeastOne || this.imgSet;
    this.imageChangedEvent = event;
    this.checkBoxUsePicture.val(true);
    // }
  }
  checkBoxUseThisPictureChange(event: any): void {
    let checked = event.args.checked;
    if (checked) {
      this.imgSet = true;
    }
    else {
      this.imgSet = false;
    }
  }

  manageError = (data: any): void => {
    this.glowMessage.ShowGlowByError(data);
  }

  save() {
    if ((this.cropperReady && this.checkBoxUsePicture.val()) || (this.id > 0)) {
      this.projectFormImageValue.id = this.id;
      this.projectFormImageValue.idfProject = this.idfProject;
      this.projectFormImageValue.idfProjectForm = this.idfProjectForm;
      if (this.croppedImage == '')
        this.projectFormImageValue.image = null;
      else
        this.projectFormImageValue.image = this.croppedImage;
      this.projectFormImageValue.formDateTime = this.cmbFormDateTime.val();
      let body = {
        ProjectFormImageValue: this.projectFormImageValue
      }
      this.projectFormService.SaveProjectFormImageValue(body)
        .subscribe(result => {
          if (result)
            this.router.navigate(['/projects/editproject/', this.idfProject]);
        });
    }
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
