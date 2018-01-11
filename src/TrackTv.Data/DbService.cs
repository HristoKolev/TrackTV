namespace TrackTv.Data
{
    using System.Data;
    using System.Threading.Tasks;

    using LinqToDB;
    using LinqToDB.Data;
    using LinqToDB.DataProvider;

    public partial class DbService
    {
        public DbService(IDbConnection dbConnection, IDataProvider dataProvider)
        {
            this.DataConnection = new DataConnection(dataProvider, dbConnection, true);
        }

        private DataConnection DataConnection { get; }

        public Task DeleteAsync<TPoco>(TPoco poco)
            where TPoco : IPoco =>
            this.DataConnection.DeleteAsync(poco);

        public Task<int> InsertAsync<TPoco>(TPoco poco)
            where TPoco : IPoco =>
            this.DataConnection.InsertWithInt32IdentityAsync(poco);

        public async Task<int> SaveAsync<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            if (poco.IsNew())
            {
                return await this.InsertAsync(poco).ConfigureAwait(false);
            }

            await this.UpdateAsync(poco).ConfigureAwait(false);

            return poco.GetPrimaryKey();
        }

        public Task UpdateAsync<TPoco>(TPoco poco)
            where TPoco : IPoco =>
            this.DataConnection.UpdateAsync(poco);
    }

    public interface IPoco
    {
        int GetPrimaryKey();

        bool IsNew();

        void SetPrimaryKey(int value);
    }
}