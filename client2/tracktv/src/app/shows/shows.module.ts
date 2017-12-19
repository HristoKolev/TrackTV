import {Component, Input, NgModule, OnInit, ViewEncapsulation} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ActivatedRoute, RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {ShowsActions, showsReducer, showsSagas} from './shows-state';
import {apiClient} from '../shared/api-client';
import {parseParams, removeFalsyProperties} from '../../infrastructure/routing-helpers';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';

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

    .list-wrapper * {
      user-drag: none;
      user-select: none;
      -moz-user-select: none;
      -webkit-user-drag: none;
      -webkit-user-select: none;
      -ms-user-select: none;
    }

    @media (min-width: 768px) {

      .filter-controls input, .filter-controls select {
        width: 300px;
      }

      .list-wrapper {

        margin: 0 auto;
        text-align: center;
      }

      .list-wrapper show-summary-component {
        display: inline-block;
        max-width: 600px;
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

  get state() {
    return this.store.select(state => state.shows);
  }

  query: any = {};

  constructor(private actions: ShowsActions,
              private route: ActivatedRoute,
              private store: ReduxStoreService) {
  }

  ngOnInit(): void {

    this.route.queryParamMap
      .map(parseParams)
      .subscribe((query) => {
        this.actions.shows(query);
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
      <div><img src="http://192.168.1.104:7001/banners/{{show.showBanner}}" class="poster"></div>
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
      pointer-events: none;
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
  constructor(private store: ReduxStoreService) {

    this.store.addReducers({
      shows: showsReducer,
    });

    this.store.addSagas(showsSagas(apiClient));
  }
}
