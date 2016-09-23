import {Injectable} from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import * as toastr from 'toastr';
import {Authentication} from '../../identity/authentication.service';
import {Identity} from '../../identity/identity.service';


@Injectable()
export class LogoutGuard implements CanActivate {

    constructor(private identity : Identity,
                private authentication : Authentication) {
    }

    private afterLogout() {

        toastr.success('Successful Logout!');
    }

    public canActivate(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<boolean>|Promise<boolean>|boolean {

        if (this.identity.isAuthenticated) {

            this.authentication.logout()
                .subscribe(() => this.afterLogout(), () => this.afterLogout());
        }

        return false;
    }
}