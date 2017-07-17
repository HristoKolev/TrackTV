import { Component, OnInit } from '@angular/core';
import { AccountActions, IAccountState } from './state';
import { NgRedux } from '@angular-redux/store';

@Component({
    template: `
        <div> Username: <input [(ngModel)]="this.username"/></div>
        <div> Password <input [(ngModel)]="this.password" type="password"/></div>
        <div>
            <button (click)="this.submit()">Login</button>
        </div>

        <div *ngIf="!this.accountState?.response?.success">
            <div *ngFor="let message of this.accountState?.response?.errorMessages">
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
export class LoginComponent implements OnInit {

    username: string;
    password: string;

    accountState: IAccountState;

    constructor(private accountActions: AccountActions,
                private ngRedux: NgRedux<any>) {

    }

    ngOnInit(): void {

        this.ngRedux
            .select((store: { account: IAccountState }) => store.account)
            .subscribe((x: any) => this.accountState = x);
    }

    submit(): void {

        this.accountActions.login({
            username: this.username,
            password: this.password,

        });
    }
}
