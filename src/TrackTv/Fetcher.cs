namespace TrackTv
{
    using System;
    using System.Threading.Tasks;

    using TvDbSharper;

    public class Fetcher
    {
        // ReSharper disable once StyleCop.SA1305
        public Fetcher(TrackTvDbContext context, ITvDbClient tvDbClient)
        {
            this.Context = context;
            this.TvDbClient = tvDbClient;
        }

        private TrackTvDbContext Context { get; }

        private ITvDbClient TvDbClient { get; }

        public async Task AddShow(string imdbId)
        {
            throw new NotImplementedException();
        }
    }
}