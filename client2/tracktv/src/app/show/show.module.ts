import {CommonModule} from '@angular/common';
import {ActivatedRoute, RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {Component, NgModule, OnInit, ViewEncapsulation} from '@angular/core';
import {ShowActions, showReducer, showSagas} from './show.state';
import {parseParams} from '../../infrastructure/routing-helpers';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {ApiClient} from '../shared/api-client';

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

  get showState() {
    return this.store.select(state => state.show);
  }

  get sessionState() {
    return this.store.select(state => state.session);
  }

  constructor(private actions: ShowActions,
              private route: ActivatedRoute,
              private store: ReduxStoreService) {
  }

  ngOnInit(): void {

    this.route.paramMap
      .map(parseParams)
      .map(params => params.showId)
      .subscribe(this.actions.show.bind(this.actions));
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
  constructor(private store: ReduxStoreService, apiClient: ApiClient) {

    this.store.addReducers({
      show: showReducer,
    });

    this.store.addSagas(showSagas(apiClient));
  }
}
