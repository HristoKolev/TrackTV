import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ShowsService} from './shows.service';
import {NetworkShowsModel, SimpleShow} from './shows.models';
import {ResolveData} from '../shared/router.models';

@Component({
    moduleId: module.id,
    templateUrl: 'shows-by-network.component.html',
})
export class ShowsByNetworkComponent implements OnInit {

    public shows : SimpleShow[];

    private totalCount : number;

    private network : string;

    private currentPage : number = 1;

    private pageSize : number = 24;

    constructor(private showsService : ShowsService,
                private route : ActivatedRoute) {
    }

    private populateShows(data : NetworkShowsModel) {

        this.shows = data.shows || [];
        this.totalCount = data.count;
        this.network = data.networkName;
    }

    private getPage(page : number = 1) : void {

        this.showsService.network(this.network, page)
            .subscribe((data : NetworkShowsModel) => {

                this.populateShows(data);

                this.currentPage = page;
            });
    }

    public ngOnInit() : any {

        this.route.data.forEach((data : ResolveData<NetworkShowsModel>) => {

            this.populateShows(data.model)
        });
    }
}