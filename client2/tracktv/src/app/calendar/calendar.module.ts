import {ChangeDetectionStrategy, Component, NgModule, OnInit, ViewEncapsulation} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {SharedModule} from '../shared/shared.module';
import {CalendarActions, calendarReducer, calendarSagas} from './calendar.state';
import {apiClient} from '../shared/api-client';
import {format} from 'date-fns';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.Default,
  template: `
    <div *ngIf="state | async as data" class="wrapper">
      <div *ngFor="let name of dayNames()" class="header">{{name}}</div>
      <ng-container *ngFor="let week of data.weeks">
        <div *ngFor="let day of week" class="day" [ngClass]="{'empty': !day.episodes.length}">
          <div class="day-header">{{formatDate(day.date)}}</div>
          <div class="episode-list">
            <div *ngFor="let episode of day.episodes">
              {{ episode.showName }} - {{ getEpisodeNumber(episode) }}
            </div>
          </div>
        </div>
      </ng-container>
    </div>
  `,
  styles: [`
    @media (min-width: 1000px) {
      .wrapper {
        display: grid;
        grid-template-columns: repeat(7, 1fr);
        grid-template-rows: auto repeat(6, minmax(100px, auto));

        padding: 10px;
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

      .empty {
        display: none;
      }
    }
  `],
})
export class CalendarComponent implements OnInit {

  get state() {
    return this.store.select(state => state.calendar);
  }

  constructor(private actions: CalendarActions,
              private store: ReduxStoreService) {
  }

  ngOnInit(): void {

    this.actions.fetchCalendar();
  }

  formatDate(day: Date): string {
    return format(day, 'MMMM Do');
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
    FormsModule,
    SharedModule,
  ],
  declarations: [
    CalendarComponent,
  ],
  providers: [CalendarActions],
})
export class CalendarModule {

  constructor(store: ReduxStoreService) {
    store.addReducers({
      calendar: calendarReducer,
    });

    store.addSagas(calendarSagas(apiClient));
  }
}
