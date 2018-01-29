namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using log4net;
    using log4net.Util;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    using TrackTv.Services;

    /// <summary>
    /// <para>Global Exception handler.</para>
    /// <para>If the  controller defines an error message for the exception type,
    ///  we wrap it in <see cref="ApiResult"/> and return it with status code of 200.</para> 
    /// <para>If the exception can't be handled, we return an empty response with status code of 500.</para> 
    /// </summary>
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public HandleExceptionFilterAttribute(IHostingEnvironment hostingEnvironment, ILog log, MishapService mishapService)
        {
            this.HostingEnvironment = hostingEnvironment;
            this.Log = log;
            this.MishapService = mishapService;
        }

        private IHostingEnvironment HostingEnvironment { get; }

        private ILog Log { get; }

        private MishapService MishapService { get; }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            try
            {
                await this.HandleExceptionAsync(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.Log.ErrorExt(() =>
                    "\r\n\r\n" + "Exception occured while handling an exception.\r\n\r\n"
                               + $"Original exception: {context.Exception}\r\n\r\n" + $"Error handler exception: {ex}");

                throw;
            }

            await base.OnExceptionAsync(context).ConfigureAwait(false);
        }

        private async Task HandleExceptionAsync(ExceptionContext context)
        {
            Task mishapTask = this.MishapService.HandleErrorAsync(context.Exception);

            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;

            var exposeAttributes = descriptor.MethodInfo.GetCustomAttributes<ExposeErrorAttribute>();

            var exposeAttribute = exposeAttributes.FirstOrDefault(a => a.ExceptionType == context.Exception.GetType());

            if (exposeAttribute != null)
            {
                context.Result = new ObjectResult(ApiResult.Fail(exposeAttribute.Message))
                {
                    StatusCode = 200
                };

                this.Log.ErrorExt(() =>
                    "Exception was handled. " + $"(ExceptionMessage: {context.Exception.Message}, "
                                              + $"ExceptionName: {context.Exception.GetType().Name})");

                context.ExceptionHandled = true;
            }

            if (!context.ExceptionHandled && !this.HostingEnvironment.IsDevelopment())
            {
                this.Log.ErrorExt(() => $"Unhandled exception of type {context.Exception.GetType()}.", context.Exception);
                context.Result = new StatusCodeResult(500);
                context.ExceptionHandled = true;
            }

            await mishapTask.ConfigureAwait(false);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ExposeErrorAttribute : Attribute
    {
        public ExposeErrorAttribute(Type exceptionType, string message)
        {
            this.ExceptionType = exceptionType;
            this.Message = message;

            if (!typeof(Exception).IsAssignableFrom(exceptionType))
            {
                throw new NotSupportedException($"The type {exceptionType} is not an Exception type.");
            }
        }

        public Type ExceptionType { get; }

        public string Message { get; }
    }
}