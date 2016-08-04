import {Component} from  'angular2/core';
import {ApiPath, Identity, Authentication} from "./services/index";

@Component({
    moduleId: module.id,
    selector: 'app-component',
    templateUrl: 'app.component.html',
    styleUrls: ['app.component.css'],

})
export class AppComponent {

    constructor(apiPath : ApiPath, identity : Identity, authentication : Authentication) {

        (<any>window).apiPath = apiPath;
        (<any>window).identity = identity;
        (<any>window).authentication = authentication;
    }

    text : string = ', Cats are great, and so am I'
}