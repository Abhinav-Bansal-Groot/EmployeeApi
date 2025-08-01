using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;

namespace EmployeeApi.Services.Abstract
{
    public interface IAuthService
    {
        Task<QR_Response> Setup(string email);
        Task<QR_Response> Enable_Disable_QR(Enable_Disable_Request request);

        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> Login_2FA(Verify2FARequest model);
        Task<AuthResponse> RefreshToken(TokenModel tokenModel);

    }
}
