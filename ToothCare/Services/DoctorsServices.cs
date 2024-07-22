using ToothCare.Models;
using ToothCare.Models.DTO;
using ToothCare.Services.IServices;
using static Utility.SD;

namespace ToothCare.Services
{
    public class DoctorsServices : BaseServices, IDoctorsServices
    {
        private readonly IHttpClientFactory _httpClient;
        private string toothApiURL;
        public DoctorsServices(IHttpClientFactory httpClient, IConfiguration config):base(httpClient)
        {
            _httpClient = httpClient;
            toothApiURL = config.GetValue<string>("ServiceUrls:ToothCareAPI");

        }
        public Task<T> CreateAsync<T>(DoctorsCreateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                ApiData = dto,
                ApiUrl = toothApiURL + "/api/Doctors",
                ApiToken = token
            }); 
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.DELETE,
                ApiUrl = toothApiURL + "/api/Doctors/"+id,
                ApiToken = token
            });
        }

        public Task<T> GetAllAsync<T>(string? search, int pageNumber, int pageSize, string token)
        {
            if (string.IsNullOrEmpty(search))
            {
                return SendAsync<T>(new ApiRequest()
                {
                    ApiType = ApiType.GET,
                    ApiUrl = toothApiURL + "/api/Doctors?pageNumber="+pageNumber+"&pageSize="+pageSize,
                    ApiToken = token
                });
            }
            else
            {
                return SendAsync<T>(new ApiRequest()
                {
                    ApiType = ApiType.GET,
                    ApiUrl = toothApiURL + "/api/Doctors?search="+search+"&pageNumber=" + pageNumber + "&pageSize=" + pageSize,//speciality
                    ApiToken = token
                });
            }
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                ApiUrl = toothApiURL + "/api/Doctors/" + id,
                ApiToken = token
            });
        }

        public Task<T> UpdateAsync<T>(DoctorsUpdateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.PUT,
                ApiData = dto,
                ApiUrl = toothApiURL + "/api/Doctors/"+dto.Id,
                ApiToken = token
            });
        }
    }
}
