import { ApiClient, triggerAction } from '../shared/api-client';
import { actionTypes } from '../../infrastructure/redux-types';
import { globalActions } from '../global.state';
import { put } from 'redux-saga/effects';

export const myShowsActions = actionTypes('myShows').ofType<{
    MY_SHOWS_REQUEST_START: string;
    MY_SHOWS_REQUEST_SUCCESS: string;
}>();

const initialState = {};

export const myShowsReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case myShowsActions.MY_SHOWS_REQUEST_SUCCESS: {
            return {
                ...state,
                ...action.payload
            };
        }
        default: {
            return state;
        }
    }
};

export const myShowsSagas = (apiClient: ApiClient) => ({
    myShowsRequests: {
        type: myShowsActions.MY_SHOWS_REQUEST_START,
        inTransition: true,
        saga: function* () {

            const response = yield apiClient.myShows();

            yield put(triggerAction(myShowsActions.MY_SHOWS_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
});
