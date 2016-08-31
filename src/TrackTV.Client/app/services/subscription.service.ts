import {Injectable} from '@angular/core';
import {Http, Response} from  '@angular/http';
import {Observable} from  'rxjs';

import {Authentication} from  '../services/index';
import {ApiPath} from  './apiPath';

@Injectable()
export class SubscriptionService {

    constructor(private apiPath : ApiPath,
                private http : Http,
                private  authentication : Authentication) {

    }

    private subscription : (path : string) => string = this.apiPath.service('subscription');

    public subscribe(id : number) : Observable<Response> {

        return this.http.post(this.subscription('/subscribe/' + id), undefined,
            this.authentication.authenticatedOptions);
    }

    public unsubscribe(id : number) : Observable<Response> {

        return this.http.post(this.subscription('/unsubscribe/' + id), undefined,
            this.authentication.authenticatedOptions);
    }
}