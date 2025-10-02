using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Persistence.Data;
using LinkDev.IKEA.DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly Lazy<DepartmentRepository> _departmentRepository;
        private readonly Lazy<EmployeeRepository> _employeeRepository;

        public UnitOfWork(ApplicationDbContext dbContext) // Ask Runtime for an Instance of ApplicationDbContext Implicitly
        {
            _dbContext = dbContext;
            // Lazy Initialization
            _departmentRepository = new Lazy<DepartmentRepository>(new DepartmentRepository(_dbContext));
            _employeeRepository = new Lazy<EmployeeRepository>(new EmployeeRepository(_dbContext));
        }

        public IDepartmentRepository Departments => _departmentRepository.Value;
        public IEmployeeRepository Employees => _employeeRepository.Value;

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
