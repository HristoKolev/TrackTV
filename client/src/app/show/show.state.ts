import { ApiClient, triggerAction } from '../shared/api-client';
import { actionTypes } from '../../infrastructure/redux-types';
import { globalActions } from '../global.state';
import { put } from 'redux-saga/effects';
import { subscriptionActions } from '../shared/subscription.state';

export const showActions = actionTypes('show').ofType<{
    SHOW_REQUEST_START: string,
    SHOW_REQUEST_SUCCESS: string;
}>();

const initialState = {};

export const showReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case showActions.SHOW_REQUEST_SUCCESS: {
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
        type: showActions.SHOW_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.show(action.showId);

            yield put(triggerAction(showActions.SHOW_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
    showSubscribeFetchSaga: {
        type: subscriptionActions.SUBSCRIBE_REQUEST_SUCCESS,
        inTransition: true,
        saga: function* (action: any, state: any) {
            yield put({type: showActions.SHOW_REQUEST_START, showId: state.show.showId});
        },
    },
    showUnsubscribeFetchSaga: {
        type: subscriptionActions.UNSUBSCRIBE_REQUEST_SUCCESS,
        inTransition: true,
        saga: function* (action: any, state: any) {
            yield put({type: showActions.SHOW_REQUEST_START, showId: state.show.showId});
        },
    },
});
