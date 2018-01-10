namespace TrackTv.DataRetrieval
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class ExternalShowsService
    {
        public ExternalShowsService(ISearchClient client)
        {
            this.Client = client;
        }

        private ISearchClient Client { get; }

        public Task<CatalogShow[]> GetShowsByImdbIdAsync(string imdbId)
        {
            return this.ShowsByAsync(imdbId, SearchParameter.ImdbId);
        }

        public Task<CatalogShow[]> GetShowsByNameAsync(string query)
        {
            return this.ShowsByAsync(query, SearchParameter.Name);
        }

        private async Task<CatalogShow[]> ShowsByAsync(string parameter, SearchParameter parameterType)
        {
            try
            {
                var response = await this.Client.SearchSeriesAsync(parameter, parameterType).ConfigureAwait(false);

                return response.Data.Select(result => new CatalogShow
                               {
                                   Id = result.Id,
                                   Banner = result.Banner,
                                   FirstAired = result.FirstAired,
                                   Network = result.Network,
                                   Overview = result.Overview,
                                   SeriesName = result.SeriesName,
                                   Status = result.Status
                               })
                               .ToArray();
            }
            catch (TvDbServerException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return Array.Empty<CatalogShow>();
                }

                throw;
            }
        }
    }

    public class CatalogShow
    {
        public string Banner { get; set; }

        public string FirstAired { get; set; }

        public int Id { get; set; }

        public string Network { get; set; }

        public string Overview { get; set; }

        public string SeriesName { get; set; }

        public string Status { get; set; }
    }
}