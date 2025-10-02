using LinkDev.IKEA.DAL.Contracts.Repositories.BaseRepositories;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Contracts.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee, int>
    {

    }
}
