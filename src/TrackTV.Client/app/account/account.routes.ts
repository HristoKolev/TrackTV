import {ModuleWithProviders}  from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {LoginComponent} from  './login/login.component';
import {RegisterComponent} from  './register/register.component';
import {LogoutComponent} from  './logout/logout.component';

import {LogoutGuard} from  './logout/logout-guard.service';

const accountRoutes : Routes = [

    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'logout', component: LogoutComponent, canActivate: [LogoutGuard]}
];

export const accountRouting : ModuleWithProviders = RouterModule.forChild(accountRoutes);