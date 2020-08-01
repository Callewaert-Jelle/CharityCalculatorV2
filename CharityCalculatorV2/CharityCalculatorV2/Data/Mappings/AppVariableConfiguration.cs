using CharityCalculatorV2.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculatorV2.Data.Mappings
{
    public class AppVariableConfiguration : IEntityTypeConfiguration<AppVariable>
    {
        public void Configure(EntityTypeBuilder<AppVariable> builder)
        {
            // Table name
            builder.ToTable("AppVariable");

            // Primary key
            builder.HasKey(av => av.Name);

            // Properties
            builder.Property(av => av.Value);
        }
    }
}
