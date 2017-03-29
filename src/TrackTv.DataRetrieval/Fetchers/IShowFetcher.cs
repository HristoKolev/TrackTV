namespace TrackTv.DataRetrieval.Fetchers
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    using TvDbSharper.Clients.Series.Json;

    public interface IShowFetcher
    {
        Task PopulateShowAsync(Show show, Series data);
    }
}