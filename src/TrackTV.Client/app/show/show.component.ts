import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute, ROUTER_DIRECTIVES} from  '@angular/router';
import {ShowService, ShowDetails, Identity} from  '../services/index';
import {DoubleDigit, PremieredDate, Querify, WeekDayName} from  './pipes/index';

@Component({
    moduleId: module.id,
    selector: 'show-component',
    templateUrl: 'show.component.html',
    pipes: [DoubleDigit, PremieredDate, Querify, WeekDayName],
    directives: [ROUTER_DIRECTIVES]
})
export class ShowComponent implements OnInit {

    constructor(private showService : ShowService,
                private activatedRoute : ActivatedRoute,
                public identity : Identity) {
    }

    show : ShowDetails;

    ngOnInit() : void {

        this.activatedRoute.params
            .subscribe(params => {

                this.showService.getShow(params['show'])
                    .subscribe((data : ShowDetails) => {

                        this.show = data;
                    });
            });
    }
}