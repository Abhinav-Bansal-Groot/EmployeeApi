using Microsoft.AspNetCore.Identity;

namespace EmployeeApi.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? TwoFactorSecret { get; set; }
        public bool Verification_Done { get; set; }
    }
}
