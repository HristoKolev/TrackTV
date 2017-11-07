import { ChangeDetectionStrategy, Component, Injectable, Input, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { reduxStore } from '../../infrastructure/redux-store';
import { myShowsActions, myShowsReducer, myShowsSagas } from './my-shows.state';
import { apiClient } from '../shared/api-client';

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
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <my-show-component *ngFor="let show of this.myShows.data" [show]="show"></my-show-component>
    `,
})
export class MyShowsComponent implements OnInit {

    myShows: any;

    constructor(private myShowsActions: MyShowsActions) {
    }

    ngOnInit(): void {

        reduxStore.select(state => state.myShows)
            .distinctUntilChanged()
            .subscribe(myShows => {
                this.myShows = myShows;
            });

        this.myShowsActions.myShows();
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    selector: 'my-show-component',
    template: `
        <div class="tt-card my-show-card">

            <div class="last-episode">
                <div *ngIf="show.lastEpisode">
                    {{getEpisodeNumber(show.lastEpisode)}} - {{show.lastEpisode.episodeTitle}}
                    {{show.lastEpisode.firstAired}}
                </div>
            </div>

            <div class="show-plane">
                {{show.showName}}
            </div>

            <div class="next-episode">
                <div *ngIf="show.nextEpisode">
                    {{getEpisodeNumber(show.nextEpisode)}} - {{show.nextEpisode.episodeTitle}}
                    {{show.nextEpisode.firstAired}}
                </div>
            </div>

            <button *ngIf="show.isSubscribed" (click)="unsubscribe(show.showId)">Unubscribe</button>
            <button *ngIf="!show.isSubscribed" (click)="subscribe(show.showId)">Subscribe</button>
        </div>
    `,
    styles: [`
        .my-show-card {
            margin: 10px;
            cursor: pointer;
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
