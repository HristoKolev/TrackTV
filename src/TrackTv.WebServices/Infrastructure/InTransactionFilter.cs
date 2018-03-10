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

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await this.DbService.ExecuteInTransaction(async transaction =>
                      {
                          ActionExecutedContext ctx = await next().ConfigureAwait(false);

                          if (ctx.Exception != null)
                          {
                              transaction.Rollback();
                              return;
                          }

                          if (ctx.Result != null && ctx.Result is OkObjectResult jsResult && jsResult.Value is ApiResult apiResult
                              && !apiResult.Success)
                          {
                              transaction.Rollback();
                          }
                      })
                      .ConfigureAwait(false);
        }
    }
}