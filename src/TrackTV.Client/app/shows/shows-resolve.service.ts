import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {SimpleShows} from './shows.models';
import {ShowsService} from './shows.service';

@Injectable()
export class ShowsResolve implements Resolve<{shows : SimpleShows}> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<any>|Promise<any>|any {

        return this.showsService.top()
            .map((shows : SimpleShows) => ({shows}));
    }
}