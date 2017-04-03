namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Reflection;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Catches errors and wraps them in an <see cref="ErrorModel"/> that gets returned from the action with status code of 400.
    /// </summary>
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public HandleExceptionAttribute(Type exceptionType)
        {
            this.ExceptionType = exceptionType;
        }

        private Type ExceptionType { get; }

        public override void OnException(ExceptionContext context)
        {
            if (this.ExceptionType.IsInstanceOfType(context.Exception))
            {
                context.Result = new BadRequestObjectResult(new ErrorModel
                {
                    Message = context.Exception.Message,
                    ErrorType = this.ExceptionType.Name
                });

                context.ExceptionHandled = true;
            }
        }

        private class ErrorModel
        {
            public string ErrorType { get; set; }

            public string Message { get; set; }
        }
    }
}