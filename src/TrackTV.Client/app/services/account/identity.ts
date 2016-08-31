import {Injectable} from '@angular/core';
import {RequestOptions, Headers} from  '@angular/http';

import {PersistentContainer, PersistentContainerKey} from '../../shared/index';

export interface User {

    access_token : string;

    userName : string;

    isInAdminRole : string;
}

@Injectable()
export class Identity {

    constructor(container : PersistentContainer<User>) {

        this.storage = new PersistentContainerKey(container, 'user');
    }

    private storage : PersistentContainerKey<User>;

    private get user() : User {

        return this.storage.get();
    }

    private set user(value : User) {

        this.storage.set(value);
    }

    public load(user : User) {

        if (!user) {

            throw new Error('user is falsy');
        }

        this.user = user;
    }

    private clearUserData() : void {

        this.storage.remove();
    }

    private notAuthenticatedError() : Error {

        return Error('There currently is no authorized user.');
    }

    public get isAuthenticated() : boolean {

        return !!this.storage.get();
    }

    public get username() : string {

        if (this.isAuthenticated) {

            return this.user.userName;
        }
        else {

            return 'Guest';
        }
    }

    public addAuthorizationHeader(headers : any = {}) : any {

        if (!this.isAuthenticated) {

            throw this.notAuthenticatedError();
        }

        headers.Authorization = 'Bearer ' + this.user.access_token;

        return headers;
    }

    public removeUser() : void {

        if (!this.isAuthenticated) {

            throw this.notAuthenticatedError();
        }

        this.clearUserData();
    }

    public get isAdmin() {

        if (this.isAuthenticated) {

            let isInAdminRole = this.user.isInAdminRole;

            switch (isInAdminRole) {
                case 'True':
                    return true;
                case 'False':
                    return false;
                default:
                    throw Error('The property "isInAdminRole" is not present or has no valid value. Value: ' + isInAdminRole);
            }
        } else {
            return false;
        }
    }

    public get authenticatedOptions() : RequestOptions {

        if (!this.isAuthenticated) {

            throw new Error('The user is not authenticated.');
        }

        return new RequestOptions({

            headers: new Headers(this.addAuthorizationHeader())
        });
    }
}