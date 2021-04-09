using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configurations
{
    public class ExtraOrderDbConfiguration : IEntityTypeConfiguration<ExtraOrderItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ExtraOrderItem> builder)
        {
            builder
                .HasKey(e => e.Id);
        }
    }
}
