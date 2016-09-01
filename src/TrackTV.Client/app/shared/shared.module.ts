import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {DoubleDigit} from './doubleDigit.pipe';
import {PersistentContainer} from './persistentContainer';
import {LocalStorageContainer} from './localStorageContainer';

import {ApiPath} from './apiPath.service';
import {AuthGuard} from './auth-guard.service';
import {Identity} from './identity.service';
import {SubscriptionService} from './subscription.service';

@NgModule({
    imports: [
        BrowserModule,
    ],
    declarations: [
        DoubleDigit
    ],
    providers: [
        {provide: PersistentContainer, useClass: LocalStorageContainer},
        ApiPath,
        AuthGuard,
        Identity,
        SubscriptionService
    ],
    exports: [
        DoubleDigit
    ]
})
export class SharedModule {
}