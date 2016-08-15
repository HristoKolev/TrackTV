import {Component, Input, Output, EventEmitter} from  '@angular/core';
import {SimpleShow} from  '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'paged-show-list-component',
    templateUrl: 'paged-show-list.component.html',

})
export class PagedShowListComponent {

    @Input() shows : SimpleShow[];

    @Input() totalCount : number;

    @Input() currentPage : number;

    @Input() pageSize : number;

    @Output() pageChange : EventEmitter<number> = new EventEmitter<number>();

    changed(page : number) {

        this.pageChange.emit(page);
    }
}