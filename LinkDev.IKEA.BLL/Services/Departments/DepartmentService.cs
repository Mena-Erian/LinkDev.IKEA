using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
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

        public int CreateDepartment(CreateDepartmentDto department)
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

            _unitOfWork.DepartmentRepository.Add(departmentToCreate);
            return _unitOfWork.Commit();
        }

        public IEnumerable<DepartmentResponseDto> GetDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            foreach (var department in departments)
                yield return new DepartmentResponseDto(department.Id, department.Code, department.Name, department.LastModifiedOn);
        }

        public DepartmentDetailsDto? GetDepartmentsById(int departmentId)
        {
            var department = _unitOfWork.DepartmentRepository.Get(departmentId);

            if (department is null) return null;

            return new DepartmentDetailsDto(department.Id, department.Name, department.Code, department.Description, department.CreationDate, department.CreatedBy, department.CreatedOn, department.LastModifiedBy, department.LastModifiedOn);
        }

        public int UpdateDepartment(UpdateDepartmentDto department)
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

            _unitOfWork.DepartmentRepository.Update(departmentUpdated);
            return _unitOfWork.Commit();
        }
        
        public bool DeleteDepartment(int departmentId)
        {
            _unitOfWork.DepartmentRepository.Delete(departmentId);
            return _unitOfWork.Commit() > 0;
        }
    }
}
