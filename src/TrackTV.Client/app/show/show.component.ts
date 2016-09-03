import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

import {Subscription} from 'rxjs';

import {Identity, SubscriptionService} from '../shared/index';

import {ShowService} from './show.service';
import {ShowDetails} from './show.models';

@Component({
    moduleId: module.id,
    selector: 'show-component',
    templateUrl: 'show.component.html',
    styleUrls: ['show.component.css']
})
export class ShowComponent implements OnInit, OnDestroy {

    private show : ShowDetails;

    private routeSubscription : Subscription;

    constructor(private showService : ShowService,
                private subscriptionService : SubscriptionService,
                private activatedRoute : ActivatedRoute,
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

        this.routeSubscription = this.activatedRoute.params
            .subscribe(params => {

                this.showService.getShow(params['show'])
                    .subscribe((data : ShowDetails) => this.show = data);
            });
    }

    public ngOnDestroy() : void {

        this.routeSubscription.unsubscribe();
    }
}