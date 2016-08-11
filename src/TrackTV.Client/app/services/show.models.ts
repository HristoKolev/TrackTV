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

export interface SearchShows {

    count : number;

    shows : SimpleShow[];

    query : string;
}

export interface NetworkShows {

    count : number;

    shows : SimpleShow[];

    networkName : string;
}

export interface AirTime {

    hours : number;

    minutes : number;
}

export enum ShowStatus {

    Ended,

    Continuing,

    Unknown
}

export interface ShowDetails {

    airDay : number;

    banner : string;

    description : string;

    episodeCount : number;

    firstAired : Date;

    id : number;

    imdbId : string;

    isUserSubscribed : boolean;

    name : string;

    network : string;

    networkUserFriendlyId : string;

    numberOfSeasones : number;

    runtime : number;

    subscriberCount : number;

    tvDbId : number;

    userFriendlyId : string;

    status : ShowStatus;

    airTime : AirTime;
}
