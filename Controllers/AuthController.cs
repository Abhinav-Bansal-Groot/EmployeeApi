using EmployeeApi.Controllers;
using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

[Route("api/")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("2fa/qr-setup")]
    public async Task<IActionResult> QR_Setup(string email)
    {
        var result = await _authService.Setup(email);
        return HandleResponse(result);
    }

    [HttpPost("2fa/QR_Enable_Disable")]
    public async Task<IActionResult> QR_Enable_Disable(Enable_Disable_Request request)
    {
        var result = await _authService.Enable_Disable_QR(request);
        return HandleResponse(result);
    }

    [HttpPost("auth/register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        return HandleResponse(result);
    }

    [HttpPost("auth/login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        return HandleResponse(result);
    }

    [HttpPost("auth/login-2fa")]
    public async Task<IActionResult> Verify2FA(Verify2FARequest request)
    {
        var result = await _authService.Login_2FA(request);
        return HandleResponse(result);
    }

    [HttpPost("auth/refresh")]
    public async Task<IActionResult> Refresh(TokenModel tokenModel)
    {
        var result = await _authService.RefreshToken(tokenModel);
        return HandleResponse(result);
    }
}