import {Injectable} from 'angular2/core';
import {Http, Headers, RequestOptions, Response} from 'angular2/http';
import {Observable} from 'rxjs/Rx';

import {ApiPath, SimpleShows, SimpleShow} from "./index";

@Injectable()
export class ShowsService {

    constructor(private http : Http,
                private apiPath : ApiPath) {

    }

    private shows : (path : string) => string = this.apiPath.service('shows');

    private addBaseUrl(shows : SimpleShow[]) : void {

        let baseUrl = this.apiPath.path();

        for (let show of shows) {

            show.poster = baseUrl + show.poster;
            show.banner = baseUrl + show.banner;
        }
    }

    public top() : Observable<SimpleShows> {

        return this.http.get(this.shows('/top'), undefined)
            .map((res : Response) : SimpleShows => {

                const data : SimpleShows = res.json() as SimpleShows;

                this.addBaseUrl(data.running);
                this.addBaseUrl(data.ended);

                return data;
            });
    }
}

