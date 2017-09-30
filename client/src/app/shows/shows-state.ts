import { actionTypes, ReduxReducer } from '../../infrastructure/redux-types';
import { ApiClient, triggerAction } from '../shared/api-client';
import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { put } from 'redux-saga/effects';
import { globalActions } from '../global.state';

export const showsActions = actionTypes('shows').ofType<{
    TOP_SHOWS_REQUEST_START: string;
    TOP_SHOWS_REQUEST_SUCCESS: string;
}>();

const initialState = {
    topShows: {
        totalCount: 0,
        items: [],
        pageSize: 10,
        currentPage: 1,
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
        default: {
            return state;
        }
    }
};

export const showsSagas = (apiClient: ApiClient) => {

    return {
        topShowsRequestSaga: {
            type: showsActions.TOP_SHOWS_REQUEST_START,
            inTransition: true,
            saga: function* (action: any) {

                const response = yield apiClient.topShows(action.page);

                yield put(triggerAction(showsActions.TOP_SHOWS_REQUEST_SUCCESS, globalActions.GLOBAL_ERROR, response));
            },
        },
    };
};

@Injectable()
export class ShowsActions {
    constructor(private ngRedux: NgRedux<any>) {
    }

    fetchTopShows(page: number) {
        this.ngRedux.dispatch({
            type: showsActions.TOP_SHOWS_REQUEST_START,
            page,
        });
    }
}
