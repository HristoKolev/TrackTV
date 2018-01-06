import {ReduxReducer} from '../infrastructure/redux/meta';
import {getPersistedState} from '../infrastructure/redux/redux-persist-service';
import {globalActions} from '../infrastructure/redux/redux-global-actions';

export interface ISettingsState {
  baseUrl: string;
  showsPageSize: number;
}

const initialSettingsState: ISettingsState = {
  baseUrl: 'https://api-tracktv.hristo.tech',
  showsPageSize: 10,
};

export const settingsReducer: ReduxReducer<ISettingsState> = (state = initialSettingsState, action) => {
  switch (action.type) {
    default: {
      return state;
    }
  }
};

export interface IGlobalState {
  errorMessages: string[];
  loading: number;
}

const initialGlobalErrorState: IGlobalState = {
  errorMessages: [],
  loading: 0,
};

export const globalErrorReducer: ReduxReducer<IGlobalState> = (state = initialGlobalErrorState, action) => {
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

const persistedState = getPersistedState();

const initialSessionState: ISessionState = persistedState.session || {

  isLoggedIn: false,
};

export const userSessionReducer: ReduxReducer<ISessionState> = (state = initialSessionState, action: any) => {

  switch (action.type) {

    case globalActions.LOGIN_USER: {

      return {
        ...state,
        access_token: action.responses.loginResponse.payload.access_token,
        isLoggedIn: true,
        user: action.responses.profileResponse.payload,
      };
    }
    case globalActions.LOGOUT_USER: {
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

