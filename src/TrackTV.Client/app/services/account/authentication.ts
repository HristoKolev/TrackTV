import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Rx';
import {Http, Headers, RequestOptions, Response} from '@angular/http';

import * as $ from 'jquery';

import {Identity} from "./identity";
import {ApiPath} from "../apiPath";
import {RegisterUser, RegisterError, LoginUser, LoginError} from './authenticationModels';

@Injectable()
export class Authentication {

    constructor(private http : Http,
                private identity : Identity,
                private apiPath : ApiPath) {

    }

    private account : (arg : string) => string = this.apiPath.service('/account');

    private getUrlEncodedOptions() : RequestOptions {

        const headers = {'Content-Type': 'application/x-www-form-urlencoded'};

        return new RequestOptions({headers: new Headers(headers)});
    }

    private getAuthenticationOptions() : RequestOptions {

        const headers = this.identity.addAuthorizationHeader();

        return new RequestOptions({headers: new Headers(headers)});
    }

    public signup(user : RegisterUser) {

        return this.http.post(this.account('/register'), $.param(user), this.getUrlEncodedOptions())
            .catch((err : Response) => {

                let errorData = err.json();

                if (errorData.modelState && errorData.modelState['model.Password']) {

                    return Observable.throw(RegisterError.InvalidPassword);
                }

                return Observable.throw(RegisterError.ServerError);
            });
    }

    public login(user : LoginUser) {

        user.grant_type = 'password';

        return this.http.post(this.apiPath.loginPath, $.param(user), this.getUrlEncodedOptions())
            .map((res : Response) => res.json())
            .do(user => {

                this.identity.load(user);
            })
            .catch((res : Response) => {

                const error = res.json();

                if (error && error.error_description === 'The user name or password is incorrect.') {

                    return Observable.throw(LoginError.InvalidCredentials);
                }

                return Observable.throw(LoginError.ServerError);
            });
    }

    public logout() {

        return this.http.post(this.account('/logout'), undefined, this.getAuthenticationOptions())
            .do((response : Response) => this.identity.removeUser())
            .catch((error : Response) => {

                // removing the user, despite the server being unavailable
                this.identity.removeUser();

                return Observable.throw(error.json())
            });
    }

    public get authenticatedOptions() : RequestOptions {

        if (!this.identity.isAuthenticated) {

            throw new Error('The user is not authenticated.');
        }

        return new RequestOptions({

            headers: new Headers(this.identity.addAuthorizationHeader())
        });
    }
}