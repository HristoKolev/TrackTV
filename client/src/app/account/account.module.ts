import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { AccountActions } from './state';

const routes: Routes = [
    {path: 'login', component: LoginComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
    ],
    declarations: [
        LoginComponent,
    ],
    providers: [
        AccountActions,
    ],
})
export class AccountModule {
}
