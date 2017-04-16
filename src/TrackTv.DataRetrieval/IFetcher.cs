namespace TrackTv.DataRetrieval
{
    using System;
    using System.Threading.Tasks;

    public interface IFetcher
    {
        Task AddShowAsync(int theTvDbId);

        Task UpdateAllRecordsAsync(DateTime from);

        Task UpdateEpisodeAsync(int episodeId);

        Task UpdateEpisodesAsync(int showId);

        Task UpdateShowAsync(int showId);
    }
}