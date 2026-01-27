import { Injectable } from '@angular/core';
//import { JwtHelper } from 'angular2-jwt';

@Injectable()
export class CommonHelper {

    constructor() { }

    getTime = (timex: string): string => {
        if (timex) {
            return timex.indexOf("T") !== -1 ? timex.split("T")[1] : timex.split(" ")[1];
        }
        return "Current";
    }

    playAudio() {
        let audio = new Audio();
        audio.src = "../../../assets/audio/alarm.mp3";
        audio.load();
        audio.play();
    }
    //this.playAudio();

    convertMinsToHrsMins = (secsInput): any => {
        let isNegative: boolean = secsInput < 0;
        let secs: any = isNegative ? secsInput * -1 : secsInput;
        var hours = Math.floor(secs / (60 * 60));
        var divisor_for_minutes = secs % (60 * 60);
        var minutes = Math.floor(divisor_for_minutes / 60);
        var divisor_for_seconds = divisor_for_minutes % 60;
        var seconds = Math.ceil(divisor_for_seconds);
        if (isNaN(hours)) {
            hours = 0;
        }
        if (isNaN(minutes)) {
            minutes = 0;
        }
        if (isNaN(seconds)) {
            seconds = 0;
        }

        var h2 = hours < 10 ? '0' + hours : hours;
        var m2 = minutes < 10 ? '0' + minutes : minutes;
        var s2 = seconds < 10 ? '0' + seconds : seconds;

        return isNegative ? "-" + h2 + ':' + m2 + ':' + s2 : " " + h2 + ':' + m2 + ':' + s2;
    }

    convertHrsminsToSecs = (hm: string): number => {
        var hours = parseInt(hm.substr(1, 2));
        var mins = parseInt(hm.substr(4, 2));
        var secs = parseInt(hm.substr(7, 2));
        return secs + mins * 60 + hours * 3600;
    }
}