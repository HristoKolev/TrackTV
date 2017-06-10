import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import { ApiPath } from './apiPath.service';
import { Identity } from '../identity/identity.service';

@Injectable()
export class SubscriptionService {

    private readonly subscription: (path: string) => string = this.apiPath.service('/subscription');

    constructor(private apiPath: ApiPath,
                private http: Http,
                private  identity: Identity) {
    }

    public subscribe(id: number): Observable<Response> {

        return this.http.post(this.subscription('/subscribe/' + id), null,
            this.identity.authenticatedOptions);
    }

    public unsubscribe(id: number): Observable<Response> {

        return this.http.post(this.subscription('/unsubscribe/' + id), null,
            this.identity.authenticatedOptions);
    }
}
