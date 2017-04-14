namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using TrackTv.Services.Exceptions;

    /// <summary>
    /// <para>Global Exception handler.</para>
    /// <para>If the exception has the attribute <see cref="ExposeErrorAttribute"/> 
    /// we wrap it in a <see cref="ErrorModel"/> and return it with an status code of 400.</para> 
    /// <para>If the exception doesn't have a <see cref="ExposeErrorAttribute"/> attribute, we return an empty response with status code of 500.</para> 
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

        /// <summary>
        /// Returns all types that have the <see cref="ExposeErrorAttribute"/> attribute.
        /// </summary>
        private static Type[] GetApiErrorExceptions()
        {
            var assembly = Assembly.GetEntryAssembly();

            var all = assembly.GetReferencedAssemblies()
                              .Select(Assembly.Load)
                              .Concat(new[]
                              {
                                  assembly
                              });

            return all.SelectMany(a => a.DefinedTypes)
                      .Where(t => t.GetCustomAttribute<ExposeErrorAttribute>() != null)
                      .Select(t => t.AsType())
                      .ToArray();
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