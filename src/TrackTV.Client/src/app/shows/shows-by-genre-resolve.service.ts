import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {SimpleShows} from './shows.models';
import {ShowsService} from './shows.service';

@Injectable()
export class ShowsByGenreResolve implements Resolve<{shows : SimpleShows, genre : string}> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<{shows : SimpleShows, genre : string}> {

        const {genre} = route.params;

        return this.showsService.genre(genre)
            .map((shows : SimpleShows) => ({shows, genre}));
    }
}