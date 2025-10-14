using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//FluentValidation library 
namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<int> CreateDepartmentAsync(CreateDepartmentDto department)
        {
            var departmentToCreate = new Department()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,

                CreatedBy = "",
                LastModifiedBy = ""
            };

            _unitOfWork.Departments.Add(departmentToCreate);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<DepartmentResponseDto>> GetDepartmentsAsync()
        {
            return await _unitOfWork.Departments.GetAll()
                 .Select(department => new DepartmentResponseDto(department.Id, department.Code, department.Name, department.LastModifiedOn)).ToListAsync();

            //List<DepartmentResponseDto> departmentResponseDtos = new List<DepartmentResponseDto>();


            //yield return new DepartmentResponseDto(department.Id, department.Code, department.Name, department.LastModifiedOn);
            //foreach (var department in departments)
            //departmentResponseDtos.Add(new DepartmentResponseDto(department.Id, department.Code, department.Name, department.LastModifiedOn));

            //return departmentResponseDtos;
        }

        public async Task<DepartmentDetailsDto?> GetDepartmentsByIdAsync(int departmentId)
        {
            //var department = _unitOfWork.Departments.GetByIdAsync(departmentId);
            var department = await _unitOfWork.Departments.GetAsync(d => d.Id == departmentId, query => query.Include(d => d.Manager));

            if (department is null) return null;

            return new DepartmentDetailsDto(department.Id,
                                            department.Name,
                                            department.Code,
                                            department.Description,
                                            department.CreationDate,
                                            department.CreatedBy,
                                            department.CreatedOn,
                                            department.LastModifiedBy,
                                            department.LastModifiedOn,
                                            department.Manager?.FirstName
                                           );
        }

        public async Task<int> UpdateDepartmentAsync(UpdateDepartmentDto department)
        {
            var departmentUpdated = new Department()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = "",
                LastModifiedBy = ""
            };

            _unitOfWork.Departments.Update(departmentUpdated);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int departmentId)
        {
            _unitOfWork.Departments.Delete(departmentId);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
