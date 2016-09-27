import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {ShowsService} from './shows.service';
import {SearchShows} from './shows.models';

@Injectable()
export class ShowsByNameResolve implements Resolve<SearchShows> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<SearchShows> {

        const {query} = route.params;

        return this.showsService.search(query);
    }
}