namespace TrackTv.Services.MyShows
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Services.MyShows.Models;

    public interface IMyShowsService
    {
        Task<MyShow[]> GetAllAsync(int profileId, DateTime time);
    }
}