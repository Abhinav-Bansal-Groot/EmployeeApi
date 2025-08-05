using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;

namespace EmployeeApi.Services.Abstract
{
    public interface IAuthService
    {
        Task<QR_Response> Generate_QR(string email);
        Task<QR_Response> Enable_Disable_QR(string email, string option);

        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> Verification(Verify2FARequest model);
        Task<AuthResponse> RefreshToken(TokenModel tokenModel);

    }
}
