import {ChangeDetectionStrategy, Component, NgModule, OnInit, ViewEncapsulation} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {CalendarActions, calendarReducer, calendarSagas} from './calendar.state';
import {format} from 'date-fns';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {ApiClient} from '../shared/api-client';
import {ScrollToModule, ScrollToService} from '@nicky-lenaers/ngx-scroll-to';

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <div *ngIf="state | async as data" class="wrapper">
      <div *ngFor="let name of dayNames()" class="header no-interact">{{name}}</div>
      <ng-container *ngFor="let week of data.weeks">
        <div *ngFor="let day of week" class="day" [ngClass]="{'empty': !day.episodes.length, 'today': day.isToday}"
             [attr.id]="day.isToday ? 'today' : null">
          <div class="day-header no-interact">{{formatHeader(day)}}</div>
          <div class="episode-list">
            <div *ngFor="let episode of day.episodes">
              <span [routerLink]="['/show', episode.showId]" class="episode no-interact">
                {{ episode.showName }} - {{ getEpisodeNumber(episode) }}
              </span>
            </div>

            <div *ngIf="!day.episodes.length && day.isToday" class="no-episodes">
              No episodes today
            </div>
          </div>
        </div>
      </ng-container>
    </div>
  `,
  styles: [`
    .episode {
      color: #e20f00;
      cursor: pointer;
      display: block;
    }

    .episode-list div:not(:first-of-type) {
      margin-top: 8px;
    }

    .wrapper {
      padding: 10px;
    }

    .today {
      background-color: #dbffdc;
    }

    .no-episodes {
      text-align: center;
    }

    @media (min-width: 1000px) {
      .wrapper {
        display: grid;
        grid-template-columns: repeat(7, 1fr);
        grid-template-rows: auto repeat(6, minmax(100px, auto));
      }

      .header {
        text-align: center;
        padding: 10px;
        background-color: #f44336;
        color: white;
      }

      .day {
        border: 1px solid #eee;
      }

      .day-header {
        width: 100%;
        padding: 2px;
        padding-left: 5px;
        background-color: #eee;
        box-sizing: border-box;
      }

      .episode-list {
        font-size: 13px;
        padding: 5px;
      }

      .no-episodes * {
        display: none;
      }
    }

    @media (max-width: 999px) {

      .header {
        display: none;
      }

      .day {
        border: 1px solid #eee;
        box-shadow: 5px 5px 5px #eee;
        margin-bottom: 7px;
      }

      .day-header {
        text-align: center;
        padding: 3px;
        background-color: #ff6b07;
        color: white;
      }

      .episode-list {
        padding: 10px;
      }

      .episode-list div:not(:first-of-type) {
        margin-top: 20px;
      }

    }
  `],
})
export class CalendarComponent implements OnInit {

  get state() {
    return this.store.select(state => state.calendar);
  }

  constructor(private actions: CalendarActions,
              private store: ReduxStoreService,
              private scroll: ScrollToService) {
  }

  ngOnInit(): void {

    this.actions.fetchCalendar();

    setTimeout(() => {
      this.scroll.scrollTo(
        {
          offset: -150,
          easing: 'easeInOutCubic',
          target: 'today'
        });
    }, 500);
  }

  formatHeader(day: any): string {

    let header = format(day.date, 'MMMM Do');

    header = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][new Date(day.date).getDay()] + ', ' + header;

    if (day.isToday) {

      return 'Today (' + header + ')';
    }

    return header;
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

  getEpisodeNumber(episode: any) {
    return `S${episode.seasonNumber.toString().padStart(2, '0')}E${episode.episodeNumber.toString().padStart(2, '0')}`;
  }
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: '', component: CalendarComponent},
    ]),
    ScrollToModule.forRoot(),
    FormsModule,
  ],
  declarations: [
    CalendarComponent,
  ],
  providers: [CalendarActions],
})
export class CalendarModule {

  constructor(store: ReduxStoreService, apiClient: ApiClient) {
    store.addReducers({
      calendar: calendarReducer,
    });

    store.addSagas(calendarSagas(apiClient));
  }
}

declare module '../../infrastructure/redux/redux-state' {

  interface IReduxState {
    calendar: any;
  }
}
