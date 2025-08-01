using EmployeeApi.Data;
using EmployeeApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using EmployeeApi.Repositories.Abstract;
using EmployeeApi.Specifications;

namespace EmployeeApi.Repositories.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly SpecificationLogic<Employee> _specLogic;
        public EmployeeRepository(ApplicationDbContext context, SpecificationLogic<Employee> specLogic)
        {
            _context = context;
            _specLogic = specLogic;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken)
        {
            return await _context.Employees
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Employees
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(e => e.EmployeeId == id, cancellationToken);
        }

        //public async Task<IEnumerable<Employee>> GetBySpecification_old(ISpecification<Employee> spec)
        //{
        //    IQueryable<Employee> query = _context.Employees.AsQueryable();

        //    if (spec.Criteria != null)
        //        query = query.Where(spec.Criteria);
        //    if (spec.OrderBy != null)
        //        query = query.OrderBy(spec.OrderBy);
        //    if (spec.IsPagingEnabled)
        //        query = query.Skip(spec.Skip).Take(spec.Take);


        //    return await query.ToListAsync();
        //}
        public async Task<IEnumerable<Employee>> GetBySpecification(ISpecification<Employee> spec)
        {
            //var query = SpecificationLogic<Employee>.GetQuery(_context.Employees.AsQueryable(), spec);
            var query = _specLogic.GetQuery(spec);
            return await query.ToListAsync();
        }
    }
}
