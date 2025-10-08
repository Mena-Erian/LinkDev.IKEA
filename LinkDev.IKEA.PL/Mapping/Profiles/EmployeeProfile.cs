using AutoMapper;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Persistance.Data.Migrations;
using LinkDev.IKEA.DAL.Persistence.Common;
using LinkDev.IKEA.PL.ViewModels.Employees;

namespace LinkDev.IKEA.PL.Mapping.Profiles
{
    public class EmployeeProfile : Profile
    {

        public EmployeeProfile()
        {
            /// CreateMap<PaginatedResult<EmployeeDto>, IEnumerable<EmployeeViewModel>>()
            ///     .ForMember(d => d.Select(d => d.FullName))
            ///     ;

            CreateMap<EmployeeDto, EmployeeViewModel>().ReverseMap()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                .ForMember(d => d.DepartmentId, opt => opt.Ignore())
                .ForMember(d => d.DepartmentMngName, opt => opt.Ignore());

            CreateMap<EmployeeDetailsDto, EmployeeDetailsViewModel>()
                // Map properties from the nested Employee object
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Employee.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Employee.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Employee.Age))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Employee.Salary))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Employee.Address))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Employee.IsActive))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Employee.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Employee.PhoneNumber))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.Employee.HireDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Employee.Gender))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.Employee.EmployeeType))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Employee.Image))
                .ForMember(dest => dest.DepartmentMngName, opt => opt.MapFrom(src => src.Employee.DepartmentMngName))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Employee.DepartmentId))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Employee.CreatedBy))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.Employee.CreatedOn))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.Employee.LastModifiedBy))
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(src => src.Employee.LastModifiedOn))
                // Map the Department directly (it's already the right type)
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                // FullName is a computed property in the destination, so it will be calculated automatically
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Employee.FullName));

            CreateMap<EmployeeCreateViewModel, CreateEmployeeDto>()
                    .ConstructUsing(src => new CreateEmployeeDto(
                src.Id,
                src.FirstName,
                src.LastName,
                src.Email,
                src.PhoneNumber,
                src.Address,
                src.Salary,
                src.IsActive,
                src.Age,
                null, // Image - not in ViewModel, will be handled separately
                src.HiringDate, // Note: property name mismatch HiringDate -> HireDate
                src.Gender,
                src.EmployeeType,
                src.DepartmentId
            ))

            .ForAllMembers(opt => opt.Ignore()); // Ignore all members since we're using ConstructUsing


            CreateMap<EmployeeDto, EmployeeEditViewModel>()
                ;

            CreateMap<EmployeeEditViewModel, UpdateEmployeeDto>()
                .ConstructUsing(employeeModel =>
                new UpdateEmployeeDto(
                employeeModel.Id,
                employeeModel.FirstName,
                employeeModel.LastName,
                employeeModel.Email ?? null,
                employeeModel.PhoneNumber ?? null,
                employeeModel.Address ?? null,
                employeeModel.Salary,
                employeeModel.IsActive,
                employeeModel.Age,
                 default,
                employeeModel.HiringDate,
                employeeModel.Gender,
                employeeModel.EmployeeType,
                employeeModel.DepartmentId ?? null)
                )
                .ForAllMembers(opt => opt.Ignore());


        }
    }
}
