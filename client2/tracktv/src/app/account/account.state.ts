import {ApiClient} from '../shared/api-client';
import {put} from 'redux-saga/effects';
import {globalActions} from '../global.state';
import {routerActions} from '../../infrastructure/redux/router';
import {actionTypes, ReduxReducer} from '../../infrastructure/redux/meta';

export interface ILoginState {

  errorMessages?: string[];
}

export interface IRegisterState {

  errorMessages?: string[];
}

export const accountActions = actionTypes('account').ofType<{
  LOGIN_REQUEST_START: string;
  LOGIN_REQUEST_SUCCESS: string;
  LOGIN_REQUEST_FAILED: string;
  LOGIN_CLEAR_ERROR_MESSAGES: string;

  REGISTER_REQUEST_START: string;
  REGISTER_REQUEST_SUCCESS: string;
  REGISTER_REQUEST_FAILED: string;
  REGISTER_CLEAR_ERROR_MESSAGES: string;
}>();

const initialLoginState = {
  errorMessages: [],
};

export const loginReducer: ReduxReducer<ILoginState> = (state = initialLoginState, action: any) => {

  switch (action.type) {

    case accountActions.LOGIN_CLEAR_ERROR_MESSAGES:
    case accountActions.LOGIN_REQUEST_START:
    case accountActions.LOGIN_REQUEST_SUCCESS: {

      return {
        ...state,
        errorMessages: [],
      };
    }
    case accountActions.LOGIN_REQUEST_FAILED: {

      let errorMessages: string[] = [];

      if (!action.responses.loginResponse.success) {

        errorMessages = action.responses.loginResponse.errorMessages;
      } else if (!action.responses.profileResponse.success) {
        errorMessages = action.responses.profileResponse.errorMessages;
      }

      return {
        ...state,
        errorMessages,
      };
    }
    default: {
      return state;
    }
  }
};

const initialRegisterState = {
  errorMessages: [],
};

export const registerReducer: ReduxReducer<IRegisterState> = (state = initialRegisterState, action: any) => {

  switch (action.type) {

    case accountActions.REGISTER_CLEAR_ERROR_MESSAGES:
    case accountActions.REGISTER_REQUEST_START:
    case accountActions.REGISTER_REQUEST_SUCCESS: {
      return {
        ...state,
        errorMessages: [],
      };
    }
    case accountActions.REGISTER_REQUEST_FAILED: {

      return {
        ...state,
        errorMessages: action.response.errorMessages,
      };
    }

    default: {
      return state;
    }
  }
};

export const accountSagas = (apiClient: ApiClient) => ({
  loginSaga: {
    type: accountActions.LOGIN_REQUEST_START,
    saga: function* (action: any) {

      yield  put({type: globalActions.START_TRANSITION});

      const loginResponse = yield apiClient.login(action.user);

      if (!loginResponse.success) {

        yield put({type: accountActions.LOGIN_REQUEST_FAILED, responses: {loginResponse}});
        yield put({type: globalActions.END_TRANSITION});
        return;
      }

      const profileResponse = yield apiClient.profile(loginResponse.payload.access_token);

      const responses = {loginResponse, profileResponse};

      if (!profileResponse.success) {

        yield put({type: accountActions.LOGIN_REQUEST_FAILED, responses});

      } else {

        yield put({type: accountActions.LOGIN_REQUEST_SUCCESS, responses});
        yield put({type: globalActions.LOGIN_USER, responses});
        yield put({type: routerActions.ROUTER_NAVIGATION_EXPLICIT, payload: [['/shows']]});
      }

      yield put({type: globalActions.END_TRANSITION});
    },
  },
  registerSaga: {
    type: accountActions.REGISTER_REQUEST_START,
    saga: function* (action: any) {

      yield put({type: globalActions.START_TRANSITION});

      const response = yield apiClient.register(action.user);

      if (!response.success) {

        yield put({type: accountActions.REGISTER_REQUEST_FAILED, response});
        yield put({type: globalActions.END_TRANSITION});
        return;
      }

      yield put({type: accountActions.REGISTER_REQUEST_SUCCESS, response});

      yield put({type: accountActions.LOGIN_REQUEST_START, user: action.user});
      yield put({type: globalActions.END_TRANSITION});
    },
  },
});

