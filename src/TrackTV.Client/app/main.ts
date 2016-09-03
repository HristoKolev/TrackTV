///<reference path="../typings/index.d.ts"/>

import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {AppModule}  from './app.module';

import {configureToastr} from './config/toastr.confing';

configureToastr();

platformBrowserDynamic().bootstrapModule(AppModule);