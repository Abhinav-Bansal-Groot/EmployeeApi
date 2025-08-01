using EmployeeApi.Features.Employees.Queries;
using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Responses;
using EmployeeApi.Services.Abstract;
using EmployeeApi.Specifications.EmployeeSpecifications;
using MediatR;

namespace EmployeeApi.Features.Employees.Handlers
{
    public class GetEmployeesByNameHandler : IRequestHandler<GetEmployeesByNameQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeService _empService;

        public GetEmployeesByNameHandler(IEmployeeService empService)
        {
            _empService = empService;
        }
        public async Task<IEnumerable<Employee>> Handle(GetEmployeesByNameQuery request, CancellationToken cancellationToken)
        { 
            int skip = (request.PageNumber - 1) * request.PageSize;
            var spec = new EmployeeByNameSpecifications(request.Name, skip, request.PageSize);

            var result = await _empService.SearchByName(spec);

            return result;
        }

    }
}
