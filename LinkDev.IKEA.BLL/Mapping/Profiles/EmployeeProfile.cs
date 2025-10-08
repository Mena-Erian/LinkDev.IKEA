using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Mapping.Profiles
{
    internal class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            //CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FirstName, options => options.MapFrom(src => $"sir. {src.FirstName}"))
                .ForMember(dest => dest.DepartmentId, options => options.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.DepartmentMngName, opitons => opitons.MapFrom(src => src.DepartmentMng))
                .ForMember(dest => dest.DepartmentMngName, options =>
                {
                    options.Condition(src => src.Department is not null);
                    options.MapFrom(src => src.Department!.Name);
                })
                .ForMember(dest => dest.FormattedHireDate, options => options.MapFrom(src => src.HireDate.ToString("MMMM d, yyyy")))

                .ReverseMap()
                .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Department, opt => opt.Ignore()) // for ignore department
                                                                         //.ForAllMembers(options => options.Ignore())  // for ignore all members 
                ;

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(_ => ""))
                .ForMember(d => d.LastModifiedBy, opt => opt.MapFrom(_ => ""));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.CreatedBy, options => options.MapFrom(_ => ""))
                .ForMember(dest => dest.LastModifiedBy, options => options.MapFrom(_ => ""));

            CreateMap<Department, DepartmentDetailsDto>()
                .ForMember(dest => dest.Manager, options =>
                {
                    options.Condition(src => src.Manager is not null);
                    options.MapFrom(src => $"{src.Manager!.FirstName} {src.Manager!.LastName}");
                })
                /*.ReverseMap()*/;



        }
    }
}
