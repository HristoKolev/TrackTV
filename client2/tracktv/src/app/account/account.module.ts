import {Component, NgModule, OnDestroy, OnInit, ViewEncapsulation} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {AccountActions, accountSagas, ILoginState, IRegisterState, loginReducer, registerReducer} from './account.state';
import {SharedModule} from '../shared/shared.module';
import {Subscription} from 'rxjs/Subscription';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {ApiClient} from '../shared/api-client';

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  template: `
    <div class="form-container tt-card">

      <television-component>
        <div>
          Welcome to TrackTv.
          <br/>
          <br/>
          Please, login.
        </div>
      </television-component>

      <input [(ngModel)]="this.username" id="username" placeholder="Username" class="tt-input"/>
      <input [(ngModel)]="this.password" id="password" placeholder="Password" type="password" class="tt-input"/>

      <button (click)="this.submit()" class="tt-button">Login</button>

      <error-container-component [errorMessages]="this.state?.errorMessages"></error-container-component>
    </div>
  `,
  styles: [`
    .form-container {

      text-align: center;

      margin: 0 auto;
      width: 80%;
      max-width: 400px;
    }

    .form-container input, .form-container button {

      display: block;
      width: 100%;
    }

    @media (min-width: 768px) {

      .form-container {
        margin-top: 10%;
      }
    }
  `],
})
export class LoginComponent implements OnInit, OnDestroy {

  username: string;
  password: string;

  state?: ILoginState;

  subscription: Subscription;

  constructor(private actions: AccountActions,
              private store: ReduxStoreService) {
  }

  ngOnInit(): void {

    this.subscription = this.store.select<ILoginState>(store => store.login)
      .subscribe((x: any) => this.state = x);

    this.actions.clearLoginErrorMessages();
  }

  ngOnDestroy(): void {

    this.subscription.unsubscribe();
  }

  submit(): void {

    this.actions.login({
      username: this.username,
      password: this.password,

    });
  }
}

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  template: `
    <div class="form-container tt-card">
      <television-component>
        <div>
          Welcome to TrackTv.
          <br/>
          <br/>
          Please, Register.
        </div>
      </television-component>

      <input [(ngModel)]="this.username" id="username" placeholder="Username" class="tt-input"/>
      <input [(ngModel)]="this.password" id="password" type="password" placeholder="Password" class="tt-input"/>
      <input [(ngModel)]="this.confirmPassword" id="confirm-password" type="password" placeholder="Confirm password"
             class="tt-input"/>

      <button (click)="this.submit()" class="tt-button">Register</button>

      <error-container-component [errorMessages]="this.state?.errorMessages"></error-container-component>
    </div>
  `,
  styles: [`
    .form-container {

      text-align: center;

      margin: 0 auto;
      width: 80%;
      max-width: 400px;
    }

    .form-container input, .form-container button {

      display: block;
      width: 100%;
    }

    @media (min-width: 768px) {

      .form-container {
        margin-top: 10%;
      }
    }

  `],
})
export class RegisterComponent implements OnInit, OnDestroy {

  username: string;
  password: string;
  confirmPassword: string;

  state?: IRegisterState;

  subscription: Subscription;

  constructor(private actions: AccountActions,
              private store: ReduxStoreService) {
  }

  ngOnInit(): void {

    this.subscription = this.store
      .select<IRegisterState>(store => store.register)
      .subscribe((x: any) => this.state = x);

    this.actions.clearRegisterErrorMessages();
  }

  ngOnDestroy(): void {

    this.subscription.unsubscribe();
  }

  submit(): void {

    this.actions.register({
      username: this.username,
      password: this.password,
    });
  }
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: 'login', component: LoginComponent},
      {path: 'register', component: RegisterComponent},
    ]),
    FormsModule,
    SharedModule,
  ],
  providers: [AccountActions]
  ,
  declarations: [
    LoginComponent,
    RegisterComponent,
  ],
})
export class AccountModule {

  constructor(store: ReduxStoreService, apiClient: ApiClient) {

    store.addReducers({
      login: loginReducer,
      register: registerReducer,
    });

    store.addSagas(accountSagas(apiClient));
  }
}
