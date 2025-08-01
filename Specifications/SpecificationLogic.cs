using EmployeeApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Specifications
{
    public class SpecificationLogic<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public  SpecificationLogic(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
        }
        public IQueryable<T> GetQuery(ISpecification<T> spec)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            if (spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            return query;
        }
    }
}
