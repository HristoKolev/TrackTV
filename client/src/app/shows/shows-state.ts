import { ApiClient, triggerAction } from '../shared/api-client';
import { put } from 'redux-saga/effects';
import { globalActions } from '../global.state';
import { actionTypes, ReduxReducer } from '../../infrastructure/redux-store';

export const showsActions = actionTypes('SHOWS').ofType<{
    FETCH_SHOWS_REQUEST_START: string;
    FETCH_SHOWS_REQUEST_SUCCESS: string;

    FETCH_GENRES_REQUEST_START: string;
    FETCH_GENRES_REQUEST_SUCCESS: string;
}>();

const initialState = {
    totalCount: 0,
    items: [],
};

export const showsReducer: ReduxReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case showsActions.FETCH_SHOWS_REQUEST_SUCCESS: {
            return {
                ...state,
                items: action.payload.data,
                totalCount: action.payload.totalCount,
            };
        }
        case showsActions.FETCH_GENRES_REQUEST_SUCCESS: {
            return {
                ...state,
                genres: action.payload,
            };
        }
        default: {
            return state;
        }
    }
};

export const showsSagas = (apiClient: ApiClient) => ({
    showsRequestSaga: {
        type: showsActions.FETCH_SHOWS_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.shows(action.query);

            yield put({type: showsActions.FETCH_GENRES_REQUEST_START});

            yield put(triggerAction(showsActions.FETCH_SHOWS_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
    genresRequestSaga: {
        type: showsActions.FETCH_GENRES_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.getGenres();

            yield put(triggerAction(showsActions.FETCH_GENRES_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },

    },
});
