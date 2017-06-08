import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {ShowsService} from './shows.service';
import {SearchShowsModel} from './shows.models';

@Injectable()
export class ShowsByNameResolve implements Resolve<SearchShowsModel> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<SearchShowsModel> {

        const {query} = route.params;

        return this.showsService.search(query);
    }
}