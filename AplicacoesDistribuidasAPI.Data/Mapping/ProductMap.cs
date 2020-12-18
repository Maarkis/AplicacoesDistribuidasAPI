using AplicacoesDistribuidasAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AplicacoesDistribuidasAPI.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired().HasDefaultValue(0.0);

            builder.Property(p => p.Description)
                .HasMaxLength(100);

            builder.Property(p => p.Amount).HasDefaultValue(1);

            builder.Property(p => p.CreateAt)
                .IsRequired()
                .HasDefaultValue(new DateTime(2010, 01, 01));


            builder.Property(p => p.UpdateAt)
                .HasDefaultValue(new DateTime(2010, 1, 1));

        }
    }
}
