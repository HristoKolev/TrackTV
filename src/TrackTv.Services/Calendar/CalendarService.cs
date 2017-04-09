﻿namespace TrackTv.Services.Calendar
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Services.Calendar.Models;
    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;

    public class CalendarService : ICalendarService
    {
        public CalendarService(IEpisodeCalendar episodeCalendar, IProfilesRepository profilesRepository)
        {
            this.EpisodeCalendar = episodeCalendar;
            this.ProfilesRepository = profilesRepository;
        }

        private IEpisodeCalendar EpisodeCalendar { get; }

        private IProfilesRepository ProfilesRepository { get; }

        public async Task<CalendarDay[][]> GetCalendarAsync(int profileId, DateTime time)
        {
            if (!await this.ProfilesRepository.ProfileExistsAsync(profileId).ConfigureAwait(false))
            {
                throw new ProfileNotFoundException(profileId);
            }

            return await this.EpisodeCalendar.CreateAsync(profileId, time).ConfigureAwait(false);
        }
    }
}