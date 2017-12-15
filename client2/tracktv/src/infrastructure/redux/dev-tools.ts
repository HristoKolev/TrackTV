import {ApplicationRef, NgZone} from '@angular/core';

export const wrapDevToolsExtension = (devToolsExtension: any, appRef: ApplicationRef, store: any) => {

  return (options?: Object) => {
    let subscription: any;

    // Make sure changes from dev tools update angular's view.
    devToolsExtension.listen(({type}: any) => {

      if (type === 'START') {

        subscription = store.select(s => s)
          .subscribe(() => {
            if (!NgZone.isInAngularZone()) {
              appRef.tick();
            }
          });
      } else if (type === 'STOP') {
        subscription();
      }
    });

    return devToolsExtension(options);
  };
};
