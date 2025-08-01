using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Responses;
using MediatR;

namespace EmployeeApi.Features.Employees.Queries
{
    public class GetEmployeesByNameQuery : IRequest<IEnumerable<Employee>>
    {
        public string Name { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetEmployeesByNameQuery(string name, int pageNumber, int pageSize)
        {
            Name = name;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
