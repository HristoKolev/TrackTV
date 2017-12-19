import {ErrorHandler, Injectable} from '@angular/core';
import {createMishapClient} from './mishap';
import {ReduxStoreService} from './redux/redux-store-service';

const mishapClient = createMishapClient({apyKey: '4e921b7e-39a9-4699-8a32-d3cf2e2d6f6f'});

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

  constructor(private store: ReduxStoreService) {
  }

  handleError(error) {

    mishapClient.log({
      recordTitle: error.rejection.message,
      recordDescription: error.rejection.stack,
      recordExtendedDescription: error.stack,
      recordContext: JSON.stringify({
        reduxState: this.store.getState(),
      }),
      recordType: 'Error',
    });

    throw error;
  }
}
