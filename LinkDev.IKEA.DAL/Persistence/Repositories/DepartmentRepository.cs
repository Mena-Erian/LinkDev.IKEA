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

        public Department? Get(int id)
        {
            /// var department = _dbContext.Departments.Local.FirstOrDefault(d => d.Id == id);
            /// 
            /// if (department is null) return _dbContext.Departments.FirstOrDefault(d => d.Id == id);
            /// 
            /// return department;

            return _dbContext.Find<Department>(id);
        }

        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (withTracking) return _dbContext.Departments;

            return _dbContext.Departments.AsNoTracking();
        }

        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var department = _dbContext.Departments.FirstOrDefault(d => d.Id == id);
            if (department is null) return false;

            _dbContext.Departments.Remove(department);

            return _dbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
