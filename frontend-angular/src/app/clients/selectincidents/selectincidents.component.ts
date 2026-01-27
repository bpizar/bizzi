// import { Component, ViewChild, AfterViewInit, Input, OnInit, OnDestroy, Injectable, EventEmitter, Output, ChangeDetectorRef  } from '@angular/core';
// //import { Observable } from 'rxjs/Rx';
// import { ActivatedRoute } from '@angular/router';
// import { ConstantService } from '../../common/services/app.constant.service';
// //import { EventEmiterParticipantModel } from '../../staff/selectstaff/selectstaff.component.model';
// import { EventEmiterInjuryModel, InjuryModel } from './selectincidents.component.model';

// import { Router } from '@angular/router';
// //import { StaffService } from '../../staff/staff.service';

// import { ClientsService } from '../clients.service';

// import { jqxWindowComponent } from 'jqwidgets-ng/jqxwindow';
// // import { jqxNumberInputComponent } from '../../../../node_modules/jqwidgets-framework/jqwidgets-ts/angular_jqxnumberinput';
// import { jqxDropDownListComponent } from 'jqwidgets-ng/jqxdropdownlist';
// //import { jqxNotificationComponent } from 'jqwidgets-ng/jqxnotification';
// import { jqxLoaderComponent } from 'jqwidgets-ng/jqxloader';
// import { AuthHelper } from '../../common/helpers/app.auth.helper';
// import { jqxListBoxComponent } from 'jqwidgets-ng/jqxlistbox';
// import { GlowMessages } from '../../common/components/glowmessages/glowmessages.component';																		

// import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

// @Component({
//     selector: 'selectincident',
//     templateUrl: '../../clients/selectincidents/selectincidents.component.template.html',
//     providers: [ConstantService,ClientsService],
//    // styleUrls: ['../../staff/selectStaff/selectStaff.component.css'],
// })

// @Injectable()
// export class SelectIncident implements OnInit, OnDestroy , AfterViewInit {

//     @Output() onAddClient = new EventEmitter<EventEmiterInjuryModel>();
//     @Input('glowmessage') glowMessage: GlowMessages;  


//     @ViewChild(jqxWindowComponent) private windowx: jqxWindowComponent;        
//     @ViewChild('staffDropDown') public staffDropDown: jqxDropDownListComponent;
//     @ViewChild('positionsDropDown') public positionsDropDown: jqxDropDownListComponent;    
//    // @ViewChild('msgNotificationError') msgNotificationError: jqxNotificationComponent;
//     @ViewChild('loaderReference') myLoader: jqxLoaderComponent;


//     @ViewChild('ClientListBox') clientListBox: jqxListBoxComponent;


//     PlaceHolderLookingFor:string;


//     manageError = (data: any): void => {
//         this.myLoader.close();
//         this.glowMessage.ShowGlowByError(data);
//     }

//     validateSelection = (): boolean => {
//         // let result = true;

//         // if (this.staffDropDown.selectedIndex() == -1) {
//         //     this.msgError = "You must select a staff";
//         //     //this.chRef.detectChanges();
//         //     this.msgNotificationError.elementRef.nativeElement.childNodes[0].innerText = this.msgError;
//         //     this.msgNotificationError.open();
//         //     result = false;
//         // }

//         // if (this.positionsDropDown.selectedIndex() == -1) {
//         //     this.msgError = "You must select a position";
//         //     //this.chRef.detectChanges();
//         //     this.msgNotificationError.elementRef.nativeElement.childNodes[0].innerText = this.msgError;
//         //     this.msgNotificationError.open();
//         //     result = false;
//         // }

//         // return result;


//         return true;
//     }

//     msgError: string = "";

//     constructor(private constantService: ConstantService,
//         private translate:TranslateService,
//         private route: ActivatedRoute,
//         private router: Router,
//         private authHelper: AuthHelper,
//         private clientsService: ClientsService,
//         private chRef: ChangeDetectorRef) { 
//             this.translate.setDefaultLang('en');  
//         }



//         setLanguageChanges=():void=>
//         {
//             this.translate.get('global_lookingfor').subscribe((res: string) => { 
//                 this.PlaceHolderLookingFor = res;
//              });  
//         };




//     ngAfterViewInit(): void { 
//         setTimeout(() => {
//             this.translate.use('en');   
//         });       
//     }

//     rendererListBox = (index, label, value): string => {
//         if (this.sourceInjuries.localData == undefined) {
//             return null;
//         }
//         var datarecord = this.sourceInjuries.localData[index];
//         if(datarecord)
//         {
//             var imgurl = '/media/images/clients/' + datarecord.img + '.png';
//             var img = '<img style=border-radius:50%;" height="40" width="40" src="' + imgurl + '"/>';            
//             var table = '<table style="height:40px; min-width: 100%;"><tr><td style="width:50px;" rowspan= "2">' + img + '</td><td style="width:auto; color:rgb(51, 51, 51); font-size:12px;"> ' + datarecord.fullName + '</td><td style="width:auto; color:black; font-size:12px;">' + "" + '</td></tr></table>';
//             return table;
//         }
//         else
//         {
//            return "-";
//         }
//     };

//     sourceInjuries: any =
//     {
//         dataType: "json",
//         dataFields: [
//             { name: 'id', type: 'string' },
//             { name: 'fullName', type: 'string' },
//             { name: 'birthDate', type: 'datetime' },
//             { name: 'phoneNumber', type: 'number' },
//             // { name: 'email', type: 'string' },
//             // { name: 'notes', type: 'string' },
//             // { name: 'img', type: 'string' },
//             // { name: 'idfClient', type: 'number' },
//         ],
//         id: 'id',
//     };

//     dataAdapterInjuriesListBox: any = new jqx.dataAdapter(this.sourceInjuries);

//     currentInjuries:any=[];

//     filterByCurrentClients=(value:any):boolean=>
//     {
//         let found:boolean = false;

//         this.currentInjuries.forEach(element => {
//             //found = found || element.id == value.id
//             found = found || element.idfClient == value.id


//         });

//         return !found;
//     }


//     show(currentInjuries:any[]): void {

//         this.setLanguageChanges();
//         this.clientListBox.clearSelection();

//         this.windowx.setTitle("Select Client");
//         this.windowx.open();        

//         this.myLoader.open();

//         this.sourceInjuries = currentInjuries;

//         this.clientsService.GetAllClients()
//             .subscribe(
//             (data:any) => {     


//                 if (data.result) {

//                     if(currentInjuries)
//                     {                                               
//                         this.sourceInjuries.localData = data.clients.filter(this.filterByCurrentClients);
//                     }
//                     else{
//                         this.sourceInjuries.localData = data.clients;
//                     }

//                     this.dataAdapterInjuriesListBox = new jqx.dataAdapter(this.sourceInjuries);
//                 }
//                 else
//                 {
//                     this.manageError(data);
//                 }

//                 this.myLoader.close();
//             },
//             error => {
//                 this.myLoader.close();
//                 this.manageError(error);
//             });        
//     }

//     OkClick = (event: any): void =>
//     {
//         if (this.validateSelection()) {            
//             let emiter = new EventEmiterInjuryModel();  
//             emiter.InjuryList = [];          

//             let i=0;

//             this.clientListBox.getCheckedItems().forEach(element => {
//                 let newInjury:InjuryModel = new InjuryModel();

//                 //newClient.id = element.originalItem.id;
//                 i--;

//                 newInjury.id = i;

//                 // newClient.fullName = element.originalItem.fullName;
//                 // newClient.email = element.originalItem.email;
//                 // newClient.birthDate = element.originalItem.birthDate;
//                 // newClient.notes = element.originalItem.notes;
//                 // newClient.img = element.originalItem.img;
//                 // newClient.phoneNumber = element.originalItem.phoneNumber;
//                 newInjury.abm = "I";
//                 // newClient.idfClient = element.originalItem.id;  

//                 emiter.InjuryList.push(newInjury);  
//             });

//             this.onAddClient.emit(emiter);
//             this.windowx.close();
//         }
//     }

//     CancelClick = (event: any): void => {
//         this.windowx.close();
//     };

//     ngOnInit(): void { }

//     ngOnDestroy() { }
// }