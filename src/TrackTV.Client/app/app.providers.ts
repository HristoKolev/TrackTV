import {MyShowsService} from './my-shows/my-shows.service';

import {ShowsService} from './shows/shows.service';

import {PaginationService} from 'ng2-pagination';

export const typeBindings = [

    ShowsService,

    MyShowsService,
 
    PaginationService
];