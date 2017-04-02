namespace TrackTv.WebServices.Infrastructure
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public HandleExceptionAttribute(Type exceptionType)
        {
            this.ExceptionType = exceptionType;
        }

        private Type ExceptionType { get; }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == this.ExceptionType)
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