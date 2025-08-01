using EmployeeApi.Models.Entities;

namespace EmployeeApi.Specifications.EmployeeSpecifications
{
    public class EmployeeByNameSpecifications : BaseSpecification<Employee>
    {
        // This constructor sets the criteria and ordering
        public EmployeeByNameSpecifications(string? name)
            : base(e =>
                (string.IsNullOrEmpty(name) || e.Name.ToLower().Contains(name.ToLower())))
        {
            ApplyOrderBy(e => e.Name);
        }

        // This constructor adds pagination support
        public EmployeeByNameSpecifications(string? name, int skip, int take):this(name)
        {
            ApplyPaging(skip, take);
        }
    }
}
