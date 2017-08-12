import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';

const emptyEpic = (actions$: any, store: any) => actions$.filter(() => false);

const epics$ = new BehaviorSubject<any>(emptyEpic);

export const rootEpic = (action$: any, store: any): Observable<any> => epics$.mergeMap(epic => epic(action$, store));

export const addEpics = (epics: any): void => {

    for (let [epicName, epic] of Object.entries(epics)) {

        console.log('Adding epic: ', epicName);

        epics$.next((...args: any[]) => epic(...args)
            .map((action: any) => ({...action, dispatchedBy: epicName}))
            .catch(console.error.bind(console)));
    }
};
