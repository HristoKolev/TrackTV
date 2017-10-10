import { actionTypes, ReduxReducer } from '../../infrastructure/redux-types';
import { ApiClient, triggerAction } from '../shared/api-client';
import { put } from 'redux-saga/effects';
import { globalActions } from '../global.state';

export const showsActions = actionTypes('shows').ofType<{
    TOP_SHOWS_REQUEST_START: string;
    TOP_SHOWS_REQUEST_SUCCESS: string;

    SEARCH_SHOWS_REQUEST_START: string;
    SEARCH_SHOWS_REQUEST_SUCCESS: string;

    GENRES_REQUEST_START: string;
    GENRES_REQUEST_SUCCESS: string;

    SHOWS_BY_GENRES_REQUEST_START: string;
    SHOWS_BY_GENRES_REQUEST_SUCCESS: string;
}>();

const initialState = {
    topShows: {
        totalCount: 0,
        items: [],
    },
    searchShows: {
        totalCount: 0,
        items: [],
    },
    showsByGenre: {
        totalCount: 0,
        items: [],
    },
};

export const showsReducer: ReduxReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case showsActions.TOP_SHOWS_REQUEST_SUCCESS: {
            return {
                ...state,
                topShows: {
                    ...state.topShows,
                    items: action.payload.data,
                    totalCount: action.payload.totalCount,
                },
            };
        }
        case showsActions.SEARCH_SHOWS_REQUEST_SUCCESS: {
            return {
                ...state,
                searchShows: {
                    ...state.searchShows,
                    items: action.payload.data,
                    totalCount: action.payload.totalCount,
                },
            };
        }
        case showsActions.SHOWS_BY_GENRES_REQUEST_SUCCESS: {
            return {
                ...state,
                showsByGenre: {
                    ...state.showsByGenre,
                    items: action.payload.data,
                    totalCount: action.payload.totalCount,
                },
            };
        }
        case showsActions.GENRES_REQUEST_SUCCESS: {
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
    topShowsRequestSaga: {
        type: showsActions.TOP_SHOWS_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.topShows(action.page);

            yield put({type: showsActions.GENRES_REQUEST_START});

            yield put(triggerAction(showsActions.TOP_SHOWS_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
    searchShowsRequestSaga: {
        type: showsActions.SEARCH_SHOWS_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.searchShows(action.query, action.page);

            yield put({type: showsActions.GENRES_REQUEST_START});

            yield put(triggerAction(showsActions.SEARCH_SHOWS_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
    genresRequestSaga: {
        type: showsActions.GENRES_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.getGenres();

            yield put(triggerAction(showsActions.GENRES_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },

    },
    showsByGenreRequestSaga: {
        type: showsActions.SHOWS_BY_GENRES_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.showsByGenre(action.genreId, action.page);

            yield put({type: showsActions.GENRES_REQUEST_START});

            yield put(triggerAction(showsActions.SHOWS_BY_GENRES_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
});
