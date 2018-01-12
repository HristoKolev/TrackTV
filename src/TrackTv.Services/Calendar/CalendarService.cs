namespace TrackTv.Services.Calendar
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;

    public class CalendarService
    {
        public CalendarService(EpisodeCalendarCalculator episodeCalendarCalculator, ProfilesRepository profilesRepository)
        {
            this.EpisodeCalendarCalculator = episodeCalendarCalculator;
            this.ProfilesRepository = profilesRepository;
        }

        private EpisodeCalendarCalculator EpisodeCalendarCalculator { get; }

        private ProfilesRepository ProfilesRepository { get; }

        public async Task<CalendarDay[][]> GetCalendarAsync(int profileId, DateTime time, DateTime today)
        {
            if (!await this.ProfilesRepository.ProfileExistsAsync(profileId).ConfigureAwait(false))
            {
                throw new ProfileNotFoundException(profileId);
            }

            return await this.EpisodeCalendarCalculator.CreateAsync(profileId, time, today).ConfigureAwait(false);
        }
    }
}