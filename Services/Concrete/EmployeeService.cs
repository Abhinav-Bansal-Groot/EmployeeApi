using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Responses;
using EmployeeApi.Repositories.Abstract;
using EmployeeApi.Services.Abstract;
using EmployeeApi.Specifications;
using EmployeeApi.Specifications.EmployeeSpecifications;

namespace EmployeeApi.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository) 
        { 
            _repository = repository;
        }
        public Task<Employee> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            return _repository.AddEmployeeAsync(employee, cancellationToken);
        }
        public Task<IEnumerable<Employee>> GetAllEmployeeAsync(CancellationToken cancellationToken) 
        {
            return _repository.GetAllEmployeesAsync(cancellationToken);
        }
        public Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _repository.GetEmployeeByIdAsync(id, cancellationToken);
        }
        public async Task<IEnumerable<Employee>> SearchByName(ISpecification<Employee> spec)
        {
            return await _repository.GetBySpecification(spec);
        }

    }
}
