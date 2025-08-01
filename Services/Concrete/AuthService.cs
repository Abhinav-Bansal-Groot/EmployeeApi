using EmployeeApi.Helpers;
using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;
using EmployeeApi.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EmployeeApi.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly GetPrincipal _getPrincipal;
        private readonly ValidateToken _validateToken;
        private readonly ITotpService _otpService;
        public AuthService(UserManager<ApplicationUser> userManager, JwtTokenGenerator tokenGenerator, 
            RefreshTokenGenerator refreshTokenGenerator, GetPrincipal getPrincipal, 
            ValidateToken validateToken, ITotpService otpService)
        {
            _userManager = userManager;            
            _tokenGenerator = tokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _getPrincipal = getPrincipal;
            _validateToken = validateToken;
            _otpService = otpService;
        }
        public async Task<QR_Response> Setup(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User not found!!");

            if (user.Enabled)
            {
                var (secret, url) = _otpService.GenerateQrCode(user.Email, "EmployeeApi");
                user.TwoFactorSecret = secret;
                user.TwoFactorEnabled = true;
                await _userManager.UpdateAsync(user);

                return new QR_Response
                {
                    Success = true,
                    Message = "Scan this QR code in Google Authenticator.",
                    TotpUrl = url
                };
            }
            return new QR_Response
            {
                Success = false,
                Message = "Enable 2FA to generate QR",
                TotpUrl = null
            };
        }

        public async Task<QR_Response> Enable_Disable_QR(Enable_Disable_Request request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            //if (user == null || !_otpService.ValidateCode(user.TwoFactorSecret, model.Code))
            //throw new Exception("Invalid credentials");

            if (user == null)
                throw new Exception("User not Found!!!");


            if (request.Enabled)
            {
                user.TwoFactorEnabled = true;
                user.Enabled = true;
                await _userManager.UpdateAsync(user);

                return new QR_Response
                {
                    Success = true,
                    Message = "2FA successfully Enabled"
                };
            }
            
            user.TwoFactorEnabled = false;
            user.Enabled = false;
            await _userManager.UpdateAsync(user);

            return new QR_Response
            {
                Success = true,
                Message = "2FA sucessfully disabled"
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, request.Role);

            var token = _tokenGenerator.GenerateToken(user, new[] { request.Role });
            return new AuthResponse { 
                Token = token, 
                Email = request.Email,
                Role = request.Role 
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new Exception("Invalid credentials");

            var enabled = false;
            var roles = await _userManager.GetRolesAsync(user);

            if (user.TwoFactorEnabled)
            {
                if(user.Enabled)
                    enabled = true;
                return new AuthResponse 
                {
                    Message = "Login using login-2FA api",
                    Email = request.Email,
                    Role = roles.FirstOrDefault(),
                    Enabled = enabled
                };
            }

            var token = _tokenGenerator.GenerateToken(user, roles);
            var refreshToken = _refreshTokenGenerator.GenerateRefreshToken(user, roles);

            return new AuthResponse {
                Message = "Successfully Logged In",
                Token = token, 
                RefreshToken = refreshToken, 
                Email = user.Email, 
                Role = roles.FirstOrDefault(),
                Enabled = enabled
            };
        }

        public async Task<AuthResponse> Login_2FA(Verify2FARequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null ||!_otpService.ValidateCode(user.TwoFactorSecret, model.Code))
                throw new Exception("Invalid credentials");

            user.Enabled = false;
            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenGenerator.GenerateToken(user, roles);
            var refreshToken = _refreshTokenGenerator.GenerateRefreshToken(user, roles);

            return new AuthResponse { 
                Message ="Successfully Logged In",
                Token = token, 
                RefreshToken = refreshToken,
                Email = user.Email, 
                Role = roles.FirstOrDefault() ,
                Enabled = false
            };
        }

        public async Task<AuthResponse> RefreshToken(TokenModel tokenModel)
        {
            ClaimsPrincipal principal;

            var jwtToken = _validateToken.Validatetoken(tokenModel.RefreshToken);

            try
            {
                principal = _getPrincipal.GetPrincipalFromExpiredToken(tokenModel.RefreshToken, validateLifetime: true);
            }
            catch
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var typeClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "typ")?.Value;
            if (typeClaim != "refresh")
                throw new SecurityTokenException("Invalid token type. Refresh token expected.");

            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                throw new SecurityTokenException("Invalid token: email claim not found");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new SecurityTokenException("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _tokenGenerator.GenerateToken(user, roles); 

            return new AuthResponse
            {
                Token = newAccessToken,
                RefreshToken = tokenModel.RefreshToken,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };
        }

    }
}
