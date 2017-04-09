namespace TrackTv.Services.Show
{
    using System.Threading.Tasks;

    using TrackTv.Services.Show.Models;

    public interface IShowService
    {
        Task<FullShow> GetFullShowAsync(int showId, int profileId);

        Task<FullShow> GetFullShowAsync(int showId);
    }
}