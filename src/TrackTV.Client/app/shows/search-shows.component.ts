import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

import {Subscription} from 'rxjs';

import {ShowsService} from './shows.service';
import {SearchShows, SimpleShow} from './shows.models';

@Component({
    moduleId: module.id,
    selector: 'search-shows-component',
    templateUrl: 'search-shows.component.html',
})
export class SearchShowsComponent implements OnInit, OnDestroy {

    private shows : SimpleShow[];

    private totalCount : number;

    private query : string;

    private currentPage : number;

    private pageSize : number = 24;

    private routeSubscription : Subscription;

    constructor(private showsService : ShowsService,
                private activatedRoute : ActivatedRoute) {
    }

    private populateShows(page : number = 1) {

        this.currentPage = page;

        this.routeSubscription = this.activatedRoute.params
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

    public ngOnDestroy() : void {

        this.routeSubscription.unsubscribe();
    }
}