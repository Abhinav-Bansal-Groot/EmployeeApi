//using EmployeeApi.Data;
//using EmployeeApi.Generics.Abstract;
//using EmployeeApi.Specifications;
//using Microsoft.EntityFrameworkCore;

//namespace EmployeeApi.Generics.Concrete
//{
//    public class GenericRepository<T> : IGenericRepository<T> where T : class
//    {
//        public readonly ApplicationDbContext _context;
//        private readonly DbSet<T> _dbSet;

//        public GenericRepository(ApplicationDbContext context)
//        {
//            _context = context;
//            _dbSet = context.Set<T>();
//        }
//        public async Task<IEnumerable<T>> FindWithSpecification(ISpecification<T> spec)
//        {
//            IQueryable<T> query = _dbSet.AsQueryable();

//            if (spec.Criteria != null)
//                query = query.Where(spec.Criteria);
//            if (spec.OrderBy != null)
//                query = query.OrderBy(spec.OrderBy);
//            if (spec.IsPagingEnabled)
//                query = query.Skip(spec.Skip).Take(spec.Take);

//            return await query.ToListAsync();
//        }
//    }
//}
