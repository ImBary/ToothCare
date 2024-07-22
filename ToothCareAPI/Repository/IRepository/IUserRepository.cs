using ToothCareAPI.Model.DTO;

namespace ToothCareAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUnique(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
