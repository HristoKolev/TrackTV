import { ApiClient, triggerAction } from '../shared/api-client';
import { globalActions } from '../global.state';
import { put } from 'redux-saga/effects';
import { actionTypes } from '../../infrastructure/redux-store';

export const myShowsActions = actionTypes('MY_SHOWS').ofType<{
    FETCH_REQUEST_START: string;
    FETCH_REQUEST_SUCCESS: string;
    SUBSCRIBE_REQUEST_START: string;
    SUBSCRIBE_REQUEST_SUCCESS: string;
    UNSUBSCRIBE_REQUEST_START: string;
    UNSUBSCRIBE_REQUEST_SUCCESS: string;
}>();


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
