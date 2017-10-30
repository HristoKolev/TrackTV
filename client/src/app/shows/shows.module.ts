import { ChangeDetectionStrategy, Component, Injectable, Input, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { reduxStore } from '../../infrastructure/redux-store';
import { showsActions, showsReducer, showsSagas } from './shows-state';
import { apiClient } from '../shared/api-client';
import { go } from '../global.state';

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
    selector: 'show-summary-component',
    template: `
        <div class="tt-card show-card" (click)="showClicked()">
            <div class="show-title">{{show.showName}}</div>
            <div class="show-details">Subscriber count: {{show.subscriberCount}} | Status: {{getStatusText(show.showStatus)}}</div>
            <div><img src="http://192.168.1.104:7001/banners/{{show.showBanner}}"></div>
        </div>
    `,
    styles: [`
        .show-card {
            margin: 20px;
            cursor: pointer;
        }

        img {
            width: 100%;
        }

        .show-details {
            text-align: center;
            margin: 10px;
        }

        .show-title {
            text-align: center;
            font-size: 20px;
            font-weight: bold;

            width: 70%;
            margin: 0 auto;
            padding: 10px;
            border-bottom: 1px solid #f44336;
        }
    `],
})
export class ShowSummaryComponent {

    @Input()
    show: any;

    getStatusText(showStatus: number) {
        switch (showStatus) {
            case 0: {
                return 'Ended';
            }
            case 1 : {
                return 'Continuing';
            }
            default: {
                throw new Error(`The showStatus is out of range. Acceptable values: (0, 1); Value: ${showStatus}`);
            }
        }
    }

    showClicked() {
        go(['/show', this.show.showId]);
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `

        <!--<genres-component [genres]="this.genres"></genres-component>-->

        <div *ngFor="let show of this.shows.items">
            <show-summary-component [show]="show"></show-summary-component>
        </div>

        <pre>{{this.shows | json}}</pre>


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
        ShowSummaryComponent,
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
