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

    shows : SimpleShow[];

    totalCount : number;

    networkName : string;

    currentPage : number;

    pageSize : number = 24;

    populateShows(page : number = 1) {

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

    ngOnInit() : any {

        this.populateShows();
    }
}