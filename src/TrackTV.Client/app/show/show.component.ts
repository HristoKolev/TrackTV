import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

import {Identity, SubscriptionService} from '../shared/index';

import {ShowDetails} from './show.models';

@Component({
    moduleId: module.id,
    selector: 'show-component',
    templateUrl: 'show.component.html',
    styleUrls: ['show.component.css']
})
export class ShowComponent implements OnInit {

    private show : ShowDetails;

    constructor(private subscriptionService : SubscriptionService,
                private route : ActivatedRoute,
                private identity : Identity) {
    }

    private subscribe(id : number) : void {

        this.subscriptionService.subscribe(id)
            .subscribe(res=> {

                this.show.isUserSubscribed = true;
                this.show.subscriberCount += 1;
            });
    }

    private unsubscribe(id : number) : void {

        this.subscriptionService.unsubscribe(id)
            .subscribe(res => {

                this.show.isUserSubscribed = false;
                this.show.subscriberCount -= 1;
            });
    }

    public ngOnInit() : void {

        this.route.data.forEach((data : { show : ShowDetails }) => {

            this.show = data.show;
        });
    }
}