import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';

const emptyEpic = (actions$: any, store: any) => actions$.filter(() => false);

const epics$ = new BehaviorSubject<any>(emptyEpic);

export const rootEpic = (action$: any, store: any): Observable<any> => epics$.mergeMap(epic => epic(action$, store)
    .catch(console.error.bind(console)));

export const addEpics = (epics: any[]): void => {

    for (let epic of epics) {
        epics$.next(epic);
    }
};
