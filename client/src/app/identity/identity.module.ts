import { NgModule } from '@angular/core';
import { Identity } from './identity.service';
import { AuthGuard } from './auth-guard.service';
import { Authentication } from './authentication.service';
import { PersistentContainer } from '../shared/persistentContainer';
import { UserContainer } from './UserContainer';

@NgModule({

    providers: [
        Identity,
        Authentication,
        AuthGuard,
        {provide: PersistentContainer, useClass: UserContainer},
    ],
})
export class IdentityModule {
}
