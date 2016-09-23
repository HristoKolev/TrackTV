import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';
import {Observable} from 'rxjs/Rx';
import {SimpleShows, SimpleShow, NetworkShows, SearchShows} from './shows.models';
import {ApiPath} from '../shared/apiPath.service';

@Injectable()
export class ShowsService {

    constructor(private http : Http,
                private apiPath : ApiPath) {
    }

    private shows : (path : string) => string = this.apiPath.service('/shows');

    private addBaseUrl(shows : SimpleShow[]) : void {

        const baseUrl = this.apiPath.path();

        for (const show of shows) {

            show.poster = baseUrl + show.poster;
            show.banner = baseUrl + show.banner;
        }
    }

    private parseSimpleShows(res : Response) : any {

        const data = res.json();

        if (data.running) {

            this.addBaseUrl(data.running);
        }

        if (data.ended) {

            this.addBaseUrl(data.ended);
        }

        if (data.shows) {

            this.addBaseUrl(data.shows);
        }

        return data;
    }

    public top() : Observable<SimpleShows> {

        return this.http.get(this.shows('/top'), null)
            .map(res => this.parseSimpleShows(res));
    }

    public genre(name : string) : Observable<SimpleShows> {

        return this.http.get(this.shows('/genre/' + name), null)
            .map(res => this.parseSimpleShows(res));
    }

    public search(query : string, page : number = 1) : Observable<SearchShows> {

        return this.http.get(this.shows('/search/' + query + '/' + page), null)
            .map(res => this.parseSimpleShows(res));
    }

    public network(name : string, page : number = 1) : Observable<NetworkShows> {

        return this.http.get(this.shows('/network/' + name + '/' + page), null)
            .map(res => this.parseSimpleShows(res));
    }
}

