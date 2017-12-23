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

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <ng-container *ngIf="state | async as show">
      <div class="tt-card show-card">

        <div class="show-title">{{show.showName}}</div>

        <div>
          <img [bannerUrl]="show.showBanner" src=""/>
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

      <pre>{{show | json}}</pre>

    </ng-container>
  `,
  styles: [`
    .show-card {
      margin: 10px;
      cursor: pointer;
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
      flex-basis: 20%;
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
