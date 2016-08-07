import {bootstrap} from '@angular/platform-browser-dynamic';
import {HTTP_PROVIDERS} from '@angular/http';

import {AppComponent} from './app.component';
import {typeBindings} from './app.bindings';
import {routerProviders} from  './app.routes';

import {configureToastr} from  './config/toastr.confing';

configureToastr();

bootstrap(AppComponent, [
    HTTP_PROVIDERS,

    routerProviders,
    typeBindings,
]);