import {ApiClient, triggerAction} from '../shared/api-client';

import {globalActions} from '../global.state';
import {put} from 'redux-saga/effects';
import {Injectable} from '@angular/core';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';

export const showActions = {
  FETCH_REQUEST_START: 'SHOW/FETCH_REQUEST_START',
  FETCH_REQUEST_SUCCESS: 'SHOW/FETCH_REQUEST_SUCCESS',

  SUBSCRIBE_REQUEST_START: 'SHOW/SUBSCRIBE_REQUEST_START',
  SUBSCRIBE_REQUEST_SUCCESS: 'SHOW/SUBSCRIBE_REQUEST_SUCCESS',

  UNSUBSCRIBE_REQUEST_START: 'SHOW/UNSUBSCRIBE_REQUEST_START',
  UNSUBSCRIBE_REQUEST_SUCCESS: 'SHOW/UNSUBSCRIBE_REQUEST_SUCCESS',
};

@Injectable()
export class ShowActions {

  constructor(private store: ReduxStoreService) {
  }

  show(showId: number) {
    this.store.dispatch({
      type: showActions.FETCH_REQUEST_START,
      showId,
    });
  }

  subscribe(showId: number) {
    this.store.dispatch({
      type: showActions.SUBSCRIBE_REQUEST_START,
      showId,
    });
  }

  unsubscribe(showId: number) {
    this.store.dispatch({
      type: showActions.UNSUBSCRIBE_REQUEST_START,
      showId,
    });
  }
}

const initialState = {};

export const showReducer = (state = initialState, action: any) => {
  switch (action.type) {
    case showActions.FETCH_REQUEST_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default: {
      return state;
    }
  }
};

export const showSagas = (apiClient: ApiClient) => ({
  showRequestSaga: {
    type: showActions.FETCH_REQUEST_START,
    inTransition: true,
    saga: function* (action: any) {

      const response = yield apiClient.show(action.showId);

      yield put(triggerAction(showActions.FETCH_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
    },
  },
  subscribeRequestSaga: {
    type: showActions.SUBSCRIBE_REQUEST_START,
    inTransition: true,
    saga: function* (action: any) {

      const response = yield apiClient.subscribe(action.showId);

      yield put(triggerAction(showActions.SUBSCRIBE_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
    },
  },
  unsubscribeRequestSaga: {
    type: showActions.UNSUBSCRIBE_REQUEST_START,
    inTransition: true,
    saga: function* (action: any) {

      const response = yield apiClient.unsubscribe(action.showId);

      yield put(triggerAction(showActions.UNSUBSCRIBE_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
    },
  },
  showSubscribeFetchSaga: {
    type: showActions.SUBSCRIBE_REQUEST_SUCCESS,
    inTransition: true,
    saga: function* (action: any, state: any) {
      yield put({type: showActions.FETCH_REQUEST_START, showId: state.show.showId});
    },
  },
  showUnsubscribeFetchSaga: {
    type: showActions.UNSUBSCRIBE_REQUEST_SUCCESS,
    inTransition: true,
    saga: function* (action: any, state: any) {
      yield put({type: showActions.FETCH_REQUEST_START, showId: state.show.showId});
    },
  },

});
