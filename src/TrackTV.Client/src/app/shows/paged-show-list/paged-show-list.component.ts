import {Component, Input, Output, EventEmitter} from '@angular/core';
import {SimpleShow} from '../shows.models';

@Component({
    moduleId: module.id,
    selector: 'paged-show-list-component',
    templateUrl: 'paged-show-list.component.html',

})
export class PagedShowListComponent {

    @Input()
    public shows : SimpleShow[];

    @Input()
    public totalCount : number;

    @Input()
    public currentPage : number;

    @Input()
    public pageSize : number;

    @Output()
    public pageChange : EventEmitter<number> = new EventEmitter<number>();

    private get paginationConfig() : any {

        return {
            itemsPerPage: this.pageSize,
            currentPage: this.currentPage,
            totalItems: this.totalCount
        };
    }
}