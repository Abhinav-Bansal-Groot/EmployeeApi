using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Responses;
using EmployeeApi.Specifications;

namespace EmployeeApi.Repositories.Abstract
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken);
        Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetBySpecification(ISpecification<Employee> spec);
    }
}
