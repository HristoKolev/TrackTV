import {Component} from  'angular2/core';
import {HTTP_PROVIDERS} from 'angular2/http';
import {ApiPath, Identity, Authentication} from "./services/index";
import {TypeBinder} from './shared/index';
import {applyBindings} from './typeBindings';

const typeBinder = new TypeBinder();
applyBindings(typeBinder);

@Component({
    moduleId: module.id,
    selector: 'app-component',
    templateUrl: 'app.component.html',
    styleUrls: ['app.component.css'],
    providers: [
        HTTP_PROVIDERS,
        ...typeBinder.bindings
    ],

})
export class AppComponent {

    constructor(apiPath : ApiPath, identity : Identity, authentication : Authentication) {

        (<any>window).apiPath = apiPath;
        (<any>window).identity = identity;
        (<any>window).authentication = authentication;
    }

    text : string = ', Cats are great, and so am I'
}