using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class AuditableEntityConfig<T> : BaseEntityConfig<T> where T : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.CreatedDate)
                .HasColumnType("datetime");

            builder.Property(p => p.LastModifiedBy)
               .HasMaxLength(256);

            builder.Property(p => p.LastModifiedDate)
                .HasColumnType("datetime");
        }
    }
}
