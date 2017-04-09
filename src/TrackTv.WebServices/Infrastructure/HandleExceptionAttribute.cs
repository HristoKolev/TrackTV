namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Reflection;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using TrackTv.Services.Show.Models;
    using TrackTv.Services.Shows.Models;
    using TrackTv.Services.Subscription.Models;

    /// <summary>
    /// Catches errors and wraps them in an <see cref="ErrorModel"/> that gets returned from the action with status code of 400.
    /// </summary>
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        static HandleExceptionAttribute()
        {
            ApiErrorExceptions = GetApiErrorExceptions();
        }

        private static Type[] ApiErrorExceptions { get; }

        public override void OnException(ExceptionContext context)
        {
            foreach (var exceptionType in ApiErrorExceptions)
            {
                if (exceptionType.IsInstanceOfType(context.Exception))
                {
                    context.Result = new BadRequestObjectResult(new ErrorModel
                    {
                        Message = context.Exception.Message,
                        ErrorCode = exceptionType.Name
                    });

                    context.ExceptionHandled = true;

                    break;
                }
            }

            if (!context.ExceptionHandled)
            {
                context.Result = new StatusCodeResult(500);
                context.ExceptionHandled = true;
            }
        }

        private static Type[] GetApiErrorExceptions()
        {
            return new[]
            {
                typeof(ShowNotFoundException),
                typeof(GenreNotFoundException),
                typeof(InvalidQueryException),
                typeof(SubscriptionException)
            };
        }

        private class ErrorModel
        {
            public string ErrorCode { get; set; }

            public string Message { get; set; }
        }
    }
}