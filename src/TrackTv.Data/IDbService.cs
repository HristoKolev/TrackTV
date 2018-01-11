namespace TrackTv.Data
{
    using System;
    using System.Threading.Tasks;

    public partial interface IDbService : IDisposable
    {
        Task DeleteAsync<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task ExecuteInTransaction(Func<Task> body);

        Task<int> InsertAsync<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task<int> SaveAsync<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task UpdateAsync<TPoco>(TPoco poco)
            where TPoco : IPoco;
    }
}