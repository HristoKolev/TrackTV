import { ChangeDetectionStrategy, Component, Injectable, Input, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { reduxStore } from '../../infrastructure/redux-store';
import { showsActions, showsReducer, showsSagas } from './shows-state';
import { apiClient } from '../shared/api-client';

@Injectable()
export class ShowsActions {

    topShows(page: number) {
        reduxStore.dispatch({
            type: showsActions.FETCH_TOP_SHOWS_REQUEST_START,
            page,
        });
    }

    searchShows(query: string, page: number) {
        reduxStore.dispatch({
            type: showsActions.SEARCH_SHOWS_REQUEST_START,
            query,
            page,
        });
    }

    showsByGenre(genreId: number, page: number) {
        reduxStore.dispatch({
            type: showsActions.FETCH_SHOWS_BY_GENRES_REQUEST_START,
            genreId,
            page,
        });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <genres-component [genres]="this.genres"></genres-component>

        <pre>{{this.genre | json}}</pre>
        <pre>{{this.shows | json}}</pre>

        <div *ngFor="let show of this.shows.items">
            <button [routerLink]="['/show', show.showId]">{{show.showName}}</button>
        </div>
    `,
})
export class ShowsByGenreComponent implements OnInit {

    shows: any;
    genres: any;
    genre: any;
    genreId: number;

    constructor(private showsActions: ShowsActions,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {

        this.route.paramMap
            .map(x => x.get('genreId') as string)
            .map(x => Number.parseInt(x, 10))
            .subscribe(genreId => {

                this.genreId = genreId;
                this.showsActions.showsByGenre(genreId, 1);
            });

        reduxStore.select(state => state.shows)
            .distinctUntilChanged()
            .subscribe(shows => {

                this.shows = shows.showsByGenre;
                this.genres = shows.genres;

                if (shows.genres) {

                    this.genre = shows.genres.filter((x: any) => x.genreId === this.genreId)[0];
                }
            });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `

        <genres-component [genres]="this.genres"></genres-component>

        <pre>{{this.shows | json}}</pre>

        <div *ngFor="let show of this.shows.items">
            <button [routerLink]="['/show', show.showId]">{{show.showName}}</button>
        </div>
    `,
})
export class TopShowsComponent implements OnInit {

    shows: any;
    genres: any;

    constructor(private showsActions: ShowsActions) {
    }

    ngOnInit(): void {

        this.showsActions.topShows(1);

        reduxStore.select(state => state.shows)
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

        <div *ngFor="let show of this.shows.items">
            <button [routerLink]="['/show', show.showId]">{{show.showName}}</button>
        </div>

    `,
})
export class SearchShowsComponent implements OnInit {

    shows: any;
    genres: any;

    query: string;

    constructor(private showsActions: ShowsActions) {
    }

    ngOnInit(): void {

        this.showsActions.searchShows(this.query, 1);

        reduxStore.select(state => state.shows)
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
        <button *ngFor="let genre of this.genres" [routerLink]="['/shows/genre', genre.genreId]">{{genre.genreName}}</button>
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
    {path: 'genre/:genreId', component: ShowsByGenreComponent},
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
        ShowsByGenreComponent,
    ],
    providers: [ShowsActions],
})
export class ShowsModule {
    constructor() {

        reduxStore.addReducers({
            shows: showsReducer,
        });

        reduxStore.addSagas(showsSagas(apiClient));
    }
}
