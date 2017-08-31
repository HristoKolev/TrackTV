import { actionTypes, ReduxEpicMap, ReduxReducer } from '../../infrastructure/redux-types';
import { ApiClient, createApiEpic } from '../shared/api-client';
import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';

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

export const showsEpics = (apiClient: ApiClient): ReduxEpicMap => {

    const topShowsRequestEpic = createApiEpic(
        showsActions.TOP_SHOWS_REQUEST_START,
        showsActions.TOP_SHOWS_REQUEST_SUCCESS,
        action => apiClient.topShows(action.page),
    );

    return {
        topShowsRequestEpic,
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
