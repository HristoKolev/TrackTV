import {Component, Input, Output, EventEmitter} from  '@angular/core';
import {SimpleShow} from  '../../services/index';
import {ShortShowComponent} from  '../short-show/short-show.component';
import {PaginationService, PaginatePipe, PaginationControlsCmp} from 'ng2-pagination';

@Component({
    moduleId: module.id,
    selector: 'paged-show-list-component',
    templateUrl: 'paged-show-list.component.html',
    directives: [PaginationControlsCmp, ShortShowComponent],
    pipes: [PaginatePipe],
    providers: [PaginationService]
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