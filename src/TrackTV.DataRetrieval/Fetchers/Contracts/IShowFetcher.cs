namespace TrackTV.DataRetrieval.Fetchers.Contracts
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Series.Json;

    public interface IShowFetcher
    {
        Task PopulateShowAsync(Show show, Series data);
    }
}