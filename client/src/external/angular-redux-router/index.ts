import { NgModule } from '@angular/core';
import { NgReduxRouter } from './router';
import { RouterAction, routerReducer } from './reducer';
import { UPDATE_ROUTE } from './actions';

@NgModule({
  providers: [ NgReduxRouter ]
})
export class NgReduxRouterModule { }

export {
  NgReduxRouter,
  RouterAction,
  routerReducer,
  UPDATE_ROUTE
};
