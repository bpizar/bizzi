import { Component, OnInit, ViewChild } from '@angular/core';
import { UploadService } from 'src/app/common/services/app.upload.service';
import { jqxCheckBoxComponent } from 'jqwidgets-ng/jqwidgets/jqxcheckbox';
import { jqxDateTimeInputComponent } from "jqwidgets-ng/jqwidgets/jqxdatetimeinput";
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { StaffService } from '../staff.service';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { forkJoin } from 'rxjs';
import { StaffFormImageValue } from './staff-forms.component.model';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';
import * as moment from 'moment';
import { FileService } from 'src/app/common/services/file.service';

@Component({
  selector: 'app-staff-form-image-value',
  templateUrl: './staff-form-image-value.component.html',
  styleUrls: ['./staff-form-image-value.component.css'],
  providers: [StaffService, ConstantService],
})
export class StaffFormImageValueComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('cmbFormDateTime') cmbFormDateTime: jqxDateTimeInputComponent;
  imagePipe = new ImagePipe();
  id: number;
  idfStaff: number;
  idfStaffForm: number;
  ImagePath: string = '';
  staffFormImageValue: StaffFormImageValue = new StaffFormImageValue()
  // Crop image
  imgSet: boolean = false;
  imageChangedEvent: any = '';
  croppedImage: any = '';
  cropperReady = false;
  selectAtLeastOne: boolean = false;
  template: string = 'ImagePortrait';
  staffName: string;
  staffFormName: string;
  staffInformation: string;
  showInfo: boolean = false;
  fileNameTD: string;

  @ViewChild('checkBoxUsePicture') checkBoxUsePicture: jqxCheckBoxComponent;
  constructor(
    public fileService: FileService,
    public staffFormService: StaffService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public http: HttpClient,
  ) {
    activatedRoute.params.subscribe(params => {
      this.id = params.id;
      this.idfStaff = params.idStaff;
      this.idfStaffForm = params.idStaffForm;
    });
  }

  ngAfterViewInit(): void {
    if (this.idfStaffForm) {
      this.loadData(this.idfStaffForm);
    }
    if (this.id) {
      this.myLoader.open();
      this.staffFormService.GetStaffFormImagenValuesForEditById(this.id).subscribe((data: any) => {
        if (data.result) {
          this.idfStaff = data.staffFormImageValue.idfStaff;
          this.idfStaffForm = data.staffFormImageValue.idfStaffForm;
          this.loadData(this.idfStaffForm);
          let fDateTime = moment(new Date(data.staffFormImageValue.formDateTime));
          this.cmbFormDateTime.setDate(fDateTime.format('M/D/YYYY'));
          this.ImagePath = this.imagePipe.transform(data.staffFormImageValue.image, 'staffFormValues');
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

  loadData(idStaffForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.staffFormService.GetStaffFormForEditById(idStaffForm),
      this.staffFormService.GetStaffForEditById(this.idfStaff),
      this.staffFormService.GetStaffFormForEditById(this.idfStaffForm)
    );
    forkJoin(tasks).subscribe(datas => {
      let data0 = datas[0] as any;
      let data1 = datas[1] as any;
      let data2 = datas[2] as any;
      if (data0.result) {
        this.template = data0.staffForm.template;
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

  manageError = (data: any): void => {
    this.glowMessage.ShowGlowByError(data);
  }

  cancel() {
    this.router.navigate(['/staff/editstaff/', this.idfStaff]);
  }
  imageCroppedBase64(image: string) {
    this.croppedImage = image;
  }

  imageLoaded() {
    this.cropperReady = true;
  }

  imageLoadFailed() {
    // this.glowMessage.msgs=[];
    // this.glowMessage.ShowGlow("error","glow_error","glow_staff_load_image_failed");
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

  save() {
    if ((this.cropperReady && this.checkBoxUsePicture.val()) || (this.id > 0)) {
      this.staffFormImageValue.id = this.id;
      this.staffFormImageValue.idfStaff = this.idfStaff;
      this.staffFormImageValue.idfStaffForm = this.idfStaffForm;
      if (this.croppedImage == '')
        this.staffFormImageValue.image = null;
      else
        this.staffFormImageValue.image = this.croppedImage;
      this.staffFormImageValue.formDateTime = this.cmbFormDateTime.val();
      let body = {
        StaffFormImageValue: this.staffFormImageValue
      }
      this.staffFormService.SaveStaffFormImageValue(body)
        .subscribe(result => {
          if (result)
            this.router.navigate(['/staff/editstaff/', this.idfStaff]);
        });
    }
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
