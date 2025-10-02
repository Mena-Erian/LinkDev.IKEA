using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Employees
{
    public class EmployeeConfigurations : BaseAuditableEntityConfigurations<int, Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Id).UseIdentityColumn(1, 1);
            builder.Property(e => e.FirstName).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.LastModifiedBy).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar(100)");

            builder.Property(e => e.Gender).HasConversion(
                (gender) => gender.ToString(),
                (gender) => Enum.Parse<Gender>(gender)
            );

            builder.Property(e => e.EmployeeType).HasConversion(
                    empType => empType.ToString(),
                    empType => Enum.Parse<EmployeeType>(empType)
                );

            // Work
            // One Department TO Employees Many

            builder.HasOne(e => e.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
            ;
        }
    }
}
