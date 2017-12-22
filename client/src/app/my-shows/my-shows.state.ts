import {ApiClient, triggerAction} from '../shared/api-client';
import {put} from 'redux-saga/effects';
import {Injectable} from '@angular/core';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {globalActions} from '../../infrastructure/redux/redux-global-actions';

export const myShowsActions = {
  FETCH_REQUEST_START: 'MY_SHOWS/FETCH_REQUEST_START',
  FETCH_REQUEST_SUCCESS: 'MY_SHOWS/FETCH_REQUEST_SUCCESS',
  SUBSCRIBE_REQUEST_START: 'MY_SHOWS/SUBSCRIBE_REQUEST_START',
  SUBSCRIBE_REQUEST_SUCCESS: 'MY_SHOWS/SUBSCRIBE_REQUEST_SUCCESS',
  UNSUBSCRIBE_REQUEST_START: 'MY_SHOWS/UNSUBSCRIBE_REQUEST_START',
  UNSUBSCRIBE_REQUEST_SUCCESS: 'MY_SHOWS/UNSUBSCRIBE_REQUEST_SUCCESS',
};

@Injectable()
export class MyShowsActions {

  constructor(private store: ReduxStoreService) {
  }

  myShows() {
    this.store.dispatch({
      type: myShowsActions.FETCH_REQUEST_START,
    });
  }

  subscribe(showId: number) {
    this.store.dispatch({
      type: myShowsActions.SUBSCRIBE_REQUEST_START,
      showId,
    });
  }

  unsubscribe(showId: number) {
    this.store.dispatch({
      type: myShowsActions.UNSUBSCRIBE_REQUEST_START,
      showId,
    });
  }
}

const initialState = {
  shows: [],
};

export const myShowsReducer = (state = initialState, action: any) => {
  switch (action.type) {
    case myShowsActions.FETCH_REQUEST_SUCCESS: {
      return {
        ...state,
        shows: action.payload.map((show: any) => ({...show, isSubscribed: true})),
      };
    }
    case myShowsActions.SUBSCRIBE_REQUEST_SUCCESS: {
      return {
        ...state,
        shows: state.shows.map((show: any) => {

          if (show.showId === action.showId) {

            return {
              ...show,
              isSubscribed: true,
            };
          }

          return show;
        }),
      };
    }
    case myShowsActions.UNSUBSCRIBE_REQUEST_SUCCESS: {
      return {
        ...state,
        shows: state.shows.map((show: any) => {

          if (show.showId === action.showId) {

            return {
              ...show,
              isSubscribed: false,
            };
          }

          return show;
        }),
      };
    }
    default: {
      return state;
    }
  }
};

export const myShowsSagas = (apiClient: ApiClient) => ({
  myShowsRequests: {
    type: myShowsActions.FETCH_REQUEST_START,
    inTransition: true,
    saga: function* () {

      const response = yield apiClient.myShows();

      yield put(triggerAction(myShowsActions.FETCH_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
    },
  },
  myShowsSubscribe: {
    type: myShowsActions.SUBSCRIBE_REQUEST_START,
    inTransition: true,
    saga: function* (action: any) {

      const response = yield apiClient.subscribe(action.showId);

      yield put(triggerAction(
        myShowsActions.SUBSCRIBE_REQUEST_SUCCESS,
        globalActions.GLOBAL_ERROR,
        response,
        {showId: action.showId},
      ));
    },
  },
  myShowsUnsubscribe: {
    type: myShowsActions.UNSUBSCRIBE_REQUEST_START,
    inTransition: true,
    saga: function* (action: any) {

      const response = yield apiClient.unsubscribe(action.showId);

      yield put(triggerAction(
        myShowsActions.UNSUBSCRIBE_REQUEST_SUCCESS,
        globalActions.GLOBAL_ERROR,
        response,
        {showId: action.showId},
      ));
    },
  },

});
