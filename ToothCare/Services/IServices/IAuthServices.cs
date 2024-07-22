using ToothCare.Models.DTO;

namespace ToothCare.Services.IServices
{
    public interface IAuthServices
    {
        Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate);
    }
}
