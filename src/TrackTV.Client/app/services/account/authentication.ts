import {Injectable} from 'angular2/core'
import {Observable} from 'rxjs/Rx'
import {Http, Headers, RequestOptions, Response} from 'angular2/http'
import {Identity} from "./identity";
import {ApiPath} from "../apiPath";

export class RegisterUser {

    email : string;

    password : string;
    confirmPassword : string;
}

export class LoginUser {

    username : string;
    password : string;

    grant_type : string;
}

@Injectable()
export class Authentication {

    constructor(private http : Http,
                private identity : Identity,
                private apiPath : ApiPath) {
    }

    private account : (string) => string = this.apiPath.service('account');

    public signup(user : RegisterUser) {

        return this.http.post(this.account('/register'), JSON.stringify(user));
    }

    public login(user : LoginUser) {

        user.grant_type = 'password';

        return this.http.post(this.apiPath.loginPath, JSON.stringify(user));
    }

    public logout() {

        var headers = this.identity.addAuthorizationHeader();

        let observable : Observable<Response> =
            this.http.post(this.account('/logout'), undefined, new RequestOptions({headers: new Headers(headers)}));

        return observable
            .do((response : Response) => this.identity.removeUser())
            .catch((error : Response) => {

                // removing the user, despite the server being unavailable
                this.identity.removeUser();

                return Observable.throw(error.json().error)
            });
    }
}