import { actionTypes, ReduxReducer } from '../infrastructure/redux-types';

export interface ISettingsState {
    baseUrl: string;
}

const initialSettingsState: ISettingsState = {
    baseUrl: 'http://localhost:5000',
};

export const settingsReducer = (state: ISettingsState = initialSettingsState, action: any): ISettingsState => {
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

const globalActions = actionTypes('global').ofType<{
    GLOBAL_ERROR: string;
}>();

export const globalErrorReducer: ReduxReducer<IGlobalErrorState> = (state = initialGlobalErrorState, action) => {
    switch (action.type) {
        case globalActions.GLOBAL_ERROR: {
            return {
                ...state,
                errorMessages: action.errorMessages,
            };
        }
        default: {
            return state;
        }
    }
};
