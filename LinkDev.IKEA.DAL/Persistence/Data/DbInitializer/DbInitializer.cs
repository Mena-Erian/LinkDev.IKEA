using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            if (!_dbContext.Employees.Any())
            {
                var options = new JsonSerializerOptions()
                {
                    //TypeInfoResolver = new DefaultJsonTypeInfoResolver() { }
                    Converters = { new JsonStringEnumConverter(allowIntegerValues: false) }
                };


                var employeeData = File.ReadAllText("../LinkDev.IKEA.DAL/Persistence/Data/Seeds/employees.json");
                //Deserialization => FROM Json to C# Type
                List<Employee>? employees = JsonSerializer.Deserialize<List<Employee>>(employeeData, options);

                if (employees?.Count > 0)
                {
                    foreach (var emp in employees)
                    {
                        emp.CreatedBy = "";
                        emp.LastModifiedBy = "";
                    }

                    _dbContext.Employees.AddRange(employees);
                    _dbContext.SaveChanges();
                }
            }
        }

        public async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!_dbContext.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Manager"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!_dbContext.Users.Any())
            {
                var usersData = await File.ReadAllTextAsync("../LinkDev.IKEA.DAL/Persistence/Data/Seeds/Users.json");
                var users = JsonSerializer.Deserialize<List<TempSeedingApplicationUser>>(usersData ?? "");


                if (users?.Count > 0)
                {
                    foreach (var item in users)
                    {
                        var user = new ApplicationUser()
                        {
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            UserName = item.UserName,
                            Email = item.Email,
                            IsAgree = item.IsAgree,
                            EmailConfirmed = true
                        };
                        var result = await userManager.CreateAsync(user, item.Password);
                        
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, item.Role);
                        }
                    }
                }
            }
        }
    }
}

