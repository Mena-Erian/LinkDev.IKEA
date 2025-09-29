using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.DbInitializer
{
    internal class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DbInitializer(ApplicationDbContext dbContext) // Ask Runtime for Creating an instance of ApplicationDbContext (Implicitly)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
                _dbContext.Database.Migrate(); // Update-Database
        }

        public void SeedData()
        {
            if (!_dbContext.Departments.Any())
            {
                var departmentsData = File.ReadAllText("../LinkDev.IKEA.DAL/Persistence/Data/Seeds/Departments.json");
                //Deserialization => FROM Json to C# Type
                var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData
                    /*, new JsonSerializerOptions() { PropertyNameCaseInsensitive = false }*/);

                if (departments?.Count > 0)
                {
                    _dbContext.Departments.AddRange(departments);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
