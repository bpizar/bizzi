import { Component, OnInit, ViewChild } from "@angular/core";
import { ProjectForm } from './project-forms.component.model';
import { jqxButtonComponent } from 'jqwidgets-ng/jqxbuttons';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { ConstantService } from 'src/app/common/services/app.constant.service';
import { ProjectsService } from '../projects.service';
import { AuthHelper } from 'src/app/common/helpers/app.auth.helper';
import { GlowMessages } from 'src/app/common/components/glowmessages/glowmessages.component';
import * as moment from 'moment';

@Component({
  selector: "app-project-forms",
  templateUrl: "./project-forms.component.html",
  styleUrls: ["./project-forms.component.css"],
  providers: [ProjectsService, ConstantService, AuthHelper],
})
export class ProjectFormsComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  projectForms: ProjectForm[] = [];
  cargando = true;
  pagina = 1;
  totalRegistros: 0;

  recurrenceList: string[] = ['Once', 'Daily', 'Weekly', 'Biweekly', 'Monthly', 'Anualy'];
  weekRecurrenceList: string[] = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
  BiweeklyRecurrenceList: string[] = ['1st Monday', '1st Tuesday', '1st Wednesday', '1st Thursday', '1st Friday', '1st Saturday', '1st Sunday',
    '2nd Monday', '2nd Tuesday', '2nd Wednesday', '2nd Thursday', '2nd Friday', '2nd Saturday', '2nd Sunday'];

  constructor(
    public projectFormsService: ProjectsService,
    public constantService: ConstantService
  ) { }

  ngAfterViewInit(): void {
    this.load();
  }

  load() {
    this.myLoader.open();
    this.projectFormsService.GetAllProjectForms().subscribe(
      (data: any) => {

        if (data.result) {
          this.projectForms = data.projectForms;
        } else {
          this.manageError(data);
        }
        this.myLoader.close();
      },
      (error) => {
        this.myLoader.close();
        this.manageError(error);
      }
    );
  }

  manageError = (data: any): void => {
    this.glowMessage.ShowGlowByError(data);
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

  delete(projectForm: ProjectForm) {
    // Mensajes.MensajeCondicional(
    //   "SiNo",
    //   "warning",
    //   "Seguro dese Borrar el unidadProducto?",
    //   "Esta acción es irreversible!"
    // ).then((result) => {
    //   if (result.value) {
    this.projectFormsService
      .DeleteProjectForms(projectForm.id)
      .subscribe(() => this.load());
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

  getRecurrenceDetail(idfRecurrence: number, idfRecurrenceDetail: number): string {
    switch (idfRecurrence) {
      case 0:
        return;
        break;
      case 1:
        return '(' + this.militaryToHour(idfRecurrenceDetail) + ')';
        break;
      case 2:
        return '(' + this.weekRecurrenceList[idfRecurrenceDetail] + ')';
        break;
      case 3:
        return '(' + this.BiweeklyRecurrenceList[idfRecurrenceDetail] + ')';
        break;
      case 4:
        return '(' + idfRecurrenceDetail.toString() + ')';
        break;
      case 5:
        return '(' + moment().dayOfYear(idfRecurrenceDetail).format('M/D') + ')';
        break;
      default:
        break;
    }
  }

  militaryToHour(_hours: number): string {
    let hours = _hours.toString();
    if (hours.length == 4) {
      return hours.slice(0, 2) + ':' + hours.slice(2, 4);
    } else {
      return hours.slice(0, 1) + ':' + hours.slice(1, 3);
    }
  }
}
