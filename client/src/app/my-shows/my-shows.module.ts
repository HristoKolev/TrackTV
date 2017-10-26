import { ChangeDetectionStrategy, Component, Injectable, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
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
        <div *ngFor="let show of this.myShows.shows">
            {{show.showName}}

            <button *ngIf="show.isSubscribed" (click)="unsubscribe(show.showId)">Unubscribe</button>
            <button *ngIf="!show.isSubscribed" (click)="subscribe(show.showId)">Subscribe</button>
        </div>
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

    subscribe(showId: number) {
        this.myShowsActions.subscribe(showId);
    }

    unsubscribe(showId: number) {
        this.myShowsActions.unsubscribe(showId);
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
    declarations: [MyShowsComponent],
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
