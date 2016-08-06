import {Component} from  'angular2/core';
import {RouteConfig, ROUTER_DIRECTIVES} from 'angular2/router';

import {FooterComponent, HeaderComponent} from './directives/index';
import {routeDefinitions} from './app.routes';

@Component({
    moduleId: module.id,
    selector: 'app-component',
    templateUrl: 'app.component.html',
    styleUrls: ['app.component.css'],
    directives: [ROUTER_DIRECTIVES, HeaderComponent, FooterComponent]
})
@RouteConfig(routeDefinitions)
export class AppComponent {
}

