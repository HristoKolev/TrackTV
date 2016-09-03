import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Rx';
import {Http, Headers, RequestOptions, Response} from '@angular/http';

import * as $ from 'jquery';

import {Identity, ApiPath, User} from "../shared/index";
import {RegisterUser, RegisterError, LoginUser, LoginError} from './authentication.models';

@Injectable()
export class Authentication {

    private account : (arg : string) => string = this.apiPath.service('/account');
    
    constructor(private http : Http,
                private identity : Identity,
                private apiPath : ApiPath) {

    }

    private get urlEncodedOptions() : RequestOptions {

        const headers = {'Content-Type': 'application/x-www-form-urlencoded'};

        return new RequestOptions({headers: new Headers(headers)});
    }

    public signup(user : RegisterUser) : Observable<Response> {

        return this.http.post(this.account('/register'), $.param(user), this.urlEncodedOptions)
            .catch((err : Response) => {

                let errorData = err.json();

                if (errorData.modelState && errorData.modelState['model.Password']) {

                    return Observable.throw(RegisterError.InvalidPassword);
                }

                return Observable.throw(RegisterError.ServerError);
            });
    }

    public login(user : LoginUser) : Observable<User> {

        user.grant_type = 'password';

        return this.http.post(this.apiPath.loginPath, $.param(user), this.urlEncodedOptions)
            .map((res : Response) => {

                const user = res.json() as User;

                this.identity.load(user);

                return user;
            })
            .catch((res : Response) => {

                const error = res.json();

                if (error && error.error_description === 'The user name or password is incorrect.') {

                    return Observable.throw(LoginError.InvalidCredentials);
                }

                return Observable.throw(LoginError.ServerError);
            });
    }

    public logout() : Observable<Response> {

        return this.http.post(this.account('/logout'), undefined, this.identity.authenticatedOptions)
            .do((response : Response) => this.identity.removeUser());
    }
}