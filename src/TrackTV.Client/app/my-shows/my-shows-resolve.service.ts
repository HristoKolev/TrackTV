import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs/Observable';
import {MyShowsService} from './my-shows.service';
import {MyShows} from './my-shows.models';

@Injectable()
export class MyShowsResolve implements Resolve<{continuing : MyShows, ended : MyShows}> {

    constructor(private myShowsService : MyShowsService) {
    }

    public  resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<any>|Promise<any>|any {

        return Observable.forkJoin(
            this.myShowsService.continuing(),
            this.myShowsService.ended()
        ).map((data : MyShows[]) => ({continuing: data[0], ended: data[1]}));
    }
}