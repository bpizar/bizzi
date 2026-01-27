import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

const jwtHelper = new JwtHelperService();

@Injectable()
export class AuthHelper {

    constructor() { }

    //jwtHelper: JwtHelper = new JwtHelper();

    loggedIn() {
        let token = localStorage.getItem('token');
        // return !this.jwtHelper.isTokenExpired(token);
        return !jwtHelper.isTokenExpired(token);
    }

    LogOut() {
        localStorage.clear();
    }

    GetEmail = (): string => {
        let token = localStorage.getItem('token');
        let decode = jwtHelper.decodeToken(token);
        return decode.sub;
    }

    IsInRol = (rol: string): boolean => {
        let token = localStorage.getItem('token');
        let decode = jwtHelper.decodeToken(token);
        let roles = decode.roles;

        if (roles != undefined) {
            if (roles.constructor !== Array) {
                return roles.toString() == rol;
            }
            else {
                for (let r of roles) {
                    if (r == rol) {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}

//let b = this.jwtHelper.getTokenExpirationDate(token);
//let c = this.jwtHelper.isTokenExpired(token);
