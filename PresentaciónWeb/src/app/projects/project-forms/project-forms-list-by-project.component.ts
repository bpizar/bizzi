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
import { ProjectFormByProject } from './project-forms.component.model';
import { ProjectsService } from '../projects.service';
import pdfMake from "pdfmake/build/pdfmake";
import pdfFonts from "pdfmake/build/vfs_fonts";
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-project-forms-list-by-project',
  templateUrl: './project-forms-list-by-project.component.html',
  styleUrls: ['./project-forms-list-by-project.component.css'],
  providers: [ProjectsService, ConstantService, AuthHelper],
})
export class ProjectFormsListByProjectComponent {
  @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
  @ViewChild('glowmessage') glowMessage: GlowMessages;
  @ViewChild('historyWindow') historyWindow: jqxWindowComponent;
  idProject: number;
  projectFormsbyProject: ProjectFormByProject[] = [];
  _projectFormsbyProject: ProjectFormByProject[] = [];
  projectFormsHistory: ProjectFormByProject[] = [];
  cargando = true;
  pagina = 1;
  totalRegistros: 0;
  searchByName: string = '';
  searchByDescription: string = '';

  constructor(
    public projectFormService: ProjectsService,
    public constantService: ConstantService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public http: HttpClient,
  ) {
    activatedRoute.params.subscribe(params => {
      this.idProject = params.id;
    });
  }
  ngAfterViewInit(): void {
    this.load();
  }

  load() {
    this.projectFormService.GetAllProjectFormsByProject(this.idProject).subscribe(
      (data: any) => {
        if (data.result) {
          this.projectFormsbyProject = data.projectFormsbyProject.sort((a, b) => a.name.localeCompare(b.name));
          this._projectFormsbyProject = data.projectFormsbyProject.sort((a, b) => a.name.localeCompare(b.name));
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

  delete(projectForm: ProjectFormByProject) {
    // Mensajes.MensajeCondicional(
    //   "SiNo",
    //   "warning",
    //   "Seguro dese Borrar el unidadProducto?",
    //   "Esta acción es irreversible!"
    // ).then((result) => {
    //   if (result.value) {
    // this.projectFormsService
    //   .DeleteProjectForms(projectForm.id)
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
  print(projectFormId, isModal?) {
    if (isModal) {
      window.open('/#/projects/project-form-value-viewer/' + projectFormId, '_blank');
    }
    else {
      this.router.navigate(['/projects/project-form-value-viewer', projectFormId]);
    }
  }
  printImage(idfProjectFormValue: number, layout: string) {
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
      this.projectFormService.GetProjectFormImageValueImageById(idfProjectFormValue)
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
      this.projectFormsbyProject = this._projectFormsbyProject;
    else {
      switch (fieldSearch) {
        case 'name':
          this.searchByDescription = '';
          this.projectFormsbyProject = this._projectFormsbyProject.filter(staffForm =>
            staffForm.name.toLowerCase().includes(this.searchByName.toLowerCase())
          );
          break;
        case 'description':
          this.searchByName = '';
          this.projectFormsbyProject = this._projectFormsbyProject.filter(staffForm =>
            staffForm.description.toLowerCase().includes(this.searchByDescription.toLowerCase())
          );
          break;
        default:
          this.projectFormsbyProject = this._projectFormsbyProject;
          break;
      }
    }
  }
  showHistory(idProjectForm: number) {
    this.projectFormService.GetAllProjectFormsByProjectandProjectForm(this.idProject, idProjectForm).subscribe(
      (data: any) => {
        if (data.result) {
          this.projectFormsHistory = data.projectFormsbyProject;
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
