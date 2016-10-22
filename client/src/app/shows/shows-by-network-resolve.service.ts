import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {NetworkShowsModel} from './shows.models';
import {ShowsService} from './shows.service';

@Injectable()
export class ShowsByNetworkResolve implements Resolve<NetworkShowsModel> {

    constructor(private showsService : ShowsService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<NetworkShowsModel> {

        const {network} = route.params;

        return this.showsService.network(network);
    }
}