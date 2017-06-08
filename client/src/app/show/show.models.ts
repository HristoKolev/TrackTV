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
