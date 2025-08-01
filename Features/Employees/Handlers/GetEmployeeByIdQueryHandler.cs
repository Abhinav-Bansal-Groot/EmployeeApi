using EmployeeApi.Features.Employees.Queries;
using EmployeeApi.Models.Responses;
using EmployeeApi.Services.Abstract;
using MediatR;

namespace EmployeeApi.Features.Employees.Handlers
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponse>
    {
        private readonly IEmployeeService _service;

        public GetEmployeeByIdQueryHandler(IEmployeeService service)
        {
            _service = service;
        }

        public async Task<EmployeeResponse> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
        {
            var employee = await _service.GetEmployeeByIdAsync(query.Id, cancellationToken);
            if (employee == null) return null;

            return new EmployeeResponse
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email
            };
        }
    }
}
