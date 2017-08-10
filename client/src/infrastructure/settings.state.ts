import { addReducers } from './redux-store';

export interface ISettingsState {
    baseUrl: string;
}

const initialState: ISettingsState = {
    baseUrl: 'http://localhost:5000',
};

export const settingsReducer = (state: ISettingsState = initialState, action: any): ISettingsState => {
    switch (action.type) {
        default: {
            return state;
        }
    }
};

addReducers({
    settings: settingsReducer,
});
