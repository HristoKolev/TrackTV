import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SimpleShows} from './shows.models';

@Component({
    moduleId: module.id,
    selector: 'shows-component',
    templateUrl: 'shows.component.html',

})
export class ShowsComponent implements OnInit {

    private shows : SimpleShows;

    private genreName : string;

    constructor(private route : ActivatedRoute) {
    }

    public ngOnInit() : void {

        this.route.data.forEach((data : {showsModel : {shows : SimpleShows, genreName : string}}) => {

            this.shows = data.showsModel.shows;
            this.genreName = data.showsModel.genreName;
        });
    }
}