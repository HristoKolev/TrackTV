namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using log4net;
    using log4net.Util;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    using TrackTv.Services;

    /// <summary>
    /// <para>Global Exception handler.</para>
    /// <para>If the controller does not provide an error message for the exception type via <see cref="ExposeErrorAttribute"/>,
    ///  we use default error message.
    /// We wrap it in <see cref="ApiResult"/> and return it with status code of 200.</para> 
    /// </summary>
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private const string DefaultErrorMessage = "Server error. Please try again later.";

        public HandleExceptionFilterAttribute(ILog log, MishapService mishapService)
        {
            this.Log = log;
            this.MishapService = mishapService;
        }

        private ILog Log { get; }

        private MishapService MishapService { get; }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            try
            {
                await this.HandleExceptionAsync(context).ConfigureAwait(false);

                context.ExceptionHandled = true;
            }
            catch (Exception ex)
            {
                this.Log.ErrorExt(() => "\r\n\r\n"
                                        + "Exception occured while handling an exception.\r\n\r\n"
                                        + $"Original exception: {context.Exception}\r\n\r\n" + $"Error handler exception: {ex}");

                throw;
            }

            await base.OnExceptionAsync(context).ConfigureAwait(false);
        }

        private async Task HandleExceptionAsync(ExceptionContext context)
        {
            Task mishapTask = this.MishapService.HandleErrorAsync(context.Exception, context.ActionDescriptor.DisplayName);

            var exposeAttribute = GetExposeErrorAttribute(context);

            string errorMessage = exposeAttribute?.Message ?? DefaultErrorMessage;

            this.Log.ErrorExt(() => ComposeErrorMessage(context.Exception, context.ActionDescriptor));

            var apiResult = ApiResult.Fail(errorMessage);

            context.Result = new OkObjectResult(apiResult);

            await mishapTask.ConfigureAwait(false);
        }

        private static string ComposeErrorMessage(Exception exception, ActionDescriptor actionDescriptor)
        {
            return "Exception was handled. (" + $"ActionName: {actionDescriptor.DisplayName}, " + $"ExceptionMessage: {exception.Message}, "
                   + $"ExceptionName: {exception.GetType().Name})";
        }

        private static ExposeErrorAttribute GetExposeErrorAttribute(ExceptionContext context)
        {
            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var exposeAttributes = descriptor.MethodInfo.GetCustomAttributes<ExposeErrorAttribute>();
            var exposeAttribute = exposeAttributes.FirstOrDefault(a => a.ExceptionType == context.Exception.GetType());
            return exposeAttribute;
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