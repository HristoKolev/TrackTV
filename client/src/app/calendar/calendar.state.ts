import {ApiClient, triggerAction} from '../shared/api-client';
import {put} from 'redux-saga/effects';
import {ReduxReducer} from '../../infrastructure/redux/meta';
import {Injectable} from '@angular/core';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {globalActions} from '../../infrastructure/redux/redux-global-actions';

export const calendarActions = {
  FETCH_CALENDAR_REQUEST_START: 'CALENDAR/FETCH_CALENDAR_REQUEST_START',
  FETCH_CALENDAR_REQUEST_SUCCESS: 'CALENDAR/FETCH_CALENDAR_REQUEST_SUCCESS',
};

@Injectable()
export class CalendarActions {

  constructor(private store: ReduxStoreService) {
  }

  fetchCalendar() {
    this.store.dispatch({
      type: calendarActions.FETCH_CALENDAR_REQUEST_START,
    });
  }
}

const initialState = {};

export const calendarReducer: ReduxReducer = (state = initialState, action: any) => {

  switch (action.type) {

    case calendarActions.FETCH_CALENDAR_REQUEST_SUCCESS: {
      return {
        ...state,
        weeks: action.payload,
      };
    }
    default: {
      return state;
    }
  }
};

export const calendarSagas = (apiClient: ApiClient) => ({
  fetchCalendarSaga: {
    type: calendarActions.FETCH_CALENDAR_REQUEST_START,
    inTransition: true,
    saga: function* () {
      const response = yield apiClient.calendar();

      yield put(triggerAction(calendarActions.FETCH_CALENDAR_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
    },
  },
});
