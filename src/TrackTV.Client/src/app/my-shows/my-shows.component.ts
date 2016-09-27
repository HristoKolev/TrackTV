import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import * as toastr from 'toastr';
import {MyShow, MyShows} from './my-shows.models';
import {MyShowsService} from './my-shows.service';
import {SubscriptionService} from '../shared/subscription.service';


@Component({
    moduleId: module.id,
    templateUrl: 'my-shows.component.html',
})
export class MyShowsComponent implements OnInit {

    private continuing : MyShows;

    private ended : MyShows;

    private readonly pageSize : number = 10;

    private currentContinuingPage : number = 1;

    private currentEndedPage : number = 1;

    constructor(private subscriptionService : SubscriptionService,
                private myShowsService : MyShowsService,
                private route : ActivatedRoute) {
    }

    public subscribe(show : MyShow) : void {

        this.subscriptionService.subscribe(show.id)
            .subscribe(res => {

                show.subscribed = true;
            });
    }

    public unsubscribe(show : MyShow) : void {

        this.subscriptionService.unsubscribe(show.id)
            .subscribe(res => {

                show.subscribed = false;

                toastr.success('You have successfully unsubscribed from ' + show.name);
            });
    }

    public getContinuing(page : number) : void {

        this.myShowsService.continuing(page)
            .subscribe((myShows : MyShows) => {

                this.continuing = myShows;
                this.currentContinuingPage = page;
            });
    }

    public getEnded(page : number) : void {

        this.myShowsService.ended(page)
            .subscribe((myShows : MyShows) => {

                this.ended = myShows;
                this.currentEndedPage = page;
            });
    }

    public ngOnInit() : void {

        this.route.data.forEach((data : {model : {continuing : MyShows, ended : MyShows}}) => {

            this.continuing = data.model.continuing;
            this.ended = data.model.ended;
        });
    }
}