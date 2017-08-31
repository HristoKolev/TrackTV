import { actionTypes, ReduxReducer } from '../infrastructure/redux-types';

export interface ISettingsState {
    baseUrl: string;
    showsPageSize: number;
}

const initialSettingsState: ISettingsState = {
    baseUrl: 'http://localhost:5000',
    showsPageSize: 10,
};

export const settingsReducer: ReduxReducer<ISettingsState> = (state = initialSettingsState, action) => {
    switch (action.type) {
        default: {
            return state;
        }
    }
};

export interface IGlobalErrorState {
    errorMessages: string[];
}

const initialGlobalErrorState: IGlobalErrorState = {
    errorMessages: [],
};

export const globalActions = actionTypes('global').ofType<{
    GLOBAL_ERROR: string;
    LOGOUT: string;
}>();

export const globalErrorReducer: ReduxReducer<IGlobalErrorState> = (state = initialGlobalErrorState, action) => {
    switch (action.type) {
        case globalActions.GLOBAL_ERROR: {
            return {
                ...state,
                errorMessages: action.errorMessages,
            };
        }
        case  globalActions.LOGOUT: {
            return {
                ...state,
                session: {},
            };
        }
        default: {
            return state;
        }
    }
};
