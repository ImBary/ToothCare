
using ToothCare.Models;

namespace ToothCare.Services.IServices
{
    public interface IBaseServices
    {
        ApiResponse responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
