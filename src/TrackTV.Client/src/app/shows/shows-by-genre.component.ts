import {Component} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SimpleShows, ShowsByGenreModel} from './shows.models';
import {ResolveData} from '../shared/router.models';

@Component({
    moduleId: module.id,
    templateUrl: 'shows-by-genre.component.html',
})
export class ShowsByGenreComponent {

    public shows : SimpleShows;

    public genre : string;

    constructor(private route : ActivatedRoute) {
    }

    public ngOnInit() : void {

        this.route.data.forEach((data : ResolveData<ShowsByGenreModel>) => {

            this.shows = data.model.shows;
            this.genre = data.model.genre
        });
    }
}