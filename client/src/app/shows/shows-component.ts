import { Component, OnInit } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { smartComponent } from '../../infrastructure/component-helpers';
import { IShowsState, ITopShowsState } from './shows-state';

@Component({
    ...smartComponent,
    template: `
        <div>{{this.data | json}}</div>
    `,
    styles: [``],
})
export class TopShowsComponent implements OnInit {

    data: ITopShowsState;

    constructor(private ngRedux: NgRedux<{ shows: IShowsState }>) {
    }

    ngOnInit(): void {

        this.ngRedux.select(state => state.shows.topShows)
            .distinctUntilChanged()
            .subscribe(topShows => this.data = topShows);
    }
}
