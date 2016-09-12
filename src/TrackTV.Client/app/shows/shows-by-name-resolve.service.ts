import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {ShowsService} from './shows.service';
import {SearchShows} from './shows.models';

@Injectable()
export class ShowsByNameResolve implements Resolve<{searchShows : SearchShows}> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<any>|Promise<any>|any {

        const query = route.params['query'];

        return this.showsService.search(query)
            .map((searchShows : SearchShows) => ({searchShows}));
    }
}