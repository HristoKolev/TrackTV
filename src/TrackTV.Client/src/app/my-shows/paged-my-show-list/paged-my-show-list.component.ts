import {Component, Input, Output, EventEmitter} from '@angular/core';
import {MyShow} from '../my-shows.models';

@Component({
    moduleId: module.id,
    selector: 'paged-my-show-list-component',
    templateUrl: 'paged-my-show-list.component.html',
    styleUrls: ['paged-my-show-list.component.css']
})
export class PagedMyShowListComponent {

    @Input()
    public shows : MyShow[];

    @Input()
    public totalCount : number;

    @Input()
    public currentPage : number;

    @Input()
    public pageSize : number;

    @Input()
    public showNextEpisode : boolean = true;

    @Input()
    public paginationId : string;

    @Output()
    public pageChange : EventEmitter<number> = new EventEmitter<number>();

    @Output()
    public subscribe : EventEmitter<MyShow> = new EventEmitter<MyShow>();

    @Output()
    public unsubscribe : EventEmitter<MyShow> = new EventEmitter<MyShow>();

    private get paginationConfig() : any {

        return {
            id: this.paginationId,
            itemsPerPage: this.pageSize,
            currentPage: this.currentPage,
            totalItems: this.totalCount
        }
    };
}