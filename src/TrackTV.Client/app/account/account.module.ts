import {NgModule} from '@angular/core';
import {ReactiveFormsModule} from '@angular/forms';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {LogoutComponent} from './logout/logout.component';
import {LogoutGuard} from './logout/logout-guard.service';
import {accountRouting} from './account.routes';
import {IdentityModule} from '../identity/identity.module';

@NgModule({
    imports: [
        ReactiveFormsModule,
        IdentityModule,
        accountRouting
    ],
    declarations: [
        LoginComponent,
        RegisterComponent,
        LogoutComponent
    ],
    providers: [
        LogoutGuard
    ]
})
export class AccountModule {
}