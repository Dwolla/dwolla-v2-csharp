using Dwolla.Client.Models.Responses;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dwolla.Client.Rest
{
    public interface IResponseBuilder
    {
        Task<RestResponse<T>> Build<T>(HttpResponseMessage response);
        RestResponse<T> Error<T>(HttpResponseMessage response, string code, string message, string rawContent = null);
    }

    public class ResponseBuilder : IResponseBuilder
    {
        private readonly JsonSerializerOptions _jsonSettings;

        public ResponseBuilder()
        {
            _jsonSettings = new JsonSerializerOptions();
        }

        public ResponseBuilder(JsonSerializerOptions jsonSerializerOptions = null)
        {
            _jsonSettings = jsonSerializerOptions;
        }

        public async Task<RestResponse<T>> Build<T>(HttpResponseMessage response)
        {
            using (var content = response.Content)
            {
                var rawContent = await content.ReadAsStringAsync();

                try
                {
                    if ((int)response.StatusCode >= 400)
                    {
                        return Error<T>(response, JsonSerializer.Deserialize<ErrorResponse>(rawContent, _jsonSettings), rawContent);
                    }
                    else if (string.IsNullOrWhiteSpace(rawContent))
                    {
                        return new RestResponse<T>(response, default, rawContent);
                    }
                    else
                    {
                        return new RestResponse<T>(response, JsonSerializer.Deserialize<T>(rawContent, _jsonSettings), rawContent);
                    }
                }
                catch (Exception e)
                {
                    return Error<T>(response, "DeserializationException", e.Message, rawContent);
                }
            }
        }

        public RestResponse<T> Error<T>(HttpResponseMessage response, string code, string message, string rawContent) =>
            Error<T>(response, new ErrorResponse { Code = code, Message = message }, rawContent);

        private static RestResponse<T> Error<T>(HttpResponseMessage response, ErrorResponse error, string rawContent) =>
            new RestResponse<T>(response, default(T), rawContent, error);
    }
}