import { ApiClient, triggerAction } from '../shared/api-client';
import { put } from 'redux-saga/effects';
import { globalActions } from '../global.state';
import { actionTypes, ReduxReducer } from '../../infrastructure/redux-store';

export const showsActions = actionTypes('SHOWS').ofType<{
    FETCH_TOP_SHOWS_REQUEST_START: string;
    FETCH_TOP_SHOWS_REQUEST_SUCCESS: string;

    SEARCH_SHOWS_REQUEST_START: string;
    SEARCH_SHOWS_REQUEST_SUCCESS: string;

    FETCH_GENRES_REQUEST_START: string;
    FETCH_GENRES_REQUEST_SUCCESS: string;

    FETCH_SHOWS_BY_GENRES_REQUEST_START: string;
    FETCH_SHOWS_BY_GENRES_REQUEST_SUCCESS: string;
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
        case showsActions.FETCH_TOP_SHOWS_REQUEST_SUCCESS: {
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
        case showsActions.FETCH_SHOWS_BY_GENRES_REQUEST_SUCCESS: {
            return {
                ...state,
                showsByGenre: {
                    ...state.showsByGenre,
                    items: action.payload.data,
                    totalCount: action.payload.totalCount,
                },
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
    topShowsRequestSaga: {
        type: showsActions.FETCH_TOP_SHOWS_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.topShows(action.page);

            yield put({type: showsActions.FETCH_GENRES_REQUEST_START});

            yield put(triggerAction(showsActions.FETCH_TOP_SHOWS_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
    searchShowsRequestSaga: {
        type: showsActions.SEARCH_SHOWS_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.searchShows(action.query, action.page);

            yield put({type: showsActions.FETCH_GENRES_REQUEST_START});

            yield put(triggerAction(showsActions.SEARCH_SHOWS_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
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
    showsByGenreRequestSaga: {
        type: showsActions.FETCH_SHOWS_BY_GENRES_REQUEST_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.showsByGenre(action.genreId, action.page);

            yield put({type: showsActions.FETCH_GENRES_REQUEST_START});

            yield put(triggerAction(showsActions.FETCH_SHOWS_BY_GENRES_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
});
