import {Component, OnInit} from '@angular/core';

import * as toastr from "toastr";

import {SubscriptionService} from '../shared/index';
import {MyShow, MyShows} from './my-shows.models';
import {MyShowsService} from "./my-shows.service";

@Component({
    moduleId: module.id,
    selector: 'my-shows-component',
    templateUrl: 'my-shows.component.html',
})
export class MyShowsComponent implements OnInit {

    private continuing : MyShows;

    private ended : MyShows;

    private pageSize : number = 10;

    private currentContinuingPage : number = 1;

    private currentEndedPage : number = 1;

    constructor(private subscriptionService : SubscriptionService,
                private myShowsService : MyShowsService) {
    }

    subscribe(show : MyShow) : void {

        this.subscriptionService.subscribe(show.id)
            .subscribe(res => {

                show.unsubscribed = false;
            });
    }

    unsubscribe(show : MyShow) : void {

        this.subscriptionService.unsubscribe(show.id)
            .subscribe(res => {

                show.unsubscribed = true;

                toastr.success('You have successfully unsubscribed from ' + show.name);
            });
    }

    getContinuing(page : number) : void {

        this.myShowsService.continuing(page)
            .subscribe((myShows : MyShows) => {

                this.continuing = myShows;
            });
    }

    getEnded(page : number) : void {

        this.myShowsService.ended(page)
            .subscribe((myShows : MyShows) => {

                this.ended = myShows;
            });
    }

    ngOnInit() : void {

        this.getContinuing(this.currentContinuingPage);

        this.getEnded(this.currentEndedPage);
    }
}