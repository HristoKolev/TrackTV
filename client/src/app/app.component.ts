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
            name: 'Dashboard',
            link: [''],
        },
        {
            name: 'Lazy',
            link: ['lazy'],
        },
        {
            name: 'Bad Link',
            link: ['wronglink'],
        },
    ];

    constructor() {
    }

    activateEvent(event) {

        if (ENV === 'development') {
            console.log('Activate Event:', event);
        }
    }

    deactivateEvent(event) {
        if (ENV === 'development') {
            console.log('Deactivate Event', event);
        }
    }
}
