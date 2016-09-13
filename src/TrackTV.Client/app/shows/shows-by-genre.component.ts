import {Component} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SimpleShows} from './shows.models';

@Component({
    moduleId: module.id,
    templateUrl: 'shows-by-genre.component.html',
})
export class ShowsByGenreComponent {

    private shows : SimpleShows;

    private genre : string;

    constructor(private route : ActivatedRoute) {
    }

    public ngOnInit() : void {

        this.route.data.forEach((data : {model : {shows : SimpleShows, genre : string}}) => {

            this.shows = data.model.shows;
            this.genre = data.model.genre
        });
    }
}