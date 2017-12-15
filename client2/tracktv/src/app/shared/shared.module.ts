import {ChangeDetectionStrategy, Component, Input, NgModule, ViewEncapsulation} from '@angular/core';
import {CommonModule} from '@angular/common';

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

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [ErrorContainerComponent, TelevisionComponent],
  providers: [],
  exports: [ErrorContainerComponent, TelevisionComponent],
})
export class SharedModule {
}
