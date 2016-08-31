import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {ReactiveFormsModule} from  '@angular/forms';

import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {LogoutComponent} from './logout/logout.component';

import {LogoutGuard} from './logout/logout-guard.service';
import {Authentication} from './authentication.service';

import {accountRouting} from './account.routes';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        accountRouting
    ],
    declarations: [
        LoginComponent,
        RegisterComponent,
        LogoutComponent
    ],
    providers: [
        Authentication,
        LogoutGuard
    ]
})
export class AccountModule {
}