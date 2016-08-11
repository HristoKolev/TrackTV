import {Component} from  '@angular/core';
import {ROUTER_DIRECTIVES} from '@angular/router';

import {FooterComponent, HeaderComponent} from './directives/index';

import {ShowService} from  './services/show.service';

@Component({
    moduleId: module.id,
    selector: 'app-component',
    templateUrl: 'app.component.html',
    directives: [ROUTER_DIRECTIVES, HeaderComponent, FooterComponent]
})

export class AppComponent {

    constructor(showService : ShowService) {

        (<any>window).show = showService;
    }
}

