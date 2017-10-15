import { NgRedux } from '@angular-redux/store';
import { ChangeDetectionStrategy, Component, Injectable, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { reduxState } from '../../infrastructure/redux-store';
import { myShowsActions, myShowsReducer, myShowsSagas } from './my-shows.state';
import { apiClient } from '../shared/api-client';

@Injectable()
export class MyShowsActions {
    constructor(private ngRedux: NgRedux<any>) {
    }

    myShows() {
        this.ngRedux.dispatch({
            type: myShowsActions.MY_SHOWS_REQUEST_START,
        });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <pre>{{this.myShows | json}}</pre>
    `,
})
export class MyShowsComponent implements OnInit {

    myShows: any;

    constructor(private ngRedux: NgRedux<any>,
                private myShowsActions: MyShowsActions) {
    }

    ngOnInit(): void {

        this.ngRedux.select(state => state.myShows)
            .distinctUntilChanged()
            .subscribe(myShows => {
                this.myShows = myShows;
            });

        this.myShowsActions.myShows();
    }
}

const routes: Routes = [
    {path: '', component: MyShowsComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
    ],
    declarations: [MyShowsComponent],
    providers: [MyShowsActions],
})
export class MyShowsModule {
    constructor() {

        reduxState.addReducers({
            myShows: myShowsReducer,
        });

        reduxState.addSagas(myShowsSagas(apiClient));
    }
}
