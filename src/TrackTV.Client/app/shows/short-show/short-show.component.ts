import {Component, Input} from  '@angular/core';
import {SimpleShow} from '../shows.models';

@Component({
    moduleId: module.id,
    selector: 'short-show-component',
    templateUrl: 'short-show.component.html',
})
export class ShortShowComponent {

    @Input()
    private show : SimpleShow;
}