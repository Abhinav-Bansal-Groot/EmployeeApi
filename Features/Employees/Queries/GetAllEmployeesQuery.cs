using EmployeeApi.Models.Responses;
using MediatR;

namespace EmployeeApi.Features.Employees.Queries
{
    //public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeResponse>> { }
    public record GetAllEmployeesQuery() : IRequest<IEnumerable<EmployeeResponse>>{ };
}
