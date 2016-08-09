import {Component, Input} from  '@angular/core';
import {ROUTER_DIRECTIVES} from  '@angular/router';
import {SimpleShow} from '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'short-show-component',
    templateUrl: 'short-show.component.html',
    directives: [ROUTER_DIRECTIVES]
})
export class ShortShowComponent {

    @Input() show : SimpleShow;
}