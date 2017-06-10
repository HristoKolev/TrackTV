import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { accountRouting } from './account.routes';
import { IdentityModule } from '../identity/identity.module';

@NgModule({
    imports: [
        ReactiveFormsModule,
        IdentityModule,
        accountRouting,
    ],
    declarations: [
        LoginComponent,
        RegisterComponent,
    ],

})
export class AccountModule {
}
