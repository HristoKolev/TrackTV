import {HeaderComponent} from  './layout/header/header.component';
import {FooterComponent} from  './layout/footer/footer.component';

import {LoginComponent} from  './account/login/login.component';
import {RegisterComponent} from  './account/register/register.component';

import {ShowComponent} from  './show/show.component';
import {MyShowsComponent} from  './my-shows/my-shows.component';
import {PagedMyShowListComponent} from  './my-shows/paged-my-show-list/paged-my-show-list.component';
import {MyEpisodeComponent} from "./my-shows/my-episode/my-episode.component";

import {ShowsComponent} from  './shows/shows.component';
import {NetworkShowsComponent} from  './shows/network-shows.component';
import {SearchShowsComponent} from  './shows/search-shows.component';
import {ShortShowComponent} from  './shows/short-show/short-show.component';
import {GenrePanelComponent} from  './shows/genre-panel/genre-panel.component';
import {PagedShowListComponent} from  './shows/paged-show-list/paged-show-list.component';
import {ShowListComponent} from  './shows/show-list/show-list.component';
import {LogoutComponent} from  './account/logout/logout.component';

import {DoubleDigit} from  './show/pipes/doubleDigit.pipe';
import {PremieredDate} from  './show/pipes/premieredDate.pipe';
import {Querify} from  './show/pipes/querify.pipe';
import {WeekDayName} from  './show/pipes/weekDayName.pipe';
import {CapitaliseWords} from  './shows/capitaliseWords.pipe';

import {LongDate} from  './my-shows/my-episode/long-date.pipe';

import {PaginatePipe, PaginationControlsCmp} from 'ng2-pagination';

const appComponents : any[] = [

    HeaderComponent, FooterComponent,
    LoginComponent, RegisterComponent,
    ShowComponent,
    ShowsComponent, NetworkShowsComponent, SearchShowsComponent,
    ShortShowComponent,
    GenrePanelComponent, PagedShowListComponent,
    ShowListComponent,
    MyShowsComponent,
    PagedMyShowListComponent,
    MyEpisodeComponent,
    LogoutComponent
];

const appPipes : any[] = [

    DoubleDigit, PremieredDate, Querify, WeekDayName,
    CapitaliseWords,
    LongDate
];

const libComponents : any[] = [

    PaginationControlsCmp
];

const libPipes : any[] = [

    PaginatePipe
];

export const appDeclarations : any[] = [...appComponents, ...appPipes, ...libComponents, ...libPipes];