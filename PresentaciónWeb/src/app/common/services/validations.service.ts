import { Injectable } from '@angular/core';
import { FormField } from 'src/app/staff/staff-forms/staff-forms.component.model';

@Injectable()
export class ValidationService {

    arrayErrors: string[] = [];

    constructor() { }

    setFormField(formField: any, value: string) {
        switch (formField.datatype) {
            case 'text':
                this.stringValidation(formField, value)
                break;
            case 'number':
                this.numberValidation(formField, value)
                break;
            case 'decimal number':
                this.decimalValidation(formField, value)
                break;
            case 'date':
                this.dateValidation(formField, value)
                break;
            default:
                break;
        }
    }

    private stringValidation(formField: any, value: string) {
        switch (formField.constraints) {
            case 'Alphanumeric':
                if (!this.alphanumericIsvalid(value))
                    this.arrayErrors.push(formField.name);
                break;
            case 'Alphabetic':
                if (!this.alphabeticIsvalid(value))
                    this.arrayErrors.push(formField.name);
                break;
            case 'Email':
                if (!this.emailIsvalid(value))
                    this.arrayErrors.push(formField.name);
                break;
            case 'Numbers':
                if (!this.numbersIsvalid(value))
                    this.arrayErrors.push(formField.name);
                break;
            default:
                break;
        }
    }

    private numberValidation(formField: any, value: string) {
        switch (formField.constraints) {
            case 'Number > 0':
                if (!this.integerGreaterThanZero(value))
                    this.arrayErrors.push(formField.name);
                break;
            case 'Number >= 0':
                if (!this.integerEqualGreaterThanZero(value))
                    this.arrayErrors.push(formField.name);
                break;
            case 'Number < 0':
                if (!this.integerLessThanZero(value))
                    this.arrayErrors.push(formField.name);
                break;
            default:
                break;
        }
    }

    decimalValidation(formField: any, value: string) {
        switch (formField.constraints) {
            case 'Number > 0':
                if (!this.integerGreaterThanZero(value))
                    this.arrayErrors.push(formField.name);
                break;
            case 'Number >= 0':
                if (!this.integerEqualGreaterThanZero(value))
                    this.arrayErrors.push(formField.name);
                break;
            case 'Number < 0':
                if (!this.integerLessThanZero(value))
                    this.arrayErrors.push(formField.name);
                break;
            default:
                break;
        }
    }

    dateValidation(formField: any, value: string) {
        switch (formField.constraints) {
            case 'From today':
                if (!this.fromToday(value))
                    this.arrayErrors.push(formField.name);
                break;
            case 'Until today':
                if (!this.untilToday(value))
                    this.arrayErrors.push(formField.name);
                break;
            default:
                break;
        }
    }

    alphanumericIsvalid(email: string): boolean {
        let regexp = new RegExp('^[A-Za-zä-üÄ-Üá-úÁ-Ú0-9 ]+$', 'g');
        return email.match(regexp) ? true : false;
    }

    alphabeticIsvalid(email: string): boolean {
        let regexp = new RegExp('^[A-Za-zä-üÄ-Üá-úÁ-Ú ]+$', 'gi');
        return email.match(regexp) ? true : false;
    }

    emailIsvalid(email: string): boolean {
        let regexp = new RegExp('\\b[\\w\\.-]+@[\\wº\.-]+\\.\\w{2,4}\\b', 'gi');
        return email.match(regexp) ? true : false;
    }

    numbersIsvalid(email: string): boolean {
        let regexp = new RegExp('^[0-9]{1,2}([,.][0-9]{1,2})?$', 'g');
        return email.match(regexp) ? true : false;
    }

    integerGreaterThanZero(number: string): boolean {
        return parseInt(number) > 0 ? true : false;
    }

    integerEqualGreaterThanZero(number: string): boolean {
        return parseInt(number) >= 0 ? true : false;
    }

    integerLessThanZero(number: string): boolean {
        return parseInt(number) < 0 ? true : false;
    }

    fromToday(_date: string) {
        let today = new Date();
        today.setDate(today.getDate() - 1);
        let date = new Date(_date);
        return date > today ? true : false;
    }

    untilToday(_date: string) {
        let today = new Date();
        let date = new Date(_date);
        return date < today ? true : false;
    }

    getBuildedMessage() {
        let msg = '';
        let verb = '';
        if (this.arrayErrors.length > 1) {
            msg = 'The fields ';
            verb = ' are';
        }
        else {
            msg = 'The field ';
            verb = ' is';
        }
        this.arrayErrors.forEach(e => {
            msg += e + ', ';
        });
        msg = msg.slice(0, msg.length - 2);
        msg += verb + ' invalid!!';
        return msg;
    }
}