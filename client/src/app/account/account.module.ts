import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { AccountActions, accountSagas, loginReducer, registerReducer } from './account-state';
import { reduxState } from '../../infrastructure/redux-store';
import { apiClient } from '../shared/api-client';
import { SharedModule } from '../shared/shared.module';
import { RegisterComponent } from './register.component';

const routes: Routes = [
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
        SharedModule,
    ],
    declarations: [
        LoginComponent,
        RegisterComponent,
    ],
    providers: [
        AccountActions,
    ],
})
export class AccountModule {

    constructor() {

        reduxState.addReducers({
            login: loginReducer,
            register: registerReducer,
        });

        reduxState.addSagas(accountSagas(apiClient));
    }
}

