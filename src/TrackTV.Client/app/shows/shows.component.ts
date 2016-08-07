import {Component, OnInit} from  'angular2/core';
import {RouteParams} from  'angular2/router';
import {ShowsService, SimpleShows} from  '../services/index';
import {GenrePanelComponent} from  '../directives/index';

@Component({
    moduleId: module.id,
    selector: 'shows-component',
    templateUrl: 'shows.component.html',
    directives: [GenrePanelComponent]
})
export class ShowsComponent implements OnInit {

    shows : SimpleShows;

    genreName : string;

    constructor(private showService : ShowsService,
                private routeParams : RouteParams) {
    }

    ngOnInit() : void {

        const genreName = this.routeParams.get('genre');

        if (genreName) {

            this.genreName = genreName;
        }
        else {

            this.showService.top()
                .subscribe((shows : SimpleShows) => this.shows = shows);
        }
    }
}