using API.Model;

namespace API.Service
{
    public interface IAuthService
    {
        string GenerateTokenString(LoginUser user);
        Task<bool> RegisterUser(LoginUser user);
        Task<bool> Login (LoginUser user);
    }
}
