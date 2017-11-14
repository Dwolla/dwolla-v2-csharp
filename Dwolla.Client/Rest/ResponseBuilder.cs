using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dwolla.Client.Rest
{
    public interface IResponseBuilder
    {
        Task<RestResponse<T>> Build<T>(HttpResponseMessage response);
    }

    public class ResponseBuilder : IResponseBuilder
    {
        public async Task<RestResponse<T>> Build<T>(HttpResponseMessage response)
        {
            using (var content = response.Content)
                return await Build<T>(content, response);
        }

        private static async Task<RestResponse<T>> Build<T>(HttpContent content,
            HttpResponseMessage response)
        {
            if (content == null) return Error<T>(response, "Response content is null");
            var contentAsString = await content.ReadAsStringAsync();

            try
            {
                return new RestResponse<T>(response, JsonConvert.DeserializeObject<T>(contentAsString), rawContent: contentAsString);
            }
            catch (Exception ex)
            {
                return Error<T>(response, "Exception parsing JSON", ex, contentAsString);
            }
        }

        private static RestResponse<T> Error<T>(
            HttpResponseMessage response,
            string message,
            Exception innerException = null,
            string content = null) => new RestResponse<T>(
            response, default(T), new RestException(message, innerException, response.StatusCode, content), content);
    }
}