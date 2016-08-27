import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute} from  '@angular/router';

import {ShowsService, NetworkShows, SimpleShow} from  '../services/index';

@Component({
    moduleId: module.id,
    selector: 'network-shows-component',
    templateUrl: 'network-shows.component.html',

})
export class NetworkShowsComponent implements OnInit {

    constructor(private showsService : ShowsService,
                private activatedRoute : ActivatedRoute) {
    }

    private shows : SimpleShow[];

    private totalCount : number;

    private networkName : string;

    private currentPage : number;

    private pageSize : number = 24;

    private populateShows(page : number = 1) {

        this.currentPage = page;

        this.activatedRoute.params
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
}