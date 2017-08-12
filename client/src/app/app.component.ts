import { Component, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'my-app',
    styleUrls: ['./app.component.scss'],
    templateUrl: './app.component.html',
    encapsulation: ViewEncapsulation.None,
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
