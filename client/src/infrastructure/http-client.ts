import { ISettingsState } from '../app/global.state';
import { reduxState } from './redux-store';

export interface FetchResponse {
    status: number;
    body: any;
    headers: any;
    networkError: boolean;
}

class HttpClient {

    public get baseUrl(): string {

        const settings = reduxState.getState().settings as ISettingsState;

        return settings.baseUrl;
    }

    public get(url: string, headers: any = {}): Promise<FetchResponse> {

        return fetch(this.baseUrl + url, {
            method: 'get',
            headers: {...this.defaultHeaders, ...headers},
        }).then(this.parseResponse, this.handleError);
    }

    public post(url: string, body: any, headers?: any): Promise<FetchResponse> {

        headers = headers || {};

        return fetch(this.baseUrl + url, {
                method: 'post',
                headers: {...this.defaultHeaders, ...headers},
                body,
            },
        ).then(this.parseResponse, this.handleError);
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
            }), (err: any) => ({
                status: res.status,
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

        const state = reduxState.getState();

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

const randomString = (length: number) => {

    let text = '';

    //noinspection SpellCheckingInspection
    const possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

    for (let i = 0; i < length; i++) {
        text += possible.charAt(Math.floor(Math.random() * possible.length));
    }

    return text;
};

export const multipartForm = (jsonBody: any) => {

    const boundary = '------WebKitFormBoundary' + randomString(16);

    let body = boundary + '\r\n';

    body += Object.entries(jsonBody)
        .map(([key, val]) => `Content-Disposition: form-data; name="${key}"\r\n\r\n${val}`)
        .join('\r\n' + boundary + '\r\n');

    body += '\r\n' + boundary + '--\r\n';

    const headers = {
        'Content-Type': `multipart/form-data; boundary=${boundary.substring(2)}`,
        'Content-Length': body.length,
    };

    return {
        body,
        headers,
    };
};

