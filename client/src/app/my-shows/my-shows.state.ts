import { ApiClient, triggerAction } from '../shared/api-client';
import { actionTypes } from '../../infrastructure/redux-types';
import { globalActions } from '../global.state';
import { put } from 'redux-saga/effects';

export const myShowsActions = actionTypes('myShows').ofType<{
    MY_SHOWS_REQUEST_START: string;
    MY_SHOWS_REQUEST_SUCCESS: string;
    MY_SHOWS_SUBSCRIBE_START: string;
    MY_SHOWS_SUBSCRIBE_SUCCESS: string;
    MY_SHOWS_UNSUBSCRIBE_START: string;
    MY_SHOWS_UNSUBSCRIBE_SUCCESS: string;
}>();

const initialState = {
    shows: [],
};

export const myShowsReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case myShowsActions.MY_SHOWS_REQUEST_SUCCESS: {
            return {
                ...state,
                shows: action.payload.map((show: any) => ({...show, isSubscribed: true})),
            };
        }
        case myShowsActions.MY_SHOWS_SUBSCRIBE_SUCCESS: {
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
        case myShowsActions.MY_SHOWS_UNSUBSCRIBE_SUCCESS: {
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
        type: myShowsActions.MY_SHOWS_REQUEST_START,
        inTransition: true,
        saga: function* () {

            const response = yield apiClient.myShows();

            yield put(triggerAction(myShowsActions.MY_SHOWS_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
        },
    },
    myShowsSubscribe: {
        type: myShowsActions.MY_SHOWS_SUBSCRIBE_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.subscribe(action.showId);

            yield put(triggerAction(
                myShowsActions.MY_SHOWS_SUBSCRIBE_SUCCESS,
                globalActions.GLOBAL_ERROR,
                response,
                {showId: action.showId},
            ));
        },
    },
    myShowsUnsubscribe: {
        type: myShowsActions.MY_SHOWS_UNSUBSCRIBE_START,
        inTransition: true,
        saga: function* (action: any) {

            const response = yield apiClient.unsubscribe(action.showId);

            yield put(triggerAction(
                myShowsActions.MY_SHOWS_UNSUBSCRIBE_SUCCESS,
                globalActions.GLOBAL_ERROR,
                response,
                {showId: action.showId},
            ));
        },
    },

});
