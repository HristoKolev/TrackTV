namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for all Poco classes.
    /// </summary>
    public interface IPoco<T>
    {
        /// <summary>		
        /// <para>Returns the primary key for the table.</para>
        /// </summary>   
        int GetPrimaryKey();

        /// <summary>		
        /// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
        bool IsNew();

        /// <summary>		
        /// <para>Sets the primary key for the table.</para>
        /// </summary> 
        void SetPrimaryKey(int value);

        /// <summary>
        /// <para>Clones the current object and returns the clone.</para>
        /// </summary>
        T Clone();
    }


    public partial interface IDbService : IDisposable
    {
        Task Delete<TPoco>(TPoco poco)
            where TPoco : IPoco<TPoco>;

        Task Delete<TPoco>(int[] ids)
            where TPoco : IPoco<TPoco>;

        Task Delete<TPoco>(int id)
            where TPoco : IPoco<TPoco>;

        Task ExecuteInTransaction(Func<Task> body);

        Task ExecuteInTransaction(Func<IDbTransaction, Task> body, TimeSpan? timeout = null);

        Task<int> Insert<TPoco>(TPoco poco) where TPoco : IPoco<TPoco>;

        Task<int> Save<TPoco>(TPoco poco) where TPoco : IPoco<TPoco>;

        Task Update<TPoco>(TPoco poco) where TPoco : IPoco<TPoco>;

        void BulkInsertSync<TPoco>(IEnumerable<TPoco> list) where TPoco : IPoco<TPoco>;
    }
}