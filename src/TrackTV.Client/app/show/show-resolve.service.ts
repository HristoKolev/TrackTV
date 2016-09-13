import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {ShowDetails} from './show.models';
import {ShowService} from './show.service';

@Injectable()
export class ShowResolve implements Resolve<ShowDetails> {

    constructor(private showService : ShowService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<ShowDetails> {

        const {show} = route.params;

        return this.showService.getShow(show);
    }
}