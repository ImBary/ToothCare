using ToothCare.Models;
using ToothCare.Models.DTO;
using ToothCare.Services.IServices;
using static Utility.SD;

namespace ToothCare.Services
{
    public class ClientsServices : BaseServices, IClientsServices
    {
        private readonly IHttpClientFactory _httpClient;
        private string toothApiURL;
        public ClientsServices(IHttpClientFactory httpClient, IConfiguration config) : base(httpClient)
        {
            _httpClient = httpClient;
            toothApiURL = config.GetValue<string>("ServiceUrls:ToothCareAPI");
            
        }

        public Task<T> CreateAsync<T>(ClientsCreateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                ApiData = dto,
                ApiUrl = toothApiURL + "/api/Clients",
                ApiToken = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.DELETE,
                ApiUrl = toothApiURL + "/api/Clients/"+id,
                ApiToken = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                ApiUrl = toothApiURL + "/api/Clients",
                ApiToken = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                ApiUrl = toothApiURL + "/api/Clients/" + id,
                ApiToken = token
            });
        }

        public Task<T> UpdateAsync<T>(ClientsUpdateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.PUT,
                ApiData = dto,
                ApiUrl = toothApiURL + "/api/Clients/" + dto.Id,
                ApiToken = token
            });
        }
    }
}
