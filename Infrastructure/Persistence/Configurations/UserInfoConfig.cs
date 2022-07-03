using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class UserInfoConfig : AuditableEntityConfig<UserInfo>
    {
        public override void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
