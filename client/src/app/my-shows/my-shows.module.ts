import {ChangeDetectionStrategy, Component, Input, NgModule, OnInit, ViewEncapsulation} from '@angular/core';
import {RouterModule} from '@angular/router';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {MyShowsActions, myShowsReducer, myShowsSagas} from './my-shows.state';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {ApiClient} from '../shared/api-client';
import {format} from 'date-fns';
import {SharedModule} from '../shared/shared.module';

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.OnPush,
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

  get state() {
    return this.store.select(state => state.myShows);
  }

  constructor(private actions: MyShowsActions,
              private store: ReduxStoreService) {
  }

  ngOnInit(): void {

    this.actions.myShows();
  }
}

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  selector: 'my-show-component',
  template: `
    <div class="tt-card my-show-card">

      <div class="show-title no-interact" [routerLink]="['/show', this.show.showId]">
        {{show.showName}}
      </div>

      <div class="banner-container">
        <img [bannerUrl]="show.showBanner" src="" class="banner"/>
      </div>

      <div class="episodes no-interact">
        <div class="last-episode">
          <ng-container *ngIf="show.lastEpisode">
            <img src="assets/left-arrow.png">
            <div class="episode-summary">
              <div class="episode-time">{{format(show.lastEpisode.firstAired)}}</div>
              <div class="episode-title">{{getEpisodeNumber(show.lastEpisode)}}</div>
            </div>
          </ng-container>
        </div>

        <div class="separator"></div>

        <div class="next-episode">

          <ng-container *ngIf="show.nextEpisode">
            <div class="episode-summary">
              <div class="episode-time">{{format(show.nextEpisode.firstAired)}}</div>
              <div class="episode-title">{{getEpisodeNumber(show.nextEpisode)}}</div>
            </div>
            <img src="assets/right-arrow.png" class="right-arrow">
          </ng-container>


        </div>
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

    .episodes {
      padding: 5px 10px;
      display: flex;
      justify-content: space-between;
    }

    .last-episode, .next-episode {
      display: flex;
      flex-basis: 49%;
    }

    img {
      flex-basis: 20%;
    }

    .episode-summary {
      flex-basis: 80%;
    }

    .episode-summary {

      text-align: center;
      height: 70px;
      margin-top: 4px;
    }

    .separator {
      border-right: 1px solid black;
    }

    img:not(.banner) {
      opacity: .4;
    }

    .episode-time, .episode-title {
      margin-top: 10px;
    }

    .banner {
      width: 94%;
    }

    .banner-container {
      text-align: center;
      margin-top: 10px;
    }
  `],
})
export class MyShowComponent {

  @Input()
  show: any;

  constructor(private actions: MyShowsActions) {
  }

  subscribe(showId: number) {
    this.actions.subscribe(showId);
  }

  unsubscribe(showId: number) {
    this.actions.unsubscribe(showId);
  }

  getEpisodeNumber(episode: any) {
    return `S${episode.seasonNumber.toString().padStart(2, '0')}E${episode.episodeNumber.toString().padStart(2, '0')}`;
  }

  format(dateString) {
    return format(dateString, 'MMMM DD');
  }
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: '', component: MyShowsComponent},
    ]),
    FormsModule,
    SharedModule
  ],
  declarations: [MyShowsComponent, MyShowComponent],
  providers: [MyShowsActions],
})
export class MyShowsModule {
  constructor(store: ReduxStoreService, apiClient: ApiClient) {

    store.addReducers({
      myShows: myShowsReducer,
    });

    store.addSagas(myShowsSagas(apiClient));
  }
}

declare module '../../infrastructure/redux/redux-state' {

  interface IReduxState {
    myShows: any;
  }
}
