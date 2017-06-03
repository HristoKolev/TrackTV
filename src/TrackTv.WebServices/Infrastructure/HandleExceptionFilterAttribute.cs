namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Linq;
    using System.Reflection;

    using log4net;
    using log4net.Util;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using TrackTv.Services.Exceptions;

    /// <summary>
    /// <para>Global Exception handler.</para>
    /// <para>If the exception has the attribute <see cref="ExposeErrorAttribute"/> 
    /// we wrap it in a <see cref="ErrorModel"/> and return it with an status code of 400.</para> 
    /// <para>If the exception doesn't have a <see cref="ExposeErrorAttribute"/> attribute, we return an empty response with status code of 500.</para> 
    /// </summary>
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        static HandleExceptionFilterAttribute()
        {
            ApiErrorExceptions = GetApiErrorExceptions();
        }

        public HandleExceptionFilterAttribute(IHostingEnvironment hostingEnvironment, ILog log)
        {
            this.HostingEnvironment = hostingEnvironment;
            this.Log = log;
        }

        private static Type[] ApiErrorExceptions { get; }

        private IHostingEnvironment HostingEnvironment { get; }

        private ILog Log { get; }

        public override void OnException(ExceptionContext context)
        {
            for (int i = 0; i < ApiErrorExceptions.Length; i++)
            {
                Type exceptionType = ApiErrorExceptions[i];

                if (exceptionType.IsInstanceOfType(context.Exception))
                {
                    var errorModel = new ErrorModel
                    {
                        Message = context.Exception.Message,
                        ErrorCode = exceptionType.Name
                    };

                    context.Result = new BadRequestObjectResult(errorModel);

                    this.Log.ErrorExt(() => $"Exception was handled. (Message: {errorModel.Message}, ErrorCode: {errorModel.ErrorCode})");

                    context.ExceptionHandled = true;

                    break;
                }
            }

            if (!context.ExceptionHandled && !this.HostingEnvironment.IsDevelopment())
            {
                this.Log.ErrorExt(() => $"Unhandled exception of type {context.Exception.GetType()}.", context.Exception);
                context.Result = new StatusCodeResult(500);
                context.ExceptionHandled = true;
            }
        }

        /// <summary>
        /// Returns all types that have the <see cref="ExposeErrorAttribute"/> attribute.
        /// </summary>
        private static Type[] GetApiErrorExceptions()
        {
            var assembly = Assembly.GetEntryAssembly();

            var all = assembly.GetReferencedAssemblies().Select(Assembly.Load).Concat(new[]
            {
                assembly
            });

            return all.SelectMany(a => a.DefinedTypes).Where(t => t.GetCustomAttribute<ExposeErrorAttribute>() != null)
                      .Select(t => t.AsType()).ToArray();
        }

        private class ErrorModel
        {
            /// <summary>
            /// The exception type name
            /// </summary>
            public string ErrorCode { get; set; }

            public string Message { get; set; }
        }
    }
}