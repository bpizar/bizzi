import { Component, OnInit, Input, Output, ViewChild, ChangeDetectorRef } from '@angular/core';
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
import { StaffFormByStaff } from './staff-forms.component.model';
import { StaffService } from '../staff.service';
import pdfMake from "pdfmake/build/pdfmake";
import pdfFonts from "pdfmake/build/vfs_fonts";
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
pdfMake.vfs = pdfFonts.pdfMake.vfs;
import * as moment from 'moment';

@Component({
  selector: 'app-staff-forms-list-by-staff',
  templateUrl: './staff-forms-list-by-staff.component.html',
  styleUrls: ['./staff-forms-list-by-staff.component.css'],
  providers: [StaffService, ConstantService, AuthHelper],
})
export class StaffFormsListByStaffComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('historyWindow') historyWindow: jqxWindowComponent;
  idStaff: number;
  staffFormsbyStaff: StaffFormByStaff[] = [];
  _staffFormsbyStaff: StaffFormByStaff[] = [];
  staffFormsHistory: StaffFormByStaff[] = [];
  cargando = true;
  pagina = 1;
  totalRegistros: 0;
  searchByName: string = '';
  searchByDescription: string = '';

  constructor(
    public staffFormService: StaffService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public http: HttpClient,
  ) {
    activatedRoute.params.subscribe(params => {
      this.idStaff = params.id;
    });
  }

  ngAfterViewInit(): void {
    this.load();
  }

  load() {
    this.staffFormService.GetAllStaffFormsByStaff(this.idStaff).subscribe(
      (data: any) => {
        if (data.result) {
          this.staffFormsbyStaff = data.staffFormsbyStaff.sort((a, b) => a.name.localeCompare(b.name));
          this._staffFormsbyStaff = data.staffFormsbyStaff.sort((a, b) => a.name.localeCompare(b.name));
        } else {
          // this.manageError(data);
        }
        // this.myLoader.close();
      },
      (error) => {
        // this.myLoader.close();
        // this.manageError(error);
      }
    );
  }

  // buscar(termino: string) {
  //   if (termino.length <= 0) {
  //     this.cargar();
  //     this.pagina = 1;
  //     return;
  //   }

  //   this.cargando = true;

  //   this.unidadProductoService
  //     .buscar(termino)
  //     .subscribe((unidadesProducto: UnidadProductoDetallado[]) => {
  //       this.unidadesProducto = unidadesProducto;
  //       this.cargando = false;
  //       this.pagina = 1;
  //     });
  // }

  delete(staffForm: StaffFormByStaff) {
    // Mensajes.MensajeCondicional(
    //   "SiNo",
    //   "warning",
    //   "Seguro dese Borrar el unidadProducto?",
    //   "Esta acción es irreversible!"
    // ).then((result) => {
    //   if (result.value) {
    // this.staffFormsService
    //   .DeleteStaffForms(staffForm.id)
    //   .subscribe(() => this.load());
    // } else {
    //   Mensajes.Mostrar(
    //     "Ligero",
    //     "error",
    //     "Cancelado",
    //     "No se hicieron modificaciones"
    //   );
    // }
    // });
  }

  print(staffFormId, isModal?) {
    if (isModal) {
      window.open('/#/staff/staff-form-value-viewer/' + staffFormId, '_blank');
    }
    else {
      // var docDefinition= 
      // {
      //   content:[
      //     JSON.parse('{"text": "Tables", "style": "header"}')
      //           ]
      // };
      // pdfMake.createPdf(docDefinition).open();
      this.router.navigate(['/staff/staff-form-value-viewer', staffFormId]);
    }
  }

  printImage(idStaffFormValue: number, layout: string) {
    let prtWidth = 0;
    let prtHeight = 0;
    if (layout == 'ImagePortrait') {
      prtWidth = 520;
      prtHeight = 752;
    }
    else {
      prtWidth = 752;
      prtHeight = 520;
    }
    const tasks = [];
    tasks.push(
      this.staffFormService.GetStaffFormImageValueImageById(idStaffFormValue)
    );
    forkJoin(tasks)
      .subscribe(datas => {
        var docDefinition = {
          pageOrientation: layout.replace('Image', ''),
          content: [
            {
              image: 'binder',
              fit: [prtWidth, prtHeight],
              alignment: 'center'
            },
          ],
          images: {
            binder: datas[0]
          }

        }
        pdfMake.createPdf(docDefinition).print();
      });
  }

  showHistory(idStaffForm: number) {
    this.staffFormService.GetAllStaffFormsByStaffandStaffForm(this.idStaff, idStaffForm).subscribe(
      (data: any) => {
        if (data.result) {
          this.staffFormsHistory = data.staffFormsbyStaff;
        } else {
          this.manageError(data);
        }
      },
      (error) => {
        this.manageError(error);
      }
    );
    this.historyWindow.open();
  }

  manageError = (data: any): void => {
    this.glowMessage.ShowGlowByError(data);
  }

  search(fieldSearch) {
    if ((this.searchByName.trim() == '') && (this.searchByDescription.trim() == ''))
      this.staffFormsbyStaff = this._staffFormsbyStaff;
    else {
      switch (fieldSearch) {
        case 'name':
          this.searchByDescription = '';
          this.staffFormsbyStaff = this._staffFormsbyStaff.filter(staffForm =>
            staffForm.name.toLowerCase().includes(this.searchByName.toLowerCase())
          );
          break;
        case 'description':
          this.searchByName = '';
          this.staffFormsbyStaff = this._staffFormsbyStaff.filter(staffForm =>
            staffForm.description.toLowerCase().includes(this.searchByDescription.toLowerCase())
          );
          break;
        default:
          this.staffFormsbyStaff = this._staffFormsbyStaff;
          break;
      }
    }
  }
}
