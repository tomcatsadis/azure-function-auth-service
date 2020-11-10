using Newtonsoft.Json;

namespace AuthService.API.Services.Response
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public ErrorResponse(string error, string message)
        {
            Error = error;
            Message = message;
        }

        public static ErrorResponse NotFound(string message = "Data not found.")
        {
            return new ErrorResponse(
                error: "not_found",
                message: message
            );
        }

        public static ErrorResponse Conflict(string message = "Data already exist.")
        {
            return new ErrorResponse(
                error: "conflict",
                message: message
            );
        }

        public static ErrorResponse BadRequest(string message = "Bad Request.")
        {
            return new ErrorResponse(
                error: "bad_request",
                message: message
            );
        }
    }

    public class ErrorParametersResponse : ErrorResponse
    {
        public ErrorParametersResponse() 
            : base("invalid_parameters", "Request parameters is not valid.")
        {
        }

        [JsonProperty("invalid_parameters")]
        public object InvalidParametersObject { get; set; }
    }
}
