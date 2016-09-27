import {NgModule} from '@angular/core';
import {HttpModule} from '@angular/http';
import {DoubleDigit} from './doubleDigit.pipe';
import {LocalStorageContainer} from './localStorageContainer';
import {ApiPath} from './apiPath.service';
import {SubscriptionService} from './subscription.service';
import {IdentityModule} from '../identity/identity.module';
import {PersistentContainer} from './persistentContainer';

@NgModule({
    imports: [
        HttpModule,
        IdentityModule
    ],
    declarations: [
        DoubleDigit
    ],
    providers: [
        ApiPath,
        SubscriptionService
    ],
    exports: [
        DoubleDigit
    ]
})
export class SharedModule {
}