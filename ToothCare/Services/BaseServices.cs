using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using ToothCare.Models;
using ToothCare.Services.IServices;
using static Utility.SD;

namespace ToothCare.Services
{
    public class BaseServices : IBaseServices
    {
        public ApiResponse responseModel { get; set; }
        public IHttpClientFactory _httpClient {  get; set; }
        public BaseServices(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            _httpClient = httpClient;
        }
       

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("ToothCareApi");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.ApiUrl);

                if (apiRequest.ApiData != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.ApiData),Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType) 
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);

                return APIResponse;

            }
            catch (Exception ex) 
            {
                var dto = new ApiResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false

                };

                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;

            }
        }
    }
}
