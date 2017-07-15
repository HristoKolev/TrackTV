import { Component } from '@angular/core';

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

    submit(): void {
        console.log(this.username, this.password);
    }
}
