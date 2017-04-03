namespace TrackTv.Services.Show
{
    using System.Threading.Tasks;

    using TrackTv.Services.Show.Models;

    public interface IShowService
    {
        Task<FullShow> GetFullShowAsync(int id, int profileId = default(int));
    }
}