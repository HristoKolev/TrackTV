export interface MyShows {

    count : number;

    shows : MyShow[];
}

export interface MyShow {

    id : number;

    name : string;

    userFriendlyId : string;

    lastEpisode : SimpleEpisode;

    nextEpisode : SimpleEpisode;

    subscribed : boolean;
}

export interface SimpleEpisode {

    firstAired : Date;

    number : number;

    seasonNumber : number;

    title : string;
}

export interface MyShowsModel {

    continuing : MyShows,

    ended : MyShows
}