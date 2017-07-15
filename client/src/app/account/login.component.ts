import { Component } from '@angular/core';
import { AccountActions } from './state';

@Component({
    template: `
        <div> Username: <input type="text" [(ngModel)]="this.username"/></div>
        <div> Password <input type="password" [(ngModel)]="this.password"/></div>
        <div>
            <button (click)="this.submit()">Login</button>
        </div>
    `,
    styles: [],
})
export class LoginComponent {

    username: string;
    password: string;

    constructor(public accountActions: AccountActions) {

    }

    submit(): void {

        console.log(this.username, this.password);

        this.accountActions.login({
            username: this.username,
            password: this.password,

        });
    }
}
