using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Departments
{
    public class DepartmentConfigurations : BaseAuditableEntityConfigurations<int, Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);

            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Code).HasColumnType("varchar(10)");
            builder.Property(D => D.Name).HasColumnType("varchar(100)");
            builder.Property(D => D.Description).HasColumnType("varchar(100)");

            //Manage Relationship
            // one  Department TO  Manager    One
            builder.HasOne(d => d.Manager)
                   .WithOne(d => d.DepartmentMng)
                   .HasForeignKey<Department>(d=> d.ManagerId)
                   .OnDelete(DeleteBehavior.SetNull);
                   ;


            //builder.HasMany(d => d.Employees)
            //       .WithOne()
            //       .HasForeignKey(d => d.DepartmentId)     
            //       ;

        }
    }
}
