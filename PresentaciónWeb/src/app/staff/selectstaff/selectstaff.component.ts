import { Component, ViewChild, AfterViewInit, OnInit, Input, OnDestroy, Injectable, EventEmitter, Output, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConstantService } from '../../common/services/app.constant.service';
import { EventEmiterParticipantModel } from './selectstaff.component.model';
import { Router } from '@angular/router';
import { StaffService } from '../staff.service';
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { jqxDropDownListComponent } from 'jqwidgets-ng/jqxdropdownlist';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { AuthHelper } from '../../common/helpers/app.auth.helper';
import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
// import { jqxNotificationComponent } from 'jqwidgets-ng/jqxnotification';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';

@Component({
    selector: 'selectstaff',
    templateUrl: '../../staff/selectstaff/selectstaff.component.template.html',
    providers: [ConstantService, StaffService],
    styleUrls: ['../../staff/selectstaff/selectstaff.component.css'],
})

@Injectable()
export class SelectStaff implements OnInit, OnDestroy, AfterViewInit {

    @Input('glowmessage') glowMessage: GlowMessages;
    @Output() onAddParticipant = new EventEmitter<EventEmiterParticipantModel>();
    @ViewChild(jqxWindowComponent) private windowx: jqxWindowComponent;
    @ViewChild('staffDropDown') public staffDropDown: jqxDropDownListComponent;
    @ViewChild('positionsDropDown') public positionsDropDown: jqxDropDownListComponent;
    // @ViewChild('msgNotificationError') msgNotificationError: jqxNotificationComponent;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    imagePipe = new ImagePipe();

    PlaceHolderLookingFor: string;
    PlaceHolderParticipant: String;
    PlaceHolderPosition: String;

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    validateSelection = (): boolean => {
        let result = true;

        if (this.staffDropDown.selectedIndex() == -1) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_a_staff");
            result = false;
        }

        if (this.positionsDropDown.selectedIndex() == -1) {
            this.glowMessage.ShowGlow("warn", "glow_invalid", "glow_project_select_a_position");
            result = false;
        }
        return result;
    }

    msgError: string = "";

    constructor(private translate: TranslateService,
        private constantService: ConstantService,
        private route: ActivatedRoute,
        private router: Router,
        private authHelper: AuthHelper,
        private staffService: StaffService,
        private chRef: ChangeDetectorRef) {
        this.translate.setDefaultLang('en');
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
            this.translate.get('global_lookingfor').subscribe((res: string) => {
                this.PlaceHolderLookingFor = res;
            });

            this.translate.get('projects_select_participant').subscribe((res: string) => {
                this.PlaceHolderParticipant = res;
            });

            this.translate.get('projects_select_position').subscribe((res: string) => {
                this.PlaceHolderPosition = res;
            });
        });
    }

    rendererListBoxStaff = (index, label, value): string => {
        if (this.sourceStaff.localData == undefined) {
            return null;
        }
        var datarecord = this.sourceStaff.localData[index];
        if (datarecord != undefined) {
            var imgurl = this.imagePipe.transform(datarecord.img, 'users');
            var img = '<img height="30" width="30" src="' + imgurl + '"/>';
            var table = '<table style="min-width: 120px;"><tr><td style="width: 30px;" rowspan="2">' + img + '</td><td style="color:rgb(51, 51, 51); font-size:12px;">' + datarecord.fullName + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + datarecord.positionName + '</td></tr></table>';
            return table;
        }
        else {
            return label;
        }
    };

    sourceStaff: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'fullName', type: 'string' },
                { name: 'idfUser', type: 'number' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterStaffListBox: any = new jqx.dataAdapter(this.sourceStaff);

    sourcePositions: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'name', type: 'string' },
            ],
            id: 'id',
            async: true
        }

    dataAdapterPositionsListBox: any = new jqx.dataAdapter(this.sourcePositions);

    show(): void {
        this.translate.get('projects_select_participant_title_window').subscribe((res: string) => {
            this.windowx.setTitle(res);
        });

        this.windowx.open();
        this.myLoader.open();
        this.staffService.GetAllStaff()
            .subscribe(
                (data: any) => {
                    if (data.result) {
                        this.sourcePositions = data.positions;
                        this.dataAdapterPositionsListBox = new jqx.dataAdapter(this.sourcePositions);
                        this.sourceStaff.localData = data.staffs;
                        this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaff);
                    }
                    else {
                        this.manageError(data);
                    }
                    this.myLoader.close();
                },
                error => {
                    this.myLoader.close();
                    this.manageError(error);
                });
    }

    OkClick = (event: any): void => {
        if (this.validateSelection()) {
            let emiter = new EventEmiterParticipantModel();
            //emiter.Hours = this.hoursTextBox.val(),
            emiter.IdfPosition = this.positionsDropDown.val();
            emiter.IdfStaff = this.staffDropDown.val();
            emiter.abm = "I";
            let item = this.staffDropDown.getSelectedItem();
            let found = this.sourceStaff.localData.filter(x => x.id == item.value)[0];
            emiter.PositionName = this.positionsDropDown.getSelectedItem().label
            emiter.UserName = found.fullName;;
            emiter.IdUser = found.idfUser;
            emiter.img = found.img;
            this.onAddParticipant.emit(emiter);
            this.windowx.close();
        }
    }

    CancelClick = (event: any): void => {
        this.windowx.close();
    };

    ngOnInit(): void { }

    ngOnDestroy() { }
}