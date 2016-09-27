import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Rx';
import {Http, Headers, RequestOptions, Response} from '@angular/http';
import {RegisterUser, RegisterError, LoginUser, LoginError} from './authentication.models';
import {Identity} from './identity.service';
import {ApiPath} from '../shared/apiPath.service';
import {User} from './identity.models';

@Injectable()
export class Authentication {

    private readonly account : (arg : string) => string = this.apiPath.service('/account');

    constructor(private http : Http,
                private identity : Identity,
                private apiPath : ApiPath) {
    }

    private queryParams(source : any) : string {

        const array : string[] = [];

        for (const key in source) {

            //noinspection JSUnfilteredForInLoop
            array.push(encodeURIComponent(key) + "=" + encodeURIComponent(source[key]));
        }

        return array.join("&");
    }

    private get urlEncodedOptions() : RequestOptions {

        const headers = {'Content-Type': 'application/x-www-form-urlencoded'};

        return new RequestOptions({headers: new Headers(headers)});
    }

    private parseSignupError(error : any) : RegisterError {

        if (error.modelState && error.modelState['model.Password']) {

            return RegisterError.InvalidPassword;
        }

        return RegisterError.ServerError;
    }

    public signup(user : RegisterUser) : Observable<Response> {

        return this.http.post(this.account('/register'), this.queryParams(user), this.urlEncodedOptions)
            .catch((res : Response) => Observable.throw(this.parseSignupError(res.json())));
    }

    private parseLoginError(error : any) : LoginError {

        if (error && error.error_description === 'The user name or password is incorrect.') {

            return LoginError.InvalidCredentials;
        }

        return LoginError.ServerError;
    }

    public login(user : LoginUser) : Observable<User> {

        user.grant_type = 'password';

        return this.http.post(this.apiPath.loginPath, this.queryParams(user), this.urlEncodedOptions)
            .map((res : Response) => {

                const user = res.json() as User;

                this.identity.load(user);

                return user;
            })
            .catch((res : Response) => Observable.throw(this.parseLoginError(res.json())));
    }

    public logout() : Observable<Response> {

        return this.http.post(this.account('/logout'), null, this.identity.authenticatedOptions)
            .do((response : Response) => this.identity.removeUser());
    }
}