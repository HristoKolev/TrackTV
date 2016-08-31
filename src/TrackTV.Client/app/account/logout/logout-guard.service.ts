import {Injectable} from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot} from  '@angular/router';
import {Observable} from  'rxjs';

import * as toastr from 'toastr';

import {Authentication, Identity} from  '../../services/index';

@Injectable()
export class LogoutGuard implements CanActivate {

    constructor(private identity : Identity,
                private authentication : Authentication) {
    }

    private afterLogout() {

        toastr.success('Successful Logout!');
    }

    canActivate(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<boolean>|Promise<boolean>|boolean {

        if (this.identity.isAuthenticated) {

            this.authentication.logout()
                .subscribe(this.afterLogout.bind(this), this.afterLogout.bind(this));

        }

        return false;
    }
}