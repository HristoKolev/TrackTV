import {
  ChangeDetectionStrategy,
  Component,
  Directive,
  ElementRef,
  Input,
  NgModule,
  OnChanges,
  OnDestroy,
  OnInit,
  SimpleChanges,
  ViewEncapsulation
} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ApiClient} from './api-client';
import {HttpClient} from './http-client';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {Subscription} from 'rxjs/Subscription';
import {SettingsState} from '../../infrastructure/settings';

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <div *ngIf="this.errorMessages && this.errorMessages.length" class="message-container">
      <div *ngFor="let message of this.errorMessages" class="error-message">{{message}}</div>
    </div>
  `,
  styles: [`
    .error-message {

      padding: 8px;
      width: 80%;
      margin: 1px auto 0;
    }

    .error-message:not(:first-of-type) {
      border-top: 1px solid #f44336;
    }

    .message-container {

      color: #f44336;
      border: 1px solid #f44336;
      margin-top: 15px;
    }
  `],
  selector: 'error-container-component',
})
export class ErrorContainerComponent {

  @Input()
  errorMessages: string[];
}

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'television-component',
  template: `
    <div class="television-container">
      <img src="assets/television-big.png">
      <div class="overlay">
        <ng-content></ng-content>
      </div>
    </div>
  `,
  styles: [`
    .television-container {
      position: relative;
    }

    .television-container * {
      max-width: 100%;

      user-drag: none;
      user-select: none;
      -moz-user-select: none;
      -webkit-user-drag: none;
      -webkit-user-select: none;
      -ms-user-select: none;
    }

    img {
      width: 100%;
    }

    .overlay {
      left: 0;
      position: absolute;
      text-align: center;
      vertical-align: middle;
      top: 30%;
      width: 100%;
      color: white;
      font-family: 'Lobster', sans-serif;
    }

    @media (min-width: 768px) {

      .overlay {
        font-size: 30px;
      }
    }

    @media (max-width: 767px) {
      .overlay {
        font-size: 20px;
      }
    }
  `],
})
export class TelevisionComponent {
}

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'logged-in-component',
  template: `
    <ng-container *ngIf="state | async as session">
      <ng-container *ngIf="session.isLoggedIn">
        <ng-content></ng-content>
      </ng-container>
    </ng-container>
  `,
})
export class LoggedInComponent {

  get state() {
    return this.store.select(s => s.session);
  }

  constructor(private store: ReduxStoreService) {
  }
}

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'logged-out-component',
  template: `
    <ng-container *ngIf="state | async as session">
      <ng-container *ngIf="!session.isLoggedIn">
        <ng-content></ng-content>
      </ng-container>
    </ng-container>
  `,
})
export class LoggedOutComponent {

  get state() {
    return this.store.select(s => s.session);
  }

  constructor(private store: ReduxStoreService) {
  }
}

@Directive({
  selector: '[bannerUrl]'
})
export class BannerUrlDirective implements OnInit, OnDestroy, OnChanges {

  @Input()
  bannerUrl: string;

  subscription: Subscription;

  settings: SettingsState;

  constructor(private elementRef: ElementRef, private store: ReduxStoreService) {
  }

  ngOnInit(): void {
    this.subscription = this.store.select(state => state.settings)
      .subscribe(settings => {
        this.settings = settings;

        this.updateBannerUrl();
      });
  }

  private updateBannerUrl() {
    if (this.settings) {

      let url;

      if (this.bannerUrl) {
        url = `${this.settings.baseUrl}/banners/${this.bannerUrl}`;

      } else {

        url = 'assets/no-poster.png';
      }

      this.elementRef.nativeElement.setAttribute('src', url);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.updateBannerUrl();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}

const components = [
  ErrorContainerComponent,
  TelevisionComponent,
  LoggedInComponent,
  LoggedOutComponent,
  BannerUrlDirective
];

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: components,
  exports: components,
  providers: [
    ApiClient,
    HttpClient
  ],
})
export class SharedModule {
}
