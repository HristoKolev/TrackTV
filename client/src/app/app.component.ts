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
    ];

    constructor() {
    }

    activateEvent(event: any) {

        if (ENV === 'development') {
            console.log('Activate Event:', event);
        }
    }

    deactivateEvent(event: any) {
        if (ENV === 'development') {
            console.log('Deactivate Event', event);
        }
    }
}
