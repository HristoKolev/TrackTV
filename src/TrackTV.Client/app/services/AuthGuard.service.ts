import {Injectable} from '@angular/core';
import {CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot} from  '@angular/router';
import {Observable} from  'rxjs';
import {Identity} from "./account/identity";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router : Router,
                private identity : Identity) {
    }

    canActivate(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<boolean> | Promise<boolean>|boolean {

        if (this.identity.isAuthenticated) {
            return true;
        }

        this.router.navigate(['/login']);

        return false;
    }
}