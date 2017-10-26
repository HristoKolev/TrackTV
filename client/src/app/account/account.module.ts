import { ChangeDetectionStrategy, Component, Injectable, NgModule, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { accountActions, accountSagas, ILoginState, IRegisterState, loginReducer, registerReducer } from './account.state';
import { reduxStore } from '../../infrastructure/redux-store';
import { apiClient } from '../shared/api-client';
import { SharedModule } from '../shared/shared.module';
import { Subscription } from 'rxjs/Subscription';

export interface UserLogin {
    username: string;
    password: string;
}

export interface UserRegister {
    username: string;
    password: string;
}

@Injectable()
export class AccountActions {

    login(user: UserLogin) {
        reduxStore.dispatch({type: accountActions.LOGIN_REQUEST_START, user});
    }

    register(user: UserRegister) {
        reduxStore.dispatch({type: accountActions.REGISTER_REQUEST_START, user});
    }

    clearLoginErrorMessages() {
        reduxStore.dispatch({type: accountActions.LOGIN_CLEAR_ERROR_MESSAGES});
    }

    clearRegisterErrorMessages() {
        reduxStore.dispatch({type: accountActions.REGISTER_CLEAR_ERROR_MESSAGES});
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <div> Username: <input [(ngModel)]="this.username" id="username"/></div>
        <div> Password <input [(ngModel)]="this.password" id="password" type="password"/></div>
        <div>
            <button (click)="this.submit()">Login</button>
        </div>

        <error-container-component [errorMessages]="this.state?.errorMessages"></error-container-component>
    `,
})
export class LoginComponent implements OnInit, OnDestroy {

    username: string;
    password: string;

    state?: ILoginState;

    subscription: Subscription;

    constructor(private accountActions: AccountActions) {

    }

    ngOnInit(): void {

        this.subscription = reduxStore.select((store: { login: ILoginState }) => store.login)
            .distinctUntilChanged()
            .subscribe((x: any) => this.state = x);

        this.accountActions.clearLoginErrorMessages();
    }

    ngOnDestroy(): void {

        this.subscription.unsubscribe();
    }

    submit(): void {

        this.accountActions.login({
            username: this.username,
            password: this.password,

        });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <div> Email: <input [(ngModel)]="this.username" id="username"/></div>
        <div> Password <input [(ngModel)]="this.password" id="password" type="password"/></div>
        <div> Confirm password <input [(ngModel)]="this.confirmPassword" type="password"/></div>
        <div>
            <button (click)="this.submit()">Register</button>
        </div>

        <error-container-component [errorMessages]="this.state?.errorMessages"></error-container-component>
    `,
})
export class RegisterComponent implements OnInit, OnDestroy {

    username: string;
    password: string;
    confirmPassword: string;

    state?: IRegisterState;

    subscription: Subscription;

    constructor(private accountActions: AccountActions) {
    }

    ngOnInit(): void {

        this.subscription = reduxStore
            .select((store: { register: IRegisterState }) => store.register)
            .distinctUntilChanged()
            .subscribe((x: any) => this.state = x);

        this.accountActions.clearRegisterErrorMessages();
    }

    ngOnDestroy(): void {

        this.subscription.unsubscribe();
    }

    submit(): void {

        this.accountActions.register({
            username: this.username,
            password: this.password,
        });
    }
}

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

        reduxStore.addReducers({
            login: loginReducer,
            register: registerReducer,
        });

        reduxStore.addSagas(accountSagas(apiClient));
    }
}
