import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { globalActions, go } from '../global.state';
import { reduxStore } from '../../infrastructure/redux-store';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    selector: 'header-component',
    template: `
        <header>
            <nav>
                <div class="inner-nav">
                    <i id="bars" class="fa fa-bars" aria-hidden="true" (click)="this.toggleNavigationBars()"></i>


                    <ul [ngClass]="{'closed': this.navigationClosed}">

                        <li class="brand">
                            <i class="fa fa-television" aria-hidden="true"></i>
                            TrackTv
                        </li>

                        <li *ngFor="let link of links">
                            <a [routerLink]="link.link" *ngIf="link.link" routerLinkActive="active-link"
                               (click)="this.closeNavigation()">{{link.name}}</a>

                            <a (click)="link.func(); this.closeNavigation()" *ngIf="link.func" class="func-link">{{link.name}}</a>
                        </li>
                    </ul>
                </div>
            </nav>
        </header>
    `,
    styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {

    //noinspection JSMismatchedCollectionQueryUpdate
    links: any[];

    navigationClosed = true;

    ngOnInit(): void {

        const allLinks = [
            {name: 'Calendar', link: ['/calendar'], role: 'user'},
            {name: 'My Shows', link: ['/my-shows'], role: 'user'},
            {name: 'Shows', link: ['/shows'], role: 'public'},
            {name: 'Login', link: ['/account/login'], role: 'unregistered'},
            {name: 'Logout', func: () => this.logout(), role: 'user'},
            {name: 'Register', link: ['/account/register'], role: 'unregistered'},
        ];

        reduxStore.select(state => state.session)
            .distinctUntilChanged()
            .subscribe(sessionState => {

                this.links = allLinks.filter(link => {
                    if (link.role === 'public') {
                        return true;
                    }

                    if (link.role === 'user') {
                        return sessionState.isLoggedIn;
                    }

                    if (link.role === 'unregistered') {
                        return !sessionState.isLoggedIn;
                    }

                    throw new Error(`Invalid role: ${link.role}`);
                });
            });
    }

    logout() {
        reduxStore.dispatch({
            type: globalActions.LOGOUT_USER,
        });

        go(['/shows']);
    }

    toggleNavigationBars() {
        this.navigationClosed = !this.navigationClosed;
    }

    closeNavigation() {
        this.navigationClosed = true;
    }
}
