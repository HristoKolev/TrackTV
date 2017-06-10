import { Injectable } from '@angular/core';

@Injectable()
export class ApiPath {

    private readonly baseServiceUrl: string = 'http://192.168.1.103:7000';

    public get loginPath(): string {

        return this.combine(this.baseServiceUrl, '/connect/token');
    }

    public service(name: string): (...args: string[]) => string {

        return (...args) => this.combine(this.baseServiceUrl, 'api', name, ...args);
    }

    private combine(...args: string[]): string {

        const paths = [];

        for (let i = 0; i < args.length; i += 1) {

            let arg = args[i].replace(/\\/g, '/').replace(/^\/*/g, '');

            if (i < args.length - 1) {

                arg = arg.replace(/\/*$/g, '');
            }

            paths.push(arg);
        }

        return paths.join('/');
    }
}
