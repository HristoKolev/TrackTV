export interface MonthRouteInfo {

    year: number;

    month: number;
}

export interface CalendarNavigationInfo {

    previosMonth: MonthRouteInfo;

    thisMonth: MonthRouteInfo;

    nextMonth: MonthRouteInfo;
}

export interface CalendarEpisode {

    firstAired: Date;

    number: number;

    seasonNumber: number;

    showName: string;

    title: string;

    userFriendlyId: string;
}

export interface  CalendarDay {

    date: Date;

    episodes: CalendarEpisode[];
}

export interface CalendarModel {

    navigationInfo: CalendarNavigationInfo;

    weeks: CalendarDay[][];
}
