import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';

import {Observable} from 'rxjs';

import {ApiPath, Identity} from '../shared/index';
import {MyShows} from './my-shows.models';

@Injectable()
export class MyShowsService {

    constructor(private http : Http,
                private apiPath : ApiPath,
                private  identity : Identity) {

    }

    private myShows : (path : string) => string = this.apiPath.service('/myshows');

    public continuing(page = 1) : Observable<MyShows> {

        return this.http.get(this.myShows('/continuing/' + page), this.identity.authenticatedOptions)
            .map((res : Response) => this.processResponse(res));
    }

    public ended(page = 1) : Observable<MyShows> {

        return this.http.get(this.myShows('/ended/' + page), this.identity.authenticatedOptions)
            .map((res : Response) => this.processResponse(res));
    }

    private processResponse(res : Response) : MyShows {

        const data = res.json() as MyShows;

        for (let show of data.shows) {

            if (show.lastEpisode) {

                show.lastEpisode.firstAired = new Date(show.lastEpisode.firstAired.toString());
            }

            if (show.nextEpisode) {

                show.nextEpisode.firstAired = new Date(show.nextEpisode.firstAired.toString());
            }
        }

        return data;
    }
}