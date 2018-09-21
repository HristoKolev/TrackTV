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

        public Task SaveApiResult(object jsonObj, ApiChangeType type, int thetvdbid)
        {
            int apiResponseID = this.DbService.Poco.ApiResponses
                                  .Where(poco => 
                                     (type == ApiChangeType.Show && poco.ApiResponseShowThetvdbid == thetvdbid) || 
                                     (type == ApiChangeType.Episode && poco.ApiResponseEpisodeThetvdbid == thetvdbid))
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
                case ApiChangeType.Show :
                    result.ApiResponseShowThetvdbid = thetvdbid;
                    break;
                case ApiChangeType.Episode :
                    result.ApiResponseEpisodeThetvdbid = thetvdbid;
                    break;
                default :
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return this.DbService.Save(result);
        }
    }
}