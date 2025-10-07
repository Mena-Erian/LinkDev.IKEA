using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;
using LinkDev.IKEA.DAL.Persistence.Data;
using LinkDev.IKEA.DAL.Persistence.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public PaginatedResult<Employee> GetAll(QueryParameters queryParameters)
        {
            Expression<Func<Employee, bool>>? filter = null;

            // Apply Filtration
            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                filter = e => e.FirstName.ToLower().Contains(queryParameters.SearchTerm ?? "") ||
                         e.LastName.ToLower().Contains(queryParameters.SearchTerm ?? "");
            }

            Func<IQueryable<Employee>, IQueryable<Employee>>? includes = null;
            includes = e => e.Include(nameof(Employee.Department));

            bool withTracking = false;
            return base.GetAll(queryParameters, filter, null, includes, withTracking);
        }
    }


}
