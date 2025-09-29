using LinkDev.IKEA.DAL.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common
{
    internal class BaseAuditableEntityConfigurations<TKey, TEntity> : BaseEntityConfigurations<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : BaseAuditableEntity<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(D => D.CreatedBy).HasColumnType("varchar(50)");
            builder.Property(D => D.LastModifiedBy).HasColumnType("varchar(50)");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETUTCDate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETUTCDate()");
        }
    }
}
