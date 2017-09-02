import { Action } from 'redux';
import { Observable } from 'rxjs/Observable';
import { FetchResponse, httpClient, urlEncodeBody, urlEncodedHeader } from '../../infrastructure/http-client';
import { reduxState } from '../../infrastructure/redux-store';
import { ActionsObservable } from 'redux-observable';
import { globalActions } from '../global.state';
import { ReduxEpic } from '../../infrastructure/redux-types';

export interface ApiResponse {
    errorMessages: string[];
    payload?: any;
    success: boolean;
}

const networkIsDownMessage = 'Network error. Please, try again later.';

const errorResponse = (errorMessages: string[]) => ({errorMessages, success: false} as ApiResponse);
const successResponse = (payload: any) => ({payload, errorMessages: [], success: true} as ApiResponse);

export class ApiClient {

    public profile(token: string): Observable<ApiResponse> {

        return httpClient.get('/api/user/profile', {'Authorization': `Bearer ${token}`}).map(this.parseResponse);
    }

    public login(userLogin: any): Observable<ApiResponse> {

        return httpClient.post('/connect/token', urlEncodeBody({...userLogin, grant_type: 'password'}), urlEncodedHeader)
            .map(response => {
                if (response.networkError) {
                    return errorResponse([networkIsDownMessage]);
                } else if (response.body.error) {
                    return errorResponse([response.body.error_description]);
                } else {
                    return successResponse(response.body);
                }
            });
    }

    public topShows(page: number): Observable<FetchResponse> {

        const showsPageSize = reduxState.getState().settings.showsPageSize;

        return httpClient.get(`/api/public/shows/top/${page}/${showsPageSize}`);
    }

    private parseResponse(response: FetchResponse): ApiResponse {

        if (response.networkError) {
            return errorResponse([networkIsDownMessage]);
        }

        return response.body;
    }
}

export const apiClient = new ApiClient();

export type ActionSelector = (response: FetchResponse) => Action;

export const triggerAction = (successActionType: string, failureActionType: string): ActionSelector => {

    return (response => {

        if (response.networkError) {

            return {type: failureActionType, errorMessages: [networkIsDownMessage]};
        }

        if (!response.body.success) {

            return {type: failureActionType, errorMessages: response.body.errorMessages};
        }

        return {type: successActionType, payload: response.body.payload};
    });
};

export const createApiEpic = (startActionType: string, successActionType: string, apiCall: (action: any) => Observable<any>): ReduxEpic => {
    return (actions$: ActionsObservable<any>, store: any) => {
        return actions$
            .ofType(startActionType)
            .switchMap(apiCall)
            .map(triggerAction(successActionType, globalActions.GLOBAL_ERROR));
    };
};
