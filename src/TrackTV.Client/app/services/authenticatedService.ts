import {Identity} from  './account/identity';
import {Headers, RequestOptions} from '@angular/http';

export abstract class AuthenticatedService {

    constructor(protected identity : Identity) {
    }

    protected authenticatedOptions() : RequestOptions {

        if (!this.identity.isAuthenticated) {

            throw new Error('The user is not authenticated.');
        }

        return new RequestOptions({

            headers: new Headers(this.identity.addAuthorizationHeader())
        });
    }
}