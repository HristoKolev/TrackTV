namespace TrackTv.WebServices.Infrastructure
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using TrackTv.Data;

    /// <summary>
    /// Opens a transaction at the start of the request and if no exception is thrown, commits it at the end.
    /// </summary>
    public class InTransactionFilter : IAsyncActionFilter
    {
        public InTransactionFilter(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return this.DbService.ExecuteInTransactionAndCommit(async transaction =>
            {
                var ctx = await next().ConfigureAwait(false);

                if (ctx.Exception != null)
                {
                    await transaction.RollbackAsync().ConfigureAwait(false);
                    return;
                }

                if (ctx.Result != null 
                    && ctx.Result is OkObjectResult jsResult
                    && jsResult.Value is ApiResult apiResult
                    && !apiResult.Success)
                {
                    await transaction.RollbackAsync().ConfigureAwait(false);
                }
            });
        }
    }
}