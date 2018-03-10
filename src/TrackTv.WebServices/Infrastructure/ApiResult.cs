namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Defines a result for an API call that is either successful or failed.
    /// A successful result is defined as a result without any errors.
    /// A successful result may or may not have a payload object.
    /// A failed result is defined as a result that has at least one error.
    /// A failed result must NOT have a payload object.
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// Error messages. Cannot be null. Use empty array to signify a successful result.
        /// </summary>
        public string[] ErrorMessages { get; private set; }

        /// <summary>
        /// The payload of a successful result. Failed results cannot have a payload.
        /// </summary>
        public object Payload { get; private set; }

        /// <summary>
        /// Indicates if the result is successful. This is the only definition of a successful result.
        /// </summary>
        public bool Success => this.ErrorMessages.Length == 0;

        /// <summary>
        /// Creates a failed result from any number of error messages.
        /// </summary>
        public static ApiResult Fail(params string[] messages)
        {
            var errorMessages = messages.Where(errorMessage => !string.IsNullOrWhiteSpace(errorMessage)).ToArray();

            if (errorMessages.Length == 0)
            {
                throw new ArgumentException("There must be at least 1 non-null, non white-space error message.");
            }

            return new ApiResult
            {
                ErrorMessages = errorMessages
            };
        }

        /// <summary>
        /// Creates a failed result from an exception object.
        /// </summary>
        public static ApiResult Fail(Exception exception) => Fail(exception.Message);

        /// <summary>
        /// Creates a successful result from a payload object.
        /// </summary>
        public static ApiResult Ok<T>(T payload)
        {
            return new ApiResult
            {
                ErrorMessages = Array.Empty<string>(),
                Payload = payload
            };
        }

        /// <summary>
        /// Creates a successful result without a payload.
        /// </summary>
        public static ApiResult Ok() => Ok<object>(null);
    }

    public static class ControllerExtensions
    {
        public static ActionResult Failure(this ControllerBase controller, ModelStateDictionary modelState)
        {
            var messages = modelState.Values.Where(entry => entry.ValidationState == ModelValidationState.Invalid)
                                     .SelectMany(entry => entry.Errors)
                                     .Select(error => error.ErrorMessage)
                                     .ToArray();

            return controller.Ok(ApiResult.Fail(messages));
        }

        public static ActionResult Failure(this ControllerBase controller, params string[] messages) => controller.Ok(ApiResult.Fail(messages));

        public static ActionResult Failure(this ControllerBase controller, Exception exception) => controller.Ok(ApiResult.Fail(exception));

        public static ActionResult Success<T>(this ControllerBase controller, T payload) => controller.Ok(ApiResult.Ok(payload));

        public static ActionResult Success(this ControllerBase controller) => controller.Ok(ApiResult.Ok());
    }
}