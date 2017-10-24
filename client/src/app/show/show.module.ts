import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ChangeDetectionStrategy, Component, Injectable, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { reduxState } from '../../infrastructure/redux-store';
import { showActions, showReducer, showSagas } from './show.state';
import { apiClient } from '../shared/api-client';

@Injectable()
export class ShowActions {
    constructor(private ngRedux: NgRedux<any>) {
    }

    show(showId: number) {
        this.ngRedux.dispatch({
            type: showActions.SHOW_REQUEST_START,
            showId,
        });
    }

    subscribe(showId: number) {
        this.ngRedux.dispatch({
            type: showActions.SUBSCRIBE_REQUEST_START,
            showId,
        });
    }

    unsubscribe(showId: number) {
        this.ngRedux.dispatch({
            type: showActions.UNSUBSCRIBE_REQUEST_START,
            showId,
        });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <pre>{{this.show | json}}</pre>

        <div *ngIf="this.session?.isLoggedIn">
            <button *ngIf="!this.show?.isUserSubscribed" (click)="subscribe()">Subscribe</button>
            <button *ngIf="this.show?.isUserSubscribed" (click)="unsubscribe()">Unsubscribe</button>
        </div>
    `,
})
export class ShowComponent implements OnInit {

    show: any;
    session: any;

    constructor(private ngRedux: NgRedux<any>,
                private showActions: ShowActions,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {

        this.route.paramMap
            .map(x => x.get('showId') as string)
            .map(x => Number.parseInt(x, 10))
            .subscribe(showId => {

                this.showActions.show(showId);
            });

        this.ngRedux.select(state => state)
            .distinctUntilChanged()
            .subscribe(state => {

                this.show = state.show;
                this.session = state.session;
            });
    }

    subscribe() {
        this.showActions.subscribe(this.show.showId);
    }

    unsubscribe() {
        this.showActions.unsubscribe(this.show.showId);
    }
}

const routes: Routes = [
    {path: ':showId', component: ShowComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
    ],
    declarations: [ShowComponent],
    providers: [ShowActions],
})
export class ShowModule {
    constructor() {

        reduxState.addReducers({
            show: showReducer,
        });

        reduxState.addSagas(showSagas(apiClient));
    }
}
