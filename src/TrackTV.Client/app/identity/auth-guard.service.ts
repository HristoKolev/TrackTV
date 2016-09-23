import {Injectable} from '@angular/core';
import {CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Identity} from './identity.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router : Router,
                private identity : Identity) {
    }

    public canActivate(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : boolean {

        if (this.identity.isAuthenticated) {
            return true;
        }

        this.router.navigate(['/login']);

        return false;
    }
}