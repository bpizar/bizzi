import { Component, ViewChild, AfterViewInit, OnInit, ChangeDetectorRef } from '@angular/core';
import { ConstantService } from '../common/services/app.constant.service';
import { GeoTrackingService } from './geotracking.service';
import { jqxGridComponent } from 'jqwidgets-ng/jqxgrid';
import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
import { jqxButtonComponent } from 'jqwidgets-ng/jqxbuttons';
import { jqxInputComponent } from 'jqwidgets-ng/jqxinput';
import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
import { Router } from '@angular/router';
import { AuthHelper } from '../common/helpers/app.auth.helper';
import { jqxListBoxComponent } from 'jqwidgets-ng/jqxlistbox';
import { jqxComboBoxComponent } from 'jqwidgets-ng/jqxcombobox';
import { GlowMessages } from '../common/components/glowmessages/glowmessages.component';
import { SchedulingService } from '../scheduling/scheduling.service';
import { StaffService } from '../staff/staff.service';
import { CommonHelper } from '../common/helpers/app.common.helper';
import { GMapModule } from 'primeng/gmap';
import { jqxDateTimeInputComponent } from '../../../node_modules/jqwidgets-ng/jqxdatetimeinput';
import { timeInterval } from '../../../node_modules/rxjs/operators';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { ImagePipe } from 'src/app/common/pipes/image.pipe';

declare var google: any;

@Component({
    selector: 'geotracking',
    templateUrl: '../geotracking/geotracking.component.template.html',
    providers: [GeoTrackingService, ConstantService, AuthHelper, SchedulingService, StaffService, CommonHelper],
    styleUrls: ['../reports/styleReport.component.css']
})

export class GeoTracking implements OnInit, AfterViewInit {

    geoTimeTrackingList: any[];
    geoTimeTrackingAuto: any[];
    PlaceHolderLookingFor: string;
    currentPeriod: number = -1;
    currentStaff: number = -1;
    currentUser: number = -1;
    currentDateTime: Date;
    serverDateTime: Date;
    @ViewChild('periodsDropDown') periodsDropDown: jqxListBoxComponent;
    @ViewChild('loaderReference') myLoader: jqxLoaderComponent;
    @ViewChild('dateX') dateX: jqxDateTimeInputComponent;
    @ViewChild('glowmessage') glowMessage: GlowMessages;
    @ViewChild('listBoxStaffFilter') public listBoxStaffFilter: jqxListBoxComponent;
    imagePipe = new ImagePipe();
    setLanguageEspecialControls = (): void => {
        this.translate.get('global_lookingfor').subscribe((res: string) => {
            this.PlaceHolderLookingFor = res;
        });
    }

    getCurrentCulture = (): string => {
        return this.translate.currentLang == "en" ? "en" : "es-BO";
    }

    constructor(private translate: TranslateService,
        private clientsServiceService: GeoTrackingService,
        private schedulingService: SchedulingService,
        private CommonHelper: CommonHelper,
        private staffService: StaffService,
        private chRef: ChangeDetectorRef,
        private authHelper: AuthHelper,
        private geoTrackingService: GeoTrackingService,
        private constantService: ConstantService,
        private router: Router) {
        this.translate.setDefaultLang('en');
    }

    // SOURCE PERIODS
    sourcePeriodsListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'id', type: 'number' },
                { name: 'dateJoin', type: 'string' },
                { name: 'from', type: 'date' },
                { name: 'to', type: 'date' },
                { name: 'close', type: 'boolean' },
                { name: 'state', type: 'string' },
            ],
            id: 'idfStaffProjectPosition',
            async: true
        }

    dataAdapterPeriod: any = new jqx.dataAdapter(this.sourcePeriodsListBox);
    loadAutoTracking: boolean;

    changeLoadAutoGeoTracking = (event: any): void => {
        this.loadAutoTracking = event.target.checked;

        if (this.currentPeriod > -1) {
            this.GetGeoTracking();
        }
    }

    getFormatDate = (dinput: any): any => {
        let d = new Date(dinput);
        //let days: number = isBeginDate ? Number(Number(d.getDate()) + 1) : Number(d.getDate());
        let days: number = Number(Number(d.getDate()) + 0);
        let month: number = Number(Number(d.getMonth()) + 1);
        return d.getFullYear() + "-" + (month <= 9 ? "0" + month : month) + "-" + (days <= 9 ? "0" + days : days);
    }

    GetGeoTracking = (): void => {
        let isLoading = false;
        if (this.dateX.val() && Number(this.periodsDropDown.selectedIndex()) >= 0 && !isLoading) {
            this.myLoader.open();
            isLoading = true;
            let dateByCulture = this.getFormatDate(this.dateX.getDate());// this.dateX.getDate(); 
            this.geoTrackingService
                .GeoTracking(this.loadAutoTracking, this.currentPeriod, dateByCulture)
                .subscribe(
                    (data: any) => {
                        this.sourceStaffListBox.localData = data.staffForGeoTrackingList;
                        this.dataAdapterStaffListBox = new jqx.dataAdapter(this.sourceStaffListBox);
                        this.geoTimeTrackingList = data.geoTimeTrackingList;
                        this.geoTimeTrackingAuto = data.geoTimeTrackingAuto;
                        this.myLoader.close();
                        isLoading = false;
                        this.initOverlays();
                    },
                    error => {
                        this.myLoader.close();
                        this.manageError(error);
                        isLoading = false;
                    });
        }
    }

    dateChanged = (event: any): void => {
        if (this.currentPeriod > -1) {
            this.GetGeoTracking();
        }
    }

    periodFrom: Date;
    periodTo: Date;

    PeriodSelectDrowDown = (event: any): void => {
        if (this.currentPeriod == event.args.item.value) {
            return;
        }
        this.listBoxStaffFilter.clearSelection();
        this.periodFrom = event.args.item.originalItem.from; //new Date(Date.parse(event.args.item.originalItem.from)).toUTCString() 
        this.periodTo = event.args.item.originalItem.to;
        this.dateX.setMinDate(this.periodFrom);
        this.dateX.setMaxDate(this.periodTo);
        this.currentPeriod = event.args.item.value;

        if (this.currentDateTime >= this.periodFrom && this.currentDateTime <= this.periodTo) {
            this.dateX.val(this.currentDateTime);
        }
        else {
            if (this.currentDateTime > this.periodTo) {
                this.dateX.val(this.periodTo);
            }
            else {
                this.dateX.val(this.periodFrom);
            }
        }

        if (this.currentPeriod > -1) {
            this.GetGeoTracking();
        }
    }

    // SOURCE LISTBOX STAFF
    sourceStaffListBox: any =
        {
            dataType: "json",
            dataFields: [
                { name: 'idfUser', type: 'number' },
                { name: 'idfStaff', type: 'number' },
                { name: 'idfProject', type: 'number' },
                { name: 'fullUserName', type: 'string' },
                { name: 'projectName', type: 'string' },
                { name: 'positionName', type: 'string' },
                { name: 'color', type: 'string' },
                { name: 'group', type: 'string' },
                { name: 'idfStaffProjectPosition', type: 'number' },
                { name: 'img', type: 'string' },
            ],
            id: 'idfStaffProjectPosition',
            async: true
        }

    dataAdapterStaffListBox: any = new jqx.dataAdapter(this.sourceStaffListBox);

    rendererListBoxStaff = (index, label, value): string => {

        if (this.sourceStaffListBox.localData) {
            var datarecord = this.sourceStaffListBox.localData[index];
            if (datarecord != undefined) {
                var imgurl = this.imagePipe.transform(datarecord.img, 'users');
                var img = '<img style="border-radius:50%;" height="30" width="30" src="' + imgurl + '"/>';
                var grayColor0 = "var(--first-color)";
                var geoIcon = datarecord.currentState == "none" ? ' <i class="icon ion-ios-warning" style="font-size:18px; color:' + grayColor0 + ';"></i>' : datarecord.currentState == "ckeckout" ? '<i class="icon ion-ios-pin" style="font-size:18px; display: inline-block;  transform: rotate(180deg); color:red;"></i>' : '<i class="icon ion-ios-pin" style="font-size:18px; color:limegreen;"></i>';
                var table = '<table border=0 style="min-width: 120px; width:90%;"><tr><td style="width: 30px;" rowspan="2">' + img + '</td><td  style="color:rgb(51, 51, 51); font-size:12px;">' + datarecord.fullUserName + '</td><td rowspan="2" style="width: 30px;">' + geoIcon + '</td></tr><tr><td style="color:rgb(51, 51, 51); font-size:10px;">' + this.CommonHelper.convertMinsToHrsMins(datarecord.hours) + " " + datarecord.positionName + '</td></tr></table>';
                return table;
            }
            else {
                if (label != undefined) {
                    var values = label.split("|");
                    var divSquareProject = "<span style=' border:1px solid white; height:15px; width:15px !important; background-color:" + values[0] + ";'>&nbsp;&nbsp;</span>";

                    return divSquareProject + "&nbsp;&nbsp;" + values[1];
                }
                else {
                    return label;
                }
            }
        }
    };

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.translate.use('en');
            this.setLanguageEspecialControls();
            this.myLoader.open();
            this.schedulingService.GetPeriods()
                .subscribe(
                    (data: any) => {
                        if (data.result) {
                            this.serverDateTime = new Date(data.currentDateTime);
                            //this.currentDateTime = new Date(Date.parse(data.currentDateTime));
                            this.currentDateTime = new Date(data.currentDateTime);
                            this.sourcePeriodsListBox.localData = data.periodsList;
                            this.dataAdapterPeriod = new jqx.dataAdapter(this.sourcePeriodsListBox);
                        }
                        else {
                            this.manageError(data);
                        }

                        this.myLoader.close();
                    },
                    error => {
                        this.myLoader.close();
                        this.manageError(error);
                    }
                );
        });
    }

    manageError = (data: any): void => {
        this.myLoader.close();
        this.glowMessage.ShowGlowByError(data);
    }

    // periodsBindingComplete = (event: any) => {
    //     if (this.sourcePeriodsListBox.localData != undefined) {
    //         if (this.sourcePeriodsListBox.localData.length > 0) {
    //             this.periodsDropDown.selectedIndex(0);
    //         }
    //     }
    // }
    periodsBindingComplete = (event: any): void => {

        let found = false;
        if (this.sourcePeriodsListBox.localData != undefined) {
            if (this.sourcePeriodsListBox.localData.length > 0) {
                let i = 0;
                this.sourcePeriodsListBox.localData.forEach(element => {
                    let dtf = new Date(element.from);
                    let dtt = new Date(element.to);
                    if (this.serverDateTime > dtf && this.serverDateTime < dtt) {
                        // alert("si entra");                   
                        this.periodsDropDown.selectedIndex(i);
                        found = true;
                    }
                    i++;
                });

                if (!found) {
                    this.periodsDropDown.selectedIndex(0);
                }
            }
        }
    }
    //     map: any;

    //     setMap(event) {

    // this.map = new google.maps.Map();

    // ("READY");

    //         this.map = event.map;
    //     }

    options: any;
    overlays: any[];
    dialogVisible: boolean;
    markerTitle: string;
    selectedPosition: any;
    infoWindow: any;
    draggable: boolean;
    initMap(): void {
    }

    ngOnInit(): void {
        this.options = {
            center: { lat: 36.890257, lng: 30.707417 },
            zoom: 18
        };
        // this.initOverlays();
        this.infoWindow = new google.maps.InfoWindow();
    }

    handleMapClick(event) {
        this.dialogVisible = true;
        this.selectedPosition = event.latLng;
    }

    handleOverlayClick(event) {
        let isMarker = event.overlay.getTitle != undefined;

        if (isMarker) {
            let title = event.overlay.getTitle();
            this.infoWindow.setContent('' + title + '');
            this.infoWindow.open(event.map, event.overlay);
            event.map.setCenter(event.overlay.getPosition());
        }
    }
    /*
    addMarker() {
        this.overlays.push(new google.maps.Marker({position:{lat: this.selectedPosition.lat(), lng: this.selectedPosition.lng()}, title:this.markerTitle, draggable: this.draggable}));
        this.markerTitle = null;
        this.dialogVisible = false;
    }
    */

    handleDragEnd(event) {
    }

    getTitle = (time: string, note: string, state: string): string => {
        note = note != undefined ? note : "";
        let rotatecss = state == "end" ? "display: inline-block;  transform: rotate(180deg);" : "";
        let colorcss = state == "end" ? "color:red;" : "color:limegreen;";
        return '<i class="icon ion-ios-pin" style=" ' + rotatecss + '  font-size:18px; ' + colorcss + '"></i>' + time + " " + note;
    }

    clickTimeGeo(map, event, state) {
        let latAux = state == 'start' ? event.latitude : event.endLat;
        let lonAux = state == 'start' ? event.longitude : event.endLong;
        if (latAux && lonAux) {
            map.setCenter({ lat: latAux, lng: lonAux });
        }
        else {
            this.glowMessage.ShowGlow("warn", "Not Available", "The current tracking is in progress");
        }
    }

    selectStaff = (event: any): void => {
        this.currentStaff = event.args.item.value; //record.value;
        this.currentUser = event.args.item.originalItem.idfUser;
    }

    initOverlays() {
        this.overlays = [];
        this.geoTimeTrackingList.forEach(g => {
            var imgurl = this.imagePipe.transform(g.img, 'users');
            let newMarker = new google.maps.Marker({ icon: { url: imgurl, scale: 10, strokeColor: '#f00', strokeWeight: 5, fillColor: '#00f', fillOpacity: 1, scaledSize: new google.maps.Size(40, 40) }, position: { lat: g.latitude, lng: g.longitude }, title: this.getTitle(g.start, g.startNote, 'start') });
            this.overlays.push(newMarker);
            if (g.endLat && g.endLong) {
                newMarker = new google.maps.Marker({ icon: { url: imgurl, scale: 10, strokeColor: '#f00', strokeWeight: 5, fillColor: '#00f', fillOpacity: 1, scaledSize: new google.maps.Size(40, 40) }, position: { lat: g.endLat, lng: g.endLong }, title: this.getTitle(g.start, g.endNote, 'end') });
                this.overlays.push(newMarker);
            }
        });

        this.geoTimeTrackingAuto.forEach(g => {
            var imgurl = this.imagePipe.transform(g.img, 'users');
            let newMarker = new google.maps.Marker({
                icon: {
                    url: imgurl,
                    scale: 10,
                    strokeColor: '#f00',
                    strokeWeight: 5,
                    fillColor: '#00f',
                    fillOpacity: 1,
                    scaledSize: new google.maps.Size(40, 40)
                },
                position: { lat: g.latitude, lng: g.longitude }, title: this.getTitle(g.start, 'auto Tracking', 'start')
            });

            /*
                                var newMarker = new google.maps.Circle({
                                    center:{lat: g.latitude, lng: g.longitude},
                                    radius:1,
                                    strokeColor:"#0000FF",
                                    strokeOpacity:0.8,
                                    strokeWeight:2,
                                    fillColor:"#0000FF",
                                    fillOpacity:0.4,
                                    //title:this.getTitle(g.start,'auto Tracking','start')
                                  });
            */



            //this.overlays.push(newMarker);

            if (g.latitude && g.longitude) {
                this.overlays.push(newMarker);
                newMarker = new google.maps.Marker({ icon: { url: imgurl, scale: 10, strokeColor: '#f00', strokeWeight: 5, fillColor: '#00f', fillOpacity: 1, scaledSize: new google.maps.Size(40, 40) }, position: { lat: g.latitude, lng: g.longitude }, title: this.getTitle(g.start, 'auto Tracking', 'start') });
            }
        });
    }

    zoomIn(map) {
        map.setZoom(map.getZoom() + 1);
    }

    zoomOut(map) {
        map.setZoom(map.getZoom() - 1);
    }

    clear() {
        this.overlays = [];
    }

    // public ngOnDestroy() {
    //     this.myLoader.close();
    // }
}