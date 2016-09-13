import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ShowsService} from './shows.service';
import {NetworkShows, SimpleShow} from './shows.models';

@Component({
    moduleId: module.id,
    templateUrl: 'shows-by-network.component.html',
})
export class ShowsByNetworkComponent implements OnInit {

    private shows : SimpleShow[];

    private totalCount : number;

    private network : string;

    private currentPage : number = 1;

    private pageSize : number = 24;

    constructor(private showsService : ShowsService,
                private route : ActivatedRoute) {
    }

    private populateShows(data : NetworkShows) {

        this.shows = data.shows || [];
        this.totalCount = data.count;
        this.network = data.networkName;
    }

    private getPage(page : number = 1) : void {

        this.showsService.network(this.network, page)
            .subscribe((data : NetworkShows) => {

                this.populateShows(data);

                this.currentPage = page;
            });
    }

    public ngOnInit() : any {

        this.route.data.forEach((data : {model : NetworkShows}) => {

            this.populateShows(data.model)
        });
    }
}