import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgRedux } from '@angular-redux/store';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    selector: 'loading-component',
    template: `
        <div [ngClass]="{'loaded': !this.global.loading}">
            <div id="loader-wrapper">
                <div id="loader"></div>
                <div class="loader-section"></div>
            </div>
        </div>
    `,
    styleUrls: ['loader.css'],
})
export class LoadingComponent implements OnInit {

    global: any;

    constructor(private ngRedux: NgRedux<any>) {
    }

    public ngOnInit(): void {

        this.ngRedux.select(state => state.global)
            .distinctUntilChanged()
            .subscribe(global => {
                this.global = global;
            });
    }
}
