using EmployeeApi.Models.Entities;
using System;
using System.Linq.Expressions;

namespace EmployeeApi.Specifications
{
    public class BaseSpecification<T>: ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; }
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        public void ApplyOrderBy(Expression<Func<T, object>> orderBy)
        { 
            OrderBy = orderBy; 
        }
    }
}
