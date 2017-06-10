import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { DoubleDigit } from './doubleDigit.pipe';
import { ApiPath } from './apiPath.service';
import { SubscriptionService } from './subscription.service';
import { IdentityModule } from '../identity/identity.module';

@NgModule({
    imports: [
        HttpModule,
        IdentityModule,
    ],
    declarations: [
        DoubleDigit,
    ],
    providers: [
        ApiPath,
        SubscriptionService,
    ],
    exports: [
        DoubleDigit,
    ],
})
export class SharedModule {
}
