import {ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {reduxStore} from '../../infrastructure/redux-store';

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.Default,
  selector: 'loading-component',
  template: `
    <div [ngClass]="{'loaded': !this.loading}">
      <div id="loader-wrapper">
        <div id="loader"></div>
        <div class="loader-section"></div>
      </div>
    </div>
  `,
})
export class LoadingComponent implements OnInit {

  loading: boolean;

  firstLoad = true;

  public ngOnInit(): void {

    reduxStore.select(state => state.global)
      .distinctUntilChanged()
      .switchMap(global => Observable.of(global).delay((global.loading > 0 || this.firstLoad) ? 100 : 0))
      .subscribe(global => {
        this.loading = !!Math.max(global.loading, 0);

        if (this.firstLoad) {

          this.firstLoad = false;

          this.removeInitialLoader();
        }
      });
  }

  private removeInitialLoader() {
    setTimeout(() => {

      (window as any).document
        .getElementById('initial-loader')
        .remove();
    }, 0);
  }
}
