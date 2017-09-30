import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { globalActions } from '../global.state';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    selector: 'header-component',
    template: `
        <header>

            <h1>TrackTV</h1>

            <ul id="my-navlist">

                <li *ngFor="let link of links">
                    <button [routerLink]="link.link" routerLinkActive="active-link">{{link.name}}</button>
                </li>

                <li *ngIf="!this.sessionState.isLoggedIn">
                    <button [routerLink]="['/account/login']">Login</button>
                </li>

                <li *ngIf="!this.sessionState.isLoggedIn">
                    <button [routerLink]="['/account/register']">Register</button>
                </li>

                <li *ngIf="this.sessionState.isLoggedIn">
                    <button (click)="this.logout()">Logout</button>
                </li>
            </ul>
        </header>
    `,
    styles: [`
        #my-navlist li {
            display: inline;
            list-style-type: none;
            padding-right: 20px;
        }
    `],
})
export class HeaderComponent implements OnInit {

    //noinspection JSMismatchedCollectionQueryUpdate
    public links: any[];

    public sessionState: any;

    constructor(private ngRedux: NgRedux<any>) {
    }

    public ngOnInit(): void {

        this.links = [
            {
                name: 'Lazy',
                link: ['lazy'],
            },
            {
                name: 'Shows',
                link: ['shows'],
            },
        ];

        this.ngRedux.select(state => state.session)
            .distinctUntilChanged()
            .subscribe(sessionState => {
                this.sessionState = sessionState;
            });
    }

    public logout() {
        this.ngRedux.dispatch({
            type: globalActions.USER_LOGOUT,
        });
    }
}
