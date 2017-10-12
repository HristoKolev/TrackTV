import { actionTypes } from '../../infrastructure/redux-types';
import { globalActions } from '../global.state';
import { put } from 'redux-saga/effects';
import { ApiClient, triggerAction } from './api-client';

export const subscriptionActions = actionTypes('subscription').ofType<{
    SUBSCRIBE_REQUEST_START: string,
    SUBSCRIBE_REQUEST_SUCCESS: string,

    UNSUBSCRIBE_REQUEST_START: string,
    UNSUBSCRIBE_REQUEST_SUCCESS: string,

}>();

export const subscribeSagas = (apiClient: ApiClient) => ({
    subscribeRequestSaga: {
        type: subscriptionActions.SUBSCRIBE_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.subscribe(action.showId);

            yield put(triggerAction(subscriptionActions.SUBSCRIBE_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
    unsubscribeRequestSaga: {
        type: subscriptionActions.UNSUBSCRIBE_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.unsubscribe(action.showId);

            yield put(triggerAction(subscriptionActions.UNSUBSCRIBE_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
});
