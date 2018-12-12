import {CommonModule} from '@angular/common';
import {ActivatedRoute, RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {ChangeDetectionStrategy, Component, NgModule, OnInit, ViewEncapsulation} from '@angular/core';
import {ShowActions, showReducer, showSagas} from './show.state';
import {parseParams} from '../../infrastructure/routing-helpers';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {ApiClient} from '../shared/api-client';
import {IReduxState} from '../../infrastructure/redux/redux-state';
import {SharedModule} from '../shared/shared.module';
import {format} from 'date-fns';

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <ng-container *ngIf="state | async as show">
      <div class="tt-card show-card">

        <div class="show-title no-interact">{{show.showName}}</div>

        <div class="no-interact">
          <img [bannerUrl]="show.showBanner" src=""/>
        </div>

        <div class="details">
          <div class="columns">
            <div class="header">Air Day</div>
            <div class="content">{{dayNames()[show.airDay]}}</div>
          </div>

          <div class="columns">
            <div class="header">Air Time</div>
            <div class="content">{{show.airTime?.hour}}:{{show.airTime?.minute}}</div>
          </div>

          <div class="columns">
            <div class="header">First Aired</div>
            <div class="content">{{formatDate(show.firstAired)}}</div>
          </div>

          <div class="columns">
            <div class="header">Status</div>
            <div class="content">{{showStatuses()[show.showStatus]}}</div>
          </div>

          <div class="columns">
            <div class="header">Network</div>
            <div class="content">{{show.networkName}}</div>
          </div>

          <logged-in-component>

            <div class="button-wrapper">

              <button *ngIf="show.isUserSubscribed"
                      (click)="unsubscribe(show.showId)"
                      class="tt-button subscription-button">
                Unsubscribe
              </button>

              <button *ngIf="!show.isUserSubscribed"
                      (click)="subscribe(show.showId)"
                      class="tt-button subscription-button">
                Subscribe
              </button>
            </div>
          </logged-in-component>

        </div>

        <div class="tt-card">
          {{show.showDescription}}
        </div>

        <div class="tt-card">
          <div *ngIf="show.imdbId" class="detail-button-wrapper imdb">
            <a class="tt-button" href="https://www.imdb.com/title/{{show.imdbId}}/" target="_blank">View on IMDB</a>
          </div>

          <div *ngIf="show.theTvDbId" class="detail-button-wrapper thetvdb">
            <a class="tt-button" href="https://www.thetvdb.com/?tab=series&id={{show.theTvDbId}}" target="_blank">View on TheTvDB</a>
          </div>
        </div>

      </div>

    </ng-container>
  `,
  styles: [`
    .show-card {
      margin: 10px;
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

    img {
      width: 100%;
    }

    .details {
      margin: 5px;
    }

    .columns {
      display: flex;
      border-bottom: 1px solid #eee;
    }

    .columns:first-of-type {
      border-top: 1px solid #eee;
    }

    .header {
      flex: 1;
      border-left: 1px solid #eee;
    }

    .content {
      flex: 3;
    }

    .header, .content {
      padding: 2px 10px;
      border-right: 1px solid #eee;
      box-shadow: 1px 1px 1px #dcdcdc;
    }

    .detail-button-wrapper {
      text-align: center;
    }

    .detail-button-wrapper a {
      width: 100%;
      display: block;
      margin-top: 5px;
    }

    .detail-button-wrapper.imdb a {
      background-color: #ff6b07;
    }

    .detail-button-wrapper.imdb a:hover {
      background-color: #d45905;
    }

    .detail-button-wrapper.thetvdb a {
      background-color: #b6d415;
    }

    .detail-button-wrapper.thetvdb a:hover {
      background-color: #a1bb13;
    }

    @media (min-width: 768px) {
      .show-card {
        max-width: 800px;
        margin: 0 auto;
      }
    }
  `]
})
export class ShowComponent implements OnInit {

  get state() {
    return this.store.select(s => s.show);
  }

  constructor(private actions: ShowActions,
              private route: ActivatedRoute,
              private store: ReduxStoreService) {
  }

  ngOnInit(): void {

    this.route.paramMap
      .map(parseParams)
      .map(params => params.showId)
      .subscribe(showId => {
        this.actions.fetchShow(showId);
      });
  }

  subscribe(showId: number) {
    this.actions.subscribe(showId);
  }

  unsubscribe(showId: number) {
    this.actions.unsubscribe(showId);
  }

  dayNames() {
    return [
      'Monday',
      'Tuesday',
      'Wednesday',
      'Thursday',
      'Friday',
      'Saturday',
      'Sunday',
    ];
  }

  showStatuses() {
    return [
      'Unknown',
      'Continuing',
      'Ended'
    ];
  }

  formatDate(date) {
    if (!date) {
      return 'Unknown';
    }

    return format(date, 'DD.MM.YYYY');
  }
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: ':showId', component: ShowComponent},
    ]),
    FormsModule,
    SharedModule
  ],
  declarations: [ShowComponent],
  providers: [ShowActions],
})
export class ShowModule {
  constructor(private store: ReduxStoreService, apiClient: ApiClient) {

    this.store.addReducers({
      show: showReducer,
    });

    this.store.addSagas(showSagas(apiClient));
  }
}

declare module '../../infrastructure/redux/redux-state' {

  interface IReduxState {
    show: any;
  }
}
