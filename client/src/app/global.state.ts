import { actionTypes, ReduxReducer } from '../infrastructure/redux-types';
import { reduxState } from '../infrastructure/redux-store';

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
    loading: boolean;
}

const initialGlobalErrorState: IGlobalErrorState = {
    errorMessages: [],
    loading: false,
};

export const globalActions = actionTypes('global').ofType<{
    GLOBAL_ERROR: string;
    USER_LOGIN: string;
    USER_LOGOUT: string;
    START_TRANSITION: string;
    END_TRANSITION: string;
}>();

export const globalErrorReducer: ReduxReducer<IGlobalErrorState> = (state = initialGlobalErrorState, action) => {
    switch (action.type) {
        case globalActions.GLOBAL_ERROR: {
            return {
                ...state,
                errorMessages: action.errorMessages,
            };
        }
        case globalActions.START_TRANSITION: {
            return {
                ...state,
                loading: true,
            };
        }
        case globalActions.END_TRANSITION: {
            return {
                ...state,
                loading: false,
            };
        }
        default: {
            return state;
        }
    }
};

export interface ISessionState {

    isLoggedIn: boolean;
    user?: any;
    access_token?: string;
}

const initialSessionState: ISessionState = {

    isLoggedIn: false,
};

export const userSessionReducer: ReduxReducer<ISessionState> = (state = initialSessionState, action: any) => {

    switch (action.type) {

        case globalActions.USER_LOGIN: {

            return {
                ...state,
                access_token: action.responses.loginResponse.payload.access_token,
                isLoggedIn: true,
                user: action.responses.profileResponse.payload,
            };
        }
        case  globalActions.USER_LOGOUT: {
            return {
                ...state,
                access_token: undefined,
                user: undefined,
                isLoggedIn: false,
            };
        }
        default: {
            return state;
        }
    }
};

export const loadingStart = (action: any) => {

    reduxState.dispatch({type: globalActions.START_TRANSITION, triggeredBy: action.type});

    return action;
};

export const loadingStop = (action: any) => {
    if (Array.isArray(action)) {

        return [...action, {type: globalActions.END_TRANSITION}];
    } else {

        return [action, {type: globalActions.END_TRANSITION}];
    }
};
