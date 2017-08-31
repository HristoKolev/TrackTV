import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { globalActions } from '../global.state';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    selector: 'header-component',
    template: `
        <header>

            <h1>Cats App</h1>

            <ul id="my-navlist">
                <li *ngFor="let link of links">
                    <button [routerLink]="link.link" routerLinkActive="active-link">{{link.name}}</button>
                </li>

                <li *ngIf="this.accountState?.session.isLoggedIn">
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

    private links: any[];

    private accountState: any;

    constructor(private ngRedux: NgRedux<any>) {
    }

    public ngOnInit(): void {

        const links = [
            {
                name: 'Lazy',
                link: ['lazy'],
            },
            {
                name: 'Shows',
                link: ['shows'],
            },
        ];

        this.ngRedux.select(state => state.account)
            .distinctUntilChanged()
            .subscribe(accountState => {

                if (accountState && accountState.session.isLoggedIn) {

                    this.links = links;
                } else {
                    this.links = [...links, {name: 'Login', link: ['/account/login']}];
                }

                this.accountState = accountState;
            });
    }

    public logout() {
        this.ngRedux.dispatch({
            type: globalActions.LOGOUT,
        });
    }
}
