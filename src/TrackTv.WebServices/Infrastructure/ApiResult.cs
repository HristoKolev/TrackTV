namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ApiResult
    {
        public string[] ErrorMessages { get; set; }

        public object Payload { get; set; }

        public bool Success => this.ErrorMessages.Length == 0;

        public static ApiResult FromErrorMessages(params string[] messages)
        {
            return new ApiResult
            {
                ErrorMessages = messages.Where(errorMessage => !string.IsNullOrWhiteSpace(errorMessage)).ToArray()
            };
        }

        public static ApiResult FromException(Exception exception)
        {
            return new ApiResult
            {
                ErrorMessages = new[]
                {
                    exception.Message
                }
            };
        }

        public static ApiResult FromModelState(ModelStateDictionary modelState)
        {
            var messages = modelState.Values.Where(entry => entry.ValidationState == ModelValidationState.Invalid)
                                     .SelectMany(entry => entry.Errors).Select(error => error.ErrorMessage).ToArray();
            return new ApiResult
            {
                ErrorMessages = messages
            };
        }

        public static ApiResult Ok<T>(T payload)
        {
            return new ApiResult
            {
                ErrorMessages = Array.Empty<string>(),
                Payload = payload
            };
        }

        public static ApiResult Ok()
        {
            return new ApiResult
            {
                ErrorMessages = Array.Empty<string>()
            };
        }
    }

    public static class ControllerExtensions
    {
        public static ActionResult Failure(this ControllerBase controller, ModelStateDictionary modelState)
        {
            return controller.Ok(ApiResult.FromModelState(modelState));
        }

        public static ActionResult Failure(this ControllerBase controller, params string[] messages)
        {
            return controller.Ok(ApiResult.FromErrorMessages(messages));
        }

        public static ActionResult Success<T>(this ControllerBase controller, T payload)
        {
            return controller.Ok(ApiResult.Ok(payload));
        }

        public static ActionResult Success(this ControllerBase controller)
        {
            return controller.Ok(ApiResult.Ok());
        }
    }
}