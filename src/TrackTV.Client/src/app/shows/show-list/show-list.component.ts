import {Component, Input} from '@angular/core';
import {SimpleShow} from '../shows.models';

@Component({
    moduleId: module.id,
    selector: 'show-list-component',
    templateUrl: 'show-list.component.html',
})
export class ShowListComponent {

    @Input()
    public shows : SimpleShow[];
}