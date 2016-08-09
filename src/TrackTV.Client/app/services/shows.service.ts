import {Injectable} from '@angular/core';
import {Http, Headers, RequestOptions, Response} from '@angular/http';
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

    private parseSimpleShows(res : Response) : SimpleShows {

        const data : SimpleShows = res.json() as SimpleShows;

        this.addBaseUrl(data.running);
        this.addBaseUrl(data.ended);

        return data;
    }

    public top() : Observable<SimpleShows> {

        return this.http.get(this.shows('/top'), undefined)
            .map(res => this.parseSimpleShows(res));
    }

    public genre(name : string) : Observable<SimpleShows> {

        return this.http.get(this.shows('/genre/' + name), undefined)
            .map(res => this.parseSimpleShows(res));
    }

    public search(query : string, page? : number) : Observable<SimpleShows> {

        page = page || 1;

        return this.http.get(this.shows('/search/' + query + '/' + page), undefined)
            .map(res => this.parseSimpleShows(res));
    }

    public network(name : string, page? : number) : Observable<SimpleShows> {

        page = page || 1;

        return this.http.get(this.shows('/network/' + name + '/' + page), undefined)
            .map(res => this.parseSimpleShows(res));
    }
}

