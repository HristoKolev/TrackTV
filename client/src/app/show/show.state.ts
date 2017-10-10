import { ApiClient, triggerAction } from '../shared/api-client';
import { actionTypes } from '../../infrastructure/redux-types';
import { globalActions } from '../global.state';
import { put } from 'redux-saga/effects';

export const showActions = actionTypes('show').ofType<{
    SHOW_REQUEST_START: string,
    SHOW_REQUEST_SUCCESS: string;
}>();

const initialState = {
    show: {},
};

export const showReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case showActions.SHOW_REQUEST_SUCCESS: {
            return {
                ...state,
                show: action.payload,
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
});
