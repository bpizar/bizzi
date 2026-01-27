import { Component, OnInit, AfterViewInit, NgModule, Injectable, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'saveactiondisplay',
    template: '<div [ngClass]="{\'saveactiondisplay\': mustHideTryAgain(), \'saveactiondisplayred\': !mustHideTryAgain()}" >{{txt}} <span (click)="launchMethodNow(currentStamp)"  *ngIf="!mustHideTryAgain()">Try again</span></div>',
})

@Injectable()
export class SaveActionDisplay implements OnInit, AfterViewInit {
    txt: string = "";
    @Output() mustSave: EventEmitter<number> = new EventEmitter();

    constructor() { }

    currentStamp: number = 0;
    lastStamp: number = 0;
    saved_: boolean = true;

    mustHideTryAgain = (): boolean => {
        // return this.saved_ || this.currentStamp == 0;
        return this.saved_ || this.currentStamp == 0 || this.lastStamp != this.currentStamp;
    }

    ngOnInit() { }

    IsValid = (clientStamp: number): boolean => {
        return clientStamp >= this.currentStamp;
    }

    setDirty = (): void => {
        this.currentStamp++;
        this.launchMethod(this.currentStamp)
    };

    launchMethodNow = (val: number): void => {
        this.txt = "saving...";
        this.lastStamp = val;
        this.mustSave.emit(val);
    }

    launchMethod = (val: number): void => {
        setTimeout(() => {
            if (val == this.currentStamp) {
                this.txt = "saving...";
                setTimeout(() => {
                    this.lastStamp = val;
                    this.mustSave.emit(val);
                }, 1000);
            }
        }, 500)
    }

    // getText=():string=>
    // {
    //     // timeSince
    //     return this.txt;
    // }

    saved = (saved: boolean): void => {
        // if(saved)
        // {
        //     this.lastDate = new Date();
        // }
        this.saved_ = saved;
        this.txt = this.saved_ ? "saved " : "Document not Saved";
    }

    isSaved() {
        return this.mustHideTryAgain();
    }

    ngAfterViewInit(): void {
    };

    // lastDate:Date;

    // intervals:any = [
    //     { label: 'year', seconds: 31536000 },
    //     { label: 'month', seconds: 2592000 },
    //     { label: 'day', seconds: 86400 },
    //     { label: 'hour', seconds: 3600 },
    //     { label: 'minute', seconds: 60 },
    //     { label: 'second', seconds: 0 }
    //   ];

    // timeSince=(date:Date):string => {
    //     const seconds = Math.floor((Date.now() - date.getTime()) / 1000);
    //     const interval = this.intervals.find(i => i.seconds < seconds);
    //     const count = Math.floor(seconds / interval.seconds);
    //     return count + " "  + interval.label + " "  + (count !== 1 ? 's' : '') + " ago";
    //   }

}