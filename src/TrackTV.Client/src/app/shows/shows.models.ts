export interface SimpleShow {

    name : string;

    userFriendlyId : string;

    episodeCount : number;

    subscriberCount : number;

    banner : string;

    poster : string;
}

export interface Genre {

    name : string;

    userFriendlyId : string;
}

export interface SimpleShows {

    ended : SimpleShow[];

    running : SimpleShow[];

    genres : Genre[];
}

export interface ShowsModel {

    shows : SimpleShows,
}

export interface SearchShowsModel {

    count : number;

    shows : SimpleShow[];

    query : string;
}

export interface NetworkShowsModel {

    count : number;

    shows : SimpleShow[];

    networkName : string;
}

export interface ShowsByGenreModel {

    shows : SimpleShows,

    genre : string
}
