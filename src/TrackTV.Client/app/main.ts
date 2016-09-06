///<reference path="../typings/index.d.ts"/>

import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {AppModule}  from './app.module';

platformBrowserDynamic().bootstrapModule(AppModule);

import {configureToastr} from './_config/toastr.config';
configureToastr();