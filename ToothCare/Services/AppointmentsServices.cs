using ToothCare.Models;
using ToothCare.Models.DTO;
using ToothCare.Services.IServices;
using static Utility.SD;

namespace ToothCare.Services
{
    public class AppointmentsServices : BaseServices, IAppointmentsServices
    {
        private readonly IHttpClientFactory _httpClient;
        private string toothApiURL;
        public AppointmentsServices(IHttpClientFactory httpClient, IConfiguration config) : base(httpClient)
        {
            _httpClient = httpClient;
            toothApiURL = config.GetValue<string>("ServiceUrls:ToothCareAPI");

        }
        public Task<T> CreateAsync<T>(AppointmentsCreateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                ApiData = dto,
                ApiUrl = toothApiURL + "/api/Appointments",
                ApiToken = token

            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.DELETE,
                ApiUrl = toothApiURL + "/api/Appointments/"+id,
                ApiToken = token

            });
        }

        public Task<T> GetAllAsync<T>(int? search, string token)
        {
            if(search != null)
            {
                return SendAsync<T>(new ApiRequest()
                {
                    ApiType = ApiType.GET,
                    ApiUrl = toothApiURL + "/api/Appointments?search="+search,//client id
                    ApiToken = token

                });
            }
            else
            {
                return SendAsync<T>(new ApiRequest()
                {
                    ApiType = ApiType.GET,
                    ApiUrl = toothApiURL + "/api/Appointments",
                    ApiToken = token

                });
            }
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                ApiUrl = toothApiURL + "/api/Appointments/" + id,
                ApiToken = token

            });
        }

        public Task<T> UpdateAsync<T>(AppointmentsUpdateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.PUT,
                ApiData = dto,
                ApiUrl = toothApiURL + "/api/Appointments/" + dto.Id,
                ApiToken = token

            });
        }
    }
}
