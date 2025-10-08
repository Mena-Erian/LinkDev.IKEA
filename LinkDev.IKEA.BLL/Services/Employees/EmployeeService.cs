using AutoMapper;
using Azure;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
            ValidateEmployeeCreateBusinessRules(employeeDto);

            if (employeeDto is not null)
            {
                /// var employee = new Employee()
                /// {
                ///     Id = employeeDto.Id,
                ///     FirstName = employeeDto.FirstName,
                ///     LastName = employeeDto.LastName,
                ///     Email = employeeDto.Email,
                ///     Age = employeeDto.Age,
                ///     Salary = employeeDto.Salary,
                ///     Address = employeeDto.Address,
                ///     DepartmentId = employeeDto.DepartmentId,
                ///     Gender = employeeDto.Gender,
                ///     EmployeeType = employeeDto.EmployeeType,
                ///     PhoneNumber = employeeDto?.PhoneNumber,
                ///     CreatedBy = "",
                ///     LastModifiedBy = "",
                /// };

                var employee = _mapper.Map<Employee>(employeeDto);

                employee.HireDate = employee.HireDate;
                employee.IsActive = true;

                _unitOfWork.Employees.Add(employee);
            }

            return _unitOfWork.Commit();
        }

        public EmployeeDto? GetEmployeeById(int employeeId)
        {
            var employee = _unitOfWork.Employees.Get(employeeId);

            if (employee is null) return null;

            /// var employeeDto = new EmployeeDto(
            ///     employee.Id,
            ///     employee.FirstName,
            ///     employee.LastName,
            ///     employee.Email ?? null,
            ///     employee.PhoneNumber ?? null,
            ///     employee.Address ?? null,
            ///     employee.Salary,
            ///     employee.IsActive,
            ///     employee.Age,
            ///     employee.Image ?? null,
            ///     employee.HireDate,
            ///     employee.Gender,
            ///     employee.EmployeeType,
            ///     employee.DepartmentId ?? null,
            ///     employee.DepartmentMng?.Name,
            ///     employee.CreatedBy,
            ///     employee.CreatedOn,
            ///     employee.LastModifiedBy,
            ///     employee.LastModifiedOn
            ///     );

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            employeeDto = _mapper.Map<Employee, EmployeeDto>(employee);


            return employeeDto;
        }

        public EmployeeDetailsDto? GetEmployeeDetailsById(int employeeId)
        {
            var employee = _unitOfWork.Employees.Get(
                    filter: e => e.Id == employeeId,
                    includes: e => e.Include(e => e.Department)
                );
            if (employee is null) return null;

            /// var employeeDto = new EmployeeDto(
            ///     employee.Id,
            ///     employee.FirstName,
            ///     employee.LastName,
            ///     employee.Email ?? null,
            ///     employee.PhoneNumber ?? null,
            ///     employee.Address ?? null,
            ///     employee.Salary,
            ///     employee.IsActive,
            ///     employee.Age,
            ///     employee.Image ?? null,
            ///     employee.HireDate,
            ///     employee.Gender,
            ///     employee.EmployeeType,
            ///     employee.DepartmentId ?? null,
            ///     employee.DepartmentMng?.Name,
            ///     employee.CreatedBy,
            ///     employee.CreatedOn,
            ///     employee.LastModifiedBy,
            ///     employee.LastModifiedOn
            ///     );

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            var department = employee.Department as Department;

            DepartmentDetailsDto? departmentDetailsDto = null;

            if (department is not null)
                /// departmentDetailsDto = new DepartmentDetailsDto(
                ///     department.Id,
                ///     department.Name,
                ///     department.Code,
                ///     department.Description,
                ///     department.CreationDate,
                ///     department.CreatedBy,
                ///     department.CreatedOn,
                ///     department.LastModifiedBy,
                ///     department.LastModifiedOn,
                ///     department.Manager?.FirstName
                /// 
                /// );
                departmentDetailsDto = _mapper.Map<Department, DepartmentDetailsDto>(department);

            int yearsOfExperience = DateTime.Now.Year - employee.HireDate.Year;

            return new EmployeeDetailsDto(employeeDto, departmentDetailsDto, 3);
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            var employees = _unitOfWork.Employees.GetAll(true);

            List<EmployeeDto> employeesDto = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                employeesDto.Add(new EmployeeDto(
                    employee.Id,
                    employee.FirstName,
                    employee.LastName,
                    employee.Email ?? null,
                    employee.PhoneNumber ?? null,
                    employee.Address ?? null,
                    employee.Salary,
                    employee.IsActive,
                    employee.Age,
                    employee.Image ?? null,
                    employee.HireDate,
                    employee.Gender,
                    employee.EmployeeType,
                    employee.DepartmentId ?? null,
                    employee.DepartmentMng?.Name,
                    employee.CreatedBy,
                    employee.CreatedOn,
                    employee.LastModifiedBy,
                    employee.LastModifiedOn
                ));
            }

            return employeesDto;
        }

        PaginatedResult<EmployeeDto> IEmployeeService.GetEmployees(QueryParameters queryParameters)
        {

            var employees = _unitOfWork.Employees.GetAll(
                queryParameters: queryParameters
                //includes: e => e.Include(nameof(Employee.Department)),
                //filter: e => e.FirstName.ToLower().Contains(queryParameters.SearchTerm ?? "") ||
                //             e.LastName.ToLower().Contains(queryParameters.SearchTerm ?? "")

                );

            if (employees is null) return null!;

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees.Data);

            PaginatedResult<EmployeeDto> paginatedResult = new PaginatedResult<EmployeeDto>()
            {
                //Data = employees.Data.Select(emp => new EmployeeDto(emp.Id, emp.FirstName, emp.LastName, emp.Email ?? null, emp.PhoneNumber ?? null, emp.Address ?? null, emp.Salary, emp.IsActive, emp.Age, emp.Image ?? null, emp.HireDate, emp.Gender, emp.EmployeeType, emp.DepartmentId ?? null, emp.DepartmentMng?.Name, emp.CreatedBy, emp.CreatedOn, emp.LastModifiedBy, emp.LastModifiedOn)),
                Data = employeesDto,
                PageIndex = employees.PageIndex,
                PageSize = employees.PageSize,
                TotalCount = employees.TotalCount,
            };


            return paginatedResult;
        }

        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            ValidateEmployeeUpdateBusinessRules(employeeDto);

            if (employeeDto is null) return 0;


            /// _unitOfWork.Employees.Update(new Employee()
            /// {
            ///     Id = employeeDto.Id,
            ///     FirstName = employeeDto.FirstName,
            ///     LastName = employeeDto.LastName,
            ///     Email = employeeDto.Email,
            ///     Age = employeeDto.Age,
            ///     Salary = employeeDto.Salary,
            ///     Address = employeeDto.Address,
            ///     DepartmentId = employeeDto.DepartmentId,
            ///     Gender = employeeDto.Gender,
            ///     EmployeeType = employeeDto.EmployeeType,
            ///     PhoneNumber = employeeDto.PhoneNumber,
            ///     CreatedBy = "",
            ///     LastModifiedBy = "",
            ///     HireDate = employeeDto.HireDate,
            ///     IsActive = employeeDto.IsActive,
            ///     Image = employeeDto.Image,
            /// });

            var employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.Employees.Update(employee);

            return _unitOfWork.Commit();
        }


        public bool ChangeEmployeeStatus(int id, bool activation)
        {
            var employee = _unitOfWork.Employees.Get(id);
            if (employee == null)
                throw new Exception($"Employee with Id {id} does not exist.");

            if (employee.IsActive == activation)
                throw new Exception($"Employee with Id {id} is already {(activation ? "Active" : "inactive")} exist.");

            employee.IsActive = activation;
            return _unitOfWork.Commit() > 0;
        }

        public bool DeleteEmployee(int employeeId)
        {
            _unitOfWork.Employees.Delete(employeeId);
            return _unitOfWork.Commit() > 0;
        }

        #region Helper Methods
        private void ValidateEmployeeCreateBusinessRules(CreateEmployeeDto employee)
        {
            if (employee.DepartmentId.HasValue)
            {
                var department = _unitOfWork.Departments.Get(employee.DepartmentId.Value);

                if (department is null)
                    throw new Exception($"Department with Id {employee.DepartmentId.Value} does not exist.");


                int minAge = 18;
                if (employee.Age <= minAge)
                    throw new Exception($"Employee must be at least {minAge} years old. Current age is {employee.Age}.");


                int minSalary = 5000;
                if (employee.Salary < minSalary)
                    throw new Exception($"Salary must be greater that {minSalary}. and Current Salary is {employee.Salary}");
            }
        }

        private void ValidateEmployeeUpdateBusinessRules(UpdateEmployeeDto employeeDto)
        {


            if (employeeDto.DepartmentId.HasValue)
            {

                var department = _unitOfWork.Departments.Get(employeeDto.DepartmentId.Value);

                if (department is null)
                    throw new Exception($"Department with Id {employeeDto.DepartmentId.Value} does not exist.");

            }

            var employeeBeforeUpdate = _unitOfWork.Employees.Get(employeeDto.Id);

            if (employeeBeforeUpdate == null)
                throw new Exception($"NOT Valid Id {employeeDto.Id}");


            var minUpdatedSalary = employeeBeforeUpdate.Salary + (employeeBeforeUpdate.Salary * 1.1m);
            if (employeeBeforeUpdate.Salary != employeeBeforeUpdate.Salary)
            {
                if (employeeDto.Salary < minUpdatedSalary)
                    throw new Exception($"Salary must be greater that {minUpdatedSalary}.Because the Last Current Salary is {employeeBeforeUpdate.Salary} and The Salary you want to update to is {employeeDto.Salary} and it should to be greater than this at least 10%");
            }
        }


        #endregion
    }
}
