import {NgModule} from '@angular/core';
import {ReduxPersistService} from './persist';
import {ReduxRouterService} from './router';

@NgModule({
  providers: [ReduxRouterService, ReduxPersistService],
})
export class ReduxHelperModule {
}
