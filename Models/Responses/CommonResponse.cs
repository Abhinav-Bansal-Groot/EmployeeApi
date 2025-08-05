namespace EmployeeApi.Models.Responses
{
    public class CommonResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
    }
}
