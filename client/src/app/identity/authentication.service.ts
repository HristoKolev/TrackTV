import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { Headers, Http, RequestOptions, Response } from '@angular/http';
import { LoginError, LoginUser, RegisterError, RegisterUser } from './authentication.models';
import { Identity } from './identity.service';
import { ApiPath } from '../shared/apiPath.service';
import { User } from './identity.models';

@Injectable()
export class Authentication {

    private readonly auth: (arg: string) => string = this.apiPath.service('/public/auth');

    constructor(private http: Http,
                private identity: Identity,
                private apiPath: ApiPath) {
    }

    public signup(user: RegisterUser): Observable<Response> {

        let url = this.auth('/register');
        console.log(url);

        return this.http.post(url, this.queryParams(user), this.urlEncodedOptions)
            .catch((res: Response) => Observable.throw(this.parseSignupError(res.json())));
    }

    public login(user: LoginUser): Observable<User> {

        user.grant_type = 'password';

        return this.http.post(this.apiPath.loginPath, this.queryParams(user), this.urlEncodedOptions)
            .map((res: Response) => {

                const user = res.json() as User;

                this.identity.load(user);

                return user;
            })
            .catch((res: Response) => Observable.throw(this.parseLoginError(res.json())));
    }

    public logout(): Observable<Response> {

        return this.http.post(this.auth('/logout'), null, this.identity.authenticatedOptions)
            .do((response: Response) => this.identity.removeUser(), () => this.identity.removeUser());
    }

    private queryParams(source: any): string {

        const array: string[] = [];

        for (const key in source) {

            //noinspection JSUnfilteredForInLoop
            array.push(encodeURIComponent(key) + '=' + encodeURIComponent(source[key]));
        }

        return array.join('&');
    }

    private get urlEncodedOptions(): RequestOptions {

        const headers = {'Content-Type': 'application/x-www-form-urlencoded'};

        return new RequestOptions({headers: new Headers(headers)});
    }

    private parseSignupError(error: any): RegisterError {

        if (error.modelState && error.modelState['model.Password']) {

            return RegisterError.InvalidPassword;
        }

        return RegisterError.ServerError;
    }

    private parseLoginError(error: any): LoginError {

        if (error && error.error_description === 'The user name or password is incorrect.') {

            return LoginError.InvalidCredentials;
        }

        return LoginError.ServerError;
    }
}
