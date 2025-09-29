using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Contracts.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool withTracking = false);
        
        Department? Get(int id);
        
        void Add(Department entity);

        void Update(Department entity);

        void Delete(int id);
    }
}
