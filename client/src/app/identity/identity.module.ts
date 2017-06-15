import { NgModule } from '@angular/core';
import { Identity } from './identity.service';
import { AuthGuard } from './auth-guard.service';
import { Authentication } from './authentication.service';

@NgModule({

    providers: [
        Identity,
        Authentication,
        AuthGuard,
    ],
})
export class IdentityModule {
}
