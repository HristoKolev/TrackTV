import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {SimpleShows, ShowsByGenreModel} from './shows.models';
import {ShowsService} from './shows.service';

@Injectable()
export class ShowsByGenreResolve implements Resolve<ShowsByGenreModel> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<ShowsByGenreModel> {

        const {genre} = route.params;

        return this.showsService.genre(genre)
            .map((shows : SimpleShows) => ({shows, genre}));
    }
}