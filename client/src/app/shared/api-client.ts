import { FetchResponse, httpClient, multipartForm, urlEncodeBody, urlEncodedHeader } from '../../infrastructure/http-client';
import { reduxState } from '../../infrastructure/redux-store';
import { Promise } from 'es6-promise';

export interface ApiResponse {
    errorMessages: string[];
    payload?: any;
    success: boolean;
}

const networkIsDownMessage = 'Network error. Please, try again later.';
const serverErrorMessage = 'Server error. Please, try again later.';

const errorResponse = (errorMessages: string[]) => ({errorMessages, success: false} as ApiResponse);
const successResponse = (payload: any) => ({payload, errorMessages: [], success: true} as ApiResponse);

export class ApiClient {

    public profile(token: string): Promise<ApiResponse> {

        return httpClient.get('/api/user/profile', {'Authorization': `Bearer ${token}`})
            .then(this.parseResponse);
    }

    public login(userLogin: any): Promise<ApiResponse> {

        return httpClient.post('/connect/token', urlEncodeBody({...userLogin, grant_type: 'password'}), urlEncodedHeader)
            .then(response => {
                if (response.networkError) {
                    return errorResponse([networkIsDownMessage]);
                } else if (response.body.error) {
                    return errorResponse([response.body.error_description]);
                } else {
                    return successResponse(response.body);
                }
            });
    }

    public register(user: any): Promise<ApiResponse> {

        const form = multipartForm(user);

        return httpClient.post('/api/public/auth/register', form.body, form.headers)
            .then(this.parseResponse);
    }

    public topShows(page: number): Promise<ApiResponse> {

        return httpClient.get(`/api/public/shows/top/${page}/${this.showsPageSize}`)
            .then(this.parseResponse);
    }

    public searchShows(query: string, page: number): Promise<ApiResponse> {

        return httpClient.get(`/api/public/shows/search/${query}/${page}/${this.showsPageSize}`)
            .then(this.parseResponse);
    }

    public getGenres(): Promise<ApiResponse> {

        return httpClient.get(`/api/public/genres`)
            .then(this.parseResponse);
    }

    public showsByGenre(genreId: number, page: number): Promise<ApiResponse> {

        return httpClient.get(`/api/public/shows/genre/${genreId}/${page}/${this.showsPageSize}`)
            .then(this.parseResponse);
    }

    public show(showId: number): Promise<ApiResponse> {
        return httpClient.get(`/api/public/show/${showId}`)
            .then(this.parseResponse);
    }

    private get showsPageSize() {
        return reduxState.getState().settings.showsPageSize;
    }

    private parseResponse(response: FetchResponse): ApiResponse {

        if (response.networkError) {
            return errorResponse([networkIsDownMessage]);
        }

        if (!response.body) {
            return errorResponse([serverErrorMessage]);
        }

        return response.body;
    }

}

export const apiClient = new ApiClient();

export const triggerAction = (successActionType: string, failureActionType: string, response: ApiResponse): any => {

    if (response.success) {

        return {type: successActionType, payload: response.payload};
    }

    return {type: failureActionType, errorMessages: response.errorMessages};
};
