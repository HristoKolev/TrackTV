import {Component, Input} from  '@angular/core';
import {ROUTER_DIRECTIVES} from  '@angular/router';
import {SimpleShow} from '../../services/index';
import {ShortShowComponent} from  '../short-show/short-show.component';

@Component({
    moduleId: module.id,
    selector: 'show-list-component',
    templateUrl: 'show-list.component.html',
    directives: [ROUTER_DIRECTIVES, ShortShowComponent]
})
export class ShowListComponent {

    @Input() shows : SimpleShow[];
}