import { actionTypes, ReduxReducer } from '../infrastructure/redux-types';

export interface ISettingsState {
    baseUrl: string;
    showsPageSize: number;
}

const initialSettingsState: ISettingsState = {
    baseUrl: 'http://192.168.1.103:7000',
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
    loading: number;
}

const initialGlobalErrorState: IGlobalErrorState = {
    errorMessages: [],
    loading: 0,
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
                loading: state.loading + 1,
            };
        }
        case globalActions.END_TRANSITION: {
            return {
                ...state,
                loading: state.loading - 1,
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
