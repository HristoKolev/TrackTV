import {Component, Input} from  '@angular/core';
import {ROUTER_DIRECTIVES} from  '@angular/router';
import {Genre} from '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'genre-panel-component',
    templateUrl: 'genre-panel.component.html',
    directives: [ROUTER_DIRECTIVES]
})
export class GenrePanelComponent {

    @Input() genres : Genre[];

    list : number[] = [1, 2, 3];
}