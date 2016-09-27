import {NgModule} from '@angular/core';
import {HttpModule} from '@angular/http';
import {DoubleDigit} from './doubleDigit.pipe';
import {PersistentContainer} from './persistentContainer';
import {LocalStorageContainer} from './localStorageContainer';
import {ApiPath} from './apiPath.service';
import {SubscriptionService} from './subscription.service';
import {Ng2PaginationModule, PaginationControlsCmp, PaginatePipe} from 'ng2-pagination';
import {IdentityModule} from '../identity/identity.module';

@NgModule({
    imports: [
        HttpModule,
        Ng2PaginationModule,
        IdentityModule
    ],
    declarations: [
        DoubleDigit
    ],
    providers: [
        {provide: PersistentContainer, useClass: LocalStorageContainer},
        ApiPath,
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