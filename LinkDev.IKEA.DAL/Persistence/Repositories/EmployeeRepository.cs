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

        public Task<PaginatedResult<Employee>> GetAllAsync(QueryParameters parameters)
        {
            Expression<Func<Employee, bool>>? filter = null;

            // Apply Filtration
            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                filter = e => e.FirstName.ToLower().Contains(parameters.SearchTerm ?? "") ||
                         e.LastName.ToLower().Contains(parameters.SearchTerm ?? "");
            }

            Func<IQueryable<Employee>, IQueryable<Employee>>? includes = null;
            includes = e => e.Include(nameof(Employee.Department));

            // Apply Ordering
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? orderby = null;

            if (!parameters.SortAscending.HasValue) parameters.SortAscending = false;

            orderby = parameters.SortBy?.ToLower() switch
            {
                "name" => parameters.SortAscending.Value ?
                            query => query.OrderBy(e => e.FirstName).ThenBy(e => e.LastName)
                            : query => query.OrderByDescending(e => e.FirstName).ThenBy(e => e.LastName),

                "email" => parameters.SortAscending.Value ?
                              query => query.OrderBy(e => e.Email)
                            : query => query.OrderByDescending(e => e.Email),

                "hireDate" => parameters.SortAscending.Value ?
                              query => query.OrderBy(e => e.HireDate)
                            : query => query.OrderByDescending(e => e.HireDate),

                "status" => parameters.SortAscending.Value ?
                               query => query.OrderBy(e => e.IsActive)
                             : query => query.OrderByDescending(e => e.IsActive),


                _ => parameters.SortAscending.Value ?
                query => query.OrderBy(e => e.FirstName).ThenBy(e => e.LastName)
                : query => query.OrderByDescending(e => e.FirstName).ThenBy(e => e.LastName)
            };

            bool withTracking = false;
            return base.GetAllAsync(parameters, filter, orderby, includes, withTracking);
        }
    }


}
