import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

import {Identity, SubscriptionService} from '../shared/index';

import {ShowService} from './show.service';
import {ShowDetails} from './show.models';

@Component({
    moduleId: module.id,
    selector: 'show-component',
    templateUrl: 'show.component.html',
    styleUrls: ['show.component.css']
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