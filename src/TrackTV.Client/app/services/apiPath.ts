import {Injectable} from 'angular2/core';

@Injectable()
export class ApiPath {

    private baseServiceUrl : string = 'http://localhost:5050';

    private baseApiPath : string = this.baseServiceUrl + '/api';

    public get loginPath() : string {

        return this.baseServiceUrl + '/token';
    }

    public rawPath(name) : string {

        return this.baseApiPath + name;
    }

    public service(serviceName) {

        return path => this.rawPath('/' + serviceName + path);
    }

    public path(name) : string {

        return this.baseServiceUrl + (name || '');
    }
}