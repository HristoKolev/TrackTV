import {Component, Input} from  '@angular/core';
import {SimpleShow} from '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'short-show-component',
    templateUrl: 'short-show.component.html',
})
export class ShortShowComponent {

    @Input() show : SimpleShow;
}