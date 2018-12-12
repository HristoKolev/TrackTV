import {ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewEncapsulation} from '@angular/core';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {Subscription} from 'rxjs/Subscription';
import {Observable} from 'rxjs/Observable';

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
export class LoadingComponent implements OnInit, OnDestroy {


  loading: boolean;


  subscription: Subscription;

  constructor(private store: ReduxStoreService) {
  }

  public ngOnInit(): void {

    this.loading = !!Math.max(this.store.getState().global.loading, 0);

    this.removeInitialLoader();

    this.subscription = this.store.select(state => state.global)
      .switchMap(global => Observable.of(global).delay(100))
      .map(global => !!Math.max(global.loading, 0))
      .subscribe(loading => {
        this.loading = loading;
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private removeInitialLoader() {
    const loadingElement = (window as any).document
      .getElementById('initial-loader');

    if (loadingElement) {

      loadingElement
        .remove();
    }
  }
}
