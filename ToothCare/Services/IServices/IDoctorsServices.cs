using Microsoft.AspNetCore.Mvc;
using ToothCare.Models.DTO;

namespace ToothCare.Services.IServices
{
    public interface IDoctorsServices
    {
        Task<T> GetAllAsync<T>(string? search, int pageNumber , int pageSize, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(DoctorsCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(DoctorsUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
