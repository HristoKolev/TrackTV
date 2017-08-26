import { actionTypes, ReduxReducer } from '../../infrastructure/redux-types';

export interface ShowListItem {
    banner: string;
    imdbId: string;
    name: string;
    status: number;
    subscriberCount: number;
    id: number;
}

export interface IShowsState {
    topShows: ITopShowsState;
}

export interface ITopShowsState {
    totalCount: number;
    shows: ShowListItem[];
}

const initialState = {
    topShows: {
        totalCount: 0,
        shows: [],
    },
};

export const showsActions = actionTypes('shows').ofType<{
    TOP_SHOWS_REQUEST_START: string;
    TOP_SHOWS_REQUEST_SUCCESS: string;
    TOP_SHOWS_REQUEST_FAILED: string;
}>();

export const showsReducer: ReduxReducer<IShowsState> = (state = initialState, action: any) => {
    switch (action.type) {

        case showsActions.TOP_SHOWS_REQUEST_SUCCESS: {
            return {
                ...state,
                topShows: action.payload,
            };
        }

        default: {
            return state;
        }
    }
};
