import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute, ROUTER_DIRECTIVES} from  '@angular/router';
import {ShowService, ShowDetails, Identity} from  '../services/index';

import {DoubleDigit} from  './doubleDigit.pipe';
import {PremieredDate} from  './premieredDate.pipe';
import {Querify} from  './querify.pipe';
import {WeekDayName} from  './weekDayName.pipe';

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