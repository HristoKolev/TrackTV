namespace TrackTv.Updater
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using TrackTv.Data;

    public class ApiResultRepository
    {
        public ApiResultRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public Task SaveApiResult(object jsonObj, ApiResultType type, int thetvdbid)
        {
            int apiResponseID = this.DbService.ApiResponses
                                  .Where(poco =>
                                      poco.ApiResponseShowThetvdbid == thetvdbid || poco.ApiResponseEpisodeThetvdbid == thetvdbid)
                                  .Select(poco => poco.ApiResponseID)
                                  .FirstOrDefault();

            var result = new ApiResponsePoco
            {
                ApiResponseID = apiResponseID,
                ApiResponseBody = JsonConvert.SerializeObject(jsonObj),
                ApiResponseLastUpdated = DateTime.UtcNow,
            };

            switch (type)
            {
                case ApiResultType.Show :
                    result.ApiResponseShowThetvdbid = thetvdbid;
                    break;
                case ApiResultType.Episode :
                    result.ApiResponseEpisodeThetvdbid = thetvdbid;
                    break;
                default :
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return this.DbService.Save(result);
        }
    }

    public enum ApiResultType
    {
        Show,

        Episode
    }
}