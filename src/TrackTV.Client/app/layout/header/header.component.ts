import {Component} from '@angular/core';
import {Identity} from '../../identity/identity.service';


@Component({
    moduleId: module.id,
    selector: 'header-component',
    templateUrl: 'header.component.html'
})
export class HeaderComponent {

    constructor(private identity : Identity) {
    }
}