namespace TrackTv.Services
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;

    public class MyShowsService
    {
        public MyShowsService(
            IDbConnection dbConnection,
            SubscriptionRepository subscriptionRepository,
            ProfilesRepository profilesRepository)
        {
            this.SubscriptionRepository = subscriptionRepository;
            this.ProfilesRepository = profilesRepository;
            this.DbConnection = dbConnection;
        }

        private IDbConnection DbConnection { get; }

        private ProfilesRepository ProfilesRepository { get; }

        private SubscriptionRepository SubscriptionRepository { get; }

        public async Task<MyShow[]> GetAllAsync(int profileId, DateTime time)
        {
            if (!await this.ProfilesRepository.ProfileExistsAsync(profileId))
            {
                throw new ProfileNotFoundException(profileId);
            }

            var showIds = await this.SubscriptionRepository.GetSubscriptionIdsByProfileIdAsync(profileId);

            var episodesSummaries = await this.GetEpisodesSummariesAsync(showIds, time);

            return episodesSummaries;
        }

        private static MyShow Map(MyShow show, MyEpisode lastEpisode, MyEpisode nextEpisode)
        {
            show.LastEpisode = lastEpisode;
            show.NextEpisode = nextEpisode;

            return show;
        }

        private async Task<MyShow[]> GetEpisodesSummariesAsync(int[] showIds, DateTime time)
        {
            const string Query = @"
                SELECT s1.*, lastEpisode.* , nextEpisode.* from shows s1 JOIN
                  (SELECT s.ShowId as ShowId,
                      (SELECT e1.EpisodeId from episodes e1
                        WHERE e1.ShowId = s.ShowId and e1.SeasonNumber != 0 and e1.FirstAired is not NULL
                        AND e1.FirstAired <= @time
                        ORDER BY e1.FirstAired DESC LIMIT 1) as LastEpisodeId,

                      (SELECT e2.EpisodeId from episodes e2
                        WHERE e2.ShowId = s.ShowId and e2.SeasonNumber != 0 and  e2.FirstAired is not NULL
                        AND e2.FirstAired > @time
                        ORDER BY e2.FirstAired LIMIT 1) as NextEpisodeId
                   from shows s) sub on s1.ShowId = sub.ShowId
                LEFT JOIN episodes nextEpisode on nextEpisode.EpisodeId = sub.NextEpisodeId
                LEFT JOIN episodes lastEpisode on lastEpisode.EpisodeId = sub.LastEpisodeId

                WHERE s1.ShowId in @showIds";

            var parameters = new
            {
                showIds,
                time
            };

            var shows = await this
                              .DbConnection
                              .QueryAsync<MyShow, MyEpisode, MyEpisode, MyShow>(Query, Map, parameters,
                                                                                splitOn: "ShowId,EpisodeId,EpisodeId")
                              ;

            return shows.ToArray();
        }
    }

    public class MyShow
    {
        public MyEpisode LastEpisode { get; set; }

        public MyEpisode NextEpisode { get; set; }

        public string ShowBanner { get; set; }

        public int ShowId { get; set; }

        public string ShowName { get; set; }

        public ShowStatus ShowStatus { get; set; }
    }

    public class MyEpisode
    {
        public int EpisodeId { get; set; }

        public int EpisodeNumber { get; set; }

        public string EpisodeTitle { get; set; }

        public DateTime? FirstAired { get; set; }

        public int SeasonNumber { get; set; }
    }
}