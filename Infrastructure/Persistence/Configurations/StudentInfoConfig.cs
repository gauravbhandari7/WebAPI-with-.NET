using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class StudentInfoConfig : AuditableEntityConfig<StudentInfo>
    {
        public override void Configure(EntityTypeBuilder<StudentInfo> builder)
        {
            base.Configure(builder);

            builder.HasIndex(p => p.RollNo).IsUnique();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
