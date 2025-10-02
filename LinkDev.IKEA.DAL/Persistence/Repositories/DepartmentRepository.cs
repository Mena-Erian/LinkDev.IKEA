using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// var department = _dbContext.Departments.Local.FirstOrDefault(d => d.Id == id);
        /// 
        /// if (department is null) return _dbContext.Departments.FirstOrDefault(d => d.Id == id);
        /// 
        /// return department;
        public Department? Get(int id) => _dbContext.Find<Department>(id);

        public IEnumerable<Department> GetAll(bool withTracking = false)
            => withTracking ? _dbContext.Departments : _dbContext.Departments.AsNoTracking();

        public void Add(Department entity) => _dbContext.Departments.Add(entity);

        public void Update(Department entity) => _dbContext.Departments.Update(entity);

        public void Delete(int id)
        {
            var department = _dbContext.Departments.FirstOrDefault(d => d.Id == id);
            if (department is not null)
                _dbContext.Departments.Remove(department);
        }
    }
}
