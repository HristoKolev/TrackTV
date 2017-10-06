import { ChangeDetectionStrategy, Component, Injectable, Input, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { reduxState } from '../../infrastructure/redux-store';
import { showsActions, showsReducer, showsSagas } from './shows-state';
import { apiClient } from '../shared/api-client';
import { NgRedux } from '@angular-redux/store';

@Injectable()
export class ShowsActions {
    constructor(private ngRedux: NgRedux<any>) {
    }

    topShows(page: number) {
        this.ngRedux.dispatch({
            type: showsActions.TOP_SHOWS_REQUEST_START,
            page,
        });
    }

    searchShows(query: string, page: number) {
        this.ngRedux.dispatch({
            type: showsActions.SEARCH_SHOWS_REQUEST_START,
            page,
            query,
        });
    }

    getGenres() {
        this.ngRedux.dispatch({
            type: showsActions.GENRES_REQUEST_START,
        });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <genres-component [genres]="this.genres"></genres-component>

        <pre>{{this.shows | json}}</pre>
    `,
})
export class ShowsByGenreComponent implements OnInit {

    shows: any;
    genres: any;
    genre: any;

    constructor(private ngRedux: NgRedux<any>,
                private showsActions: ShowsActions) {
    }

    ngOnInit(): void {

        this.showsActions.topShows(1);

        this.ngRedux.select(state => state.shows)
            .distinctUntilChanged()
            .subscribe(shows => {
                this.shows = shows.topShows;
                this.genres = shows.genres;
            });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `

        <genres-component [genres]="this.genres"></genres-component>

        <pre>{{this.shows | json}}</pre>

    `,
})
export class TopShowsComponent implements OnInit {

    shows: any;
    genres: any;

    constructor(private ngRedux: NgRedux<any>,
                private showsActions: ShowsActions) {
    }

    ngOnInit(): void {

        this.showsActions.topShows(1);

        this.ngRedux.select(state => state.shows)
            .distinctUntilChanged()
            .subscribe(shows => {
                this.shows = shows.topShows;
                this.genres = shows.genres;
            });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <div> Query <input [(ngModel)]="this.query" type="text"/></div>
        <div>
            <button (click)="this.search()">Search</button>
        </div>

        <genres-component [genres]="this.genres"></genres-component>

        <pre>{{this.shows | json}}</pre>

    `,
})
export class SearchShowsComponent implements OnInit {

    shows: any;
    genres: any;

    query: string;

    constructor(private ngRedux: NgRedux<any>,
                private showsActions: ShowsActions) {
    }

    ngOnInit(): void {

        this.showsActions.searchShows(this.query, 1);

        this.ngRedux.select(state => state.shows)
            .distinctUntilChanged()
            .subscribe(shows => {
                this.shows = shows.searchShows;
                this.genres = shows.genres;
            });
    }

    search(): void {
        this.showsActions.searchShows(this.query, 1);
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    selector: 'genres-component',
    template: `
        <button *ngFor="let genre of this.genres">{{genre.genreName}}</button>
    `,
})
export class GenresComponent {

    @Input()
    genres: any;
}

const routes: Routes = [
    {path: '', redirectTo: 'top', pathMatch: 'full'},
    {path: 'top', component: TopShowsComponent},
    {path: 'search', component: SearchShowsComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
    ],
    declarations: [
        TopShowsComponent,
        SearchShowsComponent,
        GenresComponent,
    ],
    providers: [ShowsActions],
})
export class ShowsModule {
    constructor() {

        reduxState.addReducers({
            shows: showsReducer,
        });

        reduxState.addSagas(showsSagas(apiClient));
    }
}
