import {bootstrap} from 'angular2/platform/browser';
import {HTTP_PROVIDERS} from 'angular2/http';
import {ROUTER_PROVIDERS} from 'angular2/router';

import {AppComponent} from './app.component';
import {TypeBinder} from './shared/index';
import {applyBindings} from './app.bindings';

import {configureToastr} from  './config/toastr.confing';

configureToastr();

const typeBinder = new TypeBinder();
applyBindings(typeBinder);

const globalDependencies = [
    HTTP_PROVIDERS,
    ROUTER_PROVIDERS,
    ...typeBinder.bindings
];

bootstrap(AppComponent, globalDependencies);