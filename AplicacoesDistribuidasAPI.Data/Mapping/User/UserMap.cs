using AplicacoesDistribuidasAPI.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacoesDistribuidasAPI.Data.Mapping.User
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(60);

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.Property(p => p.CreateAt)
                .IsRequired()
                .HasDefaultValue(new DateTime(0001, 01, 01));


            builder.Property(p => p.UpdateAt)
                .HasDefaultValue(null);
        }
    }
}
