import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {SimpleShows, ShowsModel} from './shows.models';
import {ShowsService} from './shows.service';

@Injectable()
export class ShowsResolve implements Resolve<ShowsModel> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<ShowsModel> {

        return this.showsService.top()
            .map((shows : SimpleShows) => ({shows}));
    }
}