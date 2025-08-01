namespace EmployeeApi.Models.Responses
{
    public class QR_Response
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public string? TotpUrl { get; set; }
    }
}