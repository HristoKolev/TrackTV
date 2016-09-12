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
import {Ng2PaginationModule, PaginationControlsCmp, PaginatePipe} from 'ng2-pagination';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        Ng2PaginationModule
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
        DoubleDigit,
        PaginationControlsCmp,
        PaginatePipe
    ]
})
export class SharedModule {
}