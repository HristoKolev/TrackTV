import {ApiClient, triggerAction} from '../shared/api-client';
import {put} from 'redux-saga/effects';
import {globalActions} from '../global.state';
import {actionTypes, ReduxReducer} from '../../infrastructure/redux/meta';

export const calendarActions = actionTypes('CALENDAR').ofType<{
  FETCH_CALENDAR_REQUEST_START: string;
  FETCH_CALENDAR_REQUEST_SUCCESS: string;
}>();

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
