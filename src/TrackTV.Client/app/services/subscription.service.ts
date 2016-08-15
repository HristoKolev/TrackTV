import {Injectable} from '@angular/core';
import {Http, Response} from  '@angular/http';
import {Observable} from  'rxjs';

import {AuthenticatedService} from  './authenticatedService';
import {Identity} from  './account/identity';
import {ApiPath} from  './apiPath';

@Injectable()
export class SubscriptionService extends AuthenticatedService {

    constructor(identity : Identity,
                private apiPath : ApiPath,
                private http : Http) {

        super(identity);
    }

    private subscription : (path : string) => string = this.apiPath.service('subscription');

    subscribe(id : number) : Observable<Response> {

        return this.http.post(this.subscription('/subscribe/' + id), undefined, this.authenticatedOptions);
    }

    unsubscribe(id : number) : Observable<Response> {

        return this.http.post(this.subscription('/unsubscribe/' + id), undefined, this.authenticatedOptions);
    }
}