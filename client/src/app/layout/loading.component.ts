import {ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';

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

  constructor(private store: ReduxStoreService) {
  }

  public ngOnInit(): void {

    this.store.select(state => state.global)
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

      const loadingElement = (window as any).document
        .getElementById('initial-loader');

      if (loadingElement) {

        loadingElement
          .remove();
      }
    }, 0);
  }
}
