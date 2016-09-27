import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ShowsService} from './shows.service';
import {SearchShows, SimpleShow} from './shows.models';

@Component({
    moduleId: module.id,
    templateUrl: 'shows-by-name.component.html',
    styleUrls: ['shows-by-name.component.css']
})
export class ShowsByNameComponent implements OnInit {

    public shows : SimpleShow[];

    private totalCount : number;

    private query : string;

    private currentPage : number;

    private readonly pageSize : number = 24;

    constructor(private showsService : ShowsService,
                private route : ActivatedRoute) {
    }

    private getPage(page : number = 1) {

        this.showsService.search(this.query, page)
            .subscribe((data : SearchShows) => {

                this.populateShows(data);

                this.currentPage = page;
            });
    }

    private populateShows(data : SearchShows) {

        this.shows = data.shows || [];
        this.totalCount = data.count;
        this.query = data.query;
    }

    public ngOnInit() : void {

        this.route.data.forEach((data : {model : SearchShows}) => {

            this.populateShows(data.model);
        });
    }
}