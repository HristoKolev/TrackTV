import {Component, Input} from '@angular/core';
import {Genre} from '../shows.models';

@Component({
    moduleId: module.id,
    selector: 'genre-panel-component',
    templateUrl: 'genre-panel.component.html',
    styleUrls: ['genre-panel.component.css']
})
export class GenrePanelComponent {

    @Input()
    private genres : Genre[];
}