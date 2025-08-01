using EmployeeApi.Services.Abstract;
using OtpNet;
namespace EmployeeApi.Services.Concrete
{
    public class TotpService : ITotpService
    {
        public (string Secret, string Url) GenerateQrCode(string email, string appName)
        {
            var secret = KeyGeneration.GenerateRandomKey(20);
            var base32Secret = Base32Encoding.ToString(secret);

            var url = $"otpauth://totp/{appName}:{email}?secret={base32Secret}&issuer={appName}";

            //var qrGEnerator = new QRCodeGenerator();
            //var qrCodeData = qrGEnerator.CreateQrCode(totpUrl, QRCodeGenerator.ECCLevel.Q);
            //var qrCode = new PngByteQRCode(qrCodeData);
            //var qrCodeBytes = qrCode.GetGraphic(20);;

            return (base32Secret, url);
        }

        public bool ValidateCode(string secret, string code)
        {
            var totp = new Totp(Base32Encoding.ToBytes(secret));
            return totp.VerifyTotp(code, out _, new VerificationWindow(2, 2));
        }
    }
}
