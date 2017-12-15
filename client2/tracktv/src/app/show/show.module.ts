import {CommonModule} from '@angular/common';
import {ActivatedRoute, RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {Component, Injectable, NgModule, OnInit, ViewEncapsulation} from '@angular/core';
import {reduxStore} from '../../infrastructure/redux-store';
import {showActions, showReducer, showSagas} from './show.state';
import {apiClient} from '../shared/api-client';
import {parseParams} from '../../infrastructure/routing-helpers';

@Injectable()
export class ShowActions {

  show(showId: number) {
    reduxStore.dispatch({
      type: showActions.FETCH_REQUEST_START,
      showId,
    });
  }

  subscribe(showId: number) {
    reduxStore.dispatch({
      type: showActions.SUBSCRIBE_REQUEST_START,
      showId,
    });
  }

  unsubscribe(showId: number) {
    reduxStore.dispatch({
      type: showActions.UNSUBSCRIBE_REQUEST_START,
      showId,
    });
  }
}

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  template: `
    <ng-container *ngIf="showState | async as show">
      <ng-container *ngIf="sessionState | async as session">

        <pre>{{show | json}}</pre>

        <ng-container *ngIf="session.isLoggedIn">
          <button *ngIf="!show.isUserSubscribed" (click)="subscribe(show.showId)">Subscribe</button>
          <button *ngIf="show.isUserSubscribed" (click)="unsubscribe(show.showId)">Unsubscribe</button>
        </ng-container>
      </ng-container>
    </ng-container>
  `,
})
export class ShowComponent implements OnInit {

  showState: any = reduxStore.select(state => state.show);
  sessionState: any = reduxStore.select(state => state.session);

  constructor(private actions: ShowActions,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {

    this.route.paramMap
      .map(parseParams)
      .map(params => params.showId)
      .subscribe(this.actions.show);
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
  ],
  declarations: [ShowComponent],
  providers: [ShowActions],
})
export class ShowModule {
  constructor() {

    reduxStore.addReducers({
      show: showReducer,
    });

    reduxStore.addSagas(showSagas(apiClient));
  }
}
