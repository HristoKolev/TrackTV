import {Injectable} from '@angular/core';
import {Http, Response, RequestOptions} from  '@angular/http';
import {Observable} from  'rxjs';

import {AuthenticatedService} from  './authenticatedService';
import {Identity} from  './account/identity';
import {ApiPath} from  './apiPath';
import {ShowDetails} from  './show.models';

@Injectable()
export class ShowService extends AuthenticatedService {

    constructor(identity : Identity,
                private apiPath : ApiPath,
                private http : Http) {

        super(identity);
    }

    private show : (path : string) => string = this.apiPath.service('show');

    private baseUrl : string = this.apiPath.path();

    public getShow(name : string) : Observable<ShowDetails> {

        let options : RequestOptions = undefined;

        if (this.identity.isAuthenticated) {

            options = this.authenticatedOptions;
        }

        return this.http.get(this.show('/' + name), options)
            .map((res : Response) => {

                const data = res.json();

                data.banner = this.baseUrl + data.banner;

                if (data.firstAired) {

                    data.firstAired = new Date(data.firstAired);
                }

                return data as ShowDetails;
            });
    }
}