import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CanActivate } from '@angular/router';
import { AuthHelper } from '../helpers/app.auth.helper';

@Injectable()
export class SchedulingGuard implements CanActivate {

    constructor(private router: Router, private authHelper: AuthHelper) { }

    canActivate() {
        if (this.authHelper.IsInRol("admin") ||
            this.authHelper.IsInRol("schedulingeditor") ||
            this.authHelper.IsInRol("user") ||
            this.authHelper.IsInRol("viewer")) {
            return true;
        }
        this.router.navigate(['unauthorized']);
    }

    canActivateWithOuthRedirecting() {
        if (this.authHelper.IsInRol("admin") ||
            this.authHelper.IsInRol("schedulingeditor") ||
            this.authHelper.IsInRol("user") ||
            this.authHelper.IsInRol("viewer")) {
            return true;
        }
        return false;
    }
}

