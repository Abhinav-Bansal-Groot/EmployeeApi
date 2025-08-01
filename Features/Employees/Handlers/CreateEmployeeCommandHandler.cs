using EmployeeApi.Features.Employees.Commands;
using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Responses;
using EmployeeApi.Services.Abstract;
using MediatR;

namespace EmployeeApi.Features.Employees.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeService _service;

        public CreateEmployeeCommandHandler(IEmployeeService service)
        {
            _service = service;
        }

        public async Task<EmployeeResponse> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = command.Request.Name,
                Email = command.Request.Email
            };

            var result = await _service.AddEmployeeAsync(employee, cancellationToken);

            return new EmployeeResponse
            {
                EmployeeId = result.EmployeeId,
                Name = result.Name,
                Email = result.Email
            };
        }
    }
}
