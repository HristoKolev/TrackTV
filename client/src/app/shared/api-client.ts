import { Action } from 'redux';
import { Observable } from 'rxjs/Observable';
import { FetchResponse, httpClient } from '../../infrastructure/http-client';

class ApiClient {

    public profile(): Observable<FetchResponse> {
        return httpClient.get('/api/user/profile');
    }
}

export const apiClient = new ApiClient();

export type ActionSelector = (response: FetchResponse) => Action;

export const triggerAction = (successActionType: string, failureActionType: string): ActionSelector => {

    return (response => {

        if (response.networkError) {

            return {type: failureActionType, errorMessages: ['Server is down. Please, try again later.']};
        }

        if (!response.body.success) {

            return {type: failureActionType, errorMessages: response.body.errorMessages};
        }

        return {type: successActionType, data: response.body.payload};
    });
};
