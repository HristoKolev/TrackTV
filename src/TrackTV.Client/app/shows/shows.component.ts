import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute} from  '@angular/router';
import {ShowsService, SimpleShows} from  '../services/index';

@Component({
    moduleId: module.id,
    selector: 'shows-component',
    templateUrl: 'shows.component.html',

})
export class ShowsComponent implements OnInit {

    shows : SimpleShows;

    genreName : string;

    constructor(private showsService : ShowsService,
                private activatedRoute : ActivatedRoute) {
    }

    ngOnInit() : void {

        this.activatedRoute.params.subscribe(params => {

            const genreName = params['genre'];

            if (genreName) {

                this.genreName = genreName;

                this.showsService.genre(genreName)
                    .subscribe((shows : SimpleShows) => this.shows = shows);
            }
            else {

                this.showsService.top()
                    .subscribe((shows : SimpleShows) => this.shows = shows);
            }
        });

    }
}