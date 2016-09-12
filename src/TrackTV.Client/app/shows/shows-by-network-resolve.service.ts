import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {SimpleShows, NetworkShows} from './shows.models';
import {ShowsService} from './shows.service';

@Injectable()
export class ShowsByNetworkResolve implements Resolve<{networkShows : NetworkShows}> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<any>|Promise<any>|any {

        const network = route.params['network'];

        return this.showsService.network(network)
            .map((networkShows : NetworkShows) => ({networkShows}));
    }
}