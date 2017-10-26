import { showsActions, showsReducer } from './shows-state';
import { testReducer, testReducerWithAction } from '../../infrastructure/test-helpers';

describe('showsReducer', () => {

    testReducer(showsReducer);

    describe('Action: ' + showsActions.FETCH_TOP_SHOWS_REQUEST_SUCCESS, () => {

        const payload = {key: '__VALUE__'};

        const defaultAction = {
            type: showsActions.FETCH_TOP_SHOWS_REQUEST_SUCCESS,
            payload,
        };

        testReducerWithAction(showsReducer, defaultAction);

        it('adds the payload to the current state', () => {

            const prevState = {};

            const newState = showsReducer(prevState, defaultAction);

            expect(newState).toEqual({
                topShows: payload,
            });
        });
    });

});
