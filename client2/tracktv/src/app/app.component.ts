import { Component, ViewEncapsulation } from '@angular/core';

@Component({
    encapsulation: ViewEncapsulation.None,
    selector: 'app-component',
    template: `
        <header-component></header-component>

        <router-outlet></router-outlet>

        <loading-component></loading-component>
    `,
    styleUrls: ['./app.component.css'],
})
export class AppComponent {
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    template: '<h3>Error 404: Not found</h3>',
})
export class NotFound404Component {
}
