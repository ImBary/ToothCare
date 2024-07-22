
using ToothCare.Models;
using ToothCare.Models.DTO;

namespace ToothCare.Services.IServices
{
    public interface IAppointmentsServices
    {
        Task<T> GetAllAsync<T>(int? search,string token);
        Task<T> GetAsync<T>(int id,string token);
        Task<T> CreateAsync<T>(AppointmentsCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(AppointmentsUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
