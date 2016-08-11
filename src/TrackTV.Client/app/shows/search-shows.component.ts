import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute} from  '@angular/router';

import {ShowsService, SearchShows, SimpleShow} from  '../services/index';
import {PagedShowListComponent} from './paged-show-list/paged-show-list.component';

@Component({
    moduleId: module.id,
    selector: 'search-shows-component',
    templateUrl: 'search-shows.component.html',
    directives: [PagedShowListComponent]
})
export class SearchShowsComponent implements OnInit {

    constructor(private showsService : ShowsService,
                private activatedRoute : ActivatedRoute) {
    }

    shows : SimpleShow[];

    totalCount : number;

    query : string;

    currentPage : number;

    pageSize : number = 24;

    populateShows(page : number = 1) {

        this.currentPage = page;

        this.activatedRoute.params
            .subscribe(params => {

                this.showsService.search(params['query'], page)
                    .subscribe((data : SearchShows) => {

                        this.shows = data.shows || [];
                        this.totalCount = data.count;
                        this.query = data.query;
                    });
            });
    }

    ngOnInit() : void {

        this.populateShows();
    }
}