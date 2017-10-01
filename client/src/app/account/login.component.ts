import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { AccountActions, ILoginState } from './account-state';
import { NgRedux } from '@angular-redux/store';
import { Subscription } from 'rxjs/Subscription';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <div> Username: <input [(ngModel)]="this.email"/></div>
        <div> Password <input [(ngModel)]="this.password" type="password"/></div>
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

    constructor(private accountActions: AccountActions,
                private ngRedux: NgRedux<any>) {

    }

    ngOnInit(): void {

        this.subscription = this.ngRedux
            .select((store: { login: ILoginState }) => store.login)
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
