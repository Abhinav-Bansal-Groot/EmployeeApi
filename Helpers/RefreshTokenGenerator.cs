using EmployeeApi.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeApi.Helpers
{
    public class RefreshTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public RefreshTokenGenerator (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateRefreshToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("typ", "refresh")
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var refreshTokenValidityInDays = _configuration.GetValue<int>("Jwt:RefreshTokenValidityInDays");

            var refreshToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(refreshTokenValidityInDays),
            signingCredentials: creds
        );

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }
    }
}
