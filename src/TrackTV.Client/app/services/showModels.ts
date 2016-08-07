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