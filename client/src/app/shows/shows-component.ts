import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { ShowsActions } from './shows-state';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <pre>{{this.data | json}}</pre>
    `,
    styles: [`
    `],
})
export class TopShowsComponent implements OnInit {

    data: any;

    constructor(private ngRedux: NgRedux<any>,
                private showsActions: ShowsActions) {
    }

    ngOnInit(): void {

        this.showsActions.fetchTopShows(1);

        this.ngRedux.select(state => state.shows.topShows)
            .distinctUntilChanged()
            .subscribe(topShows => this.data = topShows);
    }
}
