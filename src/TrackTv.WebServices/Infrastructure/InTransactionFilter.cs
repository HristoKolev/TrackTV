namespace TrackTv.WebServices.Infrastructure
{
    using System.Data;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using MySql.Data.MySqlClient;

    /// <summary>
    /// Opens a transaction at the start of the request and if no exception is thrown, commits it at the end.
    /// </summary>
    public class InTransactionFilter : IAsyncActionFilter
    {
        public InTransactionFilter(MySqlConnection connection)
        {
            this.Connection = connection;
        }

        private MySqlConnection Connection { get; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (this.Connection.State != ConnectionState.Open)
            {
                await this.Connection.OpenAsync().ConfigureAwait(false);
            }

            using (var transaction = await this.Connection.BeginTransactionAsync().ConfigureAwait(false))
            {
                var ctx = await next().ConfigureAwait(false);

                if (ctx.Result != null && ctx.Result is OkObjectResult jsResult && jsResult.Value is ApiResult apiResult)
                {
                    if (apiResult.Success)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                else
                {
                    if (ctx.Exception != null)
                    {
                        transaction.Rollback();
                    }
                    else
                    {
                        transaction.Commit();
                    }
                }
            }
        }
    }
}