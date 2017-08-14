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

@Component({
    ...smartComponent,
    selector: 'my-not-found',
    template: '<h3>Error 404: Not found</h3>',
})
export class NotFound404Component {
}
