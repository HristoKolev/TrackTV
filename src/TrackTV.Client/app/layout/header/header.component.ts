import {Component} from  '@angular/core';

import {Identity} from  '../../shared/index';

@Component({
    moduleId: module.id,
    selector: 'header-component',
    templateUrl: 'header.component.html'
})
export class HeaderComponent {

    constructor(private identity : Identity) {
    }
}