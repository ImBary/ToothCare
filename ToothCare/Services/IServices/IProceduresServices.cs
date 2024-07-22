using ToothCare.Models.DTO;

namespace ToothCare.Services.IServices
{
    public interface IProceduresServices
    {
        Task<T> GetAllAsync<T>(string? search, int pageNumber, int pageSize, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ProceduresCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ProceduresUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
