import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';

import {DoubleDigit} from './doubleDigit.pipe';
import {PersistentContainer} from './persistentContainer';
import {LocalStorageContainer} from './localStorageContainer';

import {ApiPath} from './apiPath.service';
import {AuthGuard} from './auth-guard.service';
import {Identity} from './identity.service';
import {SubscriptionService} from './subscription.service';

import {PaginationService, PaginatePipe, PaginationControlsCmp} from 'ng2-pagination';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
    ],
    declarations: [
        DoubleDigit,

        PaginatePipe,
        PaginationControlsCmp
    ],
    providers: [
        {provide: PersistentContainer, useClass: LocalStorageContainer},
        ApiPath,
        AuthGuard,
        Identity,
        SubscriptionService,

        PaginationService,
    ],
    exports: [
        DoubleDigit,

        PaginatePipe,
        PaginationControlsCmp
    ]
})
export class SharedModule {
}