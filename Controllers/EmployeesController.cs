using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;
using MediatR;
using EmployeeApi.Features.Employees.Commands;
using EmployeeApi.Features.Employees.Queries;
using Microsoft.AspNetCore.Authorization;
using EmployeeApi.Specifications.EmployeeSpecifications;
namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        private readonly IMediator _mediator;
        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateRequest request)
        {
            var result = await _mediator.Send(new CreateEmployeeCommand(request));
            return HandleResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllEmployeesQuery());
            return HandleResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetById(int id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchEmployees(
            [FromQuery] string? name,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            var query = new GetEmployeesByNameQuery(name, pageNumber, pageSize);
            var result = await _mediator.Send(query);

            var all_emp = await _mediator.Send(new GetAllEmployeesQuery());
            var totalRecords = all_emp.Count();

            var mapped = result.Select(e => new EmployeeResponse
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Email = e.Email,
            });

            var paginatedResult = new PaginatedResponse<EmployeeResponse>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Data = mapped
            };
            return Ok(paginatedResult);
        }
    }
}
