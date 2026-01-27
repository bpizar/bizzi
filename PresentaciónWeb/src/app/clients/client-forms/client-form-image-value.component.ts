import { Component, OnInit, ViewChild } from '@angular/core';
import { UploadService } from 'src/app/common/services/app.upload.service';
import { jqxCheckBoxComponent } from 'jqwidgets-ng/jqwidgets/jqxcheckbox';
import { jqxButtonComponent } from 'jqwidgets-ng/jqwidgets/jqxbuttons';
import { jqxDateTimeInputComponent } from "jqwidgets-ng/jqwidgets/jqxdatetimeinput";
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientsService } from '../clients.service';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { forkJoin } from 'rxjs';
import { ClientFormImageValue } from './client-forms.component.model';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqwidgets/jqxloader';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';
import * as moment from 'moment';
import { FileService } from 'src/app/common/services/file.service';

@Component({
  selector: 'app-client-form-image-value',
  templateUrl: './client-form-image-value.component.html',
  styleUrls: ['./client-form-image-value.component.css'],
  providers: [ClientsService, ConstantService],
})
export class ClientFormImageValueComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('cmbFormDateTime') cmbFormDateTime: jqxDateTimeInputComponent;
  imagePipe = new ImagePipe();
  id: number;
  idfClient: number;
  idfClientForm: number;
  ImagePath: string = '';
  clientFormImageValue: ClientFormImageValue = new ClientFormImageValue()
  // Crop image
  imgSet: boolean = false;
  imageChangedEvent: any = '';
  croppedImage: any = '';
  cropperReady = false;
  selectAtLeastOne: boolean = false;
  template: string = 'ImagePortrait';
  clientName: string;
  clientFormName: string;
  clientInformation: string;
  showInfo: boolean = false;
  fileNameTD: string;

  @ViewChild('checkBoxUsePicture') checkBoxUsePicture: jqxCheckBoxComponent;
  constructor(
    public fileService: FileService,
    public clientFormService: ClientsService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public http: HttpClient,
  ) {
    activatedRoute.params.subscribe(params => {
      this.id = params.id;
      this.idfClient = params.idClient;
      this.idfClientForm = params.idClientForm;
    });
  }

  ngAfterViewInit(): void {
    if (this.idfClientForm) {
      this.loadData(this.idfClientForm);
    }
    if (this.id) {
      this.myLoader.open();
      this.clientFormService.GetClientFormImagenValuesForEditById(this.id).subscribe((data: any) => {
        if (data.result) {
          this.idfClient = data.clientFormImageValue.idfClient;
          this.idfClientForm = data.clientFormImageValue.idfClientForm;
          this.loadData(this.idfClientForm);
          let fDateTime = moment(new Date(data.clientFormImageValue.formDateTime));
          this.cmbFormDateTime.setDate(fDateTime.format('M/D/YYYY'));
          this.ImagePath = this.imagePipe.transform(data.clientFormImageValue.image, 'clientFormValues');
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

  loadData(idClientForm: number) {
    this.myLoader.open();
    const tasks = [];
    tasks.push(
      this.clientFormService.GetClientFormForEditById(idClientForm),
      this.clientFormService.GetClient(this.idfClient),
      this.clientFormService.GetClientFormForEditById(this.idfClientForm)
    );
    forkJoin(tasks).subscribe(datas => {
      let data0 = datas[0] as any;
      let data1 = datas[1] as any;
      let data2 = datas[2] as any;
      if (data0.result) {
        this.template = data0.clientForm.template;
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

  cancel() {
    this.router.navigate(['/clients/editclient/', this.idfClient]);
  }
  imageCroppedBase64(image: string) {
    this.croppedImage = image;
  }

  imageLoaded() {
    this.cropperReady = true;
  }

  imageLoadFailed() {
    // this.glowMessage.msgs=[];
    // this.glowMessage.ShowGlow("error","glow_error","glow_client_load_image_failed");
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
      this.clientFormImageValue.id = this.id;
      this.clientFormImageValue.idfClient = this.idfClient;
      this.clientFormImageValue.idfClientForm = this.idfClientForm;
      if (this.croppedImage == '')
        this.clientFormImageValue.image = null;
      else
        this.clientFormImageValue.image = this.croppedImage;
      this.clientFormImageValue.formDateTime = this.cmbFormDateTime.val();
      let body = {
        ClientFormImageValue: this.clientFormImageValue
      }
      this.clientFormService.SaveClientFormImageValue(body)
        .subscribe(result => {
          if (result)
            this.router.navigate(['/clients/editclient/', this.idfClient]);
        });
    }
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
