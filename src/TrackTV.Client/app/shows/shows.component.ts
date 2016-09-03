import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

import {Subscription} from 'rxjs';

import {ShowsService} from './shows.service';
import {SimpleShows} from './shows.models';

@Component({
    moduleId: module.id,
    selector: 'shows-component',
    templateUrl: 'shows.component.html',

})
export class ShowsComponent implements OnInit, OnDestroy {

    private shows : SimpleShows;

    private genreName : string;

    private routeSubscription : Subscription;

    constructor(private showsService : ShowsService,
                private activatedRoute : ActivatedRoute) {
    }

    public ngOnInit() : void {

        this.routeSubscription = this.activatedRoute.params.subscribe(params => {

            const genreName = params['genre'];

            if (genreName) {

                this.genreName = genreName;

                this.showsService.genre(genreName)
                    .subscribe((shows : SimpleShows) => this.shows = shows);
            }
            else {

                this.showsService.top()
                    .subscribe((shows : SimpleShows) => this.shows = shows);
            }
        });
    }

    public ngOnDestroy() : void {

        this.routeSubscription.unsubscribe();
    }
}