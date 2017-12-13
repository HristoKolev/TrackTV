import { ApiClient, triggerAction } from '../shared/api-client';

import { globalActions } from '../global.state';
import { put } from 'redux-saga/effects';
import { actionTypes } from '../../infrastructure/redux-store';

export const showActions = actionTypes('SHOW').ofType<{
    FETCH_REQUEST_START: string,
    FETCH_REQUEST_SUCCESS: string;

    SUBSCRIBE_REQUEST_START: string,
    SUBSCRIBE_REQUEST_SUCCESS: string,

    UNSUBSCRIBE_REQUEST_START: string,
    UNSUBSCRIBE_REQUEST_SUCCESS: string,
}>();

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
