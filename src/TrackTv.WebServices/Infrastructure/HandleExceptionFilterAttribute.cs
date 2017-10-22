namespace TrackTv.WebServices.Infrastructure
{
    using System;

    using log4net;
    using log4net.Util;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// <para>Global Exception handler.</para>
    /// <para>If the  controller defines an error message for the exception type,
    ///  we wrap it in <see cref="ApiResult"/> and return it with status code of 200.</para> 
    /// <para>If the exception can't be handled, we return an empty response with status code of 500.</para> 
    /// </summary>
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public HandleExceptionFilterAttribute(IHostingEnvironment hostingEnvironment, ILog log)
        {
            this.HostingEnvironment = hostingEnvironment;
            this.Log = log;
        }

        private IHostingEnvironment HostingEnvironment { get; }

        private ILog Log { get; }

        public override void OnException(ExceptionContext context)
        {
            try
            {
                this.HandleException(context);
            }
            catch (Exception ex)
            {
                this.Log.ErrorExt(() => "\r\n\r\n" + "Exception occured while handling an exception.\r\n\r\n"
                                        + $"Original exception: {context.Exception}\r\n\r\n" + $"Error handler exception: {ex}");

                throw;
            }
        }

        private void HandleException(ExceptionContext context)
        {
            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;

            var exposeAttribute = descriptor.MethodInfo.FirstAttribute<ExposeErrorAttribute>();

            if (exposeAttribute != null && exposeAttribute.ExceptionType.IsInstanceOfType(context.Exception))
            {
                context.Result = new ObjectResult(ApiResult.FromErrorMessages(exposeAttribute.Message))
                {
                    StatusCode = 200
                };

                this.Log.ErrorExt(() => $"Exception was handled. " + $"(ExceptionMessage: {context.Exception.Message}, "
                                        + $"ExceptionName: {context.Exception.GetType().Name})");

                context.ExceptionHandled = true;
            }

            if (!context.ExceptionHandled && !this.HostingEnvironment.IsDevelopment())
            {
                this.Log.ErrorExt(() => $"Unhandled exception of type {context.Exception.GetType()}.", context.Exception);
                context.Result = new StatusCodeResult(500);
                context.ExceptionHandled = true;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ExposeErrorAttribute : Attribute
    {
        public ExposeErrorAttribute(Type exceptionType, string message)
        {
            this.ExceptionType = exceptionType;
            this.Message = message;

            exceptionType.AssertIs<Exception>();
        }

        public Type ExceptionType { get; }

        public string Message { get; }
    }
}