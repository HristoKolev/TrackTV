import { Component, Injectable, Input, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, ParamMap, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { reduxStore } from '../../infrastructure/redux-store';
import { showsActions, showsReducer, showsSagas } from './shows-state';
import { apiClient } from '../shared/api-client';
import { Observable } from 'rxjs/Observable';

const parseParams = (paramMap: ParamMap) => paramMap.keys
    .reduce((result: any, key: string) => ({...result, [key]: paramMap.get(key)}), {});

const removeFalsyProperties = (obj: any): any => {
    const result = {} as any;

    for (let [key, value] of Object.entries(obj)) {
        if (value) {

            result[key] = value;
        }
    }

    return result;
};

@Injectable()
export class ShowsActions {

    shows(query: any) {
        reduxStore.dispatch({
            type: showsActions.FETCH_SHOWS_REQUEST_START,
            query,
        });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    template: `
        <div *ngIf="state | async as data">
            <div class="filter-controls">
                <input type="text" class="tt-input" placeholder="Show name" [(ngModel)]="this.query.showName"/>
                <select class="tt-input" [(ngModel)]="this.query.genreId">
                    <option value="">All Genres</option>
                    <option *ngFor="let genre of data.genres"
                            [ngValue]="genre.genreId" [attr.value]="genre.genreId">
                        {{genre.genreName}}
                    </option>
                </select>
                <button class="tt-button" [routerLink]="['./']"
                        [queryParams]="this.cleanQuery">Search
                </button>
            </div>

            <div class="list-wrapper">
                <show-summary-component *ngFor="let show of data.items" [show]="show"></show-summary-component>
            </div>
        </div>
    `,
    styles: [`

        .filter-controls {
            margin: 0 auto;
            text-align: center;
        }

        .filter-controls input {
            display: inline-block;
        }

        @media (min-width: 768px) {

            .list-wrapper {

                margin: 0 auto;
                text-align: center;
            }

            .list-wrapper show-summary-component {
                display: inline-block;
                max-width: 600px;
            }

            .filter-controls input, .filter-controls select {
                width: 300px;
            }

            button {
                width: 120px;
            }
        }

        @media (max-width: 767px) {

            .filter-controls {
                margin-bottom: 20px;
            }

            .filter-controls input, .filter-controls select {
                display: block;
                margin: 5px auto;
                width: 90%;
            }

            .filter-controls button {
                width: 90%;
            }
        }

    `],
})
export class ShowsComponent implements OnInit {

    state: Observable<any> = reduxStore.select(state => state.shows);

    query: any = {};

    constructor(private showsActions: ShowsActions, private route: ActivatedRoute) {
    }

    ngOnInit(): void {

        this.route.queryParamMap
            .map(parseParams)
            .subscribe((query) => {
                this.showsActions.shows(query);
                this.query = {...query, genreId: query.genreId || ''};
            });

    }

    get cleanQuery() {
        return removeFalsyProperties(this.query);
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    selector: 'show-summary-component',
    template: `
        <div class="tt-card show-card" [routerLink]="['/show', this.show.showId]">
            <div class="show-title">{{show.showName}}</div>
            <div class="show-details">Subscriber count: {{show.subscriberCount}} | Status: {{getStatusText(show.showStatus)}}</div>
            <div><img src="http://localhost:5000/banners/{{show.showBanner}}" class="poster"></div>
        </div>
    `,
    styles: [`
        .show-card {
            margin: 10px;
            cursor: pointer;
            padding: 0;
        }

        .poster {
            width: 94%;
            margin: 0 10px;
            margin-bottom: 10px;
        }

        .show-details {
            text-align: center;
            margin: 10px;
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
    `],
})
export class ShowSummaryComponent {

    @Input()
    show: any;

    getStatusText(showStatus: number) {
        return ['Unknown', 'Continuing', 'Ended'][showStatus];
    }
}

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild([
            {path: '', component: ShowsComponent},
        ]),
        FormsModule,
    ],
    declarations: [
        ShowsComponent,
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
