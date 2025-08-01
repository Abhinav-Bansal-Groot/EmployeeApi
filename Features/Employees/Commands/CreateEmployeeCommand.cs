using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;
using MediatR;
namespace EmployeeApi.Features.Employees.Commands
{
    //public class CreateEmployeeCommand : IRequest<EmployeeResponse>
    //{
    //    public EmployeeCreateRequest Request { get; }
    //    public CreateEmployeeCommand(EmployeeCreateRequest request) 
    //    {
    //        Request = request;
    //    }
    //}

    public record CreateEmployeeCommand(EmployeeCreateRequest Request) : IRequest<EmployeeResponse>;
}
