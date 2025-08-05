using EmployeeApi.Controllers;
using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;
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

    [HttpPost("2fa/{email}/{option}")]
    public async Task<IActionResult> QR_Enable_Disable([FromRoute]string email, [FromRoute]string option)
    {
        var result = await _authService.Enable_Disable_QR(email, option);
        return HandleResponse(result);
    }

    [HttpPost("2fa/generate")]
    public async Task<IActionResult> Generate_QR(string email)
    {
        var result = await _authService.Generate_QR(email);
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
        return Ok(new CommonResponse<AuthResponse>
        {
            Status = 200,
            Data = result
        });
    }

    [HttpPost("auth/verification")]
    public async Task<IActionResult> Verification(Verify2FARequest request)
    {
        var result = await _authService.Verification(request);
        return HandleResponse(result);
    }

    [HttpPost("auth/refresh")]
    public async Task<IActionResult> Refresh(TokenModel tokenModel)
    {
        var result = await _authService.RefreshToken(tokenModel);
        return HandleResponse(result);
    }
}
