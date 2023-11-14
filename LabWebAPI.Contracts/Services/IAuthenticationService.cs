
using LabWebAPI.Contracts.DTO.Authentications;

namespace LabWebAPI.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task RegistrationAsync(UserRegistrationDTO model);
        Task<UserAutorizationDTO> LoginAsync(UserLoginDTO model);
        Task<UserAutorizationDTO> RefreshTokenAsync(UserAutorizationDTO userTokensDTO);
        Task LogoutAsync(UserAutorizationDTO userTokensDTO);
    }
}
