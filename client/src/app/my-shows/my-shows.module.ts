import { Component, Injectable, Input, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { RouterModule } from '@angular/router';
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
        <div *ngIf="state | async as data" class="list-wrapper">
            <my-show-component *ngFor="let show of data.shows" [show]="show"></my-show-component>
        </div>
    `,
    styles: [`

        @media (min-width: 768px) {

            .list-wrapper {

                margin: 0 auto;
                text-align: center;
                max-width: 1200px;
            }

            .list-wrapper my-show-component {
                display: inline-block;
                width: 370px;
            }

        }
    `],
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
                <ng-container *ngIf="show.lastEpisode">
                    <img src="./left-arrow.png">
                    <div class="episode-summary">
                        <div class="episode-time">November 7</div>
                        <div class="episode-title">{{getEpisodeNumber(show.lastEpisode)}}</div>
                    </div>
                </ng-container>
                <ng-container *ngIf="show.nextEpisode">
                    <div class="episode-summary">
                        <div class="episode-time">November 7</div>
                        <div class="episode-title">{{getEpisodeNumber(show.nextEpisode)}}</div>
                    </div>
                    <img src="./right-arrow.png" class="right-arrow">
                </ng-container>
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
            background-color: #ff6b07;

            width: 100%;
            margin: 0 auto;
            padding: 5px 0;
            font-family: monospace;
        }

        .button-wrapper {
            text-align: center;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 10px;
        }

        .subscription-button {
            width: 100%;
            text-transform: capitalize;
            margin-top: 10px;
        }

        img, .episode-summary {
            float: left;
        }

        img.right-arrow {
            float: right;
        }

        .episode-summary {
            width: 40%;
            text-align: center;
            height: 70px;
            margin-top: 4px;
        }

        .episodes {
            padding: 5px 10px;
        }

        .episode-summary:first-of-type {
            border-right: 1px solid black;
        }

        img {
            opacity: .4;
        }

        .episode-title {
            margin-top: 10px;
        }

        .episode-time {
            margin-top: 10px;
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

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild([
            {path: '', component: MyShowsComponent},
        ]),
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
