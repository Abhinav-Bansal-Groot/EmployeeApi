using EmployeeApi.Models.Responses;
using MediatR;

namespace EmployeeApi.Features.Employees.Queries
{
    //public class GetEmployeeByIdQuery : IRequest<EmployeeResponse>
    //{
    //    public int Id { get; }

    //    public GetEmployeeByIdQuery(int id)
    //    {
    //        Id = id;
    //    }
    //}
    public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeResponse>; 
}
