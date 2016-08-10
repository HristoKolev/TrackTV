import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute} from  '@angular/router';
import {ShowsService, SimpleShows} from  '../services/index';
import {CapitaliseWords} from  './capitaliseWords.pipe';
import {GenrePanelComponent} from  './genre-panel/genre-panel.component';
import {ShowListComponent} from  './show-list/show-list.component';

@Component({
    moduleId: module.id,
    selector: 'shows-component',
    templateUrl: 'shows.component.html',
    directives: [GenrePanelComponent, ShowListComponent],
    pipes: [CapitaliseWords]
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

                this.showService.genre(genreName)
                    .subscribe((shows : SimpleShows) => this.shows = shows);
            }
            else {

                this.showService.top()
                    .subscribe((shows : SimpleShows) => this.shows = shows);
            }
        });

    }
}