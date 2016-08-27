import {Component, Input, Output, EventEmitter} from  '@angular/core';
import {SimpleShow} from  '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'paged-show-list-component',
    templateUrl: 'paged-show-list.component.html',

})
export class PagedShowListComponent {

    @Input()
    private shows : SimpleShow[];

    @Input()
    private totalCount : number;

    @Input()
    private currentPage : number;

    @Input()
    private pageSize : number;

    @Output()
    private pageChange : EventEmitter<number> = new EventEmitter<number>();

    private get paginationConfig() {

        return {
            itemsPerPage: this.pageSize,
            currentPage: this.currentPage,
            totalItems: this.totalCount
        };
    }

    private changed(page : number) {

        this.pageChange.emit(page);
    }
}