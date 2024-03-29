﻿using Liquid.Repository.EntityFramework.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Liquid.Data.EntityFramework.Tests.Configurations
{
    [ExcludeFromCodeCoverage]
    public class MockTypeConfiguration : IEntityTypeConfiguration<MockEntity>
    {
        public void Configure(EntityTypeBuilder<MockEntity> builder)
        {
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.MockTitle).IsRequired().HasMaxLength(50);
            builder.Property(o => o.CreatedDate).IsRequired();
            builder.Property(o => o.Active).IsRequired();
        }
    }
}