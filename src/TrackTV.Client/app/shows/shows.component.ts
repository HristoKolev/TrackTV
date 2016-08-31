import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute} from  '@angular/router';
import {ShowsService} from  './shows.service';
import {SimpleShows} from  './shows.models';

@Component({
    moduleId: module.id,
    selector: 'shows-component',
    templateUrl: 'shows.component.html',

})
export class ShowsComponent implements OnInit {

    private shows : SimpleShows;

    private genreName : string;

    constructor(private showsService : ShowsService,
                private activatedRoute : ActivatedRoute) {
    }

    public  ngOnInit() : void {

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