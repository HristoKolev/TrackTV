import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';

@Injectable()
export class ShowsByNameGuard {

    public canActivate(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : boolean {

        const {query} = route.params;

        return !!query && query !== 'undefined';
    }
}