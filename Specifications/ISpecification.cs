using System;
using System.Linq.Expressions;

namespace EmployeeApi.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria {  get; }
        Expression<Func<T, object>>? OrderBy { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }

    }
}
