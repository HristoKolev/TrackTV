import {bootstrap} from 'angular2/platform/browser';
import {AppComponent} from './app.component';
import {TypeBinder} from './shared/index';
import {applyBindings} from './typeBindings';
import {HTTP_PROVIDERS} from 'angular2/http';

const typeBinder = new TypeBinder();
applyBindings(typeBinder);

const globalDependencies = [
    HTTP_PROVIDERS,
    ...typeBinder.bindings
];

bootstrap(AppComponent, globalDependencies);