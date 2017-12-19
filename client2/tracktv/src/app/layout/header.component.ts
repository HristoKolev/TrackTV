import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {go} from '../../infrastructure/redux/router';
import {globalActions} from '../../infrastructure/redux/redux-global-actions';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';

@Component({
  encapsulation: ViewEncapsulation.Emulated,
  selector: 'header-component',
  template: `
    <header>
      <nav>
        <div class="inner-nav">
          <i id="bars" class="fa fa-bars" aria-hidden="true" (click)="this.toggleNavigationBars()"></i>

          <ul [ngClass]="{'closed': this.navigationClosed}">

            <li class="brand">
              <i class="fa fa-television" aria-hidden="true"></i>
              TrackTv
            </li>

            <li *ngFor="let link of links">
              <a [routerLink]="link.link" *ngIf="link.link" routerLinkActive="active-link"
                 (click)="this.closeNavigation()">{{link.name}}</a>

              <a (click)="link.func(); this.closeNavigation()" *ngIf="link.func" class="func-link">{{link.name}}</a>
            </li>
          </ul>
        </div>
      </nav>
    </header>
  `,
  styles: [`
    nav {
      position: fixed;
      z-index: 1;
      top: 0;
      left: 0;
      width: 100%;
      background-color: #f44336;
    }

    nav ul {
      list-style: none;
      padding: 0;
      width: 75%;
      margin: 0 auto;
    }

    nav ul li {
      color: white;
    }

    nav ul .brand {
      font-family: 'Lobster', sans-serif;
    }

    nav .inner-nav {
      padding: 20px;
    }

    nav a {
      text-decoration: none;
      color: white;
      display: block;
      font-family: 'Roboto', sans-serif;
      font-size: 13px;
      user-select: none;
    }

    i {
      cursor: pointer;
    }

    @media (min-width: 768px) {
      nav li {
        display: inline-block;
        vertical-align: middle;
      }

      nav li a {
        opacity: .75;
        padding: 0 20px;
      }

      nav li .func-link {
        cursor: pointer;
      }

      nav li a:hover, nav li a.active-link {
        opacity: 1;
      }

      #bars {
        display: none;
      }
    }

    @media (max-width: 767px) {
      nav li:not(.brand) {
        padding: 20px;
        vertical-align: middle;
      }

      nav li:not(.brand):not(:last-of-type) {
        border-bottom: 1px solid white;
      }

      nav li:nth-of-type(2) {
        margin-top: 20px;
        border-top: 1px solid white;
      }

      nav .closed li:not(.brand) {
        display: none;
      }

      #bars {
        float: right;
        color: white;
        font-size: 25px;
      }
    }
  `],
})
export class HeaderComponent implements OnInit {

  //noinspection JSMismatchedCollectionQueryUpdate
  links: any[];

  navigationClosed = true;

  constructor(private store: ReduxStoreService) {
  }

  ngOnInit(): void {

    const allLinks = [
      {name: 'Calendar', link: ['/calendar'], role: 'user'},
      {name: 'My Shows', link: ['/my-shows'], role: 'user'},
      {name: 'Shows', link: ['/shows'], role: 'public'},
      {name: 'Login', link: ['/account/login'], role: 'unregistered'},
      {name: 'Logout', func: () => this.logout(), role: 'user'},
      {name: 'Register', link: ['/account/register'], role: 'unregistered'},
    ];

    this.store.select(state => state.session)
      .subscribe(sessionState => {

        this.links = allLinks.filter(link => {
          if (link.role === 'public') {
            return true;
          }

          if (link.role === 'user') {
            return sessionState.isLoggedIn;
          }

          if (link.role === 'unregistered') {
            return !sessionState.isLoggedIn;
          }

          throw new Error(`Invalid role: ${link.role}`);
        });
      });
  }

  logout() {
    this.store.dispatch({
      type: globalActions.LOGOUT_USER,
    });

    go(['/shows']);
  }

  toggleNavigationBars() {
    this.navigationClosed = !this.navigationClosed;
  }

  closeNavigation() {
    this.navigationClosed = true;
  }
}
