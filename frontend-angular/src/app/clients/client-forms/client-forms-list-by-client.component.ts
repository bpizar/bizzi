import { Component, OnInit, Input, Output, ViewChild, ChangeDetectorRef } from '@angular/core';
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
import { ClientFormByClient } from './client-forms.component.model';
import { ClientsService } from '../clients.service';
import pdfMake from "pdfmake/build/pdfmake";
import pdfFonts from "pdfmake/build/vfs_fonts";
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-client-forms-list-by-client',
  templateUrl: './client-forms-list-by-client.component.html',
  styleUrls: ['./client-forms-list-by-client.component.css'],
  providers: [ClientsService, ConstantService, AuthHelper],
})
export class ClientFormsListByClientComponent {
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('historyWindow') historyWindow: jqxWindowComponent;
  idClient: number;
  clientFormsbyClient: ClientFormByClient[] = [];
  _clientFormsbyClient: ClientFormByClient[] = [];
  clientFormsHistory: ClientFormByClient[] = [];
  cargando = true;
  pagina = 1;
  totalRegistros: 0;
  searchByName: string = '';
  searchByDescription: string = '';

  constructor(
    public clientFormService: ClientsService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public http: HttpClient,
  ) {
    activatedRoute.params.subscribe(params => {
      this.idClient = params.id;
    });
  }
  ngAfterViewInit(): void {
    this.load();
  }

  load() {
    this.clientFormService.GetAllClientFormsByClient(this.idClient).subscribe(
      (data: any) => {
        if (data.result) {
          this.clientFormsbyClient = data.clientFormsbyClient.sort((a, b) => a.name.localeCompare(b.name));
          this._clientFormsbyClient = data.clientFormsbyClient.sort((a, b) => a.name.localeCompare(b.name));
        } else {
          this.manageError(data);
        }
      },
      (error) => {
        this.manageError(error);
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

  delete(clientForm: ClientFormByClient) {
    // Mensajes.MensajeCondicional(
    //   "SiNo",
    //   "warning",
    //   "Seguro dese Borrar el unidadProducto?",
    //   "Esta acción es irreversible!"
    // ).then((result) => {
    //   if (result.value) {
    // this.clientFormsService
    //   .DeleteClientForms(clientForm.id)
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
  print(clientFormId, isModal?) {
    if (isModal) {
      window.open('/#/clients/client-form-value-viewer/' + clientFormId, '_blank');
    }
    else {
      this.router.navigate(['/clients/client-form-value-viewer', clientFormId]);
    }
  }
  printImage(idfClientFormValue: number, layout: string) {
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
      this.clientFormService.GetClientFormImageValueImageById(idfClientFormValue)
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

  search(fieldSearch) {
    if ((this.searchByName.trim() == '') && (this.searchByDescription.trim() == ''))
      this.clientFormsbyClient = this._clientFormsbyClient;
    else {
      switch (fieldSearch) {
        case 'name':
          this.searchByDescription = '';
          this.clientFormsbyClient = this._clientFormsbyClient.filter(staffForm =>
            staffForm.name.toLowerCase().includes(this.searchByName.toLowerCase())
          );
          break;
        case 'description':
          this.searchByName = '';
          this.clientFormsbyClient = this._clientFormsbyClient.filter(staffForm =>
            staffForm.description.toLowerCase().includes(this.searchByDescription.toLowerCase())
          );
          break;
        default:
          this.clientFormsbyClient = this._clientFormsbyClient;
          break;
      }
    }
  }
  showHistory(idClientForm: number) {
    this.clientFormService.GetAllClientFormsByClientandClientForm(this.idClient, idClientForm).subscribe(
      (data: any) => {
        if (data.result) {
          this.clientFormsHistory = data.clientFormsbyClient;
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
}
