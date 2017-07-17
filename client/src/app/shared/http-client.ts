import { Observable } from 'rxjs/Observable';

export interface FetchResponse {
    status: number;
    body: any;
    headers: any;
    networkError: boolean;
}

export class HttpClient {

    public baseUrl: string = '';

    public defaultHeaders: any = {};

    public get(url: string, headers: any = {}): Observable<FetchResponse> {

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

    private handleError(err: any) {

        return {
            networkError: true,
        };
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
}

export const client = new HttpClient();

export const urlEncodeBody = (obj: any) => Object.entries(obj).map(p => p.join('=')).join('&');

export const urlEncodedHeader = {'Content-Type': 'application/x-www-form-urlencoded'};

export const networkAction = (successType: string, failureType: string) => {

    return (response: any) => ({type: response.networkError ? failureType : successType, response});
};

