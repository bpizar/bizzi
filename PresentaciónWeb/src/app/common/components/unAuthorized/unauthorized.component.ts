//import { Component, OnInit, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'unauthorized',
    templateUrl: './unauthorized.component.template.html'//,
    //styleUrls: ['app/common/ui/dialog/dialog.component.css'],
})

export class UnAuthorized implements OnInit {

    message: string = 'Unauthorized :(';
    constructor() { }

    ngOnInit() { }
}