using ToothCare.Models.DTO;

namespace ToothCare.Services.IServices
{
    public interface IClientsServices
    {
        Task<T> GetAllAsync<T>( string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ClientsCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ClientsUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
