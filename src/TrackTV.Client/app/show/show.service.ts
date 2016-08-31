import {Injectable} from '@angular/core';
import {Http, Response, RequestOptions} from  '@angular/http';
import {Observable} from  'rxjs';

import {Authentication, Identity, ApiPath} from  '../services/index';
import {ShowDetails} from  './show.models';

@Injectable()
export class ShowService {

    constructor(private identity : Identity,
                private apiPath : ApiPath,
                private http : Http,
                private authentication : Authentication) {

    }

    private show : (path : string) => string = this.apiPath.service('show');

    private baseUrl : string = this.apiPath.path();

    private processData(res : Response) {

        const data = res.json();

        data.banner = this.baseUrl + data.banner;

        if (data.firstAired) {

            data.firstAired = new Date(data.firstAired);
        }

        return data as ShowDetails;
    }

    public getShow(name : string) : Observable<ShowDetails> {

        let options : RequestOptions = undefined;

        if (this.identity.isAuthenticated) {

            options = this.authentication.authenticatedOptions;
        }

        return this.http.get(this.show('/' + name), options)
            .map((res : Response) => this.processData(res));
    }
}