import './polyfills.browser';
import './rxjs.imports';

import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';

if (ENV === 'production') {
    enableProdMode();
}

export function main() {
    return platformBrowserDynamic().bootstrapModule(AppModule)
        .catch(console.log.bind(console));
}

export function bootstrapDomReady() {
    document.addEventListener('DOMContentLoaded', main);
}

bootstrapDomReady();
