import {Component, OnInit} from  '@angular/core';
import {ActivatedRoute, ROUTER_DIRECTIVES} from  '@angular/router';
import {ShowService, ShowDetails, Identity, SubscriptionService} from  '../services/index';
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
                private subscriptionService : SubscriptionService,
                private activatedRoute : ActivatedRoute,
                private identity : Identity) {
    }

    show : ShowDetails;

    ngOnInit() : void {

        this.activatedRoute.params
            .subscribe(params => {

                this.showService.getShow(params['show'])
                    .subscribe((data : ShowDetails) => {

                        this.show = data;
                        console.log(this.show);
                    });
            });
    }

    private subscribe(id : number) {

        this.subscriptionService.subscribe(id)
            .subscribe(res=> {

                this.show.isUserSubscribed = true;
                this.show.subscriberCount += 1;
            });

    }

    private unsubscribe(id : number) {

        this.subscriptionService.unsubscribe(id)
            .subscribe(res => {

                this.show.isUserSubscribed = false;
                this.show.subscriberCount -= 1;
            });
    }
}