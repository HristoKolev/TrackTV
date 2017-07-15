export interface FetchResponse {
    status: number;
    body: any;
    headers: any;
}

class HttpClient {

    public baseUrl: string = '';

    public defaultHeaders: any = {};

    public get(url: string, headers: any): Promise<FetchResponse> {

        return fetch(this.baseUrl + url, {
            method: 'get',
            headers: {...this.defaultHeaders, ...headers},
        }).then(this.parseResponse);
    }

    public post(url: string, body: any, headers: any): Promise<FetchResponse> {

        return fetch(this.baseUrl + url, {
                method: 'post',
                headers: {...this.defaultHeaders, ...headers},
                body: JSON.stringify(body),
            },
        ).then(this.parseResponse);
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
            }));
    }
}

export const client = new HttpClient();

export const urlEncodeBody = (obj: any) => Object.entries(obj).map(p => p.join('=')).join('&');

