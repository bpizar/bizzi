import { Directive, Component, ViewChild, AfterViewInit, OnInit, OnDestroy, Injectable, EventEmitter, Output, Input, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConstantService } from '../../common/services/app.constant.service';
import { EventEmiterNoteModel } from './notes.component.model';
import { Router } from '@angular/router';
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { jqxNumberInputComponent } from 'jqwidgets-ng/jqxnumberinput';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqxdropdownlist';
import { jqxDateTimeInputComponent } from 'jqwidgets-ng/jqxdatetimeinput';
import { jqxInputComponent } from 'jqwidgets-ng/jqxinput';
import { TaskModel } from '../editproject/editproject_.component.model';
import { jqxNotificationComponent } from 'jqwidgets-ng/jqxnotification';
import { positionsModel } from '../../staff/staff.component.model';
import { settingReminderTimeModel } from '../editproject/editproject_.component.model';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
import { jqxEditorComponent } from 'jqwidgets-ng/jqxeditor';
import { jqxTabsComponent } from 'jqwidgets-ng/jqxtabs';
import { NgModule } from '@angular/core';

@Component({
    selector: 'notestask',
    templateUrl: '../../projects/notes/notes.component.template.html',
    providers: [ConstantService],
    //styleUrls: ['./edittask_.component.css'],
})

@Injectable()
export class NotesTask implements OnInit, OnDestroy, AfterViewInit {

    @Input('glowmessage') glowMessage: GlowMessages;
    @Output() onAddNote = new EventEmitter<EventEmiterNoteModel>();
    @ViewChild(jqxWindowComponent) private windowx: jqxWindowComponent;
    @ViewChild('myEditor') jqxEditor: jqxEditorComponent;
    dataAdapterClientsListBoxAux: any;
    tools: string = 'bold italic underline | format font size | color background | left center right | outdent indent | ul ol | image | link | user';

    getWidth(): any {
        if (document.body.offsetWidth < 640) {
            return '90%';
        }
        return 640;
    }

    widgetx: any;

    createCommand = (name: string): any => {
        switch (name) {
            case 'user':
                return {
                    type: 'list',
                    tooltip: 'Insert Date/Time',
                    init: (widget: any): void => {
                        this.widgetx = widget;
                        //widget.jqxDropDownList({ filterable : "true", searchMode : "'containsignorecase'", placeHolder: 'Insert Client', width: 170, source : this.dataAdapterClientsListBoxAux, valueMember : "id", displayMember : "fullName",    autoDropDownHeight: true });
                    },
                    refresh: (widget: any, style: any): void => {
                        widget.jqxDropDownList('clearSelection');
                    },
                    action: (widget: any, editor: any): any => {
                        let widgetValue = widget.val();
                        return { command: 'inserthtml', value: '<span style="color: var(--twelfth-color);">@' + this.dataAdapterClientsListBoxAux.originaldata.find(x => x.id == widgetValue).fullName + '</span><span>&nbsp;</span>' };
                    }
                }
        }
    };

    constructor(private constantService: ConstantService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngAfterViewInit(): void {

    };

    IdTask: number;
    canDeleteOrSaveProject: boolean;

    showEdit(data: TaskModel, dataAdapterClientsListBoxIn: any, val: string, canDeleteOrSaveProject: boolean): void {

        this.canDeleteOrSaveProject = canDeleteOrSaveProject;

        setTimeout(() => {
            this.IdTask = data.id;
            this.dataAdapterClientsListBoxAux = dataAdapterClientsListBoxIn;
            this.widgetx.jqxDropDownList({ filterable: "true", searchMode: "'containsignorecase'", placeHolder: 'Insert Client', width: 170, source: this.dataAdapterClientsListBoxAux, valueMember: "id", displayMember: "fullName", autoDropDownHeight: true });
            this.jqxEditor.val(val ? val : "");
            this.windowx.setTitle("Notes");
            this.windowx.open();
        });
    }

    OkClick = (event: any): void => {
        let emiter = new EventEmiterNoteModel();

        emiter.Value = this.jqxEditor.val();
        emiter.IdTask = this.IdTask;

        this.onAddNote.emit(emiter);
        this.windowx.close();
    }

    CancelClick = (event: any): void => {
        this.windowx.close();
    };

    ngOnInit(): void { }

    ngOnDestroy() { }
}