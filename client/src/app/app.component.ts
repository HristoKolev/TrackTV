import { Component } from '@angular/core';
import { smartComponent } from '../infrastructure/component-helpers';

@Component({
    ...smartComponent,
    selector: 'my-app',
    template: `
        <header>

            <h1>Cats App</h1>

            <ul id="my-navlist">
                <li *ngFor="let view of views">
                    <a [routerLink]="view.link" routerLinkActive="active-link">{{view.name}}</a>
                </li>
            </ul>
        </header>

        <router-outlet (activate)="activateEvent($event)" (deactivate)="deactivateEvent($event)"></router-outlet>
    `,
    styles: [`
        * {
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        #my-navlist li {
            display: inline;
            list-style-type: none;
            padding-right: 20px;
        }
    `],
})
export class AppComponent {

    public views: any[] = [
        {
            name: 'Lazy',
            link: ['lazy'],
        },
        {
            name: 'Login',
            link: ['account/login'],
        },
    ];

    constructor() {
    }

    activateEvent(event: any) {
    }

    deactivateEvent(event: any) {
    }
}

@Component({
    ...smartComponent,
    selector: 'my-not-found',
    template: '<h3>Error 404: Not found</h3>',
})
export class NotFound404Component {
}
