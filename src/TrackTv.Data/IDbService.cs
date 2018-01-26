namespace TrackTv.Data
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public partial interface IDbService : IDisposable
    {
        Task Delete<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task ExecuteInTransaction(Func<Task> body);

        Task ExecuteInTransaction(Func<IDbTransaction, Task> body);

        Task<int> Insert<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task<int> Save<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task Update<TPoco>(TPoco poco)
            where TPoco : IPoco;
    }
}