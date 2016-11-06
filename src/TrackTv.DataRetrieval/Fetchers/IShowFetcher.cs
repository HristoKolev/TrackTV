namespace TrackTv.DataRetrieval.Fetchers
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    using TvDbSharper.Clients.Series.Json;

    public interface IShowFetcher
    {
        Task PopulateShowAsync(Show show, Series data);
    }
}