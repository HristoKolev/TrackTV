import { Component } from '@angular/core';
import { smartComponent } from '../infrastructure/component-helpers';

@Component({
    ...smartComponent,
    selector: 'my-app',
    styleUrls: ['./app.component.scss'],
    templateUrl: './app.component.html',
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
