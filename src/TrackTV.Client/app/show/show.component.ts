import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ShowDetails} from './show.models';
import {SubscriptionService} from '../shared/subscription.service';
import {Identity} from '../identity/identity.service';

@Component({
    moduleId: module.id,
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
            .subscribe(res => {

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

        this.route.data.forEach((data : { model : ShowDetails }) => {

            this.show = data.model;
        });
    }
}