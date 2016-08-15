import {Headers, RequestOptions} from '@angular/http';

import {Identity} from  './account/identity';

export abstract class AuthenticatedService {

    constructor(protected identity : Identity) {
    }

    protected get authenticatedOptions() : RequestOptions {

        if (!this.identity.isAuthenticated) {

            throw new Error('The user is not authenticated.');
        }

        return new RequestOptions({

            headers: new Headers(this.identity.addAuthorizationHeader())
        });
    }
}