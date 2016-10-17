import {Injectable} from '@angular/core';

@Injectable()
export class ApiPath {

    private readonly baseServiceUrl : string = 'http://localhost:5050';

    private readonly baseApiPath : string = this.baseServiceUrl + '/api';

    public get loginPath() : string {

        return this.baseServiceUrl + '/token';
    }

    public rawPath(name : string) : string {

        return this.baseApiPath + name;
    }

    public service(serviceName : string) : (path : string) => string {

        return (path : string) => this.rawPath(serviceName + path);
    }

    public path(name : string = '') : string {

        return this.baseServiceUrl + name;
    }
}