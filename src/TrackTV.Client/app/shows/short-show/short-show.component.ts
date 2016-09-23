import {Component, Input} from '@angular/core';
import {SimpleShow} from '../shows.models';

@Component({
    moduleId: module.id,
    selector: 'short-show-component',
    templateUrl: 'short-show.component.html',
    styleUrls: ['short-show.component.css']
})
export class ShortShowComponent {

    @Input()
    private show : SimpleShow;
}