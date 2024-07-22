using ToothCare.Models;
using ToothCare.Models.DTO;
using ToothCare.Services.IServices;
using static Utility.SD;

namespace ToothCare.Services
{
    public class AuthServices : BaseServices, IAuthServices
    {
        private readonly IHttpClientFactory _httpClient;
        private string toothApiURL;

        public AuthServices(IHttpClientFactory httpClient, IConfiguration config) : base(httpClient)
        {
            _httpClient = httpClient;
            toothApiURL = config.GetValue<string>("ServiceUrls:ToothCareAPI");
            
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO objToCreate)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                ApiData = objToCreate,
                ApiUrl = toothApiURL + "/api/User/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                ApiData = objToCreate,
                ApiUrl = toothApiURL + "/api/User/register"
            });
        }
    }
}
