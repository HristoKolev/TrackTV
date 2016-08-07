import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute} from  '@angular/router';
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
                private activatedRoute : ActivatedRoute) {
    }

    ngOnInit() : void {

        this.activatedRoute.params.subscribe(params => {

            const genreName = params['genre'];

            if (genreName) {

                this.genreName = genreName;
            }
            else {

                this.showService.top()
                    .subscribe((shows : SimpleShows) => this.shows = shows);
            }
        });

    }
}