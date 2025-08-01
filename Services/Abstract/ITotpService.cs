namespace EmployeeApi.Services.Abstract
{
    public interface ITotpService
    {
        (string Secret, string Url) GenerateQrCode(string email, string appName);
        bool ValidateCode(string secret, string code);
    }
}
