namespace TrackTv.WebServices.Infrastructure
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static ActionResult Success<T>(this Controller controller, T payload)
        {
            return controller.Ok(ApiResult.Ok(payload));
        }
        
        public static ActionResult Success(this Controller controller)
        {
            return controller.Ok(ApiResult.Ok());
        }
    }

    public class ApiResult
    {
        public string[] ErrorMessages { get; set; }

        public object Payload { get; set; }

        public bool Success => this.ErrorMessages.Length == 0;

        public static ApiResult Error(Exception exception)
        {
            return new ApiResult
            {
                ErrorMessages = new[]
                {
                    exception.Message
                }
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
                ErrorMessages = Array.Empty<string>(),
            };
        }
    }
}