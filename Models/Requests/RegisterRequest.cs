using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Models.Requests
{
    public class RegisterRequest
    {
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; }
    }
}
