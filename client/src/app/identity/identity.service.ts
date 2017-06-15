import { Headers, RequestOptions } from '@angular/http';
import { store, User } from '../shared/MutStore';

export class Identity {

    private get user(): User {

        return store.payload.user;
    }

    private set user(value: User) {

        if (!store.payload.user) {

            store.payload.user = {} as User;
        }

        Object.assign(store.payload.user, value);

        store.save();
    }

    public get isAuthenticated(): boolean {

        return !!store.payload.user;
    }

    public get username(): string {

        if (this.isAuthenticated) {

            return this.user.username || 'No username';
        } else {

            return 'Guest';
        }
    }

    public get isAdmin() {

        return false;
    }

    public get authenticatedOptions(): RequestOptions {

        if (!this.isAuthenticated) {

            throw new Error('The user is not authenticated.');
        }

        return new RequestOptions({

            headers: new Headers(this.addAuthorizationHeader()),
        });
    }

    constructor() {

        store.load();
    }

    public load(user: User) {

        if (!user) {

            throw new Error('user is falsy');
        }

        this.user = user;
    }

    public addAuthorizationHeader(headers: any = {}): any {

        if (!this.isAuthenticated) {

            throw this.notAuthenticatedError();
        }

        headers.Authorization = 'Bearer ' + this.user.access_token;

        return headers;
    }

    public removeUser(): void {

        if (!this.isAuthenticated) {

            throw this.notAuthenticatedError();
        }

        this.clearUserData();
    }

    private clearUserData(): void {

        store.payload.user = undefined;
        store.save();
    }

    private notAuthenticatedError(): Error {

        return Error('There currently is no authorized user.');
    }
}
