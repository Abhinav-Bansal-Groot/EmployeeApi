namespace EmployeeApi.Models.Responses
{
    public class AuthResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? Email { get; set; }
        public bool? VerificationDone { get; set; }
    }
}
