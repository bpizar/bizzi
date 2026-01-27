import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CanActivate } from '@angular/router';
import { AuthHelper } from '../helpers/app.auth.helper';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private authHelper: AuthHelper) { }

    canActivate() {
        if (this.authHelper.loggedIn()) {
            return true;
        } else {
            //this.router.navigate(['unauthorized']);
            this.router.navigate(['login']);
            return false;
        }
    }
}