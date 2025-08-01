using EmployeeApi.Models.Entities;
using EmployeeApi.Models.Responses;
using EmployeeApi.Specifications;

namespace EmployeeApi.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetAllEmployeeAsync(CancellationToken cancellationToken);
        Task<Employee> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> SearchByName(ISpecification<Employee> spec);

    }
}
