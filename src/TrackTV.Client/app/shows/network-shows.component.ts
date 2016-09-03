import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

import {Subscription} from 'rxjs';

import {ShowsService} from './shows.service';
import {NetworkShows, SimpleShow} from './shows.models';

@Component({
    moduleId: module.id,
    selector: 'network-shows-component',
    templateUrl: 'network-shows.component.html',

})
export class NetworkShowsComponent implements OnInit, OnDestroy {

    private shows : SimpleShow[];

    private totalCount : number;

    private networkName : string;

    private currentPage : number;

    private pageSize : number = 24;

    private routeSubscription : Subscription;

    constructor(private showsService : ShowsService,
                private activatedRoute : ActivatedRoute) {
    }

    private populateShows(page : number = 1) : void {

        this.currentPage = page;

        this.routeSubscription = this.activatedRoute.params
            .subscribe(params => {

                this.showsService.network(params['network'], page)
                    .subscribe((data : NetworkShows) => {

                        this.shows = data.shows || [];
                        this.totalCount = data.count;
                        this.networkName = data.networkName;
                    });
            });
    }

    public ngOnInit() : any {

        this.populateShows();
    }

    public ngOnDestroy() : void {

        this.routeSubscription.unsubscribe();
    }
}