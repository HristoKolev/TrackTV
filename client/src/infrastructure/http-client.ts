import { Observable } from 'rxjs/Observable';
import { getStore } from './redux-store';
import { ISettingsState } from './settings.state';

export interface FetchResponse {
    status: number;
    body: any;
    headers: any;
    networkError: boolean;
}

export class HttpClient {

    public get baseUrl(): string {

        const settings = getStore().getState().settings as ISettingsState;

        return settings.baseUrl;
    }

    public get (url: string, headers: any = {}): Observable<FetchResponse> {

        return Observable.fromPromise(fetch(this.baseUrl + url, {
            method: 'get',
            headers: {...this.defaultHeaders, ...headers},
        }).then(this.parseResponse, this.handleError));
    }

    public post(url: string, body: any, headers: any): Observable<FetchResponse> {

        return Observable.fromPromise(fetch(this.baseUrl + url, {
                method: 'post',
                headers: {...this.defaultHeaders, ...headers},
                body,
            },
        ).then(this.parseResponse, this.handleError));
    }

    private handleError(error: any) {
        return {networkError: true};
    }

    private parseResponse(res: any): any {

        return res.json()
            .then((body: any) => ({
                status: res.status,
                body,
                headers: Array.from(res.headers.entries())
                    .reduce((acc: any, x: any) => {
                        acc[x[0]] = x[1];
                        return acc;
                    }, {}),
                networkError: false,
            }));
    }

    private get defaultHeaders(): any {

        const headers: any = {};

        const state = getStore().getState();

        let token;

        if (state.account && state.account.session) {

            token = state.account.session.access_token;
        }

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        return headers;
    }
}

export const httpClient = new HttpClient();

export const urlEncodeBody = (obj: any) => Object.entries(obj).map(p => p.join('=')).join('&');

export const urlEncodedHeader = {'Content-Type': 'application/x-www-form-urlencoded'};
