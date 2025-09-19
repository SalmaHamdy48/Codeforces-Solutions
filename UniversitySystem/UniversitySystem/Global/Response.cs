
using System.Net;

namespace UniversitySystem.Global
{
    public class Response
    {
        public bool Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }

        public static Response SuccessResponse(object? responseData, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Response
            {
                Status = true,
                StatusCode = statusCode,
                Message = message,
                Data = responseData
            };
        }

        public static Response ErrorResponse(string message, List<string>? errors = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new Response
            {
                Status = false,
                StatusCode = statusCode,
                Message = message,
                Data = new { Errors = errors ?? new List<string> { message } }
            };
        }
    }
}