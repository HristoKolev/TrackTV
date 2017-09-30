import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { AccountActions, IRegisterState } from './account-state';
import { NgRedux } from '@angular-redux/store';
import { Subscription } from 'rxjs/Subscription';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <div> Email: <input [(ngModel)]="this.email"/></div>
        <div> Password <input [(ngModel)]="this.password" type="password"/></div>
        <div> Confirm password <input [(ngModel)]="this.confirmPassword" type="password"/></div>
        <div>
            <button (click)="this.submit()">Register</button>
        </div>

        <error-container-component [errorMessages]="this.state?.errorMessages"></error-container-component>
    `,
})
export class RegisterComponent implements OnInit, OnDestroy {

    email: string;
    password: string;
    confirmPassword: string;

    state?: IRegisterState;

    subscription: Subscription;

    constructor(private accountActions: AccountActions,
                private ngRedux: NgRedux<any>) {

    }

    ngOnInit(): void {

        this.subscription = this.ngRedux
            .select((store: { register: IRegisterState }) => store.register)
            .subscribe((x: any) => this.state = x);
    }

    ngOnDestroy(): void {

        this.subscription.unsubscribe();
    }

    submit(): void {

        this.accountActions.register({
            Email: this.email,
            Password: this.password,
           // ConfirmPassword: this.confirmPassword,
        });
    }
}
