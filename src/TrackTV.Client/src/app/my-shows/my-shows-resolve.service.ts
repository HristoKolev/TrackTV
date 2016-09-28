import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs/Observable';
import {MyShowsService} from './my-shows.service';
import {MyShows, MyShowsModel} from './my-shows.models';

@Injectable()
export class MyShowsResolve implements Resolve<MyShowsModel> {

    constructor(private myShowsService : MyShowsService) {
    }

    public  resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<MyShowsModel> {

        const observables = [

            this.myShowsService.continuing(),
            this.myShowsService.ended()
        ];

        return Observable.forkJoin(observables)
            .map((data : MyShows[]) => ({continuing: data[0], ended: data[1]}));
    }
}