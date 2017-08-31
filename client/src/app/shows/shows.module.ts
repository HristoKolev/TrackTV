import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { FormsModule } from '@angular/forms';
import { TopShowsComponent } from './shows-component';
import { reduxState } from '../../infrastructure/redux-store';
import { ShowsActions, showsEpics, showsReducer } from './shows-state';
import { apiClient } from '../shared/api-client';

const routes: Routes = [
    {path: '', redirectTo: 'top', pathMatch: 'full'},
    {path: 'top', component: TopShowsComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
    ],
    declarations: [TopShowsComponent],
    providers: [ShowsActions],
})
export class ShowsModule {
    constructor() {

        reduxState.addReducers({
            shows: showsReducer,
        });

        reduxState.addEpics(showsEpics(apiClient));
    }
}

