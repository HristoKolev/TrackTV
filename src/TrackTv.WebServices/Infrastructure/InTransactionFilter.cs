namespace TrackTv.WebServices.Infrastructure
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Filters;

    using TrackTv.Data;

    /// <summary>
    /// Opens a transaction at the start of the request and if no exception is thrown, commits it at the end.
    /// </summary>
    public class InTransactionFilter : IAsyncActionFilter
    {
        public InTransactionFilter(ITransactionScopeFactory transactionScopeFactory)
        {
            this.TransactionScopeFactory = transactionScopeFactory;
        }

        private ITransactionScopeFactory TransactionScopeFactory { get; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var scope = await this.TransactionScopeFactory.CreateScopeAsync().ConfigureAwait(false))
            {
                var executedContext = await next().ConfigureAwait(false);

                if (executedContext.Exception == null)
                {
                    scope.Complete();
                }
            }
        }
    }
}