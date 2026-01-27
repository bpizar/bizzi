import { Component, AfterViewInit, ChangeDetectorRef, ViewChild } from '@angular/core';
/*import { ReportX } from './reportx/reportx.component';
import { RowModel } from './reportx/toolbarEmitter.model';
import { CellModel } from './reportx/toolbarEmitter.model';*/
import { ReportsService } from '../reports.service';
import { ConstantService } from '../../common/services/app.constant.service';
import { Router } from '@angular/router';
// import { CommonModule } from '@angular/common';
// import { BrowserModule } from '@angular/platform-browser';
// import { FormsModule } from '@angular/forms';
/*
@Component({
    selector: 'reportprojects',    
    template:`<reportx 
                 [titleReport]="'Projects / Tasks'"
                 [layout]="''"                 
                 [groupsLabels]="['Project','Period']"
                 [columns]="['Task','Assigned to','Deadline','Hours']">  
              </reportx>`,
    providers: [TimeService, ConstantService],    
})*/
@Component({
    selector: 'reportmenu',
    templateUrl: 'reportmenu.component.template.html',
    providers: [ReportsService, ConstantService],
})

// extends ReportX (no extended, instance of)
export class ReportMenu implements AfterViewInit {

    constructor(public reportsService: ReportsService,
        constantService: ConstantService,
        chRef: ChangeDetectorRef,
        router: Router) {
        //super(constantService, chRef, timeService, router);
    }

    //reportx
    /*
    @ViewChild(ReportX) reportx: ReportX;
    data: RowModel[] = [];
*/
    ngAfterViewInit(): void {
        /*this.reportx.groupsColumns = ['projectName', 'period'];
        this.reportx.columnsWidth = ['300', '200','100','100'];

        this.reportsService.GetReportProjects()
            .subscribe(
            rdata => {
                if (rdata.result) {
                    let data: RowModel[] = [];

                    for (var i = 0; i < rdata.details.length;i++)
                    {
                        let newrow = new RowModel();

                        let newcell5 = new CellModel("projectName", 10);
                        newcell5.value = rdata.details[i].projectName;
                        newrow.cells.push(newcell5);

                        let newcell6 = new CellModel("period",10);
                        newcell6.value = rdata.details[i].period;
                        newrow.cells.push(newcell6);

                        let newcell1 = new CellModel("task",200);
                        newcell1.value = rdata.details[i].task;
                        newrow.cells.push(newcell1);

                        let newcell2 = new CellModel("assignedToFullName",200);
                        newcell2.value = rdata.details[i].assignedToFullName;
                        newrow.cells.push(newcell2);

                        
                        let newcell3 = new CellModel("deadLine",40);
                        newcell3.value = rdata.details[i].deadLine;
                        newrow.cells.push(newcell3);


                        let newcell4 = new CellModel("hours",40);
                        newcell4.value = rdata.details[i].hours;
                        newrow.cells.push(newcell4);

                        data.push(newrow)
                    }

                    this.reportx.ProcessReport(data);
                }
                else {
                    alert("Error");
                }
            },
            error => {
                alert("Error");
            });
          */}
}
