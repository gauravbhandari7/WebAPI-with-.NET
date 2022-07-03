using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class TeacherInfoConfig : AuditableEntityConfig<TeacherInfo>
    {
        public override void Configure(EntityTypeBuilder<TeacherInfo> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
