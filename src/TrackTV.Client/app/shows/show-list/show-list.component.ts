import {Component, Input} from  '@angular/core';
import {SimpleShow} from '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'show-list-component',
    templateUrl: 'show-list.component.html',
})
export class ShowListComponent {

    @Input()
    private shows : SimpleShow[];
}