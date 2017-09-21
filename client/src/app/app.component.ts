import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    selector: 'my-app',
    template: `
        <header-component></header-component>

        <router-outlet (activate)="activateEvent($event)" (deactivate)="deactivateEvent($event)"></router-outlet>

        <loading-component></loading-component>

    `,
    styles: [`
        * {
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
    `],
})
export class AppComponent {

    activateEvent(event: any) {
    }

    deactivateEvent(event: any) {
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    selector: 'my-not-found',
    template: '<h3>Error 404: Not found</h3>',
})
export class NotFound404Component {
}
