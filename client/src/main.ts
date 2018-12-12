import {enableProdMode, NgModuleRef} from '@angular/core';
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';

import './rxjs.imports';

import {AppModule} from './app/app.module';
import {environment} from './environments/environment';
import {hmrBootstrap} from './hmr';

if (environment.production) {
  enableProdMode();
}

const bootstrap = () => platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.log(err));

if (environment.hmr) {
  if (module['hot']) {
    hmrBootstrap(module, bootstrap as () => Promise<NgModuleRef<any>>);
  } else {
    console.error('HMR is not enabled for webpack-dev-server!');
    console.log('Are you using the --hmr flag for ng serve?');
  }
} else {
  bootstrap();
}
