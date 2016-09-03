import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';

import {Observable} from 'rxjs';

import {ApiPath} from './apiPath.service';

import {Identity} from '../shared/index';

@Injectable()
export class SubscriptionService {

    constructor(private apiPath : ApiPath,
                private http : Http,
                private  identity : Identity) {

    }

    private subscription : (path : string) => string = this.apiPath.service('/subscription');

    public subscribe(id : number) : Observable<Response> {

        return this.http.post(this.subscription('/subscribe/' + id), undefined,
            this.identity.authenticatedOptions);
    }

    public unsubscribe(id : number) : Observable<Response> {

        return this.http.post(this.subscription('/unsubscribe/' + id), undefined,
            this.identity.authenticatedOptions);
    }
}