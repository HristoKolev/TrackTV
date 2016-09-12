import {Component, Input, Output, EventEmitter} from '@angular/core';
import {MyShow} from '../my-shows.models';

@Component({
    moduleId: module.id,
    selector: 'paged-my-show-list-component',
    templateUrl: 'paged-my-show-list.component.html',
})
export class PagedMyShowListComponent {

    @Input()
    private shows : MyShow[];

    @Input()
    private totalCount : number;

    @Input()
    private currentPage : number;

    @Input()
    private pageSize : number;

    @Input()
    private showNextEpisode : boolean = true;

    @Input()
    private paginationId : string;

    @Output()
    private pageChange : EventEmitter<number> = new EventEmitter<number>();

    @Output()
    private subscribe : EventEmitter<MyShow> = new EventEmitter<MyShow>();

    @Output()
    private unsubscribe : EventEmitter<MyShow> = new EventEmitter<MyShow>();

    private get paginationConfig() : any {

        return {
            id: this.paginationId,
            itemsPerPage: this.pageSize,
            currentPage: this.currentPage,
            totalItems: this.totalCount
        }
    };
}