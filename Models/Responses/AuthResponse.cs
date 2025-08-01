namespace EmployeeApi.Models.Responses
{
    public class AuthResponse
    {
        public string? Message { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool? Enabled { get; set; }
    }
}
