import { Component, Injectable, Input, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { reduxStore } from '../../infrastructure/redux-store';
import { myShowsActions, myShowsReducer, myShowsSagas } from './my-shows.state';
import { apiClient } from '../shared/api-client';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class MyShowsActions {

    myShows() {
        reduxStore.dispatch({
            type: myShowsActions.FETCH_REQUEST_START,
        });
    }

    subscribe(showId: number) {
        reduxStore.dispatch({
            type: myShowsActions.SUBSCRIBE_REQUEST_START,
            showId,
        });
    }

    unsubscribe(showId: number) {
        reduxStore.dispatch({
            type: myShowsActions.UNSUBSCRIBE_REQUEST_START,
            showId,
        });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    template: `
        <div *ngIf="state | async as data">
            <my-show-component *ngFor="let show of data.shows" [show]="show"></my-show-component>
        </div>
    `,
})
export class MyShowsComponent implements OnInit {

    state: Observable<any> = reduxStore.select(state => state.myShows);

    constructor(private myShowsActions: MyShowsActions) {
    }

    ngOnInit(): void {

        this.myShowsActions.myShows();
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    selector: 'my-show-component',
    template: `
        <div class="tt-card my-show-card">

            <div class="show-title">
                {{show.showName}}
            </div>

            <div class="episodes">
                <div class="last-episode-plane">
                    <ng-container *ngIf="show.lastEpisode">
                        <img src="./left-arrow.png">
                        <span class="episode-summary">
                            <span class="episode-time">November 7</span>
                            <span class="episode-title">{{show.lastEpisode.episodeTitle}}</span>
                        </span>

                    </ng-container>
                </div>
                <div class="next-episode-plane">
                    <ng-container *ngIf="show.nextEpisode">
                        <span class="episode-summary">
                            <span class="episode-time">November 7</span>
                            <span class="episode-title">{{show.nextEpisode.episodeTitle}}</span>
                        </span>
                        <img src="./right-arrow.png">
                    </ng-container>

                </div>
            </div>

            <div class="button-wrapper">
                <button *ngIf="show.isSubscribed" (click)="unsubscribe(show.showId)" class="tt-button subscription-button">Unubscribe
                </button>
                <button *ngIf="!show.isSubscribed" (click)="subscribe(show.showId)" class="tt-button subscription-button">Subscribe</button>
            </div>
        </div>
    `,
    styles: [`
        .my-show-card {
            margin: 10px;
            cursor: pointer;
            padding: 0;
        }

        .show-title {
            text-align: center;
            font-size: 20px;
            font-weight: bold;

            color: white;
            background-color: #f44336;

            width: 100%;
            margin: 0 auto;
            padding: 5px 0;
            font-family: monospace;
        }

        .last-episode-plane {
            display: inline-block;
            width: 49%;
            border-right: 1px solid black;
            margin: 10px 0;
            position: relative;
        }

        .next-episode-plane {
            display: inline-block;
            width: 49%;
            margin: 10px 0;
        }

        .episode-title {
            margin-top: 10px;
        }

        .last-episode-plane img {
            float: left;
        }

        .next-episode-plane img {
            float: right;
        }

        .button-wrapper {
            text-align: center;
        }

        .subscription-button {
            width: 93%;
            text-transform: capitalize;
            height: 35px;
            margin: 0 10px 10px;
        }

    `],
})
export class MyShowComponent {

    @Input()
    show: any;

    constructor(private myShowsActions: MyShowsActions) {
    }

    subscribe(showId: number) {
        this.myShowsActions.subscribe(showId);
    }

    unsubscribe(showId: number) {
        this.myShowsActions.unsubscribe(showId);
    }

    getEpisodeNumber(episode: any) {
        return `S${episode.seasonNumber.toString().padStart(2, '0')}E${episode.episodeNumber.toString().padStart(2, '0')}`;
    }
}

const routes: Routes = [
    {path: '', component: MyShowsComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
    ],
    declarations: [MyShowsComponent, MyShowComponent],
    providers: [MyShowsActions],
})
export class MyShowsModule {
    constructor() {

        reduxStore.addReducers({
            myShows: myShowsReducer,
        });

        reduxStore.addSagas(myShowsSagas(apiClient));
    }
}
