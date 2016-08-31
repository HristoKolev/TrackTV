import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute} from  '@angular/router';

import {ShowsService} from  './shows.service';
import {SearchShows, SimpleShow} from  './shows.models';

@Component({
    moduleId: module.id,
    selector: 'search-shows-component',
    templateUrl: 'search-shows.component.html',
})
export class SearchShowsComponent implements OnInit {

    constructor(private showsService : ShowsService,
                private activatedRoute : ActivatedRoute) {
    }

    private shows : SimpleShow[];

    private totalCount : number;

    private query : string;

    private currentPage : number;

    private pageSize : number = 24;

    private populateShows(page : number = 1) {

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

    public ngOnInit() : void {

        this.populateShows();
    }
}