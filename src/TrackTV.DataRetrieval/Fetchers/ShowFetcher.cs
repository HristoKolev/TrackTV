namespace TrackTV.DataRetrieval.Fetchers
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Models;
    using TrackTv.Models.Enums;

    using TrackTV.Data.Repositories;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Series.Json;
    using TvDbSharper.Clients.Updates;

    public class ShowFetcher
    {
        public ShowFetcher(NetworkRepository networkRepository)
        {
            this.NetworkRepository = networkRepository;

            this.DateParser = new DateParser();
        }

        private DateParser DateParser { get; }

        private NetworkRepository NetworkRepository { get; }

        public async Task PopulateShowAsync(Show show, TvDbResponse<Series> response)
        {
            this.MapToShow(show, response.Data);

            await this.AddNetworkAsync(show, response.Data.Network);
        }

        private async Task AddNetworkAsync(Show show, string networkName)
        {
            if (!show.HasNetwork() || (show.Network.Name != networkName))
            {
                var existingNetwork = await this.NetworkRepository.GetNetworkByNameAsync(networkName);

                show.Network = existingNetwork ?? new Network(networkName);
            }
        }

        private void MapToShow(Show show, Series data)
        {
            show.TvDbId = data.Id;
            show.Name = data.SeriesName;
            show.Banner = data.Banner;
            show.ImdbId = data.ImdbId;
            show.Description = data.Overview;

            long? lastUpdated = data.LastUpdated;
            show.LastUpdated = lastUpdated.ToDateTime();

            AirDay airDay;
            Enum.TryParse(data.AirsDayOfWeek, out airDay);
            show.AirDay = airDay;

            ShowStatus status;
            Enum.TryParse(data.Status, out status);
            show.Status = status;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                show.FirstAired = this.DateParser.ParseFirstAired(data.FirstAired);
            }

            if (!string.IsNullOrWhiteSpace(data.AirsTime))
            {
                show.AirTime = this.DateParser.ParseAirTime(data.AirsTime);
            }
        }
    }
}