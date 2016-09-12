import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {SimpleShows} from './shows.models';
import {ShowsService} from './shows.service';

@Injectable()
export class ShowsByGenreResolve implements Resolve<{shows : SimpleShows, genreName : string}> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<any>|Promise<any>|any {

        const genreName = route.params['genre'];

        return this.showsService.genre(genreName)
            .map((shows : SimpleShows) => ({shows, genreName}));
    }
}