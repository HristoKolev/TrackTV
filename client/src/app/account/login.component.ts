import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { AccountActions, IAccountState } from './account-state';
import { NgRedux } from '@angular-redux/store';
import { Subscription } from 'rxjs/Subscription';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <div> Username: <input [(ngModel)]="this.username"/></div>
        <div> Password <input [(ngModel)]="this.password" type="password"/></div>
        <div>
            <button (click)="this.submit()">Login</button>
        </div>

        <div *ngIf="this.accountState?.errorMessages.length">
            <div *ngFor="let message of this.accountState?.errorMessages">
                <div class="errorMessage">{{message}}</div>
            </div>
        </div>
    `,
    styles: [`
        .errorMessage {
            color: red;
        }
    `],
})
export class LoginComponent implements OnInit, OnDestroy {

    username: string;
    password: string;

    accountState: IAccountState;

    subscription: Subscription;

    constructor(private accountActions: AccountActions,
                private ngRedux: NgRedux<any>) {

    }

    ngOnInit(): void {

        this.subscription = this.ngRedux
            .select((store: { account: IAccountState }) => store.account)
            .subscribe((x: any) => this.accountState = x);
    }

    public ngOnDestroy(): void {

        this.subscription.unsubscribe();
    }

    submit(): void {

        this.accountActions.login({
            username: this.username,
            password: this.password,

        });
    }
}
