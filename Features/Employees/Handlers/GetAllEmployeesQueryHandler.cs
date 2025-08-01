using EmployeeApi.Features.Employees.Queries;
using EmployeeApi.Models.Responses;
using EmployeeApi.Services.Abstract;
using MediatR;

namespace EmployeeApi.Features.Employees.Handlers
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeResponse>>
    {
        private readonly IEmployeeService _service;

        public GetAllEmployeesQueryHandler(IEmployeeService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<EmployeeResponse>> Handle(GetAllEmployeesQuery query, CancellationToken cancellationToken)
        {
            var employees = await _service.GetAllEmployeeAsync(cancellationToken);
            return employees.Select(e => new EmployeeResponse
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Email = e.Email
            });
        }
    }
}
