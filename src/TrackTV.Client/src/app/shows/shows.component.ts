import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SimpleShows} from './shows.models';

@Component({
    moduleId: module.id,
    templateUrl: 'shows.component.html',

})
export class ShowsComponent implements OnInit {

    public shows : SimpleShows;

    constructor(private route : ActivatedRoute) {
    }

    public ngOnInit() : void {

        this.route.data.forEach((data : {model : SimpleShows}) => {

            this.shows = data.model;
        });
    }
}